# RimGPT

RimGPT is a proof-of-concept RimWorld mod that uses various AI APIs to
generate backstories, biographies and pawn conversations. The mod now patches
the game using Harmony so the biographies are generated as pawns spawn.

## Structure

- `RimGPTMod/` contains the mod files ready to drop into RimWorld's `Mods` folder.
- `Source/` contains the C# source project. Build this with `msbuild` or `dotnet` and copy the resulting DLL into `RimGPTMod/Assemblies`.

## Building

1. Ensure you have the .NET Framework 4.8 targeting pack installed.
2. Run `dotnet build` inside the `Source/RimGPT` directory.
3. Copy `bin/Debug/net48/RimGPT.dll` to `RimGPTMod/Assemblies`.

Set your API keys before launching RimWorld:

- `OPENAI_API_KEY` for the OpenAI API.
- `GEMINI_API_KEY` for the Gemini API.
- `OPENROUTER_API_KEY` for the OpenRouter API.

You can choose which provider to use in the mod settings.

This is still a minimal example for educational purposes, but it now integrates
with RimWorld through Harmony patches and supports multiple AI providers.
