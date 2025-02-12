using System.ComponentModel.DataAnnotations.Schema;

namespace TweetCopycat.Models
{
    public class LikeModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TweetId {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }

        [ForeignKey("TweetId")]
        public virtual TweetModel Tweet { get; set; }
    }
}
