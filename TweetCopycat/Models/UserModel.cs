using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TweetCopycat.Models
{
    public class UserModel : IdentityUser
    {
        public string Name { get; set; }
        public string ProfilePic { get; set; } = "default-profile-pic.png";
        public ICollection<TweetModel> Tweets { get; set; } = new List<TweetModel>();
        public ICollection<LikeModel> Likes { get; set;} = new List<LikeModel>();
        //public ICollection<NotificationModel> Notifications { get; set;} = new List<NotificationModel>();
        [NotMapped]
        public ICollection<FollowModel> Followers { get; set; } = new List<FollowModel>();
        [NotMapped]
        public ICollection<FollowModel> Following { get; set; } = new List<FollowModel>();



    }
}