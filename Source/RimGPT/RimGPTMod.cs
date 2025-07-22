using Verse;
using RimWorld;
using System.Threading.Tasks;

namespace RimGPT
{
    public class RimGPTMod : Mod
    {
        public RimGPTMod(ModContentPack content) : base(content)
        {
            Log.Message("RimGPT loaded");
        }

        public override string SettingsCategory() => "RimGPT";
    }

    [StaticConstructorOnStartup]
    public static class RimGPTInitializer
    {
        static RimGPTInitializer()
        {
            _ = InitializeAsync();
        }

        private static async Task InitializeAsync()
        {
            var client = new OpenAIClient();
            try
            {
                string? result = await client.GenerateAsync("Generate a short biography for a RimWorld colonist.");
                if (!string.IsNullOrEmpty(result))
                {
                    Log.Message($"RimGPT sample response: {result}");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error($"RimGPT failed to contact OpenAI: {ex}");
            }
        }
    }
}
