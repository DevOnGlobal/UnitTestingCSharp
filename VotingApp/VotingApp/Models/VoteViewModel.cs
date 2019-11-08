using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Models
{
    public class VoteViewModel
    {
        public int Yes { get; set; }
        public int No { get; set; }
        public String Winner { get; set; }
    }
}
