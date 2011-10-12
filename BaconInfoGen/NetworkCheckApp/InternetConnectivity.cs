// Author: Ozcan ILIKHAN
// Source address: http://www.codeproject.com/Tips/147662/Testing-Internet-Connectivity
// Date accessed: 12 October 2011

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace NetworkCheckApp
{
	/// <summary>
	/// Some of the functions of our applications may require a run-time test of internet connectivity. Once internet connectivity is detected, the functions that require internet access may temporarily be disabled and/or the user can be notified via an alert message. Otherwise, the application may result in error during operation or it may cause annoying problems for the user.
	/// </summary>
	public class ConnectStatus
	{
		#region Default arguments for checking

		/// <summary>
		/// Default enum <seealso cref="Method"/> to use to determine how to test connectivity. Value: Method.WIN_INET
		/// </summary>
		private const Method DefaultMethod = Method.WinInet;

		/// <summary>
		/// Default host to use for <see cref=" System.Net.WebRequest"/>. Value: "www.google.com"
		/// </summary>
		private const string DefaultHost = "http://www.google.com";

		/// <summary>
		/// Default IPv4 address to use for ping. Value: "74.125.237.17" (Host: "www.google.com")
		/// </summary>
		private const string DefaultIpAddress = "74.125.237.17";

		/// <summary>
		/// Default port for HTTP traffic. Value: 80
		/// </summary>
		private const int DefaultPort = 80;

		/// <summary>
		/// The default timeout to use for ping. Value: 1000
		/// </summary>
		private const int DefaultTimeout = 1000;

		#endregion

		#region Method enum

		/// <summary>
		/// Specific methods to test internet connectivity.
		/// </summary>
		public enum Method
		{
			WebRequest = 0x1,
			TcpSocket = 0x2,
			Ping = 0x3,
			DnsLookup = 0x4,
			WinInet = 0x5
		}

		#endregion

		public static bool Check(Method method, string address, int port)
		{
			string s = address;
			if (method == Method.WebRequest)
			{
				var host = new Uri(address);
				s = host.AbsoluteUri;
			}
			return Check(method, s, s, port);
		}

		/// <summary>
		/// Checks whether there is an internet connection. Defaults to calling the <see cref="IsConnectionAvailable"/> method.
		/// </summary>
		/// <param name="method"></param>
		/// <param name="host"></param>
		/// <param name="ipv4"></param>
		/// <param name="port"></param>
		/// <param name="timeout"></param>
		/// <returns>True if connected to the internet; false otherwise.</returns>
		public static bool Check(
			Method method = DefaultMethod,
			string host = DefaultHost,
			string ipv4 = DefaultIpAddress,
			int port = DefaultPort,
			int timeout = DefaultTimeout)
		{ 
			switch (method)
			{
				case Method.WebRequest:
					return WebRequestTest(host);
				case Method.TcpSocket:
					return TcpSocketTest(host, port);
				case Method.Ping:
					return PingTest(ipv4, timeout);
				case Method.DnsLookup:
					return DnsTest(host);
				case Method.WinInet:
					return IsConnectionAvailable();

				default:
					string message = string.Format(
						"The method for {0} [CODE={1,0:x2}] is not implemented.",
						method, (int) method);
					throw new NotImplementedException(message);
			}
		}

		/// <summary>
		/// We may send a web request to a website which assumed to be online always, for example google.com. If we can get a response, then obviously the device that runs our application is connected to the internet.
		/// </summary>
		/// <param name="address">The address to try to connect to. Defaults to Google.</param>
		/// <returns>Whether we are connected to the internet.</returns>
		public static bool WebRequestTest(string address = DefaultHost)
		{
			{
				var url = new Uri(address);

				WebRequest webReq = WebRequest.Create(url);
				try
				{
					WebResponse resp = webReq.GetResponse();
					resp.Close();
					return true;
				}

				catch
				{
					return false;
				}
			}
		}

		/// <summary>
		/// There can be some delay in response of web request therefore this method may not be fast enough for some applications. A better way is to check whether port 80, default port for HTTP traffic, of an always online website.
		/// </summary>
		/// <param name="address">The address to try to connect to. Defaults to <see cref="DefaultHost"/>.</param>
		/// <param name="port">The port to use. Defaults to <see cref="DefaultPort"/></param>
		/// <returns></returns>
		public static bool TcpSocketTest(string address = DefaultHost, int port = DefaultPort)
		{
			try
			{
				var client = new TcpClient(new Uri(address).Host, port);
				client.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// <see cref="Ping"/> an IP address.
		/// </summary>
		/// 
		/// <remarks>
		/// There can be some delay in response of web request, therefore this method may not be fast enough for some applications. A better way is to check whether port 80, default port for http traffic, of an always online website.
		/// 
		/// You cannot use this method in .NET Compact Framework because there is no <see cref="System.Net.NetworkInformation"/> namespace that comes with Compact Framework. However, you can use Smart Device Framework ("http://www.opennetcf.com", Community Edition is free for download) provided by OpenNETCF. This framework comes with many other useful tools that .NET Compact Framework does not contain.
		/// 
		/// Notice that I used Google’s IP address 208.69.34.231. We could use Google’s web address www.google.com:
		/// 
		/// <code>
		///	System.Net.NetworkInformation.PingReply pingStatus = ping.Send("www.google.com",1000);
		/// </code>
		/// 
		/// However, that will require DNS lookup which causes extra delay.
		/// </remarks>
		/// <param name="address"></param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		public static bool PingTest(string address = DefaultIpAddress, int timeout = DefaultTimeout)
		{
			var ping = new Ping();
			DateTime start = DateTime.Now;
			PingReply pingStatus = ping.Send(address, timeout);
			//PingReply pingStatus = ping.Send(IPAddress.Parse(address), timeout);
			var end = (long) (DateTime.Now - start).TotalMilliseconds;
			Console.WriteLine("Inside Ping method, took: " + end);
			if (pingStatus != null && pingStatus.Status == IPStatus.Success)
			{
				Console.WriteLine("Still Alive");
				return true;
			}
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static bool DnsTest(string url = DefaultHost)
		{
			try
			{
				//var ipHe =
				Dns.GetHostEntry(new Uri(url).DnsSafeHost);
				return true;
			}
			catch
			{
				return false;
			}
		}

		// WinINet API call
		[DllImport("wininet.dll")]
		private static extern bool InternetGetConnectedState(out int connDescription, int reservedValue);

		/// <summary>
		/// Check if a connection to the Internet can be established. Much faster than the other methods, as it does not require a DNS lookup.
		/// </summary>
		/// <returns></returns>
		public static bool IsConnectionAvailable()
		{
			int flags;
			return InternetGetConnectedState(out flags, 0);
		}
	}
}