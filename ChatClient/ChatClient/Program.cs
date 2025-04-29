using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5000/chat") // Same as your server hub
    .WithAutomaticReconnect()
    .Build();

// Handle incoming messages
connection.On<string, string>("ReceiveMessage", (user, message) =>
{
    Console.WriteLine($"{user}: {message}");
});

try
{
    await connection.StartAsync();
    Console.WriteLine("Connected to chat server!");
}
catch (Exception ex)
{
    Console.WriteLine($"Connection failed: {ex.Message}");
    return;
}

// Ask user name
Console.Write("Enter your username: ");
string username = Console.ReadLine();

// Message loop
while (true)
{
    var message = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(message)) continue;

    await connection.InvokeAsync("SendMessage", username, message);
}
