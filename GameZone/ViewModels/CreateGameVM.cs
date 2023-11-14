using GameZone.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels
{
    public class CreateGameVM:GameFormVm
    {


        //[AllowedExtension(FileSettings.AllowedExtension),
        //    MaxSize(FileSettings.MaxFileSizeByte)]
        //public IFormFile Cover { get; set; } = default!;

        [AllowedExtension(FileSettings.AllowedExtension),
            MaxSize(FileSettings.MaxFileSizeByte)]
        public IFormFile Cover { get; set; } = default!;


    }
}
