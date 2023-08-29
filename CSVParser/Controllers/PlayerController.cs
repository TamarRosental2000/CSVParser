using CsvHelper;
using CSVParser.Logic;
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
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ReadCSVFile _readCSVFile;

    public PlayerController(IHttpClientFactory httpClientFactory, ReadCSVFile readCSVFile)
    {
        _httpClientFactory = httpClientFactory;
        _readCSVFile= readCSVFile;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // Read the players.csv file 
        _readCSVFile
        // Process the file line by line using CsvHelper

        // Make API requests to Balldontlie's API for additional information
        // Create a List<PlayerInfoModel> containing enriched player information

        // Create a CSV file from the enriched data using CsvHelper
        // Return the CSV file as a FileStreamResult

        return File(memoryStream, "text/csv", "enriched_players.csv");
    }
}
