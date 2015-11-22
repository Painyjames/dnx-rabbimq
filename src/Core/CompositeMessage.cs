using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public abstract class CompositeMessage
    {
		public IList<BaseMessage> Messages { get; set; }

    }
}
