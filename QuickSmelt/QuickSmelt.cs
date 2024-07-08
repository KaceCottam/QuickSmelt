using BepInEx;
using BepInEx.Configuration;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using Jotunn.Configs;

namespace QuickSmelt
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class QuickSmelt : BaseUnityPlugin
    {
        public const string PluginGUID = "com.KaceCottam.QuickSmelt";
        public const string PluginName = "QuickSmelt";
        public const string PluginVersion = "0.1.0";

        private ConfigEntry<bool> Enabled;
        private ConfigEntry<int> CoalPerBar;

        private void CreateConfigValues()
        {
            // Serverside configuration
            ConfigurationManagerAttributes isAdminOnly = new ConfigurationManagerAttributes { IsAdminOnly = true };
            Enabled = Config.Bind("Server", "EnableQuickSmelt", true, new ConfigDescription("Determines whether or not the mod is enabled.", null, isAdminOnly));
            CoalPerBar = Config.Bind("Server", "CoalPerBar", 5, new ConfigDescription("Can create bars using this many coal. Ores/Scrap are crafted to bars in a 1:1 ratio.", null, isAdminOnly));
        }

        private void Awake()
        {
            CreateConfigValues();
            ItemManager.OnItemsRegistered += AddRecipes;
        }

        private void AddRecipes()
        {
            if (!Enabled.Value)
            {
                ItemManager.OnItemsRegistered -= AddRecipes;
                return;
            }

            Jotunn.Logger.LogInfo("Adding Recipes...");

            RecipeConfig coalRecipeConfig = new RecipeConfig();
            coalRecipeConfig.Name = "TripleBronze_Recipe_Coal";
            coalRecipeConfig.Item = "Coal";
            coalRecipeConfig.Amount = 1;
            coalRecipeConfig.CraftingStation = CraftingStations.Forge;
            coalRecipeConfig.MinStationLevel = 2;
            coalRecipeConfig.RequireOnlyOneIngredient = true;
            coalRecipeConfig.AddRequirement(new RequirementConfig("Wood", 1));
            coalRecipeConfig.AddRequirement(new RequirementConfig("FineWood", 1));
            coalRecipeConfig.AddRequirement(new RequirementConfig("RoundLog", 1));
            coalRecipeConfig.AddRequirement(new RequirementConfig("YggdrasilWood", 1));
            ItemManager.Instance.AddRecipe(new CustomRecipe(coalRecipeConfig));
            Jotunn.Logger.LogInfo("Added Coal Recipe.");

            RecipeConfig copperBarRecipeConfig = new RecipeConfig();
            copperBarRecipeConfig.Name = "TripleBronze_Recipe_CopperBar";
            copperBarRecipeConfig.Item = "Copper";
            copperBarRecipeConfig.Amount = 1;
            copperBarRecipeConfig.CraftingStation = CraftingStations.Forge;
            copperBarRecipeConfig.MinStationLevel = 2;
            copperBarRecipeConfig.AddRequirement(new RequirementConfig("Coal", CoalPerBar.Value));
            copperBarRecipeConfig.AddRequirement(new RequirementConfig("CopperOre", 1));
            ItemManager.Instance.AddRecipe(new CustomRecipe(copperBarRecipeConfig));
            Jotunn.Logger.LogInfo("Added Copper Bar Recipe.");

            RecipeConfig tinBarRecipeConfig = new RecipeConfig();
            tinBarRecipeConfig.Name = "TripleBronze_Recipe_TinBar";
            tinBarRecipeConfig.Item = "Tin";
            tinBarRecipeConfig.Amount = 1;
            tinBarRecipeConfig.CraftingStation = CraftingStations.Forge;
            tinBarRecipeConfig.MinStationLevel = 3;
            tinBarRecipeConfig.AddRequirement(new RequirementConfig("Coal", CoalPerBar.Value));
            tinBarRecipeConfig.AddRequirement(new RequirementConfig("TinOre", 1));
            ItemManager.Instance.AddRecipe(new CustomRecipe(tinBarRecipeConfig));
            Jotunn.Logger.LogInfo("Added Tin Bar Recipe.");

            RecipeConfig ironBarRecipe1Config = new RecipeConfig();
            ironBarRecipe1Config.Name = "TripleBronze_Recipe_IronBar_Scrap";
            ironBarRecipe1Config.Item = "Iron";
            ironBarRecipe1Config.Amount = 1;
            ironBarRecipe1Config.CraftingStation = CraftingStations.Forge;
            ironBarRecipe1Config.MinStationLevel = 4;
            ironBarRecipe1Config.AddRequirement(new RequirementConfig("Coal", CoalPerBar.Value));
            ironBarRecipe1Config.AddRequirement(new RequirementConfig("IronScrap", 1));
            ItemManager.Instance.AddRecipe(new CustomRecipe(ironBarRecipe1Config));
            Jotunn.Logger.LogInfo("Added Iron Bar Recipe (Scrap).");

            RecipeConfig ironBarRecipe2Config = new RecipeConfig();
            ironBarRecipe2Config.Name = "TripleBronze_Recipe_IronBar_Ore";
            ironBarRecipe2Config.Item = "Iron";
            ironBarRecipe2Config.Amount = 1;
            ironBarRecipe2Config.CraftingStation = CraftingStations.Forge;
            ironBarRecipe1Config.MinStationLevel = 4;
            ironBarRecipe2Config.AddRequirement(new RequirementConfig("Coal", CoalPerBar.Value));
            ironBarRecipe2Config.AddRequirement(new RequirementConfig("IronOre", 1));
            ItemManager.Instance.AddRecipe(new CustomRecipe(ironBarRecipe2Config));
            Jotunn.Logger.LogInfo("Added Iron Bar Recipe (Ore).");

            RecipeConfig silverBarRecipeConfig = new RecipeConfig();
            silverBarRecipeConfig.Name = "TripleBronze_Recipe_SilverBar";
            silverBarRecipeConfig.Item = "Silver";
            silverBarRecipeConfig.Amount = 1;
            silverBarRecipeConfig.CraftingStation = CraftingStations.Forge;
            silverBarRecipeConfig.MinStationLevel = 4;
            silverBarRecipeConfig.AddRequirement(new RequirementConfig("Coal", CoalPerBar.Value));
            silverBarRecipeConfig.AddRequirement(new RequirementConfig("SilverOre", 1));
            ItemManager.Instance.AddRecipe(new CustomRecipe(silverBarRecipeConfig));
            Jotunn.Logger.LogInfo("Added Silver Bar Recipe.");

            RecipeConfig blackMetalBarRecipeConfig = new RecipeConfig();
            blackMetalBarRecipeConfig.Name = "TripleBronze_Recipe_BlackMetalBar";
            blackMetalBarRecipeConfig.Item = "BlackMetal";
            blackMetalBarRecipeConfig.Amount = 1;
            blackMetalBarRecipeConfig.CraftingStation = CraftingStations.Forge;
            blackMetalBarRecipeConfig.MinStationLevel = 5;
            blackMetalBarRecipeConfig.AddRequirement(new RequirementConfig("Coal", CoalPerBar.Value));
            blackMetalBarRecipeConfig.AddRequirement(new RequirementConfig("BlackMetalScrap", 1));
            ItemManager.Instance.AddRecipe(new CustomRecipe(blackMetalBarRecipeConfig));
            Jotunn.Logger.LogInfo("Added Black Metal Bar Recipe.");

            RecipeConfig flametalBarRecipeConfig = new RecipeConfig();
            flametalBarRecipeConfig.Name = "TripleBronze_Recipe_FlametalBar";
            flametalBarRecipeConfig.Item = "Flametal";
            flametalBarRecipeConfig.Amount = 1;
            flametalBarRecipeConfig.CraftingStation = CraftingStations.Forge;
            flametalBarRecipeConfig.MinStationLevel = 4;
            flametalBarRecipeConfig.AddRequirement(new RequirementConfig("Coal", CoalPerBar.Value));
            flametalBarRecipeConfig.AddRequirement(new RequirementConfig("FlametalOre", 1));
            ItemManager.Instance.AddRecipe(new CustomRecipe(flametalBarRecipeConfig));
            Jotunn.Logger.LogInfo("Added Flametal Bar Recipe.");

            Jotunn.Logger.LogInfo("All Recipes Registered.");
            ItemManager.OnItemsRegistered -= AddRecipes;
        }
    }
}
