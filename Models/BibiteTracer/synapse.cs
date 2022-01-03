using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models.BibiteTracer
{
    public class synapse
    {
        public decimal weight { get; set; }
        public Int16 inNode { get; set; }
        public Int16 outNode { get; set; }
    }
}
