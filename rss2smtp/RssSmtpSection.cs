using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Xml;

namespace rss2smtp
{
	public class RssSmtpSection : ConfigurationSection
	{
		[ConfigurationProperty("From")]
		public string From {
			get {
				return (string)this["From"];
			}
			set {
				this["From"] = value;
			}
		}
		[ConfigurationProperty("To")]
		public string To {
			get {
				return (string)this["To"];
			}
			set {
				this["To"] = value;
			}
		}
		[ConfigurationProperty("Subject")]
		public string Subject {
			get {
				return (string)this["Subject"];
			}
			set {
				this["Subject"] = value;
			}
		}
		[ConfigurationProperty("Host")]
		public string Host {
			get {
				return (string)this["Host"];
			}
			set {
				this["Host"] = value;
			}
		}
		[ConfigurationProperty("Port")]
		public int Port {
			get {
				return (int)this["Port"];
			}
			set {
				this["Port"] = value;
			}
		}
		[ConfigurationProperty("User")]
		public string User {
			get {
				return (string)this["User"];
			}
			set {
				this["User"] = value;
			}
		}
		[ConfigurationProperty("Pass")]
		public string Pass {
			get {
				return (string)this["Pass"];
			}
			set {
				this["Pass"] = value;
			}
		}
		[ConfigurationProperty("EnableSsl")]
		public bool EnableSsl {
			get {
				return (bool)this["EnableSsl"];
			}
			set {
				this["EnableSsl"] = value;
			}
		}
		[ConfigurationProperty("Rss")]
		public string Rss {
			get {
				return (string)this["Rss"];
			}
			set {
				this["Rss"] = value;
			}
		}
	}
}

