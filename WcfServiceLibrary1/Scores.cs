using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
    [DataContract]
    public class Scores
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public int GamesPlayed { get; set; }

        [DataMember]
        public int HighScore { get; set; }
    }
}
