using CryptoLocalBack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBase
{
    public interface IParser
    {
        public Task<List<Monitoring>?> Parse(string data, List<Videocard> videocard);
        public Task<List<Videocard>?> GetVideocard(string data);
    }
}
