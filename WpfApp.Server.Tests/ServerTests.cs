using System;
using System.Threading.Tasks;
using Xunit;

namespace WpfApp.Server.Tests
{
    public class ServerTests : IDisposable
    {
        private readonly WpfApp.Server.MessageServer _sut;
        public ServerTests()
        {
            _sut = new MessageServer();
        }
        [Fact]
        public void It_should_post_be_about_a_topic()
        {
            Assert.True(!string.IsNullOrWhiteSpace(_sut.Topic));
        }
        
        [Fact]
        public async Task It_should_post_message_after_a_given_time()
        {
            var tsc = new TaskCompletionSource<Message>();
            _sut.MessageReceived += (o, args) =>
            {
                tsc.SetResult(args.Message);
            };
            _sut.Start(50);
            var message = await tsc.Task;
            Assert.NotNull(message);
        }
        [Fact]
        public async Task It_should_post_message_after_a_given_time_with_a_title()
        {
            var tsc = new TaskCompletionSource<Message>();
            _sut.MessageReceived += (o, args) =>
            {
                tsc.SetResult(args.Message);
            };
            _sut.Start(50);
            var message = await tsc.Task;
            Assert.False(string.IsNullOrWhiteSpace(message?.Title));
        }
        [Fact]
        public async Task It_should_post_message_after_a_given_time_with_content()
        {
            var tsc = new TaskCompletionSource<Message>();
            _sut.MessageReceived += (o, args) =>
            {
                tsc.SetResult(args.Message);
            };
            _sut.Start(50);
            var message = await tsc.Task;
            Assert.False(string.IsNullOrWhiteSpace(message?.Title), $"Expected {message?.Content} to have a value");
        }
        
        [Fact]
        public async Task It_should_post_message_after_a_given_time_with_a_sender()
        {
            var tsc = new TaskCompletionSource<Message>();
            _sut.MessageReceived += (o, args) =>
            {
                tsc.SetResult(args.Message);
            };
            _sut.Start(50);
            var message = await tsc.Task;
            Assert.False(string.IsNullOrWhiteSpace(message?.Sender));
        }
        
        [Fact]
        public async Task It_should_keep_posting_message_after_a_given_time()
        {
            var counter = 0;
            _sut.MessageReceived += (o, args) =>
            {
                counter++;
            };
            _sut.Start(50);
            await Task.Delay(110);
            Assert.Equal(2, counter);
            await Task.Delay(50);
            Assert.Equal(3, counter);
        }

        public void Dispose()
        {
            _sut.Dispose();
        }
    }
}
