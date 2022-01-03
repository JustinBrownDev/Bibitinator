using System;
using System.Collections.Generic;
using System.Text;
using Bibitinator.Models;
using System.Windows.Forms;
using System.Globalization;
using Bibitinator.Models.BibiteModel;

namespace Bibitinator
{
    public class Constructors
    {
        public List<string> InputNodeDictionary()
        {
            List<string> model = new List<string>();
            model.Add("Constant");
            model.Add("EnergyRatio");
            model.Add("Maturity");
            model.Add("LifeRatio");
            model.Add("Speed");
            model.Add("IsGrabbingObjects");
            model.Add("AttackedDamage");
            model.Add("BibiteConcentrationWeight");
            model.Add("BibiteConcentrationAngle");
            model.Add("NVisibleBibites");
            model.Add("PelletConcentrationWeight");
            model.Add("PelletConcentrationAngle");
            model.Add("NVisiblePellets");
            model.Add("MeatConcentrationWeight");
            model.Add("MeatConcentrationAngle");
            model.Add("NVisibleMeat");
            model.Add("ClosestBibiteR");
            model.Add("ClosestBibiteG");
            model.Add("ClosestBibiteB");
            model.Add("Tic");
            model.Add("Minute");
            model.Add("TimeAlive");
            model.Add("PheroSense1");
            model.Add("PheroSense2");
            model.Add("PheroSense3");
            model.Add("Phero1Angle");
            model.Add("Phero2Angle");
            model.Add("Phero3Angle");
            model.Add("InfectionRate");
            return model;
        }
        public List<string> OutputNodeDictionary()
        {
            List<string> model = new List<string>();
            model.Add("Accelerate");
            model.Add("Rotate");
            model.Add("Herding");
            model.Add("Want2Lay");
            model.Add("Want2Eat");
            model.Add("Want2Sex");
            model.Add("Grab");
            model.Add("ClkReset");
            model.Add("PhereOut1");
            model.Add("PhereOut2");
            model.Add("PhereOut3");
            model.Add("Want2Grow");
            model.Add("Want2Heal");
            model.Add("Want2Attack");
            model.Add("ImmuneSystem");
            return model;
        }
        public List<string> middleNodeDictionary()
        {
            List<string> model = new List<string>();
            model.Add("TanH");
            model.Add("Gaussian");
            model.Add("Linear");
            model.Add("Latch");
            model.Add("Sine");
            model.Add("Differential");
            model.Add("ReLu");
            model.Add("Sigmoid");
            return model;
        }

