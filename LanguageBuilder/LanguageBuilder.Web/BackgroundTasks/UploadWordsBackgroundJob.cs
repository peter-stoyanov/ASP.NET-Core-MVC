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
    public class UploadWordsBackgroundJob
    {
        private readonly IWordsService _wordsService;
        private IHubContext<NotificationsHub> _hubContext;

        public UploadWordsBackgroundJob(
            IWordsService wordsService,
            IHubContext<NotificationsHub> hubContext)
        {
            _wordsService = wordsService;
            _hubContext = hubContext;
        }

        public async Task ExecuteAsync(Dictionary<string, string> words, int languageId, string userIdentity)
        {
            if (!words.Any()) { return; }

            NotificationsHub.AddBackgroundTaskForUser(userIdentity);

            string message = string.Empty;

            try
            {
                int counter = 0;

                foreach (var wordKvp in words)
                {
                    if (await _wordsService.ExistAsync(wordKvp.Key)) { continue; }

                    _wordsService.Add(new Word
                    {
                        Content = wordKvp.Key,
                        Definition = wordKvp.Value,
                        LanguageId = languageId
                    });

                    counter++;
                }

                message = $"Your words upload has finished. {counter} new words have been stored in the database.";
            }
            catch (Exception ex)
            {
                ex.SaveToLog();
                message = "Your words upload has not finished correctly. Please try again.";
            }

            await _hubContext
                .Clients
                .Group(userIdentity)?
                .InvokeAsync("onUploadWordsCompleted", message);

            NotificationsHub.RemoveBackgroundTaskForUser(userIdentity);
        }
    }
}
