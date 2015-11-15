using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Messages.Third
{
    public class Message3 : BaseMessage
	{
		public string Message { get; set; }
		public double Number { get; set; }
		public bool IsTrue { get; set; }

		public override string ToString()
		{
			return $"{Message} {Number} {IsTrue}";
		}

	}
}
