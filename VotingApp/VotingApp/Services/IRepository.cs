using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Services
{
    public interface IRepository
    {
        void Write(string data);
        List<string> Read();
    }
}
