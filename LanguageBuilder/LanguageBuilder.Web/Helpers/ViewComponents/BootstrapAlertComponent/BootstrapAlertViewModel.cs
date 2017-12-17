using System;

namespace LanguageBuilder.Web.ViewComponents
{
    public class BootstrapAlertViewModel
    {
        public BootstrapAlertViewModel()
        { }

        public BootstrapAlertViewModel(BootstrapAlertType type, string text)
            : this(type, text, String.Empty, hasDismissButton: true)
        { }

        public BootstrapAlertViewModel(BootstrapAlertType type, string text, string strongText = "", bool hasDismissButton = true)
        {
            this.Type = type;
            this.Text = text;
            this.StrongText = strongText;
            this.HasDismissButton = hasDismissButton;
        }

        public string StrongText { get; set; }

        public string Text { get; set; }

        public bool HasDismissButton { get; set; } = true;

        public BootstrapAlertType Type { get; set; }
    }
}
