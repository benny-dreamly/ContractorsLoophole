# Contractor's Loophole

This mod has been somewhat cannibalized from SWCreeperKing's Powerwash AP mod. 

## Features

- allows you to use any loadout on any level, including levels that normally give you a specific loadout
- allows having multiple freeplay jobs/bonus jobs in progress at the same time

## future features

- independently toggleable way to force free play unlocks?


## How to install
(tutorial borrowed from SW_CreeperKing's Powerwash AP mod)

- Download [BepInEx 6.0.0-pre1](https://github.com/BepInEx/BepInEx/releases/download/v6.0.0-pre.1/BepInEx_UnityIL2CPP_x64_6.0.0-pre.1.zip).
- Extract the BepInEx zip folder you downloaded from the previous step into your game's install directory (For example: C:\Program Files (x86)\Steam\steamapps\common\PowerWash Simulator)
  - the contents from the extraction: BepinEx, mono, changelog, doorstop_config, winhttp.dll should be in the same directory as the game
- Launch the game and close it. This will finalize the BepInEx installation.
- Download and extract the `ContractorsLoophole.Zip` from the [latest release page](https://github.com/benny-dreamly/ContractorsLoophole/releases/latest).
    - Copy the `ContractorsLoophole` folder from the release zip into `BepInEx/plugins` under your game's install directory.
    - If there is no `BepInEx/plugins` folder: 
      - try running as administrator
      - if using linux/wine: type "WINEDLLOVERRIDES="winhttp=n,b" %command%" into launch options
      - check to see if your antivirus or something else that maybe interfering with BepInEx
- Launch the game again and you should see the connection input on the top left of the title screen!
- To uninstall the mod, either remove/delete the `ContractorsLoophole` folder from the plugins folder or rename the winhttp.dll file located in the game's root directory (this will disable all installed mods from running).

---

# Special Thanks

- SW_CreeperKing for their wonderful AP mod that this mod is based on
