using CraSampleApp.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraSampleApp.Domain.Domain.Service
{
    public interface ILogger
    {
        void Log(string log);
    }

    public class DatabaseLogger : ILogger
    {
        private readonly IEntityFrameworkRepository<LogEntity> _repository;
        public DatabaseLogger(IEntityFrameworkRepository<LogEntity> repository)
        {
            _repository = repository;
        }

        public void Log(string log)
        {
            _repository.Add(new LogEntity()
            {
                Log = log
            });
        }
    }

    public class FileLogger : ILogger
    {
        public void Log(string log)
        {
            File.AppendAllText(@"C:\AppLog\log.txt", log);
        }
    }
}
