using System;
using System.Threading;
using System.Threading.Tasks;
using Bogus;

namespace WpfApp.Server
{
    public class MessageReceivedEventArgs
    {
        public Message Message { get; }

        public MessageReceivedEventArgs(Message message)
        {
            Message = message;
        }
    }

    public class MessageServer : IDisposable
    {
        public Func<int, CancellationToken, Task> Delay { get; set; } = Task.Delay;

        public event Action<object, MessageReceivedEventArgs> MessageReceived = null!;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public string Topic { get; }

        private static readonly Faker<Message> Faker = new Faker<Message>()
            .StrictMode(true)
            .RuleFor(m => m.Title, t => t.Random.Words(2))
            .RuleFor(m => m.Content, t => t.Random.Words(50))
            .RuleFor(m => m.Sender, t => t.Person.Email);

        public MessageServer()
        {
            var topicFaker = new Bogus.Randomizer();
            Topic = topicFaker.Word();
        }


        public async void Start(int millisecondsBetweenMessages)
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                await Delay(millisecondsBetweenMessages, _cancellationTokenSource.Token);
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs(Faker.Generate()));
            }
        }

        public void Dispose()
        {
            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}