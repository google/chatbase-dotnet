using System;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace  Chatbase
{   
    [CollectionDataContract]
    public class MessageCollection : Collection<Message> {}

    [DataContract]
    public class MessageSet
    {
      public string api_key;
      
      [DataMember]
      private MessageCollection messages;

      public MessageSet(string key)
      {
        api_key = key;
        messages = new MessageCollection();
      }

      public void Add(Message msg)
      {
        messages.Add(msg);
      }

      public MessageCollection GetMessages()
      {
        return messages;
      }

      public Message NewMessage()
      {
        return new Message(api_key);
      }
    }
}