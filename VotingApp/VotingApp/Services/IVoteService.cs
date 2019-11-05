using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Models;

namespace VotingApp.Services
{
    public interface IVoteService
    {
        void Add(Vote vote);
        List<Vote> GetVotes();
    }
}
