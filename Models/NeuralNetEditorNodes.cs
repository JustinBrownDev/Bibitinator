using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    internal class NeuralNetEditorNodes
    {
        public class Node
        {
            public int Type { get; set; }
            public string TypeName { get; set; }
            public int Index { get; set; }
            public int Inov { get; set; }
            public int NIn { get; set; }
            public int NOut { get; set; }
            public string Desc { get; set; }
            public double Value { get; set; }
            public double LastInput { get; set; }
            public double LastOutput { get; set; }
        }
        //public JArray StaticNodes = JArray.Parse("[{\"Type\":0,\"TypeName\":\"Input\",\"Index\":0,\"Inov\":0,\"Desc\":\"Constant\",\"Value\":1.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":1,\"Inov\":0,\"Desc\":\"EnergyRatio\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":2,\"Inov\":0,\"Desc\":\"Maturity\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":3,\"Inov\":0,\"Desc\":\"LifeRatio\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":4,\"Inov\":0,\"Desc\":\"Fullness\",\"Value\":0.9222752,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":5,\"Inov\":0,\"Desc\":\"Speed\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":6,\"Inov\":0,\"Desc\":\"IsGrabbingObjects\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":7,\"Inov\":0,\"Desc\":\"AttackDamage\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":8,\"Inov\":0,\"Desc\":\"BibiteConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":9,\"Inov\":0,\"Desc\":\"BibiteConcentrationAngle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":10,\"Inov\":0,\"Desc\":\"NVisibleBibites\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":11,\"Inov\":0,\"Desc\":\"PelletConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":12,\"Inov\":0,\"Desc\":\"PelletConcentrationAngle\",\"Value\":-0.00272769528,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":13,\"Inov\":0,\"Desc\":\"NVisiblePellets\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":14,\"Inov\":0,\"Desc\":\"MeatConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":15,\"Inov\":0,\"Desc\":\"MeatConcentrationAngle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":16,\"Inov\":0,\"Desc\":\"NVisibleMeat\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":17,\"Inov\":0,\"Desc\":\"ClosestBibiteR\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":18,\"Inov\":0,\"Desc\":\"ClosestBibiteG\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":19,\"Inov\":0,\"Desc\":\"CloestBibiteB\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":20,\"Inov\":0,\"Desc\":\"Tic\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":21,\"Inov\":0,\"Desc\":\"Minute\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":22,\"Inov\":0,\"Desc\":\"TimeAlive\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":23,\"Inov\":0,\"Desc\":\"PheroSense1\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":24,\"Inov\":0,\"Desc\":\"PheroSense2\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":25,\"Inov\":0,\"Desc\":\"PheroSense3\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":26,\"Inov\":0,\"Desc\":\"Phero1Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":27,\"Inov\":0,\"Desc\":\"Phero2Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":28,\"Inov\":0,\"Desc\":\"Phero3Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":29,\"Inov\":0,\"Desc\":\"PheroHeading1\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":30,\"Inov\":0,\"Desc\":\"PheroHeading2\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":31,\"Inov\":0,\"Desc\":\"PheroHeading3\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":32,\"Inov\":0,\"Desc\":\"InfectionRate\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":33,\"Inov\":0,\"Desc\":\"Accelerate\",\"Value\":0.380619019,\"LastInput\":0.40078333,\"LastOutput\":0.380619019},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":34,\"Inov\":0,\"Desc\":\"Rotate\",\"Value\":-0.00270018727,\"LastInput\":-0.00270019379,\"LastOutput\":-0.00270018727},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":35,\"Inov\":0,\"Desc\":\"Herding\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":36,\"Inov\":0,\"Desc\":\"Want2Lay\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":37,\"Inov\":0,\"Desc\":\"Want2Eat\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":38,\"Inov\":0,\"Desc\":\"Digestion\",\"Value\":0.84410584,\"LastInput\":1.68910074,\"LastOutput\":0.84410584},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":39,\"Inov\":0,\"Desc\":\"Grab\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":40,\"Inov\":0,\"Desc\":\"ClkReset\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":41,\"Inov\":0,\"Desc\":\"Phere1Out\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":42,\"Inov\":0,\"Desc\":\"Phere2Out\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":43,\"Inov\":0,\"Desc\":\"Phere3Out\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":44,\"Inov\":0,\"Desc\":\"Want2Grow\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":45,\"Inov\":0,\"Desc\":\"Want2Heal\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":46,\"Inov\":0,\"Desc\":\"Want2Attack\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":47,\"Inov\":0,\"Desc\":\"ImmuneSystem\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0}]");
        public Node[] inputNodesOld = {
            new Node { Index = 0, Desc = "Constant", TypeName = "Input", NIn = 0, NOut = 0, Type = 0},
            new Node { Index = 1, Desc = "EnergyRatio", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 2, Desc = "Maturity", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 3, Desc = "LifeRatio", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 4, Desc = "Speed", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 5, Desc = "IsGrabbingObjects", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 6, Desc = "AttackDamage", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 7, Desc = "BibiteConcentrationWeight", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 8, Desc = "BibiteConcentrationAngle", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 9, Desc = "NVisibleBibites", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 10, Desc = "PelletConcentrationWeight", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 11, Desc = "PelletConcentrationAngle", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 12, Desc = "NVisiblePellets", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 13, Desc = "MeatConcentrationWeight", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 14, Desc = "MeatConcentrationAngle", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 15, Desc = "NVisibleMeat", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 16, Desc = "ClosestBibiteR", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 17, Desc = "ClosestBibiteG", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 18, Desc = "CloestBibiteB", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 19, Desc = "Tic", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 20, Desc = "Minute", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 21, Desc = "TimeAlive", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 22, Desc = "PheroSense1", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 23, Desc = "PheroSense2", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 24, Desc = "PheroSense3", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 25, Desc = "Phero1Angle", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 26, Desc = "Phero2Angle", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 27, Desc = "Phero3Angle", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 },
            new Node { Index = 28, Desc = "InfectionRate", TypeName = "Input", NIn = 0, NOut = 0, Type = 0 }
            };
        public Node[] inputNodesNew = {
            new Node { Index = 0, Desc = "Constant", TypeName = "Input", Type = 0},
            new Node { Index = 1, Desc = "EnergyRatio", TypeName = "Input", Type = 0 },
            new Node { Index = 2, Desc = "Maturity", TypeName = "Input", Type = 0 },
            new Node { Index = 3, Desc = "LifeRatio", TypeName = "Input", Type = 0 },
            new Node { Index = 4, Desc = "Fullness", TypeName = "Input", Type = 0 },
            new Node { Index = 5, Desc = "Speed", TypeName = "Input", Type = 0 },
            new Node { Index = 6, Desc = "IsGrabbingObjects", TypeName = "Input", Type = 0 },
            new Node { Index = 7, Desc = "AttackDamage", TypeName = "Input", Type = 0 },
            new Node { Index = 8, Desc = "BibiteConcentrationWeight", TypeName = "Input", Type = 0 },
            new Node { Index = 9, Desc = "BibiteConcentrationAngle", TypeName = "Input", Type = 0 },
            new Node { Index = 10, Desc = "NVisibleBibites", TypeName = "Input", Type = 0 },
            new Node { Index = 11, Desc = "PelletConcentrationWeight", TypeName = "Input", Type = 0 },
            new Node { Index = 12, Desc = "PelletConcentrationAngle", TypeName = "Input", Type = 0 },
            new Node { Index = 13, Desc = "NVisiblePellets", TypeName = "Input", Type = 0 },
            new Node { Index = 14, Desc = "MeatConcentrationWeight", TypeName = "Input", Type = 0 },
            new Node { Index = 15, Desc = "MeatConcentrationAngle", TypeName = "Input", Type = 0 },
            new Node { Index = 16, Desc = "NVisibleMeat", TypeName = "Input", Type = 0 },
            new Node { Index = 17, Desc = "ClosestBibiteR", TypeName = "Input", Type = 0 },
            new Node { Index = 18, Desc = "ClosestBibiteG", TypeName = "Input", Type = 0 },
            new Node { Index = 19, Desc = "CloestBibiteB", TypeName = "Input", Type = 0 },
            new Node { Index = 20, Desc = "Tic", TypeName = "Input", Type = 0 },
            new Node { Index = 21, Desc = "Minute", TypeName = "Input", Type = 0 },
            new Node { Index = 22, Desc = "TimeAlive", TypeName = "Input", Type = 0 },
            new Node { Index = 23, Desc = "PheroSense1", TypeName = "Input", Type = 0 },
            new Node { Index = 24, Desc = "PheroSense2", TypeName = "Input", Type = 0 },
            new Node { Index = 25, Desc = "PheroSense3", TypeName = "Input", Type = 0 },
            new Node { Index = 26, Desc = "Phero1Angle", TypeName = "Input", Type = 0 },
            new Node { Index = 27, Desc = "Phero2Angle", TypeName = "Input", Type = 0 },
            new Node { Index = 28, Desc = "Phero3Angle", TypeName = "Input", Type = 0 },
            new Node { Index = 29, Desc = "PheroHeading1", TypeName = "Input", Type = 0 },
            new Node { Index = 30, Desc = "PheroHeading2", TypeName = "Input", Type = 0 },
            new Node { Index = 31, Desc = "PheroHeading3", TypeName = "Input", Type = 0 },
            new Node { Index = 32, Desc = "InfectionRate", TypeName = "Input", Type = 0 }
            };
        public Node[] outputNodesOld = {
            new Node { Index = 29, Desc = "Accelerate", TypeName = "TanH", Type = 3, NIn = 0, NOut = 0},
            new Node { Index = 30, Desc = "Rotate", TypeName = "TanH", Type = 3, NIn = 0, NOut = 0},
            new Node { Index = 31, Desc = "Herding", TypeName = "TanH", Type = 3, NIn = 0, NOut = 0},
            new Node { Index = 32, Desc = "Want2Lay", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 33, Desc = "Want2Eat", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 34, Desc = "Want2Sex", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 35, Desc = "Grab", TypeName = "TanH", Type = 3, NIn = 0, NOut = 0},
            new Node { Index = 36, Desc = "ClkReset", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 37, Desc = "Phere1Out", TypeName = "ReLu", Type = 5, NIn = 0, NOut = 0},
            new Node { Index = 38, Desc = "Phere2Out", TypeName = "ReLu", Type = 5, NIn = 0, NOut = 0},
            new Node { Index = 39, Desc = "Phere3Out", TypeName = "ReLu", Type = 5, NIn = 0, NOut = 0},
            new Node { Index = 40, Desc = "Want2Grow", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 41, Desc = "Want2Heal", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 42, Desc = "Want2Attack", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 43, Desc = "ImmuneSystem", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
        };
        public Node[] outputNodesNew = {
            new Node { Index = 33, Desc = "Accelerate", TypeName = "TanH", Type = 3, NIn = 0, NOut = 0},
            new Node { Index = 34, Desc = "Rotate", TypeName = "TanH", Type = 3, NIn = 0, NOut = 0},
            new Node { Index = 35, Desc = "Herding", TypeName = "TanH", Type = 3, NIn = 0, NOut = 0},
            new Node { Index = 36, Desc = "Want2Lay", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 37, Desc = "Want2Eat", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 38, Desc = "Want2Sex", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 39, Desc = "Grab", TypeName = "TanH", Type = 3, NIn = 0, NOut = 0},
            new Node { Index = 40, Desc = "ClkReset", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 41, Desc = "Phere1Out", TypeName = "ReLu", Type = 5, NIn = 0, NOut = 0},
            new Node { Index = 42, Desc = "Phere2Out", TypeName = "ReLu", Type = 5, NIn = 0, NOut = 0},
            new Node { Index = 43, Desc = "Phere3Out", TypeName = "ReLu", Type = 5, NIn = 0, NOut = 0},
            new Node { Index = 44, Desc = "Want2Grow", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 45, Desc = "Want2Heal", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 46, Desc = "Want2Attack", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
            new Node { Index = 47, Desc = "ImmuneSystem", TypeName = "Sigmoid", Type = 1, NIn = 0, NOut = 0},
        };
        public Node[] middleNodes ={
            new Node { Type = 1, TypeName = "Sigmoid"},
            new Node { Type = 2, TypeName = "Linear"},
            new Node { Type = 3, TypeName = "TanH"},
            new Node { Type = 4, TypeName = "Sine"},
            new Node { Type = 5, TypeName = "ReLu"},
            new Node { Type = 6, TypeName = "Gaussian"},
            new Node { Type = 7, TypeName = "Latch"},
            new Node { Type = 8, TypeName = "Differential"},
        };
        public List<Node> hiddenNodes = new List<Node>();
    }
}
