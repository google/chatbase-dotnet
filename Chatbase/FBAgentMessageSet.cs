using System;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace  Chatbase
{   
    [CollectionDataContract]
    public class FBAgentMessageCollection : Collection<FBAgentMessage> {}

    [DataContract]
    public class FBAgentMessageSet
    {
      public string api_key;
      
      [DataMember]
      private FBAgentMessageCollection messages;

      public FBAgentMessageSet(string key)
      {
        api_key = key;
        messages = new FBAgentMessageCollection();
      }

      public void Add(FBAgentMessage msg)
      {
        messages.Add(msg);
      }

      public FBAgentMessageCollection GetMessages()
      {
        return messages;
      }

      public FBAgentMessage NewMessage()
      {
        return new FBAgentMessage(api_key);
      }

      public void SetChatbaseFields()
      {
          foreach(var msg in messages) { msg.SetChatbaseFields(); }
      }
    }
}