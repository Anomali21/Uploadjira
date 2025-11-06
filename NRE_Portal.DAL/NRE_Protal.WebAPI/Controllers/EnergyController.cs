using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NRE_Protal.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnergyController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<EnergyController> _logger;
    
        public EnergyController(IWebHostEnvironment environment, ILogger<EnergyController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        [HttpGet("valais")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetValaisData()
        {
            try
            {
                _logger.LogInformation("Attempting to load Valais data...");
                
                // Path to JSON file in DAL project
                var jsonPath = Path.Combine(
                    _environment.ContentRootPath, 
                    "..", 
                    "NRE_Portal.DAL", 
                    "Data", 
                    "energy_data.json"
                );
                
                _logger.LogInformation($" File path: {jsonPath}");
                
                if (!System.IO.File.Exists(jsonPath))
                {
                    _logger.LogError($"File not found: {jsonPath}");
                    return NotFound(new 
                    { 
                        error = "Data file not found", 
                        path = jsonPath,
                        currentDirectory = Directory.GetCurrentDirectory()
                    });
                }

                var jsonData = await System.IO.File.ReadAllTextAsync(jsonPath);
                var data = JsonSerializer.Deserialize<JsonElement>(jsonData);

                _logger.LogInformation("Data loaded successfully");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading data");
                return StatusCode(500, new 
                { 
                    error = ex.Message, 
                    stackTrace = ex.StackTrace 
                });
            }
        }
    }
}
