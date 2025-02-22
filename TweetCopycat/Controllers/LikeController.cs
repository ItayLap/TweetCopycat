using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TweetCopycat.Data;
using TweetCopycat.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TweetCopycat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public LikeController(AppDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLikes(int TweetId)
        {
            var LikeCount = await _Context.Likes.CountAsync(I => I.TweetId == TweetId);
            return Ok(new { TweetId = TweetId, Likes = LikeCount });
            /*
            var Likes = await _Context.Likes.Include(t => t.User).ToListAsync();
            return Ok(Likes);
            */
        }
        /*
        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] LikeModel like)
        {
            like.UserId = User.FindFirst("sub")?.Value;
            like.TweetId = ;

            _Context.Likes.Add(like);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLikes), new { id = like.Id }, like);
        }
        */
        [HttpPost("TweetId")]
        [Authorize]
        public async Task<IActionResult> AddLike(int TweetId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Invalid token");
            }
            var tweet = await _Context.Tweets.FirstOrDefaultAsync(t => t.Id == TweetId);
            if (tweet == null)
            {
                return NotFound("tweet not found");
            }
            var existingLike = await _Context.Likes
                .FirstOrDefaultAsync(I => I.TweetId == TweetId && I.UserId == userId);
            if (existingLike != null)
            {
                return BadRequest("You have already liked this  tweet");
            }
            var like = new LikeModel { TweetId = TweetId, UserId = userId };
            _Context.Likes.Add(like);
            await _Context.SaveChangesAsync();
            return Ok("Tweet liked succesfully");
        }

        [HttpPut("TweetId")]
        [Authorize]
        public async Task<IActionResult> RemoveLike(int TweetId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Invalid token");
            }  
            var like = await _Context.Likes
                .FirstOrDefaultAsync(I => I.TweetId == TweetId && I.User.Id == userId);
            if(like == null)
            {
                return NotFound("You have not liked this tweet YET!!?!");
            }
            _Context.Likes.Remove(like);
            await _Context.SaveChangesAsync();
            return Ok("Tweet liked succesfully");
        }
    }
}
