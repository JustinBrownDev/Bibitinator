using Newtonsoft.Json.Linq;
using Bibitinator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bibitinator.Models
{
    public class settingControl
    {
        public JToken jprop { get; set; }
        public settingsDictionary.setting set { get; set; }
    }
}
