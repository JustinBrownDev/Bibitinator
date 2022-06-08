using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    public class BibiteCollection
    {
        public string name { get; set; }
        public string json { get; set; }
        public string saveTo { get; set; }
        public JObject dynRoot { get; set; }
    }
}
