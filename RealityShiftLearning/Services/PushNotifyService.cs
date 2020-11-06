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
        private readonly ILogger<PushNotifyService> logger;

        public PushNotifyService(ILogger<PushNotifyService> logger)
        {
            this.logger = logger;
        }
        public async Task SendNotifyToAll(string title, string body)
        {
            var result = await FirebaseMessaging.DefaultInstance.SendAllAsync(new List<Message> {
                new Message{
                    Topic = "test",
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body
                    }
                }
            });
            logger.LogInformation($"Sending {title} fails {result.FailureCount} success {result.SuccessCount}");
        }
    }
}
