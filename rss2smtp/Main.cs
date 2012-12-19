using System;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Configuration;

namespace rss2smtp
{
	class MainClass
	{
		private static Rss2Smtp _transmitter;
		
		public static void Main(string[] args)
		{
			RssSmtpSection conf = (RssSmtpSection) ConfigurationManager.GetSection("rssSmtpGroup/rssSmtp");
			
			int interval = 10 * 1000;
			_transmitter = new Rss2Smtp(conf);
			
			Timer rssTimer = new Timer();
			rssTimer.Elapsed += new ElapsedEventHandler(UpdateEvent);
			rssTimer.Interval = interval;
			rssTimer.Start();
			
			string command = "";
			Console.Write(">_");
			while(!(command = Console.ReadLine()).Equals("exit")) {
				if(command.Equals("update")) {
					_transmitter.Update();
				}
				Console.Write(">_");
			}
			
			rssTimer.Stop();
		}

		public static void UpdateEvent(object source, ElapsedEventArgs e)
		{
			_transmitter.Update();
		}
	}
}
