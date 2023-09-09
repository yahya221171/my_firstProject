using mahya.Domain.Models.BaseEntities;

namespace Mahya.Domain.Models.Site
{
    public class Slider:BaseEntity
    {
        public string SliderImage { get; set; }
        public string SliderTitle { get; set; }  
        public string SliderText { get; set; }
        public int Price { get; set; }
        public string Href { get; set; }
        public string TextBtn { get; set; }
    }
}
