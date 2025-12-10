using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRE_Portal.DAL.Models
{
    public class EnergyData
    {
        public int Year { get; set; }
        public double Production { get; set; } // en GWh

        //This comment is so cool
        //yay

        public string Source { get; set; } = string.Empty; // solaire, hydro, etc.
    }
}

