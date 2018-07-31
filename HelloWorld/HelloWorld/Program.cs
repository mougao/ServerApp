using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using XY.MessageEntity;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //CMD_LG_CTL_REGIST mss = new CMD_LG_CTL_REGIST();
            //mss.account = "rrrrr";

            //MemoryStream ms = MessageTransformation.Serialize<CMD_LG_CTL_REGIST>(mss);

            //ms.Position = 0;
            //CMD_LG_CTL_REGIST mss2 = MessageTransformation.Deserialize<CMD_LG_CTL_REGIST>(ms);


            //Console.WriteLine("{0}", mss2.ToString());


            BlockingCollection<int> bc = new BlockingCollection<int>();


            // Spin up a Task to populate the BlockingCollection 
            Task t1 = Task.Factory.StartNew(() =>
            {
                for(int i=0;i<10;i++)
                {
                    bc.Add(i);
                }
                
            });

            Task t3 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                for (int i = 0; i < 10; i++)
                {
                    bc.Add(i+1);
                }

            });

            // Spin up a Task to consume the BlockingCollection
            Task t2 = Task.Factory.StartNew(() =>
            {
                try
                {
                    // Consume bc
                    while (true) Console.WriteLine(bc.Take());
                }
                catch (InvalidOperationException)
                {
                    // IOE means that Take() was called on a completed collection
                    Console.WriteLine("That's All!");
                }
            });



            Task.WaitAll(t1, t2,t3);
















            Console.Read();
        }
    }
}
