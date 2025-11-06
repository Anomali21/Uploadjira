using System.IO;
using System.Text.Json;
using NRE_Portal.DAL.Models;
using NRE_Portal.DAL.Repositories;
using Xunit;

public class EnergyRepositoryTests
{
    [Fact]
    public void GetEnergyData_ReturnsDataFromJson()
    {
        // arrange: create temp json file
        var tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(tempDir);
        var jsonPath = Path.Combine(tempDir, "energy_data.json");

        var sample = new[]
        {
            new EnergyData { Year = 2020, Production = 123.4, Source = "Test" },
            new EnergyData { Year = 2021, Production = 234.5, Source = "Test" }
        };

        File.WriteAllText(jsonPath, JsonSerializer.Serialize(sample));

        var repo = new EnergyRepository(jsonPath);

        // act
        var result = repo.GetEnergyData();

        // assert
        Assert.NotNull(result);
        Assert.Collection(result,
            item => {
                Assert.Equal(2020, item.Year);
                Assert.Equal(123.4, item.Production);
            },
            item => {
                Assert.Equal(2021, item.Year);
                Assert.Equal(234.5, item.Production);
            });

        // cleanup
        Directory.Delete(tempDir, true);
    }
}
