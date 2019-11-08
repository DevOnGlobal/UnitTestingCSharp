using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Services
{
    public class FileRepository : IRepository
    {
        private readonly string _filename;

        public FileRepository(string repositoryName)
        {
            _filename = "..\\..\\" + repositoryName + ".txt";
        }

        public void Write(string data)
        {
            CreateIfNotExists();
            var lines = new List<string>() { data };
            File.AppendAllLines(_filename, lines);
        }

        public List<string> Read()
        {
            CreateIfNotExists();
            return File.ReadAllLines(_filename).ToList();
        }

        public void Clear()
        {
            CreateIfNotExists();
            File.Delete(_filename);
        }

        private void CreateIfNotExists()
        {
            if (!File.Exists(_filename))
            {
                var stream = File.Create(_filename);
                stream.Dispose();
            }                
        }
    }
}
