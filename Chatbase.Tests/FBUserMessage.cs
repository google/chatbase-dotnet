using Xunit;
using Chatbase;

namespace Chatbase.UnitTests
{
    public class ChatbaseFBUserMessageUnitTests
    {
        public ChatbaseFBUserMessageUnitTests() {}

        [Fact]
        public void MessageHasFeedbackAndNotHandledAsFalse()
        {
          Chatbase.FBUserMessage msg = new Chatbase.FBUserMessage();
          Assert.False(msg.not_handled);
          Assert.False(msg.feedback);
        }

        [Theory]
        [InlineData("stub-api-key")]
        public void GivingKeyToConstructorSetsOnInstance(string key)
        {
          Chatbase.FBUserMessage msg = new Chatbase.FBUserMessage(key);
          Assert.Equal(msg.api_key, key);
        }

        [Theory]
        [InlineData("stub-message-content")]
        public void SettingMessageContentSetsOnInstance(string mc)
        {
          Chatbase.FBUserMessage msg = new Chatbase.FBUserMessage();
          Chatbase.FBUserMessage ret = msg.SetMessageContent(mc);
          Assert.Equal(msg.message.text, mc);
          // Assert that we are chain-able
          Assert.Equal(ret, msg);
        }

        [Theory]
        [InlineData("intent", "version", true, true)]
        public void SettingOnInstanceAllowsSettingCBFields(string intent, string version, bool nh, bool fb)
        {
          Chatbase.FBUserMessage msg = new Chatbase.FBUserMessage
          {
            intent = intent,
            version = version,
            not_handled = nh,
            feedback = fb
          };
          FBChatbaseFields cbFields = msg.SetChatbaseFields().GetChatbaseFields();
          Assert.Equal(cbFields.intent, intent);
          Assert.Equal(cbFields.version, version);
          Assert.Equal(cbFields.not_handled, nh);
          Assert.Equal(cbFields.feedback, fb);
        }
    }
}
