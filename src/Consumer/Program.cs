using System;
using System.Reflection;
using Components;

namespace Consumer
{
    public class Program
    {
        public void Main(string[] args)
        {
			try
			{
				var modulesToLoad = args.Length == 0? "All" : string.Join("\n", args);
                Console.WriteLine($"Loading Components \n{modulesToLoad}");
				new Components.Module().Start<Program>(args);
				Console.WriteLine($"Components Loaded");
				Console.ReadLine();
			}
			catch (Exception ex)
			{
				throw;
			}	
		}
	}
}
