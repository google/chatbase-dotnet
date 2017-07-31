using Xunit;
using Chatbase;

namespace Chatbase.UnitTests
{
    public class ChatbaseFBAgentMessageUnitTests
    {
        public ChatbaseFBAgentMessageUnitTests() {}

        [Theory]
        [InlineData("stub-api-key")]
        public void GivingKeyToConstructorSetsOnInstance(string key)
        {
          Chatbase.FBAgentMessage msg = new Chatbase.FBAgentMessage(key);
          Assert.Equal(msg.api_key, key);
        }

        [Theory]
        [InlineData("stub-rec-id")]
        public void SettingRecipientSetsOnInstance(string recID)
        {
          Chatbase.FBAgentMessage msg = new Chatbase.FBAgentMessage();
          Chatbase.FBAgentMessage ret = msg.SetRecipientID(recID);
          Assert.Equal(msg.request_body.recipient.id, recID);
          Assert.Equal(msg.response_body.recipient_id, recID);
          // Assert that we are chain-able
          Assert.Equal(ret, msg);
        }

        [Theory]
        [InlineData("stub-message-content")]
        public void SettingMessageContentSetsOnInstance(string mc)
        {
          Chatbase.FBAgentMessage msg = new Chatbase.FBAgentMessage();
          Chatbase.FBAgentMessage ret = msg.SetMessageContent(mc);
          Assert.Equal(msg.request_body.message.text, mc);
          // Assert that we are chain-able
          Assert.Equal(ret, msg);
        }
        
        [Theory]
        [InlineData("stub-message-id")]
        public void SettingMessageIdSetsOnInstance(string mID)
        {
          Chatbase.FBAgentMessage msg = new Chatbase.FBAgentMessage();
          Chatbase.FBAgentMessage ret = msg.SetMessageID(mID);
          Assert.Equal(msg.response_body.message_id, mID);
          // Assert that we are chain-able
          Assert.Equal(ret, msg);
        }

        [Theory]
        [InlineData("intent", "version", "rec-id", "msg-text", "mid1234")]
        public void ConsumingFBUserMessageSetsOnInstance(string intent, string version, string rid, string msgTxt, string mid)
        {
          Chatbase.FBUserMessage userMsg = new Chatbase.FBUserMessage
          {
            intent = intent,
            version = version
          };
          userMsg.SetMessageID(mid).SetRecipientID(rid)
            .SetMessageContent(msgTxt);
          Chatbase.FBAgentMessage msg = new Chatbase.FBAgentMessage();
          Chatbase.FBAgentMessage ret = msg.ConsumeUserMessage(userMsg);
          Assert.Equal(msg.intent, intent);
          Assert.Equal(msg.version, version);
          Assert.Equal(msg.request_body.recipient.id, rid);
          Assert.Equal(msg.response_body.recipient_id, rid);
          Assert.Equal(msg.response_body.message_id, mid);
          Assert.Equal(msg.request_body.message.text, msgTxt);
          // Assert that we are chain-able
          Assert.Equal(ret, msg);
        }

        [Theory]
        [InlineData("stub-api-key")]
        public void SettingRequiredPassesValidation(string key)
        {
          Chatbase.FBAgentMessage msg = new Chatbase.FBAgentMessage
          {
            api_key = key
          };
          Assert.True(msg.RequiredFieldsSet());
        }

        [Theory]
        [InlineData("intent", "version")]
        public void SettingOnInstanceAllowsSettingCBFields(string intent, string version)
        {
          Chatbase.FBAgentMessage msg = new Chatbase.FBAgentMessage
          {
            intent = intent,
            version = version
          };
          FBChatbaseFields cbFields = msg.SetChatbaseFields().GetChatbaseFields();
          Assert.Equal(cbFields.intent, intent);
          Assert.Equal(cbFields.version, version);
        }
    }
}
