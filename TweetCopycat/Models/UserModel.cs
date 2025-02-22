using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace TweetCopycat.Models
{
    public class UserModel : IdentityUser
    {
        /// <summary>
        /// public string Id { get; set; }
        /// </summary>
        
        [JsonIgnore]
        public ICollection<TweetModel> Tweets { get; set; } = new List<TweetModel>();

        [JsonIgnore]
        public ICollection<LikeModel> Likes { get; set; } = new List<LikeModel>();

        [JsonIgnore]
        public ICollection<FollowModel> Follows { get; set; } = new List<FollowModel>();

        [JsonIgnore]
        public ICollection<NotificationModel> Notifications { get; set; } = new List<NotificationModel>();

        public string Name { get; set; }
        public string ProfilePic {  get; set; }
    }
}
