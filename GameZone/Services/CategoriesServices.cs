using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly GameDbContext dbContext;

        public CategoriesServices(GameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<SelectListItem> GetSelectLists()
        {
            return dbContext.Categories.
                Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text).AsNoTracking().ToList();
        }
    }
}
