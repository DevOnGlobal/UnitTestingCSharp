using System;
using Xunit;
using Moq;
using VotingApp.Services;
using VotingApp.Models;
using System.IO;
using Newtonsoft.Json;

namespace VotingAppTests
{
    public class VoteServiceTests
    {
        [Fact]
        public void VoteService_AddVoteYes_SavesYes()
        {
            // Arrange
            var writer = new Mock<StreamWriter>("bla.txt");

            VoteService service = new VoteService(writer.Object);
            Vote vote = new Vote { Choice = "No" };

            var file = new Mock<File>();


            // Act
            service.Add(vote);

            // Assert
            writer.Verify(s => s.WriteLine("{\"Choice\":\"Yes\"}"), Times.Once);
            writer.VerifyNoOtherCalls();
        }
    }
}
