using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class UpdateGameVM:GameFormVm
    {
       

        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExtension(FileSettings.AllowedExtension),
            MaxSize(FileSettings.MaxFileSizeByte)]
        public IFormFile? Cover { get; set; } = default!;
    }

}
