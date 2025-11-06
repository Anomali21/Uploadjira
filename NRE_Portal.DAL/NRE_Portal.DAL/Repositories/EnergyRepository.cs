using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using NRE_Portal.DAL.Interfaces;
using NRE_Portal.DAL.Models;

namespace NRE_Portal.DAL.Repositories
{
    public class EnergyRepository : IEnergyRepository
    {
        private readonly string _jsonPath;

        public EnergyRepository(string? jsonPath = null)
        {
            if (!string.IsNullOrEmpty(jsonPath))
            {
                _jsonPath = jsonPath!;
            }
            else
            {
                // default: Data/energy_data.json relative to assembly directory
                var baseDir = Path.GetDirectoryName(typeof(EnergyRepository).Assembly.Location) ?? Directory.GetCurrentDirectory();
                _jsonPath = Path.Combine(baseDir, "Data", "energy_data.json");
            }
        }

        public IEnumerable<EnergyData> GetEnergyData()
        {
            if (!File.Exists(_jsonPath))
                return new List<EnergyData>();

            var json = File.ReadAllText(_jsonPath);
            return JsonSerializer.Deserialize<List<EnergyData>>(json) ?? new List<EnergyData>();
        }
    }
}

