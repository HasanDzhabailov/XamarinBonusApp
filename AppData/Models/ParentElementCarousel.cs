using System;
using System.Collections.Generic;
using System.Text;

namespace Real2App.AppData.Models
{
    public class ParentElementCarousel
    {
        public int Id { get; set; }
        public string ParentImageUrl { get; set; }
        public bool IsViewed { get; set; } = false;

        public List<ChildrenElementCarousel> ChildrenElementCarousels { get; set; } =
            new List<ChildrenElementCarousel>();
    }
}
