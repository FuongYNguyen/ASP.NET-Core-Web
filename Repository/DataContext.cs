using KyNiem50NamWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace KyNiem50NamWeb.Repository
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ giữa Post và User 
            modelBuilder.Entity<Post>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(u => u.Posts) // User có thể tạo nhiều bài viết
                .HasForeignKey(p => p.CreatedByUserID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa Post khi xóa User 

            modelBuilder.Entity<Post>()
                .HasOne(p => p.ModifiedByUser)
                .WithMany()
                .HasForeignKey(p => p.ModifiedByUserID)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
