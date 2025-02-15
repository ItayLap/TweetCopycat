using System.ComponentModel.DataAnnotations.Schema;

namespace TweetCopycat.Models
{
    public class FollowModel
    {
        public string Id { get; set; }

        [ForeignKey("FollowerId")]
        public virtual UserModel Follower { get; set; }

        [ForeignKey("FollowingId")]
        public virtual UserModel Following {  get; set; }

        public string FollowerId { get; set; }

        public string FollowingId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
