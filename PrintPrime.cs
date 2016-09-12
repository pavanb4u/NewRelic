using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HighestPrimInAMinute
{
    class Program
    {
        private static long primeNumber;
        static void Main(string[] args)
        {
            try
            {
                Thread oThread = new Thread(new ThreadStart(findPrimeNumbers));

                string startTime = DateTime.Now.ToString();

                oThread.Start();
                //start clock to show time each second
                Timer timer = new Timer(printStatus, "Some state", TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
                
                //wait for one minute for primeNumber generation
                Thread.Sleep(60000);

                oThread.Abort();

                oThread.Join();

                timer.Change(-1, -1); // Stop the timer from running.

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
        private static void printStatus(object state)
        {
            //Console.Write("\rTimer: {0}   ", DateTime.Now);
            Console.WriteLine(primeNumber);
        }

        /// <summary>
        /// This method finds the next valid prime number in a loop starting from 3
        /// </summary>
        public static void findPrimeNumbers()
        {
            Console.WriteLine();
            //iterate all odd numbers and check if it is a prime number
            for (long i = 3; i < long.MaxValue; i += 2)
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

    }
}
