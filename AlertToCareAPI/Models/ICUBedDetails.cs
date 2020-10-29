
using System.Collections.Generic;

namespace AlertToCareAPI.Models   //ReSharper disable all
{
    public class ICUBedDetails
    {
     
        public string IcuId { get; set; } //ReSharper restore all
        public string LayoutId { get; set; }
        public int BedsCount { get; set; }
        public List<BedDetails> Beds { get; set; }
    }
}