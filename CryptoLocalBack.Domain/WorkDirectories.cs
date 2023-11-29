using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocalBack.Domain
{
    public static class WorkDirectories
    {
        public readonly static string MainDirectoryPath = Path.Combine("bin", "crypton");

        public readonly static string StartScriptPath = Path.Combine(MainDirectoryPath, "start.sh");

    }
}
