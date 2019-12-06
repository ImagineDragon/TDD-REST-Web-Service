using Microsoft.EntityFrameworkCore;

namespace TDD_REST_Web_Service.Models
{
    public class DefaultContext : DbContext
    {
        public DefaultContext()
        {
        }

        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        {
        }

        public DbSet<DefaultModel> DefaultModels { get; set; }
    }
}
