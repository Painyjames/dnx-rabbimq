using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components
{
    public class Module
    {
		public void Start<T>(string[] args)
		{
			new CoreModule().Start<T>(args);
        }
    }
}
