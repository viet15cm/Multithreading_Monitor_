using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor_Multithreading
{
    class Program
    {
        static Singleton singleton;
        
        static void Main(string[] args)
        {
         

            singleton = Singleton.Instance();
            singleton._Laco = 0;
            Random rd = new Random();

            Task<string> task = new Task<string>((x) => {

                var slt = (Singleton)x;
                var i = 0;
                while (i < 10)
                {
                 
                    try
                    {
                        Monitor.Enter(x);
                        if (slt._Laco == 0)
                        {
                            slt._Number_1 = rd.Next(1, 10);
                            Console.WriteLine("Task 1 random = " + slt._Number_1);
                            Monitor.PulseAll(x);
                            i++;
                            slt._Laco = 1;
                        }
                        else
                        {
                            Monitor.Wait(x);
                        }
                    }
                    finally
                    {
                        
                        Monitor.Exit(x);
                       
                        
                    }
                }
              
                return "Tack 1 hoang thanh xong";
            
            }, singleton);

            Task<string> task_2 = new Task<string>((x) => {

                var slt = (Singleton)x;
                var i = 0;
                while (i < 10)
                {
                    
                    try
                    {
                        Monitor.Enter(x);
                        if (slt._Laco == 1)
                        {
                            slt._Number_2 = rd.Next(1, 10);
                            Console.WriteLine("Task 2 random = " + slt._Number_2);
                            i++;
                            Monitor.PulseAll(x);
                            slt._Laco = 2;
                        }
                        else
                        {
                            Monitor.Wait(x);
                        }
                    }
                    finally
                    {

                        Monitor.Exit(x);
                        
                    }
                
                }

                return "Tack 2 hoang thanh xong";

            }, singleton);

            Task<string> task_3 = new Task<string>((x) => {

                var slt = (Singleton)x;
                var i = 0;
                while (i < 10)
                {
                   
                    try
                    {
                        Monitor.Enter(x);
                        if (slt._Laco == 2)
                        {
                            slt._Sum = slt._Number_1 + slt._Number_2;
                            Console.WriteLine("Sum Task 3 = " + slt._Sum);
                            slt._Laco = 0;
                            i++;
                            Monitor.PulseAll(x);
                        }
                        else
                        {
                            Monitor.Wait(x);
                        }
                    }
                    finally
                    {

                        Monitor.Exit(x);
                    }
                }

                return "Tack 3 hoang thanh xong";

            }, singleton);
            
            task.Start();
            task_2.Start();
            task_3.Start();

            Console.WriteLine(task.Result);
            Console.WriteLine(task_2.Result);
            Console.WriteLine(task_3.Result);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
            
        }
    }

    class Singleton
    {
        public  int _Laco { get; set; }
        public  int _Number_1 { get; set; }
        public  int _Number_2 { get; set; }

        public int _Sum { get; set; }

        public static Singleton _instance;
        protected Singleton() { }

        public static Singleton Instance()
        {
            
            _instance = _instance ?? new Singleton();

            return _instance;
        }

        public int init { get; set; }
    }

    
}