        //Construct Model from Tree
        public bibiteModel constructModel(TreeNode tree)
        {
            decimal? NaNull(string s)
            {
                if (s.Equals("NaN"))
                {
                    return null;
                }
                else
                {
                    return Convert.ToDecimal(s);
                }
            }
            bibiteModel model = new bibiteModel();
            try
            {
                int brainVersionAdjust = new int();
                if (tree.Nodes[1].Nodes.Count == 5)
                {
                    brainVersionAdjust = 2;
                    //NEW VERSION HERE
                    model.version = 1;
                    model.body.biteProgress = Convert.ToDecimal(tree.Nodes[3].Nodes[0].Nodes[1].Nodes[0].Text);
                    for(int i = 0; i < tree.Nodes[3].Nodes[1].Nodes.Count; i++)
                    {
                        stomachContentModel stomach = new stomachContentModel();
                        stomach.material = tree.Nodes[3].Nodes[1].Nodes[i].Nodes[0].Nodes[0].Text;
                        stomach.amount = Convert.ToDecimal(tree.Nodes[3].Nodes[1].Nodes[i].Nodes[1].Nodes[0].Text);
                        stomach.averageChunkAmount = Convert.ToDecimal(tree.Nodes[3].Nodes[1].Nodes[i].Nodes[2].Nodes[0].Text);
                        model.body.stomachContent.Add(stomach);
                    }
                    model.body.health = Convert.ToDecimal(tree.Nodes[3].Nodes[2].Nodes[0].Text);
                    model.body.energy = Convert.ToDecimal(tree.Nodes[3].Nodes[3].Nodes[0].Text);
                    model.body.d2Size = Convert.ToDecimal(tree.Nodes[3].Nodes[4].Nodes[0].Text);
                    model.body.dying = Convert.ToBoolean(tree.Nodes[3].Nodes[5].Nodes[0].Text);
                    model.body.born = Convert.ToBoolean(tree.Nodes[3].Nodes[6].Nodes[0].Text);
                }
                else
                {
                    brainVersionAdjust = 0;
                    //OLD VERSION HERE
                    model.version = 0;
                    model.body.d2Size = Convert.ToDecimal(tree.Nodes[3].Nodes[1].Nodes[0].Text);
                    model.body.d1Size = Convert.ToDecimal(tree.Nodes[3].Nodes[2].Nodes[0].Text);
                    model.body.health = Convert.ToDecimal(tree.Nodes[3].Nodes[3].Nodes[0].Text);
                    model.body.maxHealth = Convert.ToDecimal(tree.Nodes[3].Nodes[4].Nodes[0].Text);
                    model.body.energy = Convert.ToDecimal(tree.Nodes[3].Nodes[5].Nodes[0].Text);
                    model.body.maxEnergy = Convert.ToDecimal(tree.Nodes[3].Nodes[6].Nodes[0].Text);
                    model.body.dying = Convert.ToBoolean(tree.Nodes[3].Nodes[9].Nodes[0].Text);
                    model.body.isWow = Convert.ToBoolean(tree.Nodes[3].Nodes[10].Nodes[0].Text);
                    model.body.born = Convert.ToBoolean(tree.Nodes[3].Nodes[11].Nodes[0].Text);
                }
                //universal constructors
                model.position0 = Convert.ToDecimal(tree.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Text);
                model.position1 = Convert.ToDecimal(tree.Nodes[0].Nodes[0].Nodes[1].Nodes[0].Text);
                model.rotation = Convert.ToDecimal(tree.Nodes[0].Nodes[1].Nodes[0].Text);
                model.scale = Convert.ToDecimal(tree.Nodes[0].Nodes[2].Nodes[0].Text);
                for (int n = 0; n < tree.Nodes[1].Nodes.Count; n++)
                {
                    model.rb2d.Add(Convert.ToDecimal(tree.Nodes[1].Nodes[n].Nodes[0].Text));
                }
                for (int n = 0; n <= tree.Nodes[2].Nodes[0].Nodes.Count; n++)
                {
                    model.genes.Add(Convert.ToDecimal(tree.Nodes[2].Nodes[0].Nodes[n].Nodes[0].Text));
                }
                model.isReady = Convert.ToBoolean(tree.Nodes[2].Nodes[1].Nodes[0].Text);
                model.gen = Convert.ToInt32(tree.Nodes[2].Nodes[2].Nodes[0].Text);
                model.body.attackedLastFrame = Convert.ToBoolean(tree.Nodes[3].Nodes[0].Nodes[0].Nodes[0].Text);
                model.body.wasteBank = NaNull(tree.Nodes[3].Nodes[7].Nodes[0].Text);
                model.body.attackDamage = NaNull(tree.Nodes[3].Nodes[8].Nodes[0].Text);
                model.maturity = Convert.ToDecimal(tree.Nodes[4].Nodes[0].Nodes[0].Text);
                model.mature = Convert.ToBoolean(tree.Nodes[4].Nodes[1].Nodes[0].Text);
                model.tic = Convert.ToInt32(tree.Nodes[5].Nodes[0].Nodes[0].Text);
                model.ticProgress = Convert.ToDecimal(tree.Nodes[5].Nodes[1].Nodes[0].Text);
                model.timeAlive = Convert.ToDecimal(tree.Nodes[5].Nodes[2].Nodes[0].Text);
                model.chronoTime = Convert.ToDecimal(tree.Nodes[5].Nodes[3].Nodes[0].Text);
                model.brain.isReady = Convert.ToBoolean(tree.Nodes[6].Nodes[0].Nodes[0].Text);
                model.brain.parent = Convert.ToBoolean(tree.Nodes[6].Nodes[1].Nodes[0].Text);
                
                for (int n = 0; n < tree.Nodes[6].Nodes.Count; n++)
                {
                    neuronModel m = new neuronModel();
                    m.type = Convert.ToInt32(tree.Nodes[6].Nodes[n].Nodes[0].Nodes[0].Text);
                    m.typeName = tree.Nodes[6].Nodes[n].Nodes[1].Nodes[0].Text;
                    m.index = Convert.ToInt32(tree.Nodes[6].Nodes[n].Nodes[1].Nodes[0].Text);
                    m.inov = Convert.ToInt32(tree.Nodes[6].Nodes[n].Nodes[2].Nodes[0].Text);
                    if(brainVersionAdjust == 0)
                    {
                        m.nIn = Convert.ToInt32(tree.Nodes[6].Nodes[n].Nodes[3].Nodes[0].Text);
                        m.nOut = Convert.ToInt32(tree.Nodes[6].Nodes[n].Nodes[4].Nodes[0].Text);
                    }
                    m.desc = tree.Nodes[6].Nodes[n].Nodes[5 - brainVersionAdjust].Nodes[0].Text;
                    m.value = Decimal.Parse(tree.Nodes[6].Nodes[n].Nodes[6 - brainVersionAdjust].Nodes[0].Text, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                    m.lastInput = Convert.ToInt32(tree.Nodes[6].Nodes[n].Nodes[7 - brainVersionAdjust].Nodes[0].Text);
                    m.lastOutput = Convert.ToInt32(tree.Nodes[6].Nodes[n].Nodes[8 - brainVersionAdjust].Nodes[0].Text);
                    model.brain.nodes.Add(m);
                }
                for (int n = 0; n < tree.Nodes[7].Nodes.Count; n++)
                {
                    SynapseModel s = new SynapseModel();
                    s.inov = Convert.ToInt32(tree.Nodes[7].Nodes[n].Nodes[0].Nodes[0].Text);
                    s.nodeIn = Convert.ToInt32(tree.Nodes[7].Nodes[n].Nodes[1].Nodes[0].Text);
                    s.nodeOut = Convert.ToInt32(tree.Nodes[7].Nodes[n].Nodes[2].Nodes[0].Text);
                    s.weight = Decimal.Parse(tree.Nodes[7].Nodes[n].Nodes[3].Nodes[0].Text, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                    s.en = Convert.ToBoolean(tree.Nodes[7].Nodes[n].Nodes[4].Nodes[0].Text);
                    model.brain.synapses.Add(s);
                }
                for (int n = 0; n < tree.Nodes[8].Nodes[0].Nodes.Count; n++)
                {
                    VirusModel v = new VirusModel();
                    v.index = n;
                    v.gen = Convert.ToInt32(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[0].Nodes[0].Text);
                    v.rootStrain = tree.Nodes[8].Nodes[0].Nodes[n].Nodes[1].Nodes[0].Text;
                    v.strain = tree.Nodes[8].Nodes[0].Nodes[n].Nodes[2].Nodes[0].Text;
                    v.cellInfection = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[3].Nodes[0].Text);
                    v.airInfection = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[4].Nodes[0].Text);
                    v.maxInfectionDuringLifetime = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[5].Nodes[0].Text);
                    for (int i = 0; i <= 12; i++)
                    {
                        v.genes.Add(Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[6].Nodes[i].Nodes[0].Text));
                    }
                    v.healthDamage = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[7].Nodes[0].Text);
                    v.energyDamage = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[8].Nodes[0].Text);
                    v.immuneDamage = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[9].Nodes[0].Text);
                    v.cellProduction = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[10].Nodes[0].Text);
                    v.skinProduction = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[11].Nodes[0].Text);
                    v.airVirionProduction = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[12].Nodes[0].Text);
                    v.healthDamageThisFrame = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[13].Nodes[0].Text);
                    v.energyDamageThisFrame = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[14].Nodes[0].Text);
                    v.immuneDamageThisFrame = Convert.ToDecimal(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[15].Nodes[0].Text);
                    v.isAlive = Convert.ToBoolean(tree.Nodes[8].Nodes[0].Nodes[n].Nodes[15].Nodes[0].Text);
                    model.activeViruses.Add(v);
                }
                //model.strainResistances??
                model.activationLevel = Convert.ToDecimal(tree.Nodes[8].Nodes[2].Nodes[0].Text);
                model.percivedInfectionLevel = Convert.ToDecimal(tree.Nodes[8].Nodes[3].Nodes[0].Text);
            }
            catch
            {
                return null;
            }
            return model;
        }
        //Construct Bibite.json from models
        public string constructJsonString(bibiteModel bibite)
        {
            //construct Json string
            int nodeCount = bibite.brain.nodes.Count;
            int synCount = bibite.brain.synapses.Count;
            int virCount = bibite.activeViruses.Count;




            string result = "{\"transform\":{\"_position\":[" + bibite.position0 + ","
                + bibite.position1 + "],\"_rotation\":" + bibite.rotation + ",\"_scale\":" + bibite.scale + "},\"rb2d\":{\"px\":" + bibite.rb2d[0] + ",\"py\":" + bibite.rb2d[1] +
                ",\"vx\":" + bibite.rb2d[2] + ",\"vy\":" + bibite.rb2d[3] + ",\"m\":" + bibite.rb2d[4] + ",\"r\":" + bibite.rb2d[5] +
                "},\"genes\":{\"genes\":{\"LayTime\":" + bibite.genes[0] + ",\"BroodTime\":" + bibite.genes[1] + ",\"HatchTime\":" + bibite.genes[2] + ",\"SizeRatio\":" +
                bibite.genes[3] + ",\"SpeedRatio\":" + bibite.genes[4] + ",\"ColorR\":" + bibite.genes[5] + ",\"ColorG\":" + bibite.genes[6] + ",\"ColorB\":" + bibite.genes[7] +
                ",\"Strength\":" + bibite.genes[8] + ",\"MutationAmountSigma\":" + bibite.genes[9] + ",\"AverageMutationNumber\":" + bibite.genes[10] + ",\"BrainMutationSigma\":" +
                bibite.genes[11] + ",\"BrainAverageMutation\":" + bibite.genes[12] + ",\"ViewAngle\":" + bibite.genes[13] + ",\"ViewRadius\":" + bibite.genes[14] + ",\"ClockSpeed\":" +
                bibite.genes[15] + ",\"PheroSense\":" + bibite.genes[16] + ",\"Diet\":" + bibite.genes[17] + ",\"ImmuneSystemStrength\":" + bibite.genes[18] + ",\"HerdSeparationWeight\":" +
                bibite.genes[19] + ",\"HerdAlignmentWeight\":" + bibite.genes[20] + ",\"HerdCohesionWeight\":" + bibite.genes[21] + ",\"HerdVelocityWeight\":" + bibite.genes[22] +
                ",\"HerdSeparationDistance\":" + bibite.genes[23] +
                "},\"isReady\":" + bibite.isReady + ",\"gen\":" + bibite.gen + "},\"body\":{\"mouth\":{\"attackedLastFrame\":" + bibite.body.attackedLastFrame.ToString() + "},\"d2Size\":" +
                bibite.body.d2Size + ",\"d1Size\":" + bibite.body.d1Size + ",\"health\":" + bibite.body.health + ",\"maxHealth\":" + bibite.body.maxEnergy + ",\"energy\":" + bibite.body.energy +
                ",\"maxEnergy\":" + bibite.body.maxEnergy + ",\"wasteBank\":\"" + bibite.body.wasteBank + "\",\"attackedDmg\":\"" + bibite.body.attackDamage + "\",\"dying\":" +
                bibite.body.dying.ToString() + ",\"isWow\":" + bibite.body.isWow.ToString() + ",\"born\":" + bibite.body.born.ToString() +
                "},\"growth\":{\"maturity\":" + bibite.maturity + ",\"mature\":" + bibite.mature.ToString() + "},\"clock\":{\"tic\":" + bibite.tic + ",\"ticProgress\":" +
                bibite.ticProgress + ",\"timeAlive\":" + bibite.timeAlive + ",\"chronoTime\":" + bibite.chronoTime + "}," +
                "\"brain\":{\"isReady\":" + bibite.isReady.ToString() + ",\"parent\":" + bibite.brain.parent.ToString() + ",\"Nodes\":[";
            //iterate over all neurons
            for (int i = 0; i < nodeCount; i++)
            {
                result += "{\"Type\":" + bibite.brain.nodes[i].type + ",\"TypeName\":" + bibite.brain.nodes[i].typeName + ",\"Index\":" + bibite.brain.nodes[i].index + ",\"Inov\":" +
                bibite.brain.nodes[i].inov + ",\"NIn\":" + bibite.brain.nodes[i].nIn + ",\"NOut\":" + bibite.brain.nodes[i].nOut + ",\"Desc\":\"" + bibite.brain.nodes[i].desc + "\",\"Value\":" +
                bibite.brain.nodes[i].value + ",\"LastInput\":" + bibite.brain.nodes[i].lastInput + ",\"LastOutput\":" + bibite.brain.nodes[i].lastOutput + "}";
                if (i != nodeCount) result += ",";
            }
            result += "],\"Synapses\":[";
            //iterate over all synapses
            for (int i = 0; i < synCount; i++)
            {
                result += "{\"Inov\":" + bibite.brain.synapses[i].inov + ",\"NodeIn\":" + bibite.brain.synapses[i].nodeIn + ",\"NodeOut\":" + bibite.brain.synapses[i].nodeOut +
                ",\"Weight\":" + bibite.brain.synapses[i].weight + ",\"En\":" + bibite.brain.synapses[i].en.ToString() + "}";
                if (i != synCount) result += ",";
            }
            result += "]},\"immuneSystem\":{\"activeViruses\":[";
            //iterate over all viruses
            for (int i = 0; i < virCount; i++)
            {
                result += "{\"Generation\":" + bibite.activeViruses[i].gen + ",\"RootStrain\":\"" + bibite.activeViruses[i].rootStrain + "\",\"Strain\":\"" + bibite.activeViruses[i].strain + "\",\"CellInfection\":" +
                    bibite.activeViruses[i].cellInfection + ",\"SkinInfection\":" + bibite.activeViruses[i].skinInfection + ",\"AirInfection\":" + bibite.activeViruses[i].airInfection +
                    ",\"MaxInfectionDuringLifetime\":" + bibite.activeViruses[i].maxInfectionDuringLifetime + ",\"Genes\":[" + bibite.activeViruses[i].genes +
                    //"0.504986644,0.231206119,0.472059429,1.45059764,0.7938081,0.8072818,0.6403645,0.132006243,1.26102376,1.2926532,1.39342022,0.177987486,0.0111954492"
                    "],\"HealthDamageFraction\":" + bibite.activeViruses[i].healthDamage + ",\"EnergyDamageFraction\":" + bibite.activeViruses[i].energyDamage + ",\"ImmuneDamageFraction\":" + bibite.activeViruses[i].immuneDamage +
                    ",\"CellProductionFraction\":" + bibite.activeViruses[i].cellProduction + ",\"SkinProductionFraction\":" + bibite.activeViruses[i].skinProduction + ",\"AirVirionProductionFraction\":" +
                    bibite.activeViruses[i].airVirionProduction + ",\"HealthDamageRateThisFrame\":" + bibite.activeViruses[i].healthDamageThisFrame + ",\"EnergyDamageRateThisFrame\":" +
                    bibite.activeViruses[i].energyDamageThisFrame + ",\"ImmuneDamageThisFrame\":" + bibite.activeViruses[i].immuneDamageThisFrame + ",\"IsAlive\":" + bibite.activeViruses[i].isAlive + "}";
                if (i != virCount) result += ",";
            }
            result += "],\"strainResistances\":[],\"activationLevel\":" + bibite.activationLevel + ",\"perceivedInfectionLevel\":" + bibite.percivedInfectionLevel + "}}\"";
            return result;
        }
    }
}
