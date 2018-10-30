using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab15
{
    class Program
    {


       

        static void Main(string[] args)
        {

            #region Task 1,2
            //foreach (Process process in Process.GetProcesses())
            //{
            //    Console.WriteLine($"ID: {process.Id} Name: {process.ProcessName} ");
            //}

            //Console.WriteLine("\nИнформация о текущем домене приложения");
            //AppDomain domain = AppDomain.CurrentDomain;
            //Console.WriteLine("Name: {0}", domain.FriendlyName);
            //Console.WriteLine("Base Directory: {0}", domain.BaseDirectory);

            //Console.WriteLine("\nСборки в текущем домене:");

            //Assembly[] assemblies = domain.GetAssemblies();
            //foreach (Assembly asm in assemblies)
            //    Console.WriteLine(asm.GetName().Name);


            //AppDomain secondaryDomain = AppDomain.CreateDomain("Secondary domain");

            //secondaryDomain.AssemblyLoad += Domain_AssemblyLoad;
            //secondaryDomain.DomainUnload += SecondaryDomain_DomainUnload;


            //Console.WriteLine("\nДомен: {0}", secondaryDomain.FriendlyName);

            //secondaryDomain.Load(new AssemblyName("lab15"));
            //assemblies = secondaryDomain.GetAssemblies();

            //Console.WriteLine("\nСборки в SecondaryDomain");
            //foreach (Assembly asm in assemblies)
            //    Console.WriteLine(asm.GetName().Name);

            //AppDomain.Unload(secondaryDomain);
            //Console.Read();
            #endregion

            #region Task 3
            //Thread thread = new Thread(new ThreadStart(PrimeNumbers));
            //thread.Start();
            //thread.Suspend();
            //Console.WriteLine($"Status: {thread.IsAlive},  Name: {thread.Name}, Priority: {thread.Priority}, ID: {thread.ManagedThreadId}");
            //thread.Resume();
            #endregion

            #region Task 4

            //Console.WriteLine("Введите число ");
            //int n = int.Parse(Console.ReadLine());

            //Thread evenThread = new Thread(new ParameterizedThreadStart(EvenNumbers));
            //Thread oddThread = new Thread(new ParameterizedThreadStart(OddNumbers));

            //oddThread.Priority = ThreadPriority.Highest;

            //evenThread.Start(n); // для задания 4.b.II перенести этот поток под oddThread 
            //oddThread.Start(n);


            //Console.WriteLine("\n");


            #endregion

            #region Task 5
            TimerCallback callback = new TimerCallback(Hate);
            Timer timer = new Timer(callback, null, 5000, 2000);

            Console.ReadLine();
            #endregion

        }

        private static void SecondaryDomain_DomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Домен выгружен из процесса");
        }

        private static void Domain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Сборка загружена");
        }
        

        public static void PrimeNumbers()
        {
            Console.WriteLine("Введите число до которого будут найдены простые числа");
            int n = int.Parse(Console.ReadLine());
            

            for(int i = 0; i <= n; i++)
            {
                if (IsPrime(i))
                {
                    
                    Console.WriteLine(i + "\n");
                    
                }
            }
        }

       
        private static bool IsPrime(int N)
        {
                
                for (int i = 2; i <= (int)(N / 2); i++)
                {
                    if (N % i == 0)
                        return false;
                }
                return true;
        }

        static Mutex mutexObj = new Mutex();

        public static void OddNumbers(object n)
        {
            

            for (int i = 0; i <= (int)n; i++)
            {
                
                if (i % 2 != 0)
                {
                    mutexObj.WaitOne(); // закомментить для задания 4.а
                    Console.WriteLine(i);
                    Thread.Sleep(100);
                    mutexObj.ReleaseMutex();
                    
                }
               
            }
        }

        public static void EvenNumbers(object n)
        {            

            for (int i = 0; i <= (int)n; i++)
            {
               
                if (i % 2 == 0)
                {
                    mutexObj.WaitOne();
                    Console.WriteLine(i);
                    Thread.Sleep(100);
                    mutexObj.ReleaseMutex();
                }
              
            }

        }

        public static void Hate(object obj)
        {
           Console.WriteLine("Ненавижу эту лабу");
            
        }

    }

}
