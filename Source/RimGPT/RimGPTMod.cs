using HarmonyLib;
using RimWorld;
using System.Threading.Tasks;
using Verse;

namespace RimGPT
{
    public class RimGPTMod : Mod
    {
        public static RimGPTSettings Settings { get; private set; } = new RimGPTSettings();
        public static IAIClient Client { get; private set; } = new OpenAIClient();
        private readonly Harmony _harmony;

        public RimGPTMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<RimGPTSettings>();
            InitializeClient();
            _harmony = new Harmony("rimworld.rimgpt.mod");
            _harmony.PatchAll();
            Log.Message("RimGPT loaded with " + Settings.Provider + " provider");
        }

        public override string SettingsCategory() => "RimGPT";

        public override void DoSettingsWindowContents(UnityEngine.Rect inRect)
        {
            Listing_Standard list = new Listing_Standard();
            list.Begin(inRect);
            list.Label("AI Provider:");
            foreach (ApiProvider provider in System.Enum.GetValues(typeof(ApiProvider)))
            {
                if (list.RadioButton(provider.ToString(), Settings.Provider == provider))
                {
                    Settings.Provider = provider;
                    InitializeClient();
                }
            }
            list.End();
            Settings.Write();
        }

        private static void InitializeClient()
        {
            switch (Settings.Provider)
            {
                case ApiProvider.Gemini:
                    Client = new GeminiClient();
                    break;
                case ApiProvider.OpenRouter:
                    Client = new OpenRouterClient();
                    break;
                default:
                    Client = new OpenAIClient();
                    break;
            }
        }
    }

    [StaticConstructorOnStartup]
    public static class RimGPTInitializer
    {
        static RimGPTInitializer()
        {
            _ = SampleAsync();
        }

        private static async Task SampleAsync()
        {
            try
            {
                string? result = await RimGPTMod.Client.GenerateAsync("Generate a short biography for a RimWorld colonist.");
                if (!string.IsNullOrEmpty(result))
                {
                    Log.Message($"RimGPT sample response: {result}");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error($"RimGPT failed to contact API: {ex}");
            }
        }
    }

    [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn")]
    public static class PawnGeneratorPatch
    {
        public static void Postfix(Pawn __result)
        {
            if (__result == null) return;
            _ = ApplyBiographyAsync(__result);
        }

        private static async Task ApplyBiographyAsync(Pawn pawn)
        {
            try
            {
                string? bio = await RimGPTMod.Client.GenerateAsync($"Generate a short biography for a RimWorld colonist named {pawn.LabelShort}. Include details about their past.");
                if (!string.IsNullOrEmpty(bio))
                {
                    Messages.Message($"Biography for {pawn.LabelShort}:\n{bio}", MessageTypeDefOf.NeutralEvent, false);
                }
            }
            catch (System.Exception ex)
            {
                Log.Error($"RimGPT biography generation failed: {ex}");
            }
        }
    }
}
