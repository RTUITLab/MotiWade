using Database;
using FirebaseAdmin.Messaging;
using Google.Apis.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityShiftLearning.Services
{
    public class PushNotifyService
    {
        private readonly LearnDbContext dbContext;
        private readonly ILogger<PushNotifyService> logger;

        public PushNotifyService(
            LearnDbContext dbContext,
            ILogger<PushNotifyService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public Task SendNotifyToUser(string title, string body, string userId) => SendNotify(title, body, userId);
        public Task SendNotifyToAll(string title, string body) => SendNotify(title, body, "all");
        private async Task SendNotify(string title, string body, string topic)
        {
            var result = await FirebaseMessaging.DefaultInstance.SendAllAsync(new List<Message> {
                new Message{
                    Topic = "all",
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body
                    }
                }
            });
            logger.LogInformation($"Sending {title} fails {result.FailureCount} success {result.SuccessCount}");
            dbContext.NotifyMessages.Add(new Models.NotifyMessage
            {
                Title = title,
                Description = body,
                Topic = topic,
                DateTimeOffset = DateTimeOffset.UtcNow
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
