using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    public class bibiteModel
    {
        //version 0 is the current public versioin
        //version 1 is the discord beta
        public Int32 version { get; set; }
        public Int32 bibiteIndex { get; set; }
        public decimal position0 { get; set; }
        public decimal position1 { get; set; }
        public decimal rotation { get; set; }
        public decimal scale { get; set; }
        public List<decimal> rb2d { get; set; }
        public List<decimal> genes { get; set; }
        public bool isReady { get; set; }
        public int gen { get; set; }
        public bibiteBodyModel body { get; set; }
        public decimal maturity { get; set; }
        public bool mature { get; set; }
        public int tic { get; set; }
        public decimal ticProgress { get; set; }
        public decimal timeAlive { get; set; }
        public decimal chronoTime { get; set; }
        public bibiteBrainModel brain { get; set; }
        public List<VirusModel> activeViruses { get; set; }
        // public List<strainResistancesModel> strainResistances { get; set; } 
        public decimal activationLevel { get; set; }
        public decimal percivedInfectionLevel { get; set; }
    }
}
