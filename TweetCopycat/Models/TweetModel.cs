using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TweetCopycat.Models
{
    public class TweetModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public virtual ICollection<LikeModel> Likes { get; set; } = new List<LikeModel>(); // Исправлено с "Like"

        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual UserModel User { get; set; }
    }
}
