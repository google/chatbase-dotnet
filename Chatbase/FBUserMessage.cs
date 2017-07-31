using System;
using System.Runtime.Serialization;

namespace Chatbase
{
    public struct SenderID { public string id; }
    public struct RecipientID { public string id; }
    public struct FBMessageContent {
      public string mid;
      public string text;
    }
    public struct FBChatbaseFields {
      public string intent;
      public string version;
      public bool not_handled;
      public bool feedback;
    }

    [DataContract]
    public class FBUserMessage
    {
        public string api_key;
		    public string version;
        public string intent;
        public bool not_handled;
		    public bool feedback;

        [DataMember]
		    public double timestamp;

        [DataMember]
        public SenderID sender;

        [DataMember]
        public RecipientID recipient;

        [DataMember]
		    public FBMessageContent message;

        [DataMember]
        private FBChatbaseFields chatbase_fields;

        public bool RequiredFieldsSet()
        {
          return !(
              String.IsNullOrEmpty(sender.id)
              || String.IsNullOrEmpty(recipient.id)
              || String.IsNullOrEmpty(message.mid)
              || String.IsNullOrEmpty(api_key)
          );
        }

        public FBUserMessage SetSenderID(string id)
        {
          sender.id = id;
          return this;
        }

        public FBUserMessage SetRecipientID(string id)
        {
          recipient.id = id;
          return this;
        }

        public FBUserMessage SetMessageID(string messageID)
        {
          message.mid = messageID;
          return this;
        }

        public FBUserMessage SetMessageContent(string messageContent)
        {
          message.text = messageContent;
          return this;
        }

        public FBUserMessage()
        {
            timestamp = Message.CurrentUnixMilliseconds();
            feedback = false;
            not_handled = false;
            sender = new SenderID();
            recipient = new RecipientID();
            message = new FBMessageContent();
        }

        public FBUserMessage(string key)
        {
            timestamp = Message.CurrentUnixMilliseconds();
            feedback = false;
            not_handled = false;
            sender = new SenderID();
            recipient = new RecipientID();
            message = new FBMessageContent();
            api_key = key;
        }

        public FBUserMessage SetChatbaseFields() {
          chatbase_fields = new FBChatbaseFields
          {
              intent = intent,
              version = version,
              not_handled = not_handled,
              feedback = feedback
          };

          return this;
        }

        public FBChatbaseFields GetChatbaseFields()
        {
          return chatbase_fields;
        }
    }
}
