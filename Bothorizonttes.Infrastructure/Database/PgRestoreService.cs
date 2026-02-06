using Bothorizonttes.Application.Interfaces;
using Bothorizonttes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Bothorizonttes.Infrastructure.Database
{
    public class PgRestoreService : IDatabaseRestore
    {
        private readonly PostgresSettings _postgres;
        private readonly PathSettings _paths;

        public PgRestoreService(PostgresSettings postgres, PathSettings paths)
        {
            _postgres = postgres;
            _paths = paths;
        }

        public void Restaurar()
        {
            var backupFile = Path.Combine(_paths.Dados, "sienge_lafe.dmpc");

            if (!File.Exists(backupFile))
                throw new FileNotFoundException("Arquivo de backup não encontrado.", backupFile);

            var psi = new ProcessStartInfo
            {
                FileName = _postgres.PgRestorePath,
                Arguments =
                    $"--host={_postgres.Host} " +
                    $"--port={_postgres.Port} " +
                    $"--dbname={_postgres.Database} " +
                    $"--username={_postgres.Username} " +
                    $"--no-owner --no-comments --clean " +
                    $"\"{backupFile}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            psi.Environment["PGPASSWORD"] = _postgres.Password;

            using var process = Process.Start(psi);
            process!.WaitForExit();

            if (process.ExitCode != 0)
                throw new Exception("Erro ao restaurar o banco.");

            Console.WriteLine("Banco restaurado com sucesso.");
        }

    }
}
