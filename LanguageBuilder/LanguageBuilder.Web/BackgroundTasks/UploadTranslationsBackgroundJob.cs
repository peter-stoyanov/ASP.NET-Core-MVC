using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Hubs;
using LanguageBuilder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.BackgroundTasks
{
    public class UploadTranslationsBackgroundJob
    {
        private readonly IWordsService _wordsService;
        private IHubContext<NotificationsHub> _hubContext;

        public UploadTranslationsBackgroundJob(
            IWordsService wordsService,
            IHubContext<NotificationsHub> hubContext)
        {
            _wordsService = wordsService;
            _hubContext = hubContext;
        }

        public async Task ExecuteAsync(Dictionary<string, string> translations, int sourceLanguageId, int targetLanguageId, string userIdentity)
        {
            if (!translations.Any()) { return; }

            NotificationsHub.AddBackgroundTaskForUser(userIdentity);

            string message = string.Empty;

            try
            {
                int counter = 0;

                foreach (var wordKvp in translations)
                {
                    var source = wordKvp.Key;
                    var target = wordKvp.Key;

                    if (String.IsNullOrEmpty(source) || String.IsNullOrEmpty(target)) { continue; }

                    if (await _wordsService.ExistAsync(source)) { continue; }

                    if (await _wordsService.ExistAsync(target)) { continue; }

                    var sourceWord = new Word { Content = source, LanguageId = sourceLanguageId };
                    var targetWord = new Word { Content = target, LanguageId = targetLanguageId };

                    await _wordsService.AddWordsWithTranslationAsync(sourceWord, targetWord);

                    counter++;
                }

                message = $"Your translations upload has finished. {counter} new translations have been stored in the database.";
            }
            catch (Exception ex)
            {
                ex.SaveToLog();
                message = "Your translations upload has not finished correctly. Please try again.";
            }

            await _hubContext
                .Clients
                .Group(userIdentity)?
                .InvokeAsync("onUploadTranslationsCompleted", message);

            NotificationsHub.RemoveBackgroundTaskForUser(userIdentity);
        }
    }
}
