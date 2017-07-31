using Xunit;
using Chatbase;

namespace Chatbase.UnitTests
{
    public class ChatbaseMessageUnitTests
    {
        private readonly Chatbase.Message _message;

        public ChatbaseMessageUnitTests()
        {
            _message = new Chatbase.Message();
        }

        [Fact]
        public void DefaultsUserMessage()
        {
            Assert.Equal(_message.type, Chatbase.Message.UserMessage);
        }
        
        [Fact]
        public void DefaultsNotHandled()
        {
            Assert.False(_message.not_handled);
        }

        [Fact]
        public void DefaultsFeedback()
        {
            Assert.False(_message.feedback);
        }

        [Fact]
        public void DefaultsDoNotCoverAllRequiredFields() {
            Assert.False(_message.RequiredFieldsSet());
        }

        [Theory]
        [InlineData("stub-api-key")]
        public void GivingKeyToConstructorSetsOnInstance(string key) {
            Chatbase.Message msg = new Chatbase.Message(key);
            Assert.Equal(msg.api_key, key);
        }

        [Theory]
        [InlineData("stub-api-key", "stub-user-id", "stub-platform")]
        public void SettingRequiredPassesValidation(string key, string uid, string plt) {
            Chatbase.Message msg = new Chatbase.Message();
            msg.api_key = key;
            msg.user_id = uid;
            msg.platform = plt;
            Assert.True(msg.RequiredFieldsSet());
        }
    }
}
