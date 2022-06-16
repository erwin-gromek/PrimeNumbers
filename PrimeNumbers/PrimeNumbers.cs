using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using System.Xml.Linq;

namespace PrimeNumbers
{
    internal class PrimeNumbers
    {
        List<int> Prime = new List<int>();
        XmlDocument doc = new XmlDocument();
        System.Timers.Timer timer = new System.Timers.Timer();
        int number = 1;
        int cycle = 0;
        bool stop = false;
        DateTime start = DateTime.Now;
        DateTime time = DateTime.Now;
        bool firstCycle = true;
        public void Start()
        {
            try
            {
                XDocument xdoc = XDocument.Load("data.xml");
                number = int.Parse(xdoc.Descendants("value").First().Value);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            stop = false;
            timer.Interval = 100;
            timer.Start();
            cycle = 0;
            if (firstCycle)
            {
                timer.Elapsed += new ElapsedEventHandler(Count);
                firstCycle = false;
            }
        }
        public void Count(object source, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            start = DateTime.Now;
            while (DateTime.Now.Subtract(start).Seconds < 300 && !stop)
            {
                time = DateTime.Now;
                int counter = 0;
                for (int divider = 1; divider <= number; divider++)
                {
                    if (number % divider == 0)
                    {
                        counter++;
                    }
                    if (counter > 2) break;
                }
                if (counter == 2)
                {
                    Prime.Add(number);
                }
                /*if (stop)
                {
                    break;
                }*/
                try { number++; } 
                catch(Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
            cycle++;
            doc.LoadXml($"<number><value>{Prime.Last()}</value><cycleNumber>{cycle}</cycleNumber><cycleTime>{DateTime.Now.Subtract(start)}</cycleTime><countingTime>{DateTime.Now.Subtract(time)}</countingTime></number>");
            try { doc.Save("data.xml"); }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            if(!stop)
                timer.Start();
            timer.Interval = 60000;
        }
        public void Stop()
        {
            timer.Stop();
            stop = true;
        }
        public string[] GetValues()
        {
            try
            {
                XDocument xdoc = XDocument.Load("data.xml");
                string[] strings = { xdoc.Descendants("value").First().Value, xdoc.Descendants("cycleNumber").First().Value, xdoc.Descendants("cycleTime").First().Value, xdoc.Descendants("countingTime").First().Value };
                return strings;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return null;
        }
    }
}