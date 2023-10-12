
namespace GameZone.Models
{
    public class Device:BaseEntity
    {
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;
        public List<GameDevice> GameDevices { get; set; }=new List<GameDevice>();
    }
}
