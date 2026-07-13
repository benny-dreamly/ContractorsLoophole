using System;
using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using ContractorsLoophole.Patches;
using UnhollowerRuntimeLib;

namespace ContractorsLoophole;

[BepInPlugin("ContractorsLoophole", "ContractorsLoophole", VersionNumber)]
public class Plugin : BasePlugin
{
    public const string VersionNumber = "v1.0.0";
    public const string ModDir = "BepInEx/plugins/ContractorsLoophole";

    public enum DebugWant
    {
        None = 0,
        General = 1,
        Stats = 2,
        Buttons = 3,
        Jobs = 4,
        Washables = 5,
        TranslateWashables = 6,
    }

    public static DebugWant IsDebug = DebugWant.None;
    public new static ManualLogSource Log;
    
    // Define the config entry here so the patch can see it
    public static ConfigEntry<bool> ConfigFreePlayUnlock;

    public static event EventHandler<Plugin> Unloaded;

    public override void Load()
    {
        Log = base.Log;
        
        // Set up the configuration setting
        // "General" is the section, "UnlockFreePlay" is the key name, false is the default value
        ConfigFreePlayUnlock = Config.Bind("General", "UnlockFreePlay", false, "If true, all Free Play jobs will be instantly unlocked.");
        
        ClassInjector.RegisterTypeInIl2Cpp<Updatinator>();
        // Harmony.CreateAndPatchAll(typeof(HasJobBeenPlayedPatch));
        Harmony.CreateAndPatchAll(typeof(FreePlayUnlockPatch));
        Harmony.CreateAndPatchAll(typeof(UnlockAllEquipmentPatch));
        Harmony.CreateAndPatchAll(typeof(AllowPlayingMultipleLevelsPatch));
        // Harmony.CreateAndPatchAll(typeof(InfiniteLiquidDecreasePatch));
        Harmony.CreateAndPatchAll(typeof(MinimumLitersPatch));
        Harmony.CreateAndPatchAll(typeof(UnlockAllLiquidsPostInitPatch));

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    public override bool Unload()
    {
        Unloaded?.Invoke(this, this);
        Log.LogInfo($"Plugin [{MyPluginInfo.PLUGIN_GUID}] has unloaded!");
        return true;
    }
}