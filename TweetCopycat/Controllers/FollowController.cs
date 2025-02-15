using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TweetCopycat.Data;
using TweetCopycat.Models;

namespace TweetCopycat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FollowController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("followers/{userId}")]
        public async Task<IActionResult> GetFollowers (string userId)
        {
            var followers = await _context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Follower)
                .ToListAsync();

            return Ok(followers);
        }

        [HttpPost("{followingId}")]
        [Authorize]
        public async Task<IActionResult> FollowUser(string followingId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || userId == followingId) 
            {
                return BadRequest("Invalid request");
            }

            var existingFollow = await _context.Follows
                .FirstOrDefaultAsync(f =>  f.FollowerId == userId && f.FollowingId == followingId);
            if (existingFollow != null) 
            {
                return BadRequest("Already following this user");
            }
            var follow = new FollowModel { FollowerId =  userId , FollowingId = followingId};
            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();
            return Ok("followed succesfully");
        }

        [HttpDelete("{folllowingId}")]
        [Authorize]
        public async Task<IActionResult> UnfollowUser(string followingId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var follow = await _context.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == userId && f.FollowingId == followingId);
            if (follow == null)
            {
                return BadRequest(" your'e not following this user");
            }
            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
            return Ok("Unfollowed!!");
        }
    }
}
