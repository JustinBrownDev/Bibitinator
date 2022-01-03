using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    public class BibiteCollection
    {
        public string name { get; set; }
        public string json { get; set; }
        public string extractFolder { get; set; }
        public BibiteReflect.Root Root { get; set; }
    }
}
