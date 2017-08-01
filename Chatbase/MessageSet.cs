// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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