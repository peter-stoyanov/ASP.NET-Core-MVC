using System;

namespace LanguageBuilder.Web.ViewComponents
{
    public class BootstrapAlertViewModel
    {
        //private BootstrapAlertType _type;

        public BootstrapAlertViewModel(BootstrapAlertType type, string text)
            : this(type, text, String.Empty, hasDismissButton: true)
        { }

        public BootstrapAlertViewModel(BootstrapAlertType type, string text, string strongText, bool hasDismissButton = true)
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

        //public BootstrapAlertType Type
        //{
        //    get
        //    {
        //        if (this.AlertKey is String alertKey)
        //        {
        //            var parsedAlertKey = Enum.TryParse(alertKey.ToLower(), out BootstrapAlertType type);

        //            this._type = parsedAlertKey ? type : BootstrapAlertType.Info;
        //        }

        //        return this._type;
        //    }

        //    set => this._type = value;
        //}
    }
}
