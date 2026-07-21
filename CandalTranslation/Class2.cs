//using Candal.Translation;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Text.Json;
//using System.Threading;
//using System.Threading.Channels;
//using System.Threading.Tasks;

//namespace CandalTranslation
//{
//    public class ExampleWorker : BackgroundService
//    {
//        private readonly Channel<string> _queue;

//        public EmailWorker(Channel<string> queue)
//        {
//            _queue = queue;
//        }

//        protected override async Task ExecuteAsync(CancellationToken cancellationToken = default)
//        {
//            await foreach (var email in _queue.Reader.ReadAllAsync(stoppingToken))
//            {
//                await SendEmailAsync(email); // tarefa pesada
//            }

//            await _queue.Writer.WriteAsync("dto.Email");
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            var db = _redis.GetDatabase();

//            while (!stoppingToken.IsCancellationRequested)
//            {
//                var messages = await db.StreamReadGroupAsync(
//                    "tasks-stream", "workers", "worker-1", ">", count: 1);

//                foreach (var msg in messages)
//                {
//                    var payload = JsonSerializer.Deserialize<Payload>(msg.Values[0].Value);
//                    await ProcessPayload(payload);

//                    await db.StreamAcknowledgeAsync("tasks-stream", "workers", msg.Id);
//                }
//            }
//        }
//    }
//}
