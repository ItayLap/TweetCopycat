using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TweetCopycat.Data;
using TweetCopycat.Models;

namespace TweetCopycat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public NotificationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(NotificationModel notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Notification sent succesfully."});
        }

        [HttpGet]
        public async Task<IActionResult> GetNotification(string userId)
        {
            var notifications = await _context.Notifications
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return Ok(notifications);
        }

        [HttpPut("mark-as-read/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound("Notification not found");
            }
            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return Ok(new {message = "Notification marked as read"});
        }


    }
}
