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
using Xunit;
using Chatbase;

namespace Chatbase.UnitTests
{
    public class ChatbaseFBUserMessageSetUnitTests
    {
      public ChatbaseFBUserMessageSetUnitTests() {}

      [Theory]
      [InlineData("api-key")]
      public void KeyToConstructorPropagatesToInstance(string key)
      {
        Chatbase.FBUserMessageSet set = new Chatbase.FBUserMessageSet(key);
        Assert.Equal(set.api_key, key);
      }

      [Theory]
      [InlineData("api-key")]
      public void MessagesMadeFromSetContainKeyGivenToConstructor(string key)
      {
        Chatbase.FBUserMessageSet set = new Chatbase.FBUserMessageSet(key);
        Chatbase.FBUserMessage msg = set.NewMessage();
        Assert.Equal(msg.api_key, key);
        set.Add(msg);
        Assert.Equal(set.GetMessages().Count, 1);
      }
    }
}
