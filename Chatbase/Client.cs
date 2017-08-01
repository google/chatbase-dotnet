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
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace Chatbase
{
    public class Client
    {
        private HttpClient client;
        public string api_key;
        public string user_id;
        public string platform;
        public string version;

        private static String ContentType
        { 
            get { return "application/json"; }
        }
        private static String SingluarMessageEndpoint
        {
            get { return "https://chatbase.com/api/message"; }
        }
        private static String BatchMessageEndpoint
        {
            get { return "https://chatbase.com/api/messages?api_key={0}"; }
        }
        private static String SingularFBUserMessageEndpoint
        {
            get { return "https://chatbase.com/api/facebook/message_received?api_key={0}&intent={1}&not_handled={2}&feedback={3}&version={4}"; }
        }
        private static String BatchFBUserMessageEndpoint
        {
            get { return "https://chatbase.com/api/facebook/message_received_batch?api_key={0}"; }
        }
        private static String SingularFBAgentMessageEndpoint
        {
            get { return "https://chatbase.com/api/facebook/send_message?api_key={0}&version={1}"; }
        }
         private static String BatchFBAgentMessageEndpoint
        {
            get { return "https://chatbase.com/api/facebook/send_message_batch?api_key={0}"; }
        }

        public async Task<HttpResponseMessage> Send(Message msg)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(
                typeof(Message));
            ser.WriteObject(stream, msg);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            StringContent content = new StringContent(json, Encoding.UTF8, Client.ContentType);
            return await client.PostAsync(Client.SingluarMessageEndpoint, content);
        }

        public async Task<HttpResponseMessage> Send(FBUserMessage msg)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(
                typeof(FBUserMessage),
                new Type[]
                {
                    typeof(SenderID),
                    typeof(RecipientID),
                    typeof(FBMessageContent),
                    typeof(FBChatbaseFields)
                }
            );
            ser.WriteObject(stream, msg);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            string url = String.Format(Client.SingularFBUserMessageEndpoint,
                msg.api_key, msg.intent, msg.not_handled, msg.feedback, msg.version);
            StringContent content = new StringContent(json, Encoding.UTF8, Client.ContentType);
            return await client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> Send(FBAgentMessage msg)
        {
            msg.SetChatbaseFields();
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(
                typeof(FBAgentMessage),
                new Type[]
                {
                    typeof(SenderID),
                    typeof(RecipientID),
                    typeof(FBMessageContent),
                    typeof(FBChatbaseFields),
                    typeof(FBAgentMessageRequestBody),
                    typeof(FBAgentMessageResponseBody)
                }
            );
            ser.WriteObject(stream, msg);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            string url = String.Format(Client.SingularFBAgentMessageEndpoint,
                msg.api_key, msg.version);
            StringContent content = new StringContent(json, Encoding.UTF8, Client.ContentType);
            return await client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> Send(MessageSet set)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(
                typeof(MessageSet),
                new Type[]{typeof(MessageCollection), typeof(Message)});
            ser.WriteObject(stream, set);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            string url = String.Format(Client.BatchMessageEndpoint, set.api_key);
            StringContent content = new StringContent(json, Encoding.UTF8, Client.ContentType);
            return await client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> Send(FBUserMessageSet set)
        {
            set.SetChatbaseFields();
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(
                typeof(FBUserMessageSet),
                new Type[]
                {
                    typeof(FBUserMessageCollection),
                    typeof(FBUserMessage),
                    typeof(FBChatbaseFields),
                    typeof(SenderID),
                    typeof(RecipientID),
                    typeof(FBMessageContent)
                }
            );
            ser.WriteObject(stream, set);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            string url = String.Format(Client.BatchFBUserMessageEndpoint, set.api_key);
            StringContent content = new StringContent(json, Encoding.UTF8, Client.ContentType);
            return await client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> Send(FBAgentMessageSet set)
        {
            set.SetChatbaseFields();
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(
                typeof(FBAgentMessageSet),
                new Type[]
                {
                    typeof(FBAgentMessageCollection),
                    typeof(FBAgentMessage),
                    typeof(FBChatbaseFields),
                    typeof(SenderID),
                    typeof(RecipientID),
                    typeof(FBMessageContent),
                    typeof(FBAgentMessageRequestBody),
                    typeof(FBAgentMessageResponseBody)
                }
            );
            ser.WriteObject(stream, set);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            string url = String.Format(Client.BatchFBAgentMessageEndpoint, set.api_key);
            StringContent content = new StringContent(json, Encoding.UTF8, Client.ContentType);
            return await client.PostAsync(url, content);
        }

        private Message setMessageWithClientProperties(Message msg)
        {
            if (!String.IsNullOrEmpty(api_key)) {
                msg.api_key = api_key;
            }
            if (!String.IsNullOrEmpty(user_id)) {
                msg.user_id = user_id;
            }
            if (!String.IsNullOrEmpty(platform)) {
                msg.platform = platform;
            }
            if (!String.IsNullOrEmpty(version)) {
                msg.version = version;
            }
            
            return msg;
        }

        public Message NewMessageFromClientParams()
        {
            return setMessageWithClientProperties(new Message());
        }

        public Message NewMessage()
        {
            return new Message();
        }

        public Client()
        {
            client = new HttpClient();
        }

        public Client(string key)
        {
            client = new HttpClient();
            api_key = key;
        }

        public Client(string key, string uid)
        {
            client = new HttpClient();
            api_key = key;
            user_id = uid;
        }

        public Client(string key, string uid, string plt)
        {
            client = new HttpClient();
            api_key = key;
            user_id = uid;
            platform = plt;
        }

        public Client(string key, string uid, string plt, string ver)
        {
            client = new HttpClient();
            api_key = key;
            user_id = uid;
            platform = plt;
            version = ver;
        }
    }
}
