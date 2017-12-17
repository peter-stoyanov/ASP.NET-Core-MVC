using System;

namespace LanguageBuilder.Web.ViewComponents
{
    public class GoogleMapsViewModel
    {
        public double Latitude { get; set; } = 48.07;

        public double Longitude { get; set; } = 13.51;

        public int Zoom { get; set; } = 4;

        public string ContainerId { get; set; } = "map";
    }
}
