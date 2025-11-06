using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRE_Portal.DAL.Models;

namespace NRE_Portal.DAL.Interfaces
{
    public interface IEnergyRepository
    {
        IEnumerable<EnergyData> GetEnergyData();
    }
}

