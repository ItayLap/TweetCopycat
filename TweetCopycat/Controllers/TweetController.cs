using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TweetCopycat.Data;
using TweetCopycat.Models;

namespace TweetCopycat.Controllers
{
    [Route("api/tweets")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public TweetController(AppDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostTweet([FromBody] TweetModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found in token" });
            }

            if (string.IsNullOrEmpty(model.Content))
            {
                return BadRequest(new { message = "Tweet content cannot be empty." });
            }

            var tweet = new TweetModel
            {
                UserId = userId,
                Content = model.Content,
                CreatedAt = DateTime.UtcNow
            };
            _context.Tweets.Add(tweet);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllTweets), new { id = tweet.Id }, tweet);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTweets()
        {
            var tweets = await _context.Tweets
                .Include(x => x.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return Ok(tweets);
        }
    }
}