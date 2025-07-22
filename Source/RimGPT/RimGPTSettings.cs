using Verse;

namespace RimGPT
{
    public class RimGPTSettings : ModSettings
    {
        public ApiProvider Provider = ApiProvider.OpenAI;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref Provider, "provider", ApiProvider.OpenAI);
        }
    }
}
