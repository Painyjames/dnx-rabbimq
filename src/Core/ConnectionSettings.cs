using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class ConnectionSettings
	{
		public string HostName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string VirtualHost { get; set; }
		public ushort RequestedHeartbeat { get; set; }
	}
}
