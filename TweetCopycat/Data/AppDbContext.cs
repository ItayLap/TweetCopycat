using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using TweetCopycat.Models;
namespace TweetCopycat.Data
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options) 
        {

        }
        public DbSet<TweetModel> Tweets { get; set; }
        public DbSet<LikeModel> Likes { get; set; }
        /*
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LikeModel>()
                .HasOne(I => I.Tweet)
                .WithMany(I => I.Like)
                .HasForeignKey(I => I.TweetId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
