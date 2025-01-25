using Microsoft.AspNetCore.Identity;

namespace TweetCopycat.Models
{
    public class UserModel : IdentityUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfilePic {  get; set; }

    }
}
