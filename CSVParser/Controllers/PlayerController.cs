using CsvHelper;
using CSVParser.Logic;
using CSVParser.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerLogic _playerLogic;
    public PlayerController(PlayerLogic playerLogic)
    {

        _playerLogic = playerLogic;

    }

    [HttpGet("listPlayer")]
    public async Task<List<PlayerModel>> GetPlayerData()
    {
       

        return await _playerLogic.GetPlayerData();
    }
    [Route("{playerId}")]
    [HttpGet]
    public async Task<PlayerModel> GetPlayer([FromRoute] int playerId)
    {
        
        return await _playerLogic.GetPlayer(playerId);
    }
}
