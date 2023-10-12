using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels
{
    public class CreateGameVM
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }=  Enumerable.Empty<SelectListItem>();
        [Display(Name = "Suported Devices")]

        public List<int> SelectedDevices { get; set; }= new List<int>();
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        public IFormFile Cover { get; set; } = default!;
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
    }
}
