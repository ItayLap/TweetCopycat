using System.ComponentModel.DataAnnotations.Schema;

namespace TweetCopycat.Models
{
    public class TweetModel
    {
        public int Id { get; set; }
        public string UserId {  get; set; }
        public string Content {  get; set; }
        public bool IsRead {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(UserId))]
        public virtual UserModel User { get; set; }
    }
}
