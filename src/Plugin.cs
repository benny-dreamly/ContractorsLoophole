using System;
using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
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

    public static event EventHandler<Plugin> Unloaded;

    public override void Load()
    {
        Log = base.Log;
        
        ClassInjector.RegisterTypeInIl2Cpp<Updatinator>();
        // Harmony.CreateAndPatchAll(typeof(HasJobBeenPlayedPatch));
        // Harmony.CreateAndPatchAll(typeof(FreePlayUnlockPatch));
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