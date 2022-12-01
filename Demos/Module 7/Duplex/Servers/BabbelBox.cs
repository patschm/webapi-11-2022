
using Microsoft.AspNetCore.SignalR;

namespace Duplex.Servers;

public class BabbelBox : Hub
{
    public async Task RegisterAsync(string user)
    {
        System.Console.WriteLine($"{user} heeft zich aangemeld");
        await Clients.All.SendAsync("hierbenik", $"{user} heeft zich aangemeld");
        //Clients.AllExcept( $"etx", base.Context.ConnectionId);
    }
    public async Task BlaatAsync(string user, string text)
    {
        System.Console.WriteLine("Blaat");
        await Clients.All.SendAsync("blaat", $"{user}> {text}");
    }
}
