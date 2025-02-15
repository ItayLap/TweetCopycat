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
        public DbSet<FollowModel> Follows { get; set; }
        /*
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

            modelBuilder.Entity<LikeModel>()
                .HasOne(I => I.User)
                .WithMany()
                .HasForeignKey(I => I.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FollowModel>()
                .HasOne(I => I.Follower)
                .WithMany()
                .HasForeignKey(I => I.FollowerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FollowModel>()
                .HasOne(I => I.Following)
                .WithMany()
                .HasForeignKey(I => I.FollowingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
