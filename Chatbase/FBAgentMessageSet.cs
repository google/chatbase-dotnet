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