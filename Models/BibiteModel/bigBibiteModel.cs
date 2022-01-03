using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    public class bigBibiteModel
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
        public bool attackedLastFrame { get; set; }
        public decimal biteProgress { get; set; }
        public decimal d2Size { get; set; }
        public decimal d1Size { get; set; }
        public decimal health { get; set; }
        public decimal maxHealth { get; set; }
        public decimal energy { get; set; }
        public decimal maxEnergy { get; set; }
        public decimal? wasteBank { get; set; }
        public decimal? attackDamage { get; set; }
        public bool dying { get; set; }
        public bool isWow { get; set; }
        public bool born { get; set; }
        public decimal maturity { get; set; }
        public bool mature { get; set; }
        public int tic { get; set; }
        public decimal ticProgress { get; set; }
        public decimal timeAlive { get; set; }
        public decimal chronoTime { get; set; }
        public decimal activationLevel { get; set; }
        public decimal percivedInfectionLevel { get; set; }
    }
}
