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
