using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousTraining
{
    class Program
    {
        //get cpu cores
        private static int CoresCount = Environment.ProcessorCount;

        static async Task Main(string[] args)
        {
            Console.Write("please input total count:");
            var input = Console.ReadLine();
            var isConvertToInt = int.TryParse(input, out var totalCount);
            if (isConvertToInt)
            {
                await CountOff(totalCount);
            }
            else
            {
                Console.WriteLine("please input correct number!");
            }
            
            Console.ReadLine();
        }

        private static async Task CountOff(int totalCount)
        {
            //generate list 
            var list = Enumerable.Range(0, totalCount).ToList();
            var taskList = new List<Task>();
            foreach (var i in list)
            {
                int k = i;

                taskList.Add(Task.Run(() =>
                {
                    Task.Delay(TimeSpan.FromSeconds(1));

                    if (k == 0)
                    {
                        Console.WriteLine($"Let us start, thread id is:{Thread.CurrentThread.ManagedThreadId:00}");
                    }
                    else if (k == list.Count() - 1)
                    {
                        Console.WriteLine($"and i am the last, thread id is:{Thread.CurrentThread.ManagedThreadId:00}");
                    }
                    else
                    {
                        Console.WriteLine($"Report after {k}, thread id is:{Thread.CurrentThread.ManagedThreadId:00}");
                    }
                }));

                //control max thread number
                if (taskList.Count > CoresCount)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => t.Status != TaskStatus.RanToCompletion).ToList();
                }
            }

            await Task.CompletedTask;
        }
    }
}