using Bibitinator.Models.BibiteModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    public class bibiteBodyModel
    {
        public bool attackedLastFrame { get; set; }
        public decimal biteProgress { get; set; }
        public List<stomachContentModel> stomachContent { get; set;} 
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
    }
}
