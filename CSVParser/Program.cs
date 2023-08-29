using CSVParser.Logic;
using Microsoft.AspNet.SignalR.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<ReadCSVFile>();
builder.Services.AddSingleton<PlayerLogic>();
builder.Services.AddSingleton<WebSocketHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseWebSockets();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await HandleWebSocketConnection(webSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});

app.MapControllers();

app.Run();

async Task HandleWebSocketConnection(WebSocket webSocket)
{
    var webSocketHandler = new WebSocketHandler(); // Create an instance of your WebSocketHandler class
    await webSocketHandler.AddSocket(webSocket);

    var buffer = new byte[1024];
    while (webSocket.State == WebSocketState.Open)
    {
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);

        if (result.MessageType == WebSocketMessageType.Text)
        {
            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Received message: {receivedMessage}");

            // Handle the received WebSocket message here
        }
        else if (result.MessageType == WebSocketMessageType.Close)
        {
            await webSocketHandler.RemoveSocket(webSocket);
            break;
        }
    }
}
