using RandomUsersApp.App;
using RandomUsersApp.Interfaces;
using RandomUsersApp.Services;

HttpClient httpClient = new HttpClient();

IRandomUserService randomUserService = new RandomUserService(httpClient);

RandomUserApp app = new RandomUserApp(randomUserService);

await app.RunAsync();