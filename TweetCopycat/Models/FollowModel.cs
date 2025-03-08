using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TweetCopycat.Models
{
    public class FollowModel
    {
        [Key]
        public int Id { get; set; } // Изменено на int

        public string FollowerId { get; set; }
        public string FollowingId { get; set; }

        [ForeignKey("FollowerId")]
        [JsonIgnore]
        public virtual UserModel Follower { get; set; }

        [ForeignKey("FollowingId")]
        [JsonIgnore]
        public virtual UserModel Following { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
