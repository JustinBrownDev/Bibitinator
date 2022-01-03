using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    public class SynapseModel
    {
        public Int32 inov { get; set; }
        public Int32 nodeIn { get; set; }
        public Int32 nodeOut { get; set; }
        public decimal weight { get; set; }
        public bool en { get; set; }
    }
}
