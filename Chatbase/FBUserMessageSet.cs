using System;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace  Chatbase
{   
    [CollectionDataContract]
    public class FBUserMessageCollection : Collection<FBUserMessage> {}

    [DataContract]
    public class FBUserMessageSet
    {
      public string api_key;
      
      [DataMember]
      private FBUserMessageCollection messages;

      public FBUserMessageSet(string key)
      {
        api_key = key;
        messages = new FBUserMessageCollection();
      }

      public void Add(FBUserMessage msg)
      {
        messages.Add(msg);
      }

      public FBUserMessageCollection GetMessages()
      {
        return messages;
      }

      public FBUserMessage NewMessage()
      {
        return new FBUserMessage(api_key);
      }

      public void SetChatbaseFields()
      {
          foreach(var msg in messages) { msg.SetChatbaseFields(); }
      }
    }
}