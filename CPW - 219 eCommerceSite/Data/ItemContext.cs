using CPW___219_eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CPW___219_eCommerceSite.Data
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {

        }

        public DbSet<item> Items { get; set; }
    }
}
