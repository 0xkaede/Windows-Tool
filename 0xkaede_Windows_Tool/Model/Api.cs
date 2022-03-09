using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _0xkaede_Windows_Tool.Model
{
    public class Info
    {
        [JsonProperty("Application")]
        public Application application { get; set; }
    }

    public class Application
    {
        [JsonProperty("MainExe")]
        public string mainexe { get; set; }

        [JsonProperty("Version")]
        public string version { get; set; }

        [JsonProperty("Games")]
        public Games games { get; set; }
    }

    public class Games
    {
        [JsonProperty("Minecraft")]
        public Minecraft minecraft { get; set; }
    }

    public class Minecraft
    {
        [JsonProperty("Installer")]
        public string installer { get; set; }

        [JsonProperty("Versions")]
        public Version versions { get; set; }
    }

    public class Version
    {
        [JsonProperty("1.8.9")]
        public string V1_8_9 { get; set; }
    }
}
