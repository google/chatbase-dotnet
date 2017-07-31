using System;
using Xunit;
using Chatbase;

namespace Chatbase.UnitTests
{
    public class ChatbaseFBAgentMessageSetUnitTests
    {
      public ChatbaseFBAgentMessageSetUnitTests() {}

      [Theory]
      [InlineData("api-key")]
      public void KeyToConstructorPropagatesToInstance(string key)
      {
        Chatbase.FBAgentMessageSet set = new Chatbase.FBAgentMessageSet(key);
        Assert.Equal(set.api_key, key);
      }

      [Theory]
      [InlineData("api-key")]
      public void MessagesMadeFromSetContainKeyGivenToConstructor(string key)
      {
        Chatbase.FBAgentMessageSet set = new Chatbase.FBAgentMessageSet(key);
        Chatbase.FBAgentMessage msg = set.NewMessage();
        Assert.Equal(msg.api_key, key);
        set.Add(msg);
        Assert.Equal(set.GetMessages().Count, 1);
      }
    }
}
