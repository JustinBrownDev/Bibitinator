using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Models
{
    public class settingsDictionary
    {
        public class setting
        {
            public string internalName { get; set; }
            public string internalLocation { get; set; }
            public string externalName { get; set; }
            public string Category { get; set; }
            public string parent { get; set; }
            public bool favorite { get; set; }
            public bool isBool { get; set; }
        }
        public setting[] knownSettings = new setting[]
        {
            //SIMULATION SETTINGS - viruses
            new setting { internalName = "virusEnabled", externalName = "Enable Viruses", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty, isBool = true},
            new setting { internalName = "virusGenerationTime", externalName = "Virus Generation Time", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "virusHealthDamageRate", externalName = "Virus Health Damage Rate", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "virusEnergyDamageRate", externalName = "Virus Energy Damage Rate", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "virusImmuneDamage", externalName = "Virus Immune Damage", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "cellInfectionProbabilityScale", externalName = "Cell Infection Probability Scale", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "cellInfectionProbabilityPower", externalName = "Cell Infection Probability Power", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "skinInfectionProbabilityScale", externalName = "Skin Infection Probability Scale", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "skinInfectionProbabilityPower", externalName = "Skin Infection Probability Power", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "airInfectionProbabilityScale", externalName = "Air Infection Probability Scale", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "airInfectionProbabilityPower", externalName = "Air Infection Probability Power", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "defaultAirVirionProduction", externalName = "Default Air Viron Production", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "airVirionSpeed", externalName = "Default Air Viron Speed", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "airVirionLifetime", externalName = "Air Viron Lifetime", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "immuneActivationHealthImpact", externalName = "Immune Activation Health Impact", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "immuneActivationEnergyImpact", externalName = "Immune Activation Energy Impact", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "immuneAbsoluteFighting", externalName = "Immune Absolute Fighting Rate", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "immuneRelativeFighting", externalName = "Immune Relative Fighting Rate", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            new setting { internalName = "virusCountFightingImpact", externalName = "Virus Count Fighting Imapact", Category = "Simulation", parent = "Viruses", internalLocation = string.Empty},
            //SIMULATION SETTINGS - pellet collision
            new setting { internalName = "pelletCollision", externalName = "Pellet Collision", Category = "Simulation", parent = "Pellet Collision", internalLocation = string.Empty, isBool = true},
            new setting { internalName = "pelletRotation", externalName = "Pellet Rotation", Category = "Simulation", parent = "Pellet Collision", internalLocation = string.Empty, isBool = true},
            new setting { internalName = "pelletMerge", externalName = "Pellet Merge", Category = "Simulation", parent = "Pellet Collision", internalLocation = string.Empty, isBool = true},
            //SIMULATION SETTINGS - cheat options
            new setting { internalName = "randomVirgin", externalName = "Randomize Virgin Births", Category = "Simulation", parent = "Cheat Options", internalLocation = string.Empty, isBool = true},
            new setting { internalName = "voidAvoidance", externalName = "Void-No-Mo", Category = "Simulation", parent = "Cheat Options", internalLocation = string.Empty, isBool = true},
            new setting { internalName = "voidAvoidanceDistance", externalName = "Void-No-Mo", Category = "Simulation", parent = "Cheat Options", internalLocation = string.Empty},
            //SIMULATION SETTINGS - world parameters
            new setting { internalName = "SimulationSize", externalName = "Size of Simulation", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty},
            new setting { internalName = "biomassDensity", externalName = "Biomass Density", Category = "Simulation", parent = "World Parameters", favorite = true, internalLocation = string.Empty},
            new setting { internalName = "pelletSpawnSlicingFactor", externalName = "Pellet Spawn Number Factor", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty},
            new setting { internalName = "pelletSpawnSlicingPower", externalName = "Pellet Spawn Number Power", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty},
            new setting { internalName = "pelletSpawnSizeFactor", externalName = "Pellet Spawn Size Factor", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty},
            new setting { internalName = "pelletSpawnConcentration", externalName = "Pellet Growth Concentration", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty},
            new setting { internalName = "pelletConcentrationSpawnType", externalName = "Pellet Spawn Concentration Type", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty},
            new setting { internalName = "pelletSpawnDrift", externalName = "Pellet Spawner Drift Power", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty},
            new setting { internalName = "plantGrowth", externalName = "Plant Growth", Category = "Simulation", parent = "World Parameters", favorite = true, internalLocation = string.Empty},
            new setting { internalName = "pelletEnergy", externalName = "Plant Pellet Energy", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty},
            new setting { internalName = "bibiteGrowth", externalName = "Bibite Spawning Rate", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty},
            new setting { internalName = "capBibiteGrowth", externalName = "Cap Bibite Spawn", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty, isBool = true},
            new setting { internalName = "limitBibiteBirth", externalName = "Limit Bibite Birth", Category = "Simulation", parent = "World Parameters", internalLocation = string.Empty, isBool = true},
            new setting { internalName = "initialSeeding", externalName = "Initial Seeding", Category ="Simulation", parent = "World Parameters" , internalLocation = string.Empty},
            //SIMULATION SETTINGS - meat rotting
            new setting { internalName = "meatRot", externalName = "Enable Meat Rotting", Category = "Simulation", parent = "Enable Meat Rotting", internalLocation = string.Empty, favorite = true, isBool = true},
            new setting { internalName = "meatRotTime", externalName = "Meat Rot Delay", Category = "Simulation", parent = "Enable Meat Rotting", internalLocation = string.Empty, favorite = true},
            new setting { internalName = "meatRotRate", externalName = "Meat Rot Rate", Category = "Simulation", parent = "Enable Meat Rotting", internalLocation = string.Empty, favorite = true},
            //SIMULATION SETTINGS - physics parameters
            new setting { internalName = "dragCoefficient", externalName = "Drag Coefficient", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "forwardForce", externalName = "Muslce Force", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "backwardFraction", externalName = "Backward Force Fraction", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "bibiteMassDensity", externalName = "Base Bibite Mass Density", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "defenceMassFactor", externalName = "Defence Mass Impact", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "defenceDamageReductionFactor", externalName = "Max Defence Damage Reduction", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "stomachContentContribution", externalName = "Stomach Content Weight Contribution", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "bitingPressure", externalName = "Biting Pressure Constant", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "throwingForce", externalName = "Throwing Force Constant", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "bitingDamage", externalName = "Biting Damage Constant", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "collisionDamageConstant", externalName = "Collision Damage Constant", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "collisionDamageThreshold", externalName = "Collision Damage Threshold", Category = "Simulation", parent = "Physics Parameters", internalLocation = string.Empty},
            new setting { internalName = "defenceMovementPenalty", externalName = "Defence Movement Penalty", Category ="Simulation", parent = "Physics Parameters" , internalLocation = string.Empty},


            //ENERGY SETTINGS - bibite aging
            new setting { internalName = "ageingThreshold", externalName = "Ageing Threshold", Category = "Energy", parent = "Bibite Aging", internalLocation = string.Empty},
            new setting { internalName = "ageStrengthPenalties", externalName = "Age Strength Penalty", Category = "Energy", parent = "Bibite Aging", internalLocation = string.Empty},
            new setting { internalName = "ageMetabolismPenalties", externalName = "Age Metabolism Penalty", Category = "Energy", parent = "Bibite Aging", internalLocation = string.Empty},
            //ENERGY SETTINGS - bibite balace
            new setting { internalName = "maxEnergyToMaxHealthRatio", externalName = "Max Energy to Max Health", Category = "Energy", parent = "Bibite Balance", internalLocation = string.Empty},
            new setting { internalName = "metabolismCost", externalName = "Default Metabolism Cost", Category = "Energy", parent = "Bibite Balance", internalLocation = string.Empty},
            new setting { internalName = "moveCost", externalName = "Default Move Cost", Category = "Energy", parent = "Bibite Balance", internalLocation = string.Empty},
            new setting { internalName = "turnCost", externalName = "Default Turn Cost", Category = "Energy", parent = "Bibite Balance", internalLocation = string.Empty},
            new setting { internalName = "neuronBirthCost", externalName = "Neuron Birth Cost", Category = "Energy", parent = "Bibite Balance", internalLocation = string.Empty},
            new setting { internalName = "synapseBirthCost", externalName = "Synapse Birth Cost", Category = "Energy", parent = "Bibite Balance", internalLocation = string.Empty},
            new setting { internalName = "neuronUpkeepCost", externalName = "Neuron Upkeep Cost", Category = "Energy", parent = "Bibite Balance", internalLocation = string.Empty},
            new setting { internalName = "synapseUpkeepCost", externalName = "Synapse Upkeep Cost", Category = "Energy", parent = "Bibite Balance", internalLocation = string.Empty},
            new setting { internalName = "pheromoneProductionCost", externalName = "Pheromone Production Cost", Category = "Energy", parent = "Bibite Balance", internalLocation = string.Empty},
            new setting { internalName = "pheromoneProductionStrength", externalName = "Pheromone Production Rate", Category = "Energy", parent = "Bibite Balance" , internalLocation = string.Empty},
            //ENERGY SETTINGS - plants
            new setting { internalName = "energyDensity", externalName = "Energy Density", Category = "Energy", internalLocation = "PlantSettings", parent = "Plants"},
            new setting { internalName = "massDensity", externalName = "Mass Density", Category = "Energy", internalLocation = "PlantSettings", parent = "Plants"},
            new setting { internalName = "hardness", externalName = "Hardness", Category = "Energy", internalLocation = "PlantSettings", parent = "Plants"},
            new setting { internalName = "digestionRate", externalName = "Base Reactivity", Category = "Energy", internalLocation = "PlantSettings", parent = "Plants"},
            new setting { internalName = "minEfficiency", externalName = "Min Conversion Efficiency", Category = "Energy", internalLocation = "PlantSettings", parent = "Plants"},
            new setting { internalName = "maxEfficiency", externalName = "Max Conversion Efficiency", Category = "Energy", internalLocation = "PlantSettings", parent = "Plants"},
            //ENERGY SETTINGS - meat
            new setting { internalName = "energyDensity", externalName = "Energy Density", Category = "Energy", internalLocation = "MeatSettings", parent = "Meat"},
            new setting { internalName = "massDensity", externalName = "Mass Density", Category = "Energy", internalLocation = "MeatSettings", parent = "Meat"},
            new setting { internalName = "hardness", externalName = "Hardness", Category = "Energy", internalLocation = "MeatSettings", parent = "Meat"},
            new setting { internalName = "digestionRate", externalName = "Base Reactivity", Category = "Energy", internalLocation = "MeatSettings", parent = "Meat"},
            new setting { internalName = "minEfficiency", externalName = "Min Conversion Efficiency", Category = "Energy", internalLocation = "MeatSettings", parent = "Meat"},
            new setting { internalName = "maxEfficiency", externalName = "Max Conversion Efficiency", Category = "Energy", internalLocation = "MeatSettings", parent = "Meat"},            
            //ENERGY SETTINGS - meat balance
            new setting { internalName = "baseBodyEnergyRatio", externalName = "Base Body Points Cost", Category = "Energy", parent = "Meat Balance", internalLocation = string.Empty},
            new setting { internalName = "healthBodyRatio", externalName = "Health to Body Constant", Category = "Energy", parent = "Meat Balance", internalLocation = string.Empty},
            new setting { internalName = "viewAngleBodyCost", externalName = "View Angle Body Cost", Category = "Energy", parent = "Meat Balance", internalLocation = string.Empty},
            new setting { internalName = "viewRadiusBodyCost", externalName = "View Radius Body Cost", Category = "Energy", parent = "Meat Balance", internalLocation = string.Empty},
            new setting { internalName = "strengthBodyCost", externalName = "Strength Body Cost", Category = "Energy", parent = "Meat Balance", internalLocation = string.Empty},
            new setting { internalName = "immuneBodyCost", externalName = "Immune System Body Cost", Category = "Energy", parent = "Meat Balance", internalLocation = string.Empty},
            

            //GENES SETTINGS - visible genes
            new setting { internalName = "colorR", externalName = "Red Color", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            new setting { internalName = "colorG", externalName = "Green Color", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            new setting { internalName = "colorB", externalName = "Blue Color", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            new setting { internalName = "diet", externalName = "Diet", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            new setting { internalName = "sizeRatio", externalName = "Size Ratio", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            new setting { internalName = "speedRatio", externalName = "Metabolism Speed", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            new setting { internalName = "viewAngle", externalName = "View Angle", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            new setting { internalName = "viewRadius", externalName = "View Radius", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            new setting { internalName = "strength", externalName = "Mouth Strength", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            new setting { internalName = "defence", externalName = "Defence", Category = "Genes", parent = "Visible Genes", internalLocation = string.Empty},
            //GENES SETTINGS - mutation genes
            new setting { internalName = "mutationChance", externalName = "Average Gene Mutations", Category = "Genes", parent = "Mutation Genes", internalLocation = string.Empty},
            new setting { internalName = "mutationVariance", externalName = "Gene Mutation Variance", Category = "Genes", parent = "Mutation Genes", internalLocation = string.Empty},
            new setting { internalName = "brainMutationChance", externalName = "Average Brain Mutations", Category = "Genes", parent = "Mutation Genes", internalLocation = string.Empty},
            new setting { internalName = "brainMutationVariance", externalName = "Brain Mutation Variance", Category = "Genes", parent = "Mutation Genes", internalLocation = string.Empty},
            new setting { internalName = "backgroundMutationChance", externalName = "Background Mutation Chance", Category = "Genes", parent = "Mutation Genes" , internalLocation = string.Empty},
            new setting { internalName = "backgroundMutationVariance", externalName = "Background Mutation Variance", Category = "Genes", parent = "Mutation Genes" , internalLocation = string.Empty},
            //GENES SETTINGS - brain mutation probabilities
            new setting { internalName = "synapseMutationChance", externalName = "Synapse Mutation Probability", Category = "Genes", parent = "Brain Mutation Probabilities", internalLocation = string.Empty},
            new setting { internalName = "neuronMutationChance", externalName = "Neuron Mutation Probability", Category = "Genes", parent = "Brain Mutation Probabilities", internalLocation = string.Empty},
            //GENES SETTINGS - synapse mutaiton probabilities
            new setting { internalName = "synapseChangeChance", externalName = "Synapse Strength Mutation Probability", Category = "Genes", parent = "Synapse Mutation Probabilities", internalLocation = string.Empty},
            new setting { internalName = "synapseFlipChance", externalName = "Synapse Flip Probability", Category = "Genes", parent = "Synapse Mutation Probabilities", internalLocation = string.Empty},
            new setting { internalName = "synapseToggleChance", externalName = "Synapse Toggle Probability", Category = "Genes", parent = "Synapse Mutation Probabilities", internalLocation = string.Empty},
            new setting { internalName = "synapseAddChance", externalName = "Synapse Add Probability", Category = "Genes", parent = "Synapse Mutation Probabilities", internalLocation = string.Empty},
            new setting { internalName = "synapseRemoveChance", externalName = "Synapse Removal Probability", Category = "Genes", parent = "Synapse Mutation Probabilities", internalLocation = string.Empty},
            //GENES SETTINGS - neuron mutation probabilities
            new setting { internalName = "neuronChangeChance", externalName = "Neuron Function Mutation Probability", Category = "Genes", parent = "Neuron Mutation Probabilities", internalLocation = string.Empty},
            new setting { internalName = "neuronAddChance", externalName = "Neuron Add Probability", Category = "Genes", parent = "Neuron Mutation Probabilities", internalLocation = string.Empty},
            new setting { internalName = "neuronRemoveChance", externalName = "Neuron Removal Probability", Category = "Genes", parent = "Neuron Mutation Probabilities", internalLocation = string.Empty},
            //GENES SETTINGS - other genes
            new setting { internalName = "layTime", externalName = "Lay Time", Category = "Genes", parent = "Other Genes", internalLocation = string.Empty},
            new setting { internalName = "broodTime", externalName = "Brood Time", Category = "Genes", parent = "Other Genes", internalLocation = string.Empty},
            new setting { internalName = "hatchTime", externalName = "Hatch Time", Category = "Genes", parent = "Other Genes", internalLocation = string.Empty},
            new setting { internalName = "growthFactor", externalName = "Growth Scale Factor", Category = "Genes", parent = "Other Genes", internalLocation = string.Empty},
            new setting { internalName = "growthMaturityFactor", externalName = "Growth Maturity Factor", Category = "Genes", parent = "Other Genes", internalLocation = string.Empty},
            new setting { internalName = "growthMaturityExponent", externalName = "Growth Maturity Exponant", Category = "Genes", parent = "Other Genes", internalLocation = string.Empty},
            new setting { internalName = "clkSpeed", externalName = "Clock Period", Category = "Genes", parent = "Other Genes", internalLocation = string.Empty},
            new setting { internalName = "pherosense", externalName = "Pheromones Sensing Radius", Category = "Genes", parent = "Other Genes", internalLocation = string.Empty},
            new setting { internalName = "baseImmuneActivation", externalName = "Base Immune Activation", Category = "Genes", parent = "Other Genes", internalLocation = string.Empty},
            //GENES SETTINGS - herding genes
            new setting { internalName = "herdSeparationWeight", externalName = "Herd Separation Weight", Category = "Genes", parent = "Herding Genes" , internalLocation = string.Empty},
            new setting { internalName = "herdAlignmentWeight", externalName = "Herd Alignment Weight", Category = "Genes", parent = "Herding Genes" , internalLocation = string.Empty},
            new setting { internalName = "herdCohesionWeight", externalName = "Herd Cohesion Weight", Category = "Genes", parent = "Herding Genes" , internalLocation = string.Empty},
            new setting { internalName = "herdVelocityWeight", externalName = "Herd Velocity Weight", Category = "Genes", parent = "Herding Genes" , internalLocation = string.Empty},
            new setting { internalName = "herdSeparationDistance", externalName = "Herd Separation Distance", Category = "Genes", parent = "Herding Genes" , internalLocation = string.Empty},

            // MISC???? not included in game
            //new setting { internalName = "killZone", externalName = "Herd Separation Distance", Category ="Genes", parent = "Herding Genes" , internalLocation = string.Empty},
            //new setting { internalName = "killZoneRadiusFactor", externalName = "Herd Separation Distance", Category ="Genes", parent = "Herding Genes" , internalLocation = string.Empty},

            
        };
    }  
}
