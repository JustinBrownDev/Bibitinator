using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class BibiteReflect
    {
        public class Transform
        {
            public List<double> _position { get; set; }
            public double _rotation { get; set; }
            public double _scale { get; set; }
        }

        public class Rb2d
        {
            public double px { get; set; }
            public double py { get; set; }
            public double vx { get; set; }
            public double vy { get; set; }
            public double m { get; set; }
            public double r { get; set; }
        }
        public class Genes1
        {
            public double LayTime { get; set; }
            public double BroodTime { get; set; }
            public double HatchTime { get; set; }
            public double SizeRatio { get; set; }
            public double SpeedRatio { get; set; }
            public double ColorR { get; set; }
            public double ColorG { get; set; }
            public double ColorB { get; set; }
            public double Strength { get; set; }
            public double MutationAmountSigma { get; set; }
            public double AverageMutationNumber { get; set; }
            public double BrainMutationSigma { get; set; }
            public double BrainAverageMutation { get; set; }
            public double ViewAngle { get; set; }
            public double ViewRadius { get; set; }
            public double ClockSpeed { get; set; }
            public double PheroSense { get; set; }
            public double Diet { get; set; }
            public double Defense { get; set; }
            public double ImmuneSystemStrength { get; set; }
            public double HerdSeparationWeight { get; set; }
            public double HerdAlignmentWeight { get; set; }
            public double HerdCohesionWeight { get; set; }
            public double HerdVelocityWeight { get; set; }
            public double HerdSeparationDistance { get; set; }
            public double GrowthScale { get; set; }
            public double GrowthMaturityFactor { get; set; }
            public double GrowthMaturityExponent { get; set; }
        }
        public class Genes
        {
            public Genes1 genes { get; set; }
            public bool isReady { get; set; }
            public int gen { get; set; }
        }

        public class Mouth
        {
            public bool attackedLastFrame { get; set; }
            public double biteProgress { get; set; }
        }

        public class Body
        {
            public Mouth mouth { get; set; }
            public double d2Size { get; set; }
            public double d1Size { get; set; }
            public double health { get; set; }
            public double maxHealth { get; set; }
            public double energy { get; set; }
            public double maxEnergy { get; set; }
            public double wasteBank { get; set; }
            public double attackedDmg { get; set; }
            public bool dying { get; set; }
            public bool isWow { get; set; }
            public bool born { get; set; }
        }

        public class Growth
        {
            public double maturity { get; set; }
            public bool mature { get; set; }
        }

        public class Clock
        {
            public int tic { get; set; }
            public double ticProgress { get; set; }
            public double timeAlive { get; set; }
            public double chronoTime { get; set; }
        }

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

        public class Synaps
        {
            public int Inov { get; set; }
            public int NodeIn { get; set; }
            public int NodeOut { get; set; }
            public double Weight { get; set; }
            public bool En { get; set; }
        }

        public class Brain
        {
            public bool isReady { get; set; }
            public bool parent { get; set; }
            public List<Node> Nodes { get; set; }
            public List<Synaps> Synapses { get; set; }
        }

        public class ImmuneSystem
        {
            public List<object> activeViruses { get; set; }
            public List<object> strainResistances { get; set; }
            public double activationLevel { get; set; }
            public double perceivedInfectionLevel { get; set; }
        }

        public class Root
        {
            public Transform transform { get; set; }
            public Rb2d rb2d { get; set; }
            public Genes genes { get; set; }
            public Body body { get; set; }
            public Growth growth { get; set; }
            public Clock clock { get; set; }
            public Brain brain { get; set; }
            public ImmuneSystem immuneSystem { get; set; }
        }
    }
}
