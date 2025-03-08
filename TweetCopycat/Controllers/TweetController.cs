using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TweetCopycat.Data;
using TweetCopycat.Models;

namespace TweetCopycat.Controllers
{
    [Route("api/[controller]")]
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
            var tweet = new TweetModel 
            { 
                UserId = userId,
                Content = model.Content,
                CreatedAt = DateTime.UtcNow 
            }; 
            _context.Tweets.Add(tweet); 
            await _context.SaveChangesAsync(); 
            return CreatedAtAction(nameof(GetTweetById), new { id = tweet.Id }, tweet);

            //if (tweet == null || string.IsNullOrEmpty(tweet.Content))
            //    return BadRequest("Invalid tweet data.");

            //var user = await _userManager.FindByIdAsync(tweet.UserId);
            //if (user == null)
            //    return BadRequest("Invalid UserId.");

            //tweet.CreatedAt = DateTime.UtcNow;
            //_context.Tweets.Add(tweet);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetTweetById), new { id = tweet.Id }, tweet);
        }

        [HttpGet]
        public async Task<IActionResult> GetTweetById()
        {
            var tweet = await _context.Tweets
                .Include(x => x.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return Ok(tweet);
        }

    }
}