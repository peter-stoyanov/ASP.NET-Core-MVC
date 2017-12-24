using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.BackgroundTasks
{
    public class BulkImportWordsBackgroundService : BackgroundService
    {
        private readonly ILogger<BulkImportWordsBackgroundService> _logger;
        private readonly IWordsService _wordsService;
        private IFormFile _file;
        private int _languageId;

        public BulkImportWordsBackgroundService(
            IFormFile file,
            int languageId,
            IServiceProvider serviceProvider,
            ILogger<BulkImportWordsBackgroundService> logger)
        {
            _logger = logger;

            _wordsService = (IWordsService)serviceProvider.GetService(typeof(IWordsService));
            _file = file;
            _languageId = languageId;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"Words upload is starting.");

            stoppingToken.Register(() =>
            {
                _logger.LogDebug($"Words upload background task is stopping.");
            });

           // job

            _logger.LogDebug($"Words upload task has finished.");
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            // Run your graceful clean-up actions
            _logger.LogDebug($"Words upload task is stopping. From StopAsync method.");
        }
    }
}

