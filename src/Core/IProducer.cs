﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public interface IProducer
    {
		void Produce<T>(T message);
		void Produce(object message, Type type);
    }
}
