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
				Console.WriteLine($"Loading Components \n{string.Join("\n", args)}");
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
