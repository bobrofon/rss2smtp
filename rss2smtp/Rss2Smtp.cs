using System;
using System.Xml;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Text;

namespace rss2smtp
{
	class Rss2Smtp
	{
		private DateTime _lastUpdate = new DateTime();
		private object _locker = new Object();
		private MailMessage _mail = new MailMessage();
		private SmtpClient _smtpServer = new SmtpClient();
		private XmlDocument _doc = new XmlDocument();
		private string _rssPath = "";
		
		public Rss2Smtp(RssSmtpSection config)
		{
			_mail.From = new MailAddress(config.From);
			_mail.To.Add(config.To);
			_mail.Subject = config.Subject;
            
			_smtpServer.Host = config.Host;
			_smtpServer.Port = config.Port;
			_smtpServer.Credentials = new System.Net.NetworkCredential(config.User, config.Pass);
			_smtpServer.EnableSsl = config.EnableSsl;
			ServicePointManager.ServerCertificateValidationCallback = 
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
			{
				return true; };
			_rssPath = config.Rss;
		}

		public void Update()
		{
			lock(_locker) {
				StringBuilder tmpBody = new StringBuilder();
				DateTime currentUpdate = _lastUpdate;
				_doc.Load(_rssPath);
				XmlNodeList itemsList = _doc.DocumentElement.SelectNodes("channel/item");
				foreach(XmlNode item in itemsList) {
					foreach(XmlNode item_p in item.ChildNodes) {
						if(item_p.Name == "pubDate") {
							DateTime dt = DateTime.Parse(item_p.InnerText);
							if(dt <= _lastUpdate) {
								_doc.DocumentElement.GetElementsByTagName("channel").Item(0).RemoveChild(item);
							} else {
								foreach(XmlNode xn in item.ChildNodes) {
									tmpBody.Append('\n');
									tmpBody.Append(xn.InnerText);
								}
								currentUpdate = currentUpdate < dt ? dt : currentUpdate;
							}
						}
					}
				}
				_lastUpdate = currentUpdate;
				if(tmpBody.Length > 0) {
					_mail.Body = tmpBody.ToString();
					_smtpServer.Send(_mail);
				}
			}
		}	
	}
}

