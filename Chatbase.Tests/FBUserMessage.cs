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

using Xunit;
using Chatbase;

namespace Chatbase.UnitTests
{
    public class ChatbaseFBUserMessageUnitTests
    {
        public ChatbaseFBUserMessageUnitTests() {}

        [Fact]
        public void MessageHasFeedbackAndNotHandledAsFalse()
        {
          Chatbase.FBUserMessage msg = new Chatbase.FBUserMessage();
          Assert.False(msg.not_handled);
          Assert.False(msg.feedback);
        }

        [Theory]
        [InlineData("stub-api-key")]
        public void GivingKeyToConstructorSetsOnInstance(string key)
        {
          Chatbase.FBUserMessage msg = new Chatbase.FBUserMessage(key);
          Assert.Equal(msg.api_key, key);
        }

        [Theory]
        [InlineData("stub-message-content")]
        public void SettingMessageContentSetsOnInstance(string mc)
        {
          Chatbase.FBUserMessage msg = new Chatbase.FBUserMessage();
          Chatbase.FBUserMessage ret = msg.SetMessageContent(mc);
          Assert.Equal(msg.message.text, mc);
          // Assert that we are chain-able
          Assert.Equal(ret, msg);
        }

        [Theory]
        [InlineData("intent", "version", true, true)]
        public void SettingOnInstanceAllowsSettingCBFields(string intent, string version, bool nh, bool fb)
        {
          Chatbase.FBUserMessage msg = new Chatbase.FBUserMessage
          {
            intent = intent,
            version = version,
            not_handled = nh,
            feedback = fb
          };
          FBChatbaseFields cbFields = msg.SetChatbaseFields().GetChatbaseFields();
          Assert.Equal(cbFields.intent, intent);
          Assert.Equal(cbFields.version, version);
          Assert.Equal(cbFields.not_handled, nh);
          Assert.Equal(cbFields.feedback, fb);
        }
    }
}
