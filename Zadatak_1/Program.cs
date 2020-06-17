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
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            for (int i = 1; i <= 2; i++)
            {
                if (i == 1)
                {
                    Thread t1 = new Thread(new ThreadStart(() => Method()))
                    {
                        Name = string.Format("THREAD_{0}", i)
                    };
                    Thread t2 = new Thread(new ThreadStart(() => Method()))
                    {
                        Name = string.Format("THREAD_{0}{1}", i + 1, i + 1)
                    };
                    Console.WriteLine(t1.Name + " has been created.");
                    Console.WriteLine(t2.Name + " has been created.");
                    stopWatch.Start();
                    t1.Start();
                    t2.Start();
                    t1.Join();
                    t2.Join();
                    stopWatch.Stop();
                    Console.WriteLine("First and second thread completed tasks in: " + stopWatch.Elapsed.TotalMilliseconds + " ms");
                }
                else
                {
                    Thread t3 = new Thread(new ThreadStart(() => Method()))
                    {
                        Name = string.Format("THREAD_{0}", i + 1)
                    };
                    Thread t4 = new Thread(new ThreadStart(() => Method()))
                    {
                        Name = string.Format("THREAD_{0}{1}", i + 2, i + 2)
                    };
                    Console.WriteLine(t3.Name + " has been created.");
                    Console.WriteLine(t4.Name + " has been created.");
                    t3.Start();
                    t4.Start();
                }
            }
            Console.ReadLine();
        }


        static void Method()
        {
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

                Console.WriteLine(sum);
            }
        }
    }
}
