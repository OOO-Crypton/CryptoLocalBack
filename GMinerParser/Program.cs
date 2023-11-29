using ParserBase;

namespace GMinerParser
{

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            HttpBase httpBase = new(new Parser(), "http://127.0.0.1:3333/2/summary");
            Console.WriteLine("Start listening on http://127.0.0.1:3333/2/summary");
            await httpBase.StartWithDelayAsync();
        }
    }
}