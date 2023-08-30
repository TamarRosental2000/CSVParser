using CSVParser.Logic;
using CSVParser.TimedService;
using CSVParser.WebSocketHandler;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<ReadCSVFile>();
builder.Services.AddSingleton<PlayerLogic>();
builder.Services.AddSingleton<WebSocketHandler>();
builder.Services.AddHostedService<TimedService>();
builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseWebSockets(new WebSocketOptions
//{
//    KeepAliveInterval = TimeSpan.FromSeconds(120),
//});

//// Than register your hubs here with a url.
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<ChatHub>("/hub/chat");
//});
//app.UseWebSockets();

//app.Use(async (context, next) =>
//{
//    if (context.Request.Path == "/ws")
//    {
//        if (context.WebSockets.IsWebSocketRequest)
//        {
//            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
//            var webSocketHandler = context.RequestServices.GetRequiredService<WebSocketHandler>();
//            await webSocketHandler.AddSocket(webSocket);

//            try
//            {
//                while (webSocket.State == WebSocketState.Open)
//                {
//                    // Handle WebSocket messages if needed
//                }
//            }
//            finally
//            {
//                await webSocketHandler.RemoveSocket(webSocket);
//                webSocket.Dispose();
//            }
//        }
//        else
//        {
//            context.Response.StatusCode = 400;
//        }
//    }
//    else
//    {
//        await next(); // Use the next delegate
//    }
//});

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/players", async context =>
//    {
//        // Fetch player data from cache or API
//        var playerData = await GetPlayerDataFromCacheOrApi();

//        context.Response.ContentType = "application/json";
//        await context.Response.WriteAsync(JsonConvert.SerializeObject(playerData));
//    });
//});
app.UseCors("AllowAll"); app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());
app.MapControllers();

app.Run();
//public async Task<IEnumerable<PlayerModel>> GetPlayerDataFromCacheOrApi()
//{
//    // Implement logic to retrieve player data from cache or API
//    // Update cache and notify WebSocket clients if data changes

//    // After updating player data, notify connected WebSocket clients
//    var webSocketHandler = context.RequestServices.GetRequiredService<WebSocketHandler>();
//    await webSocketHandler.SendPlayerListToClients(updatedPlayerData);

//    return updatedPlayerData;
//}
