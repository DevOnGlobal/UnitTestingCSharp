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
        private readonly StreamWriter writer;
        private readonly StreamReader reader;

        public VoteService(StreamWriter writer, StreamReader reader)
        {
            this.writer = writer;
            this.reader = reader;
        }

        public void Add(Vote vote)
        {
            writer.WriteLine(vote.Choice);
            writer.Flush();
        }

        public List<Vote> GetVotes()
        {
            var votes = new List<Vote>();

            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            while (!reader.EndOfStream)
            {
                string choice = reader.ReadLine();
                votes.Add(new Vote { Choice = choice });
            }

            return votes;
        }

        public String GetWinner()
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
            var writerStream = new FileStream(@"..\..\votes.txt", FileMode.Truncate);
            var writer = new StreamWriter(writerStream, Encoding.ASCII);
            writer.Flush();
        }
    }
}
