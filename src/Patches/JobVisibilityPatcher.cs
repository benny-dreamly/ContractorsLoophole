using BepInEx.Logging;
using PWS;
using HarmonyLib;
using PlayFab.Internal;
using UnityEngine;
using System;

namespace ContractorsLoophole.Patches;

public static class HasJobBeenPlayedPatch
{
    [HarmonyPatch(typeof(SaveManager), "HasJobBeenPlayed"), HarmonyPrefix]
    public static bool Prefix(ref bool __result)
    {
        Plugin.Log.LogInfo($"HasJobBeenPlayedPatch::Prefix called, patching function");
        // Pretend every job has been played
        __result = true;

        // Skip original method
        return false;
    }
}

public static class FreePlayUnlockPatch
{
    [HarmonyPatch(typeof(CampaignSaveData), "IsFreePlayUnlocked"), HarmonyPrefix]
    static bool IsFreePlayUnlocked_Prefix(ref bool __result)
    {
        // Check if the user enabled the setting. If they DID NOT, skip our mod logic.
        if (!Plugin.ConfigFreePlayUnlock.Value)
        {
            return true; // Return true to let the original game method run normally
        }
        
        // If the config IS true, this code runs:
        __result = true;  // All jobs unlocked, period
        return false;     // Skip original method entirely
    }
}

public static class UnlockAllEquipmentPatch
{
    [HarmonyPatch(typeof(GameStateManager), "IsEquipmentUnlocked", typeof(BaseEquipmentData)), HarmonyPrefix]
    public static bool IsEquipmentUnlocked_Prefix(ref bool __result)
    {
        // Plugin.Log.LogInfo("IsEquipmentUnlocked patch: returning true for all equipment");
        __result = true;
        return false;
    }
}

//  SaveManager.CleanUpModeProgressRequired
public static class AllowPlayingMultipleLevelsPatch
{
    [HarmonyPatch(typeof(SaveManager), "CleanUpModeProgressRequired"), HarmonyPrefix]
    public static bool CleanUpModeProgressRequired_Prefix(ref bool __result)
    {
        __result = false;
        return false;
    }
}

public static class UnlockCanRefillPatch
{
    // Adjust "CleaningLiquidSaveDataManager" if it has a namespace or a slightly different name in your dumped assembly
    [HarmonyPatch(typeof(CleaningLiquidSaveDataManager), "CanRefill"), HarmonyPrefix]
    public static bool CanRefill_Prefix(ref bool __result)
    {
        // Force the method to return true
        __result = true;
        
        // Skip the original function execution
        return false;
    }
}

public static class InfiniteLiquidDecreasePatch
{
    // Target the Decrease function on the CleaningLiquidSaveData class
    [HarmonyPatch(typeof(CleaningLiquidSaveData), "Decrease"), HarmonyPrefix]
    public static bool Decrease_Prefix(ref float deductedAmount)
    {
        // Force the amount to be deducted to 0
        deductedAmount = 0f;
        
        // Return true to let the original method run with our modified 0 deduction
        return true;
    }
}

public static class MinimumLitersPatch
{
    // Target the get_Liters property getter on the CleaningLiquidSaveData class
    [HarmonyPatch(typeof(CleaningLiquidSaveData), "get_Liters"), HarmonyPostfix]
    public static void get_Liters_Postfix(ref float __result)
    {
        // Check if the calculated liters value is 0 or less
        if (__result <= 0f)
        {
            // Force it to be 1 liter (or something greater than 0)
            __result = 1.0f;
        }
    }
}

public static class UnlockAllLiquidsPostInitPatch
{
    // Target the initialization function for non-career mode loadouts
    [HarmonyPatch(typeof(CleaningLiquidSaveDataManager), "InitialiseNonCareerLoadout"), HarmonyPostfix]
    public static void InitialiseNonCareerLoadout_Postfix(CleaningLiquidSaveDataManager __instance)
    {
        // Safety check to ensure the instance and its internal list exist
        if (__instance != null && __instance.m_liquids != null)
        {
            // Loop through every single fluid data structure the game just initialized
            foreach (var liquidSaveData in __instance.m_liquids)
            {
                if (liquidSaveData != null)
                {
                    // Force the internal fluid amount to 100%
                    liquidSaveData.m_internalAmount = 100.0f;

                    // Give the player at least 1 bottle so it lights up on the selection menu
                    // if (liquidSaveData.m_internalBottlesInInventory <= 0)
                    // {
                    //     liquidSaveData.m_internalBottlesInInventory = 1;
                    // }

                    // Turn on the unlimited flag to ensure it doesn't deplete out of bottles
                    liquidSaveData._UnlimitedBottles_k__BackingField = true;
                }
            }
        }
    }
}