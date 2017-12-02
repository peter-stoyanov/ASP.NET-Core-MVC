﻿using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
