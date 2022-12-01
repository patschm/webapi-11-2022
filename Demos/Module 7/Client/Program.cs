using Microsoft.AspNetCore.SignalR.Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
                .WithUrl("https://ps-chat.azurewebsites.net/chatbox")
                .Build();

        System.Console.WriteLine("Uw naam aub");
    
        connection.On<string>("hierbenik", txt =>
        {
            System.Console.WriteLine(txt);

        });
        connection.On<string>("blaat", (msg) =>
        {
            System.Console.WriteLine(msg);

        });
        await connection.StartAsync();

         var naam = Console.ReadLine();
        await connection.InvokeAsync("RegisterAsync", naam);
        do
        {
            string txt = Console.ReadLine();
            await connection.InvokeAsync("BlaatAsync", naam, txt);
        }
        while (true);
    }
}