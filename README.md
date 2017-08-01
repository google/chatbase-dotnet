# Google.Chatbase
##### `Google.Chatbase` is a .NET library for the [Chatbase API](https://chatbase.com/documentation/ref)

## Account Setup
Please see the [Getting Started Section](https://chatbase.com/documentation/getting-started) for information
on configuring one's account and obtaining and API key.

## Using the library

## Using the module

#### One can send individual messages to the Generic and Facebook rest APIs:

Generic:

```CSHARP
using Chatbase;

Chatbase.Client client = new Chatbase.Client();

Chatbase.Message msg = new Chatbase.Message();
msg.api_key = "123"; // required
msg.user_id = "xyz"; // required
msg.intent = "test";
msg.version = "0.1";
msg.content = "This is a test.";
msg.type = Chatbase.Message.UserMessage; // default, required
msg.not_handled = false; // default
msg.feedback = false; //default
var resp = client.Send(msg).Result; // Get the result of the async task
// Write the return status-code from the API request
Console.WriteLine(resp.StatusCode);
```

Facebook:

```CSHARP
using Chatbase;

Chatbase.Client client = new Chatbase.Client();

// Agent messages
agnMsg = new Chatbase.FBAgentMessage();
agnMsg.SetRecipientID("123");
agnMsg.SetMessageID("456");
agnMsg.SetMessageContent("hey");
agnMsg.intent = "say-hello";
agnMsg.version = "0.2";
var firstTask = client.Send(agnMsg);

// User messages
usrMsg = new Chatbase.FBUserMessage();
usrMsg.SetSenderID("abc");
usrMsg.SetRecipientID("123");
usrMsg.SetMessageID("456");
usrMsg.SetMessageContent("hey");
usrMsg.intent = "say-hello";
usrMsg.version = "0.2";
var secondTask = client.Send(usrMsg);
```

#### One can send sets of messages as well to the Generic and Facebook rest APIs:

Generic:

```CSHARP
using Chatbase;

Chatbase.Client client = new Chatbase.Client();

Chatbase.MessageSet set = new Chatbase.MessageSet("api-key");

// New messages made from the set will have the api-key already set
Chatbase.Message msg = set.NewMessage();
// ... Set fields as one would a regular message like the example above
set.Add(msg)
var task = Client.Send(set);
```

Facebook:

```CSHARP
using Chatbase;

Chatbase.Client client = new Chatbase.Client();

// Agent Message Set
Chatbase.FBAgentMessageSet agnSet = new Chatbase.FBAgentMessageSet("api-key");
Chatbase.FBAgentMessage agnMsg = agnSet.NewMessage();
// ... Set fields as one would a regular FBAgentMessage
agnSet.add(agnMsg)
var firstTask = client.Send(agnSet);

// User Message Set
Chatbase.FBUserMessageSet usrSet = new Chatbase.FBUserMessageSet("api-key");
Chatbase.FBUserMessage usrMsg = usrSet.NewMessage();
// ... Set fields as one would a regular FBUserMessage
usrSet.add(usrMsg);
var secondTask = client.Send(usrSet);
```

#### Tests
Please place tests in `Chatbase.Tests` project directory. To run tests, from the
`Chatbase.Tests` project directory enter the following in a command-prompt:

```
$ dotnet test
```

This requires the [dotnet Core CLI tools](https://www.microsoft.com/net/core) to be installed.
