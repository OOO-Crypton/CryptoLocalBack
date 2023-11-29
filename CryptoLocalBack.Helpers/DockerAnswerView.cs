namespace CryptoLocalBack.Helpers
{
    public class DockerAnswerView
    {
        public int ExitCode { get; set; }
        public string? Stdout { get; set; }
        public string? Stderr { get; set; }
    }
}
