using Microsoft.AspNetCore.Identity;

namespace TweetCopycat.Models
{
    public class UserModel : IdentityUser
    {
        /// <summary>
        /// public string Id { get; set; }
        /// </summary>
        public ICollection<TweetModel> Tweets { get; set; } = new List<TweetModel>();
        public ICollection<LikeModel> Likes { get; set; } = new List<LikeModel>();
        public string Name { get; set; }
        public string ProfilePic {  get; set; }
    }
}
