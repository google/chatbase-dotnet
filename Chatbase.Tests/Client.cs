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
using System.Net;
using Xunit;
using Chatbase;

using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;


namespace Chatbase.UnitTests
{
    public class ChatbaseClientUnitTests
    {
        private readonly Chatbase.Client _client;

        private static void PrintNoAPIKeyWarning()
        {
          Console.WriteLine("");
          Console.WriteLine("!!!!!!!!!!!!!!! WARNING !!!!!!!!!!!!!!!!");
          Console.WriteLine("Skipping positive integration test since");
          Console.WriteLine("Env variable CB_TEST_API_KEY was not set");
          Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
          Console.WriteLine("");
        }

        public ChatbaseClientUnitTests()
        {
            _client = new Chatbase.Client();
        }

        [Fact]
        public void NewMessageReturnsNewMessage()
        {
            Assert.True(_client.NewMessage() is Chatbase.Message);
        }

        [Fact]
        public void NewMessageWithParamsReturnsNewMessage()
        {
            Assert.True(_client.NewMessageFromClientParams() is Chatbase.Message);
        }

        [Theory]
        [InlineData("stub-api-key")]
        public void SettingKeyOnClientSetsOnMessageFromParams(string key)
        {
          Chatbase.Client cl = new Chatbase.Client();
          cl.api_key = key;
          Chatbase.Message msg = cl.NewMessageFromClientParams();
          Assert.Equal(msg.api_key, key);
          // Make sure all other unset fields were not affected
          Assert.True(String.IsNullOrEmpty(msg.user_id));
          Assert.True(String.IsNullOrEmpty(msg.platform));
          Assert.True(String.IsNullOrEmpty(msg.version));
        }

        [Theory]
        [InlineData("stub-user-id")]
        public void SettingUserIdOnClientSetsOnMessageFromParams(string uid)
        {
          Chatbase.Client cl = new Chatbase.Client();
          cl.user_id = uid;
          Chatbase.Message msg = cl.NewMessageFromClientParams();
          Assert.Equal(msg.user_id, uid);
          // Make sure all other unset fields were not affected
          Assert.True(String.IsNullOrEmpty(msg.api_key));
          Assert.True(String.IsNullOrEmpty(msg.platform));
          Assert.True(String.IsNullOrEmpty(msg.version));
        }

        [Theory]
        [InlineData("stub-platform")]
        public void SettingPlatformOnClientSetsOnMessageFromParams(string plt)
        {
          Chatbase.Client cl = new Chatbase.Client();
          cl.platform = plt;
          Chatbase.Message msg = cl.NewMessageFromClientParams();
          Assert.Equal(msg.platform, plt);
          // Make sure all other unset fields were not affected
          Assert.True(String.IsNullOrEmpty(msg.api_key));
          Assert.True(String.IsNullOrEmpty(msg.user_id));
          Assert.True(String.IsNullOrEmpty(msg.version));
        }

        [Theory]
        [InlineData("stub-version")]
        public void SettingVersionOnClientSetsOnMessageFromParams(string ver)
        {
          Chatbase.Client cl = new Chatbase.Client();
          cl.version = ver;
          Chatbase.Message msg = cl.NewMessageFromClientParams();
          Assert.Equal(msg.version, ver);
          // Make sure all other unset fields were not affected
          Assert.True(String.IsNullOrEmpty(msg.api_key));
          Assert.True(String.IsNullOrEmpty(msg.user_id));
          Assert.True(String.IsNullOrEmpty(msg.platform));
        }

        [Theory]
        [InlineData("stub-api-key", "stub-user-id", "stub-platform", "stub-version")]
        public void SettringClientPropsSetsOnMessageFromParams(string key, string uid, string plt, string ver) {
            Chatbase.Client cl = new Chatbase.Client();
            cl.api_key = key;
            cl.user_id = uid;
            cl.platform = plt;
            cl.version = ver;
            Chatbase.Message msg = cl.NewMessageFromClientParams();
            Assert.Equal(msg.api_key, key);
            Assert.Equal(msg.user_id, uid);
            Assert.Equal(msg.platform, plt);
            Assert.Equal(msg.version, ver);
        }

