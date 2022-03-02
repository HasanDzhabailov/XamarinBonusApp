using System;
using System.Collections.Generic;
using System.Text;

namespace Real2App.AppData.Models
{
    public class ChildrenElementCarousel
    {
        public int Id { get; set; }
        public string ImgSourceUrl { get; set; }
        public bool IsVideo { get; set; } = false;
        public bool IsPhoto { get; set; } = true;
        public double VideoLenght { get; set; } = 0;

        public string URLTelegramBtn { get; set; } = null;
        public string URLWhatsappBtn { get; set; } = null;
        public string URLInstagramBtn { get; set; } = null;
        public string URLReal2SiteBtn { get; set; } = null;

        public bool IsTelegramIcon { get; set; } = false;
        public bool IsWhatsappIcon { get; set; } = false;
        public bool IsInstagramIcon { get; set; } = false;
        public bool IsReal2Icon { get; set; } = false;

        public int InstagramIconPosition { get; set; } = 2;
        public int TelegramIconPosition { get; set; } = 3;
        public int WhatsappIconPosition { get; set; } = 4;
        public int Real2IconPosition { get; set; } = 5;

        public int? ParentElementCarouselId { get; set; } // внешний ключ
        public ParentElementCarousel ParentElementCarousel { get; set; }
    }
}
