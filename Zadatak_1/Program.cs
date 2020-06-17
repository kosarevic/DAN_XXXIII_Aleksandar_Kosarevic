using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    /// <summary>
    /// Aplication simulates workload of 4 generated threads, and prints results on console.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Stopwatch object made for purpose of measuring time needed to complete first and second thread.
            Stopwatch stopWatch = new Stopwatch();
            //Four threades premade before being used throughout application.
            Thread t1 = new Thread(new ThreadStart(() => Method()));
            Thread t2 = new Thread(new ThreadStart(() => Method()));
            Thread t3 = new Thread(new ThreadStart(() => Method()));
            Thread t4 = new Thread(new ThreadStart(() => Method()));
            //Loop made for assigning names to threads in specific order and manner.
            for (int i = 1; i <= 2; i++)
            {
                if (i == 1)
                {
                    t1 = new Thread(new ThreadStart(() => Method()))
                    {
                        Name = string.Format("THREAD_{0}", i)
                    };
                    t2 = new Thread(new ThreadStart(() => Method()))
                    {
                        Name = string.Format("THREAD_{0}{1}", i + 1, i + 1)
                    };
                    Console.WriteLine(t1.Name + " has been created.");
                    Console.WriteLine(t2.Name + " has been created.");
                }
                else
                {
                    t3 = new Thread(new ThreadStart(() => Method()))
                    {
                        Name = string.Format("THREAD_{0}", i + 1)
                    };
                    t4 = new Thread(new ThreadStart(() => Method()))
                    {
                        Name = string.Format("THREAD_{0}{1}", i + 2, i + 2)
                    };
                    Console.WriteLine(t3.Name + " has been created.");
                    Console.WriteLine(t4.Name + " has been created.");

                }
            }
            //Stopwatch starts here.
            stopWatch.Start();
            //First and second thread get initiated first.
            t1.Start();
            t2.Start();
            //Main thread will not continue until first two threads finish their tasks.
            t1.Join();
            t2.Join();
            //Stopwatch ends measurment here, giving time past during execution of first and second thread.
            stopWatch.Stop();
            Console.WriteLine("First and second thread completed tasks in: " + stopWatch.Elapsed.TotalMilliseconds + " ms");
            //Last two threads procede with execution from here.
            t3.Start();
            t4.Start();

            Console.ReadLine();
        }

        /// <summary>
        /// Method made for puropose of assinging duties to each thread respectevly, when thread gets activated.
        /// </summary>
        static void Method()
        {
            //Workload assigned to first thread.
            if (Thread.CurrentThread.Name == "THREAD_1")
            {
                int[,] array = new int[100, 100];

                for (int i = 0; i < 100; i++)
                {
                    array[i, i] = 1;
                }

                using (var sw = new StreamWriter("..//..//Files/FileByThread_1.txt"))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 100; j++)
                        {
                            sw.Write(array[i, j]);
                        }
                        sw.Write("\n");
                    }

                    sw.Flush();
                    sw.Close();
                }
            }
            //Workload assigned to second thread.
            if (Thread.CurrentThread.Name == "THREAD_22")
            {
                int[] array = new int[1000];
                Random r = new Random();
                int num = 0;

                for (int i = 0; i < 1000; i++)
                {
                    num = r.Next(0, 10000);
                    if (num % 2 != 0)
                    {
                        array[i] = num;
                    }
                    else
                    {
                        i--;
                    }
                }

                using (var sw = new StreamWriter("..//..//Files/FileByThread_22.txt"))
                {
                    for (int i = 0; i < 1000; i++)
                    {

                        sw.Write(array[i]);
                        sw.Write("\n");
                    }

                    sw.Flush();
                    sw.Close();
                }
            }
            //Workload assinged to third thread.
            if (Thread.CurrentThread.Name == "THREAD_3")
            {
                using (StreamReader sr = File.OpenText("..//..//Files/FileByThread_1.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            //Workload assinged to forth thread.
            if (Thread.CurrentThread.Name == "THREAD_44")
            {
                int[] array = new int[1000];

                using (StreamReader sr = File.OpenText("..//..//Files/FileByThread_22.txt"))
                {
                    int count = 0;
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        array[count] = int.Parse(s);
                        count++;
                    }
                }
                int sum = 0;

                for (int i = 0; i < array.Length; i++)
                {
                    sum += array[i];
                }

                Console.WriteLine("Sum of all generated numbers: " + sum);
            }
        }
    }
}
