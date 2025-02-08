using Microsoft.AspNetCore.Mvc;
using TweetCopycat.Data;
using Microsoft.EntityFrameworkCore;
using TweetCopycat.Models;

namespace TweetCopycat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public TweetController(AppDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTweets()
        {
            var tweets = await _Context.Tweets.Include(t => t.User).ToListAsync();
            return Ok(tweets);
        }

        [HttpPost]
        public async Task<IActionResult> PostTweet([FromBody] TweetModel tweet)
        {
            if (tweet == null || string.IsNullOrEmpty(tweet.Content))
            {
                return BadRequest("Invalid tweet data");
            }
            tweet.UserId = User.FindFirst("sub")?.Value;
            tweet.CreatedAt = DateTime.UtcNow;

            _Context.Tweets.Add(tweet);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllTweets),new { id = tweet.Id }, tweet);

        }

    }
}
