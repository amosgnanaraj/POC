using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikuliSharp;

namespace SikuliSharp_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateSikuliEnvironment();
            UseSikuli();
        }

        static void CreateSikuliEnvironment()
        {
            string value;
            bool toDelete = false;
            string pathTo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sikuli");

            // Check whether the environment variable exists. D:\Sikuli
            value = Environment.GetEnvironmentVariable("SIKULI_HOME");
            // If necessary, create it.
            if (value == null)
            {
                Environment.SetEnvironmentVariable("SIKULI_HOME", pathTo);
                //toDelete = true;

                // Now retrieve it.
                value = Environment.GetEnvironmentVariable("SIKULI_HOME");
            }
            // Display the value.
            Console.WriteLine($"Test1: {value}\n");

            // Confirm that the value can only be retrieved from the process
            // environment block if running on a Windows system.
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Console.WriteLine("Attempting to retrieve Test1 from:");
                foreach (EnvironmentVariableTarget enumValue in
                                  Enum.GetValues(typeof(EnvironmentVariableTarget)))
                {
                    value = Environment.GetEnvironmentVariable("SIKULI_HOME", enumValue);
                    Console.WriteLine($"   {enumValue}: {(value != null ? "found" : "not found")}");
                }
                Console.WriteLine();
            }

            // If we've created it, now delete it.
            if (toDelete)
            {
                Environment.SetEnvironmentVariable("Test1", null);
                // Confirm the deletion.
                if (Environment.GetEnvironmentVariable("Test1") == null)
                    Console.WriteLine("Test1 has been deleted.");
            }
        }

        static void UseSikuli()
        {
            using(var session = Sikuli.CreateSession())
            {
                var pattern = Patterns.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Capture.PNG"));
                bool exists = session.Exists(pattern);
                string result = exists ? "Pattern exists" : "Pattern not found";
                Console.WriteLine(result);
                if (exists)
                {
                    session.Hover(pattern);
                }
                Console.ReadKey();
            }
        }
    }
}
