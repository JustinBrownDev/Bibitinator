using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    public class VirusModel
    {
        public Int32 index { get; set; }
        public Int32 gen { get; set; }
        public string rootStrain { get; set; }
        public string strain { get; set; }
        public decimal cellInfection { get; set; }
        public decimal airInfection { get; set; }
        public decimal skinInfection { get; set; }
        public decimal maxInfectionDuringLifetime { get; set; }
        public List<decimal> genes { get; set; }
        public decimal healthDamage { get; set; }
        public decimal energyDamage { get; set; }
        public decimal immuneDamage { get; set; }
        public decimal cellProduction { get; set; }
        public decimal skinProduction { get; set; }
        public decimal airVirionProduction { get; set; }
        public decimal healthDamageThisFrame { get; set; }
        public decimal energyDamageThisFrame { get; set; }
        public decimal immuneDamageThisFrame { get; set; }
        public bool isAlive { get; set; }
    }
}
