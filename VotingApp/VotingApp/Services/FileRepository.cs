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
        private readonly StreamWriter _writer;
        private readonly StreamReader _reader;
        private readonly Encoding encoding = Encoding.ASCII;

        public FileRepository(string repositoryName)
        {
            string filename = "..\\..\\" + repositoryName + ".txt";

            var writerStream = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            this._writer = new StreamWriter(writerStream, encoding);

            var readerStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            this._reader = new StreamReader(readerStream, encoding);
        }

        public void Write(string data)
        {
            _writer.WriteLine(data);
            _writer.Flush();
        }

        public List<string> Read()
        {
            _reader.DiscardBufferedData();
            _reader.BaseStream.Seek(0, SeekOrigin.Begin);

            List<string> lines = new List<string>();
            while (!_reader.EndOfStream)
            {
                lines.Add(_reader.ReadLine());
            }

            return lines;
        }
    }
}
