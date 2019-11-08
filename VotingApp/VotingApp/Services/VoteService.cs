using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Models;

namespace VotingApp.Services
{
    public class VoteService : IVoteService
    {
        private readonly IRepository _repository;

        public VoteService(IRepository repository)
        {
            this._repository = repository;
        }

        public void Add(Vote vote)
        {
            var validOptions = new List<string>() { "Yes", "No" };

            if (!validOptions.Contains(vote.Choice))
            {
                throw new ArgumentException("Illegal vote casted.");
            }    

            _repository.Write(vote.Choice);
        }

        public List<Vote> GetVotes()
        {
            var lines = _repository.Read();

            var votes = new List<Vote>();
            foreach (var line in lines)
            {
                var vote = new Vote { Choice = line };
                votes.Add(vote);
            }
        
            return votes;
        }

        public string GetWinner()
        {
            var Winner = "";
            var votes = GetVotes();

            int YesCount = votes.Count(v => v.Choice == "Yes");
            int NoCount = votes.Count(v => v.Choice == "No");

            if (YesCount > NoCount) Winner = "Yes";
            if (YesCount < NoCount) Winner = "No";
            if (YesCount == NoCount) Winner = "TIE";

            return Winner;
        }

        public void Reset()
        {
            _repository.Clear();
        }
    }
}
