using System.Net.Http.Headers;
using Microsoft.Identity.Client;

//ConfidentialClientApplicationBuilder.Create("").WithClientSecret("MB08Q~~zjtQzpZFTYWndSmlsE__F3GDvjLjYRbd.").Build();
var app = PublicClientApplicationBuilder
    .Create("52db6103-00b0-4b7d-a4ce-c48580b501d1")
    .WithAuthority(AzureCloudInstance.AzurePublic, "common")
    .WithRedirectUri("http://localhost/");

    var token = await app.Build()
   // .AcquireTokenByUsernamePassword
    .AcquireTokenInteractive(new string[]{ "api://b6059bec-b88f-4422-a541-08eab5cd7cad/Toegang" })
    .ExecuteAsync();
    System.Console.WriteLine(token.AccessToken);

    var client = new HttpClient();
    client.BaseAddress = new Uri("https://localhost:7028/");

    client.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", token.AccessToken);

    string data = await client.GetStringAsync("weatherforecast");
    System.Console.WriteLine(data);

    Console.ReadLine();