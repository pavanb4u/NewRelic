using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers  ;

namespace HighestPrimInAMinute
{
    class PrintPrime
    {
        //primeNumber will be accessed by Multiple Threads
        private static long primeNumber;
        private static volatile bool stopCount;

        static void Main(string[] args)
        {
            try
            {
                Thread oThread = new Thread(new ThreadStart(findPrimeNumbers));

                DateTime startTime = DateTime.Now;
                //string startTime = DateTime.Now.ToString();                
                oThread.Start();
                DateTime endTime = startTime.AddSeconds(60);
                Console.WriteLine("Program started at {0} ", startTime.ToString());

                //start clock to show time each second
               //System.Timers.Timer timer = new System.Timers.Timer (printStatus, "Current Status", TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
                System.Timers.Timer logTimer = new System.Timers.Timer(10000);
                // Hook up the Elapsed event for the timer. 
                logTimer.Elapsed += OnTimedEvent;
                logTimer.AutoReset = true;
                logTimer.Enabled = true;

                //wait for one minute for primeNumber generation
                Thread.Sleep(endTime.Subtract(DateTime.Now));
                
                stopCount = true;
                
                oThread.Join();

                logTimer.Stop();
                logTimer.Dispose();

                Console.WriteLine("\nThread started at {0} and stopped at : {1}", startTime, DateTime.Now);
                Console.WriteLine("\nMax prime number calculated: {0} ", primeNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }
            Console.ReadLine();
        }

        // prints current time
        private static void OnTimedEvent(Object state, ElapsedEventArgs args)
        {            
            Console.WriteLine("\r Current Prime number at {0} is {1}", DateTime.Now , primeNumber);
        }

        /// <summary>
        /// This method finds the next valid prime number in a loop starting from 3
        /// </summary>
        public static void findPrimeNumbers()
        {
            try
            {

                //iterate all odd numbers and check if it is a prime number
                for (long i = 3; i < long.MaxValue && !stopCount; i += 2)
                {
                    bool isPrime = true;
                    for (long j = 3; (j * j) <= i; j += 2)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                        primeNumber = i;
                }

            }
            catch (Exception ex)
            {
                //log exception
                Console.WriteLine("Failed with Exception" + ex.Message);
            }
        }
    }
}
