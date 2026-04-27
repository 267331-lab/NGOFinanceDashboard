using System;
using System.Threading;

class Program
{
    static void Main()
    {
        List<Thread> ThreadPool = new();
        int i = 1;
        while (i<20)
        {
          Thread NewThread = new(PrintSMTH);

          ThreadPool.Add(NewThread);
          i++;
        }
        
        ThreadPool.ForEach(T => Console.WriteLine(T.GetHashCode()));
        }

        public static void PrintSMTH()
        {
            Console.WriteLine("Static thread procedure. Data='{0}'");
        }
q
}