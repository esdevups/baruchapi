using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Data.D
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<AppUser> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<SubCategory> subCategories { get; set; }
        public DbSet<ProductProps> productProps { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ArticleLabel> ArticleLabels { get; set; }
        public DbSet<ProductLabel> ProductLabel { get; set; }
        public DbSet<UI> ui { get; set; }
    }
}