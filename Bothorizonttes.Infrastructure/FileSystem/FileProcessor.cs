using Bothorizonttes.Application.Interfaces;
using Bothorizonttes.Domain;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace Bothorizonttes.Infrastructure.FileSystem
{
    public class FileProcessor : IFileProcessor
    {
        private readonly PathSettings _paths;

        public FileProcessor(PathSettings paths)
        {
            _paths = paths;
        }

        public void Processar()
        {
            Console.WriteLine("Iniciando processamento de arquivos...");

            Directory.CreateDirectory(_paths.Dados);

            var arquivoFinalGz = Path.Combine(_paths.Download, "sienge_lafe.dmpc.gz");
            if (File.Exists(arquivoFinalGz))
            {
                File.Delete(arquivoFinalGz);
                Console.WriteLine("Arquivo antigo removido.");
            }

            var ultimoArquivo = Directory
                .GetFiles(_paths.Download, "sie*.gz")
                .Select(f => new FileInfo(f))
                .OrderByDescending(f => f.LastWriteTime)
                .FirstOrDefault();

            if (ultimoArquivo == null)
                throw new FileNotFoundException("Nenhum arquivo sie*.gz encontrado.");

            ultimoArquivo.MoveTo(arquivoFinalGz);
            Console.WriteLine($"Arquivo renomeado para {Path.GetFileName(arquivoFinalGz)}");

            var arquivoExtraido = Path.Combine(_paths.Dados, "sienge_lafe.dmpc");

            if (File.Exists(arquivoExtraido))
                File.Delete(arquivoExtraido);

            using var entrada = File.OpenRead(arquivoFinalGz);
            using var gzip = new GZipStream(entrada, CompressionMode.Decompress);
            using var saida = File.Create(arquivoExtraido);

            gzip.CopyTo(saida);

            Console.WriteLine("Arquivo extraído com sucesso.");
        }
    }
}