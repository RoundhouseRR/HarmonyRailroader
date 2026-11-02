# HarmonyRailroader

A shared copy of the [Harmony](https://github.com/pardeike/Harmony) patching library for Railroader mods, so each mod doesn't need to bundle its own.

**Status: prototype. There are no releases yet, so don't depend on this in a published mod.**

## How it works

HarmonyRailroader loads first and provides one copy of `0Harmony.dll`. Mods that need Harmony declare it as a dependency, and the game enables and loads it before them.

## For mod authors

Add the dependency to your `Info.json`, using the lowest Harmony version your mod needs:

```json
"dependencies": {
  "required": [
    "rr/game >=0.6.2",
    "roundhouserr/harmony-railroader >=2.3.6"
  ]
}
```

Reference Harmony at build time, but don't ship `0Harmony.dll` with your mod.

## Credits

Harmony is created by Andreas Pardeike and released under the MIT license. See [LICENSE-Harmony.txt](LICENSE-Harmony.txt).

This project is inspired by [HarmonyRimWorld](https://github.com/pardeike/HarmonyRimWorld) and [Bannerlord.Harmony](https://github.com/BUTR/Bannerlord.Harmony). It isn't affiliated with Harmony, Giraffe Lab or those projects.

## Project setup

See [SETUP-PLAN.md](SETUP-PLAN.md) for the org, security and release plan.
