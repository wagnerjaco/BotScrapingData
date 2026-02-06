using Bothorizonttes.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Bothorizonttes.Application.Services;

public class BotOrchestrator : IBotOrchestrator
{
    private readonly IBotAutomation _automation;
    private readonly IFileProcessor _fileProcessor;
    private readonly IDatabaseRestore _database;
    private readonly ILogger<BotOrchestrator> _logger;

    public BotOrchestrator(
        IBotAutomation automation,
        IFileProcessor fileProcessor,
        IDatabaseRestore database,
        ILogger<BotOrchestrator> logger)
    {
        _automation = automation;
        _fileProcessor = fileProcessor;
        _database = database;
        _logger = logger;
    }

    public async Task ExecutarAsync()
    {
        _logger.LogInformation("Bot iniciado");

        try
        {
            _logger.LogInformation("Iniciando automação no Sienge");
            await _automation.ExecutarAsync();

            _logger.LogInformation("Processando arquivos");
            _fileProcessor.Processar();

            _logger.LogInformation("Restaurando banco de dados");
            _database.Restaurar();

            _logger.LogInformation("Bot finalizado com sucesso");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro fatal durante execução do bot");
            throw;
        }
    }
}
