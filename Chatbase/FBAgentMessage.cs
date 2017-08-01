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
