using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class DevicesServices : IDevicesServices
    {
        private readonly GameDbContext dbContext;

        public DevicesServices(GameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SelectListItem> GetSelectLists()
        {
            return dbContext.Devices.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text).AsNoTracking().ToList();
        }
    }
}
