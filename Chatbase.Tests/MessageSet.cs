using System;
using Xunit;
using Chatbase;

namespace Chatbase.UnitTests
{
    public class ChatbaseMessageSetUnitTests
    {
      public ChatbaseMessageSetUnitTests() {}

      [Theory]
      [InlineData("api-key")]
      public void KeyToConstructorPropagatesToInstance(string key)
      {
        Chatbase.MessageSet set = new Chatbase.MessageSet(key);
        Assert.Equal(set.api_key, key);
      }

      [Theory]
      [InlineData("api-key")]
      public void MessagesMadeFromSetContainKeyGivenToConstructor(string key)
      {
        Chatbase.MessageSet set = new Chatbase.MessageSet(key);
        Chatbase.Message msg = set.NewMessage();
        Assert.Equal(msg.api_key, key);
        set.Add(msg);
        Assert.Equal(set.GetMessages().Count, 1);
      }
    }
}