        [Theory]
        [InlineData("integration-test-user", "integration-test-platform", "0")]
        public void SendingWithAPIKeyReturnsError(string uid, string plt, string ver)
        {
            Chatbase.Message msg = new Chatbase.Message();
            msg.user_id = uid;
            msg.platform = plt;
            msg.version = ver;
            var resp = _client.Send(msg).Result;
            Assert.Equal(resp.StatusCode, HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("integration-test-user", "integration-test-platform", "0")]
        public void SendingValidMessageReturnsSuccess(string uid, string plt, string ver)
        {
            string key = Environment.GetEnvironmentVariable("CB_TEST_API_KEY");
            Chatbase.Message msg = new Chatbase.Message();
            if (String.IsNullOrEmpty(key)) {
              ChatbaseClientUnitTests.PrintNoAPIKeyWarning();
            } else {
              msg.user_id = uid;
              msg.platform = plt;
              msg.version = ver;
              msg.api_key = key;
              var resp = _client.Send(msg).Result;
              Assert.Equal(resp.StatusCode, HttpStatusCode.OK);
            }
        }

        [Theory]
        [InlineData("integration-test-user", "integration-test-platform", "0")]
        public void SendingFromClientReturnsSuccess(string uid, string plt, string ver)
        {
            string key = Environment.GetEnvironmentVariable("CB_TEST_API_KEY");
            Chatbase.Message msg = new Chatbase.Message();
            if (String.IsNullOrEmpty(key)) {
              ChatbaseClientUnitTests.PrintNoAPIKeyWarning();
            } else {
              Chatbase.Client cl = new Chatbase.Client();
              cl.user_id = uid;
              cl.platform = plt;
              cl.version = ver;
              cl.api_key = key;
              var resp = _client.Send(cl.NewMessageFromClientParams()).Result;
              Assert.Equal(resp.StatusCode, HttpStatusCode.OK);
            }
        }

        [Theory]
        [InlineData("integration-test-user", "integration-test-platform", "0")]
        public void SendingValidSetReturnsSuccess(string uid, string plt, string ver)
        {
            string key = Environment.GetEnvironmentVariable("CB_TEST_API_KEY");
            if (String.IsNullOrEmpty(key)) {
              ChatbaseClientUnitTests.PrintNoAPIKeyWarning();
            } else {
              Chatbase.MessageSet set = new Chatbase.MessageSet(key);
              Chatbase.Message msg = set.NewMessage();
              msg.user_id = uid;
              msg.platform = plt;
              msg.version = ver;
              set.Add(msg);
              var resp = _client.Send(set).Result;
              Assert.Equal(resp.StatusCode, HttpStatusCode.OK);
            }
        }

        [Theory]
        [InlineData("stub-intent", "0", "1", "2", "mid.1234", "hey bot")]
        public void SendingValidFBUserMessageReturnsSuccess(string intent, string ver, string senderID, string recID, string msgID, string cnt)
        {
            string key = Environment.GetEnvironmentVariable("CB_TEST_API_KEY");
            if (String.IsNullOrEmpty(key)) {
              ChatbaseClientUnitTests.PrintNoAPIKeyWarning();
            } else {
              Chatbase.FBUserMessage msg = new Chatbase.FBUserMessage
              {
                api_key = key,
                intent = intent,
                version = ver
              };
              msg.SetSenderID(senderID).SetRecipientID(recID)
                .SetMessageID(msgID).SetMessageContent(cnt);
              var resp = _client.Send(msg).Result;
              Assert.Equal(resp.StatusCode, HttpStatusCode.OK);
            }
        }

        [Theory]
        [InlineData("stub-intent", "0", "1", "2", "mid.1234", "hey bot")]
        public void SendingValidFBUserMessageSetReturnsSuccess(string intent, string ver, string senderID, string recID, string msgID, string cnt)
        {
            string key = Environment.GetEnvironmentVariable("CB_TEST_API_KEY");
            if (String.IsNullOrEmpty(key)) {
              ChatbaseClientUnitTests.PrintNoAPIKeyWarning();
            } else {
              Chatbase.FBUserMessageSet set = new Chatbase.FBUserMessageSet(key);
              Chatbase.FBUserMessage msg = set.NewMessage();
              
              msg.SetSenderID(senderID).SetRecipientID(recID)
                .SetMessageID(msgID).SetMessageContent(cnt);
              msg.api_key = key;
              msg.intent = intent;
              set.Add(msg);
              var resp = _client.Send(set).Result;
              Assert.Equal(resp.StatusCode, HttpStatusCode.OK);
            }
        }

        [Theory]
        [InlineData("stub-intent", "0", "1", "2", "mid.1234", "hey bot")]
        public void SendingValidFBAgentMessageReturnsSuccess(string intent, string ver, string senderID, string recID, string msgID, string cnt)
        {
            string key = Environment.GetEnvironmentVariable("CB_TEST_API_KEY");
            if (String.IsNullOrEmpty(key)) {
              ChatbaseClientUnitTests.PrintNoAPIKeyWarning();
            } else {
              Chatbase.FBAgentMessage msg = new Chatbase.FBAgentMessage
              {
                api_key = key,
                intent = intent,
                version = ver
              };
              msg.SetRecipientID(recID).SetMessageID(msgID).SetMessageContent(cnt);
              var resp = _client.Send(msg).Result;
              Assert.Equal(resp.StatusCode, HttpStatusCode.OK);
            }
        }

        [Theory]
        [InlineData("stub-intent", "0", "1", "2", "mid.1234", "hey bot")]
        public void SendingValidFBAgentMessageSetReturnsSuccess(string intent, string ver, string senderID, string recID, string msgID, string cnt)
        {
            string key = Environment.GetEnvironmentVariable("CB_TEST_API_KEY");
            if (String.IsNullOrEmpty(key)) {
              ChatbaseClientUnitTests.PrintNoAPIKeyWarning();
            } else {
              Chatbase.FBAgentMessageSet set = new Chatbase.FBAgentMessageSet(key);
              Chatbase.FBAgentMessage msg = set.NewMessage();
              
              msg.SetRecipientID(recID).SetMessageID(msgID).SetMessageContent(cnt);
              msg.intent = intent;
              msg.version = ver;
              set.Add(msg);
              var resp = _client.Send(set).Result;
              Assert.Equal(resp.StatusCode, HttpStatusCode.OK);
            }
        }
    }
}
