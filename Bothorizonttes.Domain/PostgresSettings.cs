using System;
using System.Collections.Generic;
using System.Text;

namespace Bothorizonttes.Domain
{
    public class PostgresSettings
    {
        public string PgRestorePath { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Database { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
