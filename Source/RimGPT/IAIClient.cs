namespace RimGPT
{
    public interface IAIClient
    {
        System.Threading.Tasks.Task<string?> GenerateAsync(string prompt);
    }
}
