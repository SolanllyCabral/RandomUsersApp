using RandomUsersApp.DTOs;
using RandomUsersApp.Interfaces;

namespace RandomUsersApp.App
{
    public class RandomUserApp
    {
        private readonly IRandomUserService _randomUserService;

        public RandomUserApp(IRandomUserService randomUserService)
        {
            _randomUserService = randomUserService;
        }

        private void ShowWelcomeMessage()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("     BIENVENIDO/A A RANDOM USER APP");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine("Esta aplicación obtiene usuarios aleatorios");
            Console.WriteLine("desde la API de Random User Generator.");
            Console.WriteLine();
            Console.WriteLine("Podrás indicar cuántos usuarios deseas obtener,");
            Console.WriteLine("ver el progreso de la búsqueda y consultar sus datos.");
            Console.WriteLine();
            Console.WriteLine("Datos mostrados:");
            Console.WriteLine("- Nombre completo");
            Console.WriteLine("- Género");
            Console.WriteLine("- Correo electrónico");
            Console.WriteLine("- País");
            Console.WriteLine();
            Console.WriteLine("Presiona ENTER para comenzar...");
            Console.ReadLine();
        }

        public async Task RunAsync()
        {
            ShowWelcomeMessage();

            bool continueApp = true;

            while (continueApp)
            {
                int quantity = AskUserQuantity();

                Console.WriteLine();
                Console.WriteLine($"Buscando {quantity} usuarios aleatorios...");
                Console.WriteLine();

                List<Task<UserDto?>> tasks = new();

                for (int i = 0; i < quantity; i++)
                {
                    tasks.Add(_randomUserService.GetRandomUserAsync());
                }

                UserDto?[] users = await ExecuteWithProgressAsync(tasks);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Usuarios obtenidos:");
                Console.WriteLine("-------------------");

                int counter = 1;

                foreach (UserDto? user in users)
                {
                    if (user == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Usuario #{counter}");
                        Console.WriteLine("No se pudo obtener este usuario.");
                    }
                    else
                    {
                        ShowUser(counter, user);
                    }

                    counter++;
                }

                continueApp = AskToContinue();
            }

            Console.WriteLine();
            Console.WriteLine("Aplicación finalizada. ¡Hasta luego!");
        }

        private int AskUserQuantity()
        {
            int quantity;

            while (true)
            {
                Console.Write("¿Cuántos usuarios aleatorios deseas obtener? ");

                string? input = Console.ReadLine();

                if (int.TryParse(input, out quantity) && quantity > 0)
                {
                    return quantity;
                }

                Console.WriteLine("Por favor, ingresa un número válido mayor que 0.");
            }
        }

        private bool AskToContinue()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("¿Deseas buscar más usuarios? S/N: ");

                string? answer = Console.ReadLine()?.Trim().ToUpper();

                if (answer == "S")
                {
                    Console.WriteLine();
                    return true;
                }

                if (answer == "N")
                {
                    return false;
                }

                Console.WriteLine("Respuesta no válida. Escribe S para sí o N para no.");
            }
        }
        private async Task<UserDto?[]> ExecuteWithProgressAsync(List<Task<UserDto?>> tasks)
        {
            int totalTasks = tasks.Count;
            int completedTasks = 0;
            int successfulUsers = 0;

            List<UserDto?> users = new();

            while (tasks.Count > 0)
            {
                Task<UserDto?> completedTask = await Task.WhenAny(tasks);

                tasks.Remove(completedTask);

                UserDto? user = await completedTask;
                users.Add(user);

                completedTasks++;

                if (user != null)
                {
                    successfulUsers++;
                }

                ShowProgressBar(completedTasks, totalTasks, successfulUsers);
            }

            Console.WriteLine();

            return users.ToArray();
        }

        private void ShowProgressBar(int completed, int total, int successful)
        {
            int barLength = 30;

            double percentage = (double)completed / total;
            int filledLength = (int)(barLength * percentage);

            string filledBar = new string('█', filledLength);
            string emptyBar = new string('-', barLength - filledLength);

            Console.Write(
                $"\rProgreso: [{filledBar}{emptyBar}] {percentage:P0} | Procesados: {completed}/{total} | Exitosos: {successful}"
            );
        }

        private void ShowUser(int number, UserDto user)
        {
            Console.WriteLine();
            Console.WriteLine($"Usuario #{number}");
            Console.WriteLine($"Nombre completo: {user.FullName}");
            Console.WriteLine($"Género: {user.Gender}");
            Console.WriteLine($"Correo electrónico: {user.Email}");
            Console.WriteLine($"País: {user.Country}");
        }
    }
}