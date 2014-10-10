namespace LOGI.Framework.Toolkit.Core.Security
{
    /// <summary>
    /// Stores the result of a string hash operation when a random salt is used
    /// </summary>
    public class HashResult
    {
        public string Hash { get; set; }
        public int SaltUsed { get; set; }
    }
}
