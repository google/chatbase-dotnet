using System;
using System.Runtime.Serialization;

namespace Chatbase
{
    public struct FBAgentMessageContent { public string text; }
    public struct FBAgentMessageRequestBody
    {
      public RecipientID recipient;
      public FBAgentMessageContent message;
      public double timestamp;
    }
    public struct FBAgentMessageResponseBody
    {
      public string recipient_id;
      public string message_id;
    }

    [DataContract]
    public class FBAgentMessage
    {
        public string api_key;
		    public string version;
        public string intent;

        [DataMember]
        public FBAgentMessageRequestBody request_body;

        [DataMember]
		    public FBAgentMessageResponseBody response_body;

        [DataMember]
        private FBChatbaseFields chatbase_fields;

        public bool RequiredFieldsSet()
        {
          return !String.IsNullOrEmpty(api_key);
        }

        public FBAgentMessage SetRecipientID(string id)
        {
          request_body.recipient.id = id;
          response_body.recipient_id = id;
          return this;
        }

        public FBAgentMessage SetMessageContent(string messageContent)
        {
          request_body.message.text = messageContent;
          return this;
        }

        public FBAgentMessage SetMessageID(string id)
        {
          response_body.message_id = id;
          return this;
        }

        public FBAgentMessage()
        {
          request_body = new FBAgentMessageRequestBody();
          response_body = new FBAgentMessageResponseBody();
          request_body.timestamp = Message.CurrentUnixMilliseconds();
        }

        public FBAgentMessage(string key)
        {
          request_body = new FBAgentMessageRequestBody();
          response_body = new FBAgentMessageResponseBody();
          request_body.timestamp = Message.CurrentUnixMilliseconds();
          api_key = key;
        }

        public FBAgentMessage ConsumeUserMessage(FBUserMessage msg) 
        {
          intent = msg.intent;
          version = msg.version;
          this.SetRecipientID(msg.recipient.id);
          this.SetMessageContent(msg.message.text);
          this.SetMessageID(msg.message.mid);

          return this;
        }

        public FBAgentMessage SetChatbaseFields() {
          chatbase_fields = new FBChatbaseFields
          {
              intent = intent,
              version = version
          };

          return this;
        }

        public FBChatbaseFields GetChatbaseFields()
        {
          return chatbase_fields;
        }
    }
}
