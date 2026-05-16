using RandomUsersApp.DTOs;
using RandomUsersApp.Interfaces;
using RandomUsersApp.Models;
using System.Text.Json;

namespace RandomUsersApp.Services
{
    public class RandomUserService : IRandomUserService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://randomuser.me/api/";
        private const int MaxRetries = 3;

        public RandomUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto?> GetRandomUserAsync()
        {
            for (int attempt = 1; attempt <= MaxRetries; attempt++)
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Error HTTP: {response.StatusCode}");
                    }

                    string json = await response.Content.ReadAsStringAsync();

                    RandomUserResponse? data = JsonSerializer.Deserialize<RandomUserResponse>(json);

                    RandomUserResult? user = data?.Results?.FirstOrDefault();

                    if (user == null)
                    {
                        throw new Exception("La respuesta de la API no contiene usuarios válidos.");
                    }

                    return new UserDto
                    {
                        FullName = $"{user.Name.Title} {user.Name.First} {user.Name.Last}",
                        Gender = user.Gender,
                        Email = user.Email,
                        Country = user.Location.Country
                    };
                }
                catch (Exception ex)
                {

                    if (attempt == MaxRetries)
                    {
                        return null;
                    }

                    await Task.Delay(1000);
                }
            }

            return null;
        }
    }
}