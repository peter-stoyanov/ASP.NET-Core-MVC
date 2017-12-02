namespace ComponentLibrary
{
    public class BootstrapAlertViewModel
    {
        public string StrongText { get; set; }

        public string Text { get; set; }

        public bool HasDismissButton { get; set; } = true;

        public BootstrapAlertType Type { get; set; } = BootstrapAlertType.Info;
    }
}
