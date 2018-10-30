using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab15
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(Process process in Process.GetProcesses())
            {
                Console.WriteLine($"ID: {process.Id} Name: {process.ProcessName} ");
            }

            Console.WriteLine("\nИнформация о текущем домене приложения");
            AppDomain domain = AppDomain.CurrentDomain;
            Console.WriteLine("Name: {0}", domain.FriendlyName);
            Console.WriteLine("Base Directory: {0}", domain.BaseDirectory);

            Console.WriteLine("\nСборки в текущем домене:");

            Assembly[] assemblies = domain.GetAssemblies();
            foreach (Assembly asm in assemblies)
                Console.WriteLine(asm.GetName().Name);


            AppDomain secondaryDomain = AppDomain.CreateDomain("Secondary domain");
            
            secondaryDomain.AssemblyLoad += Domain_AssemblyLoad;
            secondaryDomain.DomainUnload += SecondaryDomain_DomainUnload;


            Console.WriteLine("\nДомен: {0}", secondaryDomain.FriendlyName);

            secondaryDomain.Load(new AssemblyName("lab15"));
            assemblies = secondaryDomain.GetAssemblies();

            Console.WriteLine("\nСборки в SecondaryDomain");
            foreach (Assembly asm in assemblies)
                Console.WriteLine(asm.GetName().Name);
           
            AppDomain.Unload(secondaryDomain);
            Console.Read();
        }

        private static void SecondaryDomain_DomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Домен выгружен из процесса");
        }

        private static void Domain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Сборка загружена");
        }


    }
    
}
