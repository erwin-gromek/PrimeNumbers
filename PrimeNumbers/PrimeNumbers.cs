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
        System.Timers.Timer timer = new System.Timers.Timer();
        int number = 1;
        int cycle = 0;
        bool stop = false;
        DateTime start = DateTime.Now;
        DateTime time = DateTime.Now;
        bool firstCycle = true;
        public void Start()
        {
            if(File.Exists("data.xml"))
            {
                XDocument xdoc = XDocument.Load("data.xml");
                number = int.Parse(xdoc.Descendants("value").Last().Value);
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
        public void Count(object source, ElapsedEventArgs e)
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
                try { number++; } 
                catch(Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
            cycle++;
            if (File.Exists("data.xml")) { 
                XDocument doc = XDocument.Load("data.xml");
                doc.Element("number").Add(new XElement("value", Prime.Last()),
                    new XElement("cycleNumber", cycle),
                    new XElement("cycleTime", DateTime.Now.Subtract(start).ToString()),
                    new XElement("countingTime", DateTime.Now.Subtract(time).ToString())
                    );
                doc.Save("data.xml");
            }
            else
            {
                XDocument doc = new XDocument(
                new XElement("number",
                    new XElement("value", Prime.Last()),
                    new XElement("cycleNumber", cycle),
                    new XElement("cycleTime", DateTime.Now.Subtract(start).ToString()),
                    new XElement("countingTime", DateTime.Now.Subtract(time).ToString())
                    ));
                doc.Save("data.xml");
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
                string[] strings = { xdoc.Descendants("value").Last().Value, xdoc.Descendants("cycleNumber").Last().Value, xdoc.Descendants("cycleTime").Last().Value, xdoc.Descendants("countingTime").Last().Value };
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