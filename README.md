# RimGPT

RimGPT is a proof-of-concept RimWorld mod that uses the OpenAI API to generate
backstories, biographies and pawn conversations with ChatGPT.

## Structure

- `RimGPTMod/` contains the mod files ready to drop into RimWorld's `Mods` folder.
- `Source/` contains the C# source project. Build this with `msbuild` or `dotnet` and copy the resulting DLL into `RimGPTMod/Assemblies`.

## Building

1. Ensure you have the .NET Framework 4.8 targeting pack installed.
2. Run `dotnet build` inside the `Source/RimGPT` directory.
3. Copy `bin/Debug/net48/RimGPT.dll` to `RimGPTMod/Assemblies`.

Set your OpenAI API key in the environment variable `OPENAI_API_KEY` before
launching RimWorld.

This is only a minimal example for educational purposes. It demonstrates how to
call ChatGPT from RimWorld but does not integrate fully with the game's UI or
pawn dialogue system.
