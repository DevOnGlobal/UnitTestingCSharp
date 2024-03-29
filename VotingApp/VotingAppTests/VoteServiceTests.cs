using System;
using Xunit;
using Moq;
using VotingApp.Services;
using VotingApp.Models;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace VotingAppTests
{
    public class VoteServiceTests
    {
        [Fact]
        public void VoteService_AddVoteYes_SavesYes()
        {
            // Arrange
            Mock<IRepository> repository = new Mock<IRepository>();
            VoteService service = new VoteService(repository.Object);
            Vote vote = new Vote { Choice = "Yes" };

            // Act
            service.Add(vote);

            // Assert
            repository.Verify(r => r.Write("Yes"), Times.Once);
        }
    }
}
