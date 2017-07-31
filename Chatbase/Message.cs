using System;
using System.Runtime.Serialization;

namespace Chatbase
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string api_key;

        [DataMember]
		public string type;

        [DataMember]
		public string user_id;

        [DataMember]
		public string platform;

        [DataMember]
		public string message;

        [DataMember]
		public string intent;

        [DataMember]
		public string version;

        [DataMember]
		public bool feedback;

        [DataMember]
		public bool not_handled;

        [DataMember]
		public double time_stamp;

        public static String UserMessage
        {
            get { return "user"; }
        }
        public static String AgentMessage
        {
            get { return "agent"; }
        }

        public static double CurrentUnixMilliseconds()
        {
            return Math.Truncate(DateTime.UtcNow.Subtract(
                new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        public bool RequiredFieldsSet()
        {
            return !(
                String.IsNullOrEmpty(api_key)
                || String.IsNullOrEmpty(type)
                || String.IsNullOrEmpty(user_id)
                || String.IsNullOrEmpty(platform)
            );
        }

        public Message()
        {
            time_stamp = Message.CurrentUnixMilliseconds();
            type = Message.UserMessage;
            not_handled = false;
            feedback = false;
        }

        public Message(string key)
        {
            time_stamp = Message.CurrentUnixMilliseconds();
            type = Message.UserMessage;
            not_handled = false;
            feedback = false;
            api_key = key;
        }
    }
}
