using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    internal class NeuralNetEditorNodes
    {
        public BibiteReflect.Node[] inputNodes = {
            new BibiteReflect.Node { Index = 0, Desc = "Constant", TypeName = "Input" },
            new BibiteReflect.Node { Index = 1, Desc = "EnergyRatio", TypeName = "Input" },
            new BibiteReflect.Node { Index = 2, Desc = "Maturity", TypeName = "Input" },
            new BibiteReflect.Node { Index = 3, Desc = "LifeRatio", TypeName = "Input" },
            new BibiteReflect.Node { Index = 4, Desc = "Speed", TypeName = "Input" },
            new BibiteReflect.Node { Index = 5, Desc = "IsGrabbingObjects", TypeName = "Input" },
            new BibiteReflect.Node { Index = 6, Desc = "AttackDamage", TypeName = "Input" },
            new BibiteReflect.Node { Index = 7, Desc = "BibiteConcentrationWeight", TypeName = "Input" },
            new BibiteReflect.Node { Index = 8, Desc = "BibiteConcentrationAngle", TypeName = "Input" },
            new BibiteReflect.Node { Index = 9, Desc = "NVisibleBibites", TypeName = "Input" },
            new BibiteReflect.Node { Index = 10, Desc = "PelletConcentrationWeight", TypeName = "Input" },
            new BibiteReflect.Node { Index = 11, Desc = "PelletConcentrationAngle", TypeName = "Input" },
            new BibiteReflect.Node { Index = 12, Desc = "NVisiblePellets", TypeName = "Input" },
            new BibiteReflect.Node { Index = 13, Desc = "MeatConcentrationWeight", TypeName = "Input" },
            new BibiteReflect.Node { Index = 14, Desc = "MeatConcentrationAngle", TypeName = "Input" },
            new BibiteReflect.Node { Index = 15, Desc = "NVisibleMeat", TypeName = "Input" },
            new BibiteReflect.Node { Index = 16, Desc = "ClosestBibiteR", TypeName = "Input" },
            new BibiteReflect.Node { Index = 17, Desc = "ClosestBibiteG", TypeName = "Input" },
            new BibiteReflect.Node { Index = 18, Desc = "CloestBibiteB", TypeName = "Input" },
            new BibiteReflect.Node { Index = 19, Desc = "Tic", TypeName = "Input" },
            new BibiteReflect.Node { Index = 20, Desc = "Minute", TypeName = "Input" },
            new BibiteReflect.Node { Index = 21, Desc = "TimeAlive", TypeName = "Input" },
            new BibiteReflect.Node { Index = 22, Desc = "PheroSense1", TypeName = "Input" },
            new BibiteReflect.Node { Index = 23, Desc = "PheroSense2", TypeName = "Input" },
            new BibiteReflect.Node { Index = 24, Desc = "PheroSense3", TypeName = "Input" },
            new BibiteReflect.Node { Index = 25, Desc = "Phero1Angle", TypeName = "Input" },
            new BibiteReflect.Node { Index = 26, Desc = "Phero2Angle", TypeName = "Input" },
            new BibiteReflect.Node { Index = 27, Desc = "Phero3Angle", TypeName = "Input" },
            new BibiteReflect.Node { Index = 28, Desc = "InfectionRate", TypeName = "Input" }
            };
        public BibiteReflect.Node[] outputNodes = {
            new BibiteReflect.Node { Index = 29, Desc = "Accelerate", TypeName = "TanH", Type = 3},
            new BibiteReflect.Node { Index = 30, Desc = "Rotate", TypeName = "TanH", Type = 3},
            new BibiteReflect.Node { Index = 31, Desc = "Herding", TypeName = "TanH", Type = 3},
            new BibiteReflect.Node { Index = 32, Desc = "Want2Lay", TypeName = "Sigmoid", Type = 1},
            new BibiteReflect.Node { Index = 33, Desc = "Want2Eat", TypeName = "Sigmoid", Type = 1},
            new BibiteReflect.Node { Index = 34, Desc = "Want2Sex", TypeName = "Sigmoid", Type = 1},
            new BibiteReflect.Node { Index = 35, Desc = "Grab", TypeName = "TanH", Type = 3},
            new BibiteReflect.Node { Index = 36, Desc = "ClkReset", TypeName = "Sigmoid", Type = 1},
            new BibiteReflect.Node { Index = 37, Desc = "Phere1Out", TypeName = "ReLu", Type = 5},
            new BibiteReflect.Node { Index = 38, Desc = "Phere2Out", TypeName = "ReLu", Type = 5},
            new BibiteReflect.Node { Index = 39, Desc = "Phere3Out", TypeName = "ReLu", Type = 5},
            new BibiteReflect.Node { Index = 40, Desc = "Want2Grow", TypeName = "Sigmoid", Type = 1},
            new BibiteReflect.Node { Index = 41, Desc = "Want2Heal", TypeName = "Sigmoid", Type = 1},
            new BibiteReflect.Node { Index = 42, Desc = "Want2Attack", TypeName = "Sigmoid", Type = 1},
            new BibiteReflect.Node { Index = 43, Desc = "ImmuneSystem", TypeName = "Sigmoid", Type = 1},
        };
        public BibiteReflect.Node[] middleNodes ={
            new BibiteReflect.Node { Type = 1, TypeName = "Sigmoid"},
            new BibiteReflect.Node { Type = 2, TypeName = "Linear"},
            new BibiteReflect.Node { Type = 3, TypeName = "TanH"},
            new BibiteReflect.Node { Type = 4, TypeName = "Sine"},
            new BibiteReflect.Node { Type = 5, TypeName = "ReLu"},
            new BibiteReflect.Node { Type = 6, TypeName = "Gaussian"},
            new BibiteReflect.Node { Type = 7, TypeName = "Latch"},
            new BibiteReflect.Node { Type = 8, TypeName = "Differential"},
        };
    }
}
