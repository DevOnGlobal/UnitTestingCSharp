using System;
using Xunit;
using Moq;
using VotingApp.Services;
using VotingApp.Models;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

namespace VotingAppTests
{
    public class VoteServiceTests
    {
        [Fact]
        public void VoteService_AddVoteYes_SavesYes()
        {
            // Arrange
            Mock<StreamWriter> writer = new Mock<StreamWriter>("out.txt");

            VoteService service = new VoteService(writer.Object, null);
            Vote vote = new Vote { Choice = "Yes" };

            // Act
            service.Add(vote);

            // Assert
            writer.Verify(s => s.WriteLine("Yes"), Times.Once);
        }


        [Fact]
        public void VoteService_AddVote_Saves1Vote()
        {
            // Arrange
            MemoryStream destination = new MemoryStream();

            var writer = new StreamWriter(destination, Encoding.ASCII);
            var reader = new StreamReader(destination, Encoding.ASCII);

            VoteService service = new VoteService(writer, reader);
            Vote vote = new Vote { Choice = "No" };

            //Act
            service.Add(vote);
            List<Vote> votes = service.GetVotes();

            // Assert
            _ = Assert.Single(votes);
        }



        [Fact]
        public void VoteService_GetVotes_getsVotes()
        {
            // Arrange
            MemoryStream destination = new MemoryStream();

            var writer = new StreamWriter(destination, Encoding.ASCII);
            var reader = new StreamReader(destination, Encoding.ASCII);

            VoteService service = new VoteService(writer, reader);
            Vote voteNo = new Vote { Choice = "No" };
            Vote voteYes = new Vote { Choice = "Yes" };

            //Act
            service.Add(voteNo);
            service.Add(voteYes);
            List<Vote> votes = service.GetVotes();

            // Assert
            Assert.Equal(2, votes.Count);
        }
    }
}
