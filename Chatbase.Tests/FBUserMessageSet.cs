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
