﻿using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Web.ViewModels.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}
