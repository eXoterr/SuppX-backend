using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppX.Storage
{

    public class DbConfig
    {
        public string Host { get; }
        public string User { get; }
        public string Password { get; }
        public string Database { get; }
    
        public string GetConnectionString()
        {
            return $"Host={Host}; Database={Database}; Username={User}; Password={Password}";
        }

        public DbConfig()
        {
            Host = Environment.GetEnvironmentVariable("DB_HOST") ?? "127.0.0.1";
            User = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
            Password = Environment.GetEnvironmentVariable("DB_PASS") ?? "postgres";
            Database = Environment.GetEnvironmentVariable("DB_NAME") ?? "postgres";
        }
    }
}
