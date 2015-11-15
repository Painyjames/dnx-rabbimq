using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Messages.Second
{
    public class Message2 : BaseMessage
	{
		public string Message { get; set; }
		public double Number { get; set; }
	}
}
