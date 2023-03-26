using Microsoft.EntityFrameworkCore;
using testAPI.Models;

namespace testAPI.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<ShortURLs> ShortURLs { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) 
            :base(options) 
        {
            
        } 
    }
}
