using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KeyAuth
{
	// Token: 0x0200000F RID: 15
	public class api
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000070B0 File Offset: 0x000052B0
		public api(string name, string ownerid, string secret, string version, string path = null)
		{
			if (ownerid.Length != 10 || secret.Length != 64)
			{
				Process.Start("https://youtube.com/watch?v=RfDTdiBq4_o");
				Process.Start("https://keyauth.cc/app/");
				Thread.Sleep(2000);
				api.error("Application not setup correctly. Please watch the YouTube video for setup.");
				Environment.Exit(0);
			}
			this.name = name;
			this.ownerid = ownerid;
			this.secret = secret;
			this.version = version;
			this.path = path;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007160 File Offset: 0x00005360
		public void init()
		{
			string text = encryption.iv_key();
			api.enckey = text + "-" + this.secret;
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "init";
			nameValueCollection["ver"] = this.version;
			nameValueCollection["hash"] = api.checksum(Process.GetCurrentProcess().MainModule.FileName);
			nameValueCollection["enckey"] = text;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			if (!string.IsNullOrEmpty(this.path))
			{
				nameValueCollection2.Add("token", File.ReadAllText(this.path));
				nameValueCollection2.Add("thash", api.TokenHash(this.path));
			}
			string text2 = api.req(nameValueCollection2);
			if (text2 == "KeyAuth_Invalid")
			{
				api.error("Application not found");
				Environment.Exit(0);
			}
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				if (response_structure.newSession)
				{
					Thread.Sleep(100);
				}
				api.sessionid = response_structure.sessionid;
				this.initialized = true;
				return;
			}
			if (response_structure.message == "invalidver")
			{
				this.app_data.downloadLink = response_structure.download;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000072BC File Offset: 0x000054BC
		public static string TokenHash(string tokenPath)
		{
			string result;
			using (SHA256 sha = SHA256.Create())
			{
				using (FileStream fileStream = File.OpenRead(tokenPath))
				{
					result = BitConverter.ToString(sha.ComputeHash(fileStream)).Replace("-", string.Empty);
				}
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007328 File Offset: 0x00005528
		public void CheckInit()
		{
			if (!this.initialized)
			{
				api.error("You must run the function KeyAuthApp.init(); first");
				Environment.Exit(0);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007344 File Offset: 0x00005544
		public string expirydaysleft(string Type, int subscription)
		{
			this.CheckInit();
			DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
			d = d.AddSeconds((double)long.Parse(this.user_data.subscriptions[subscription].expiry)).ToLocalTime();
			TimeSpan timeSpan = d - DateTime.Now;
			string a = Type.ToLower();
			if (a == "months")
			{
				return Convert.ToString(timeSpan.Days / 30);
			}
			if (a == "days")
			{
				return Convert.ToString(timeSpan.Days);
			}
			if (!(a == "hours"))
			{
				return null;
			}
			return Convert.ToString(timeSpan.Hours);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007400 File Offset: 0x00005600
		public void register(string username, string pass, string key, string email = "")
		{
			this.CheckInit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "register";
			nameValueCollection["username"] = username;
			nameValueCollection["pass"] = pass;
			nameValueCollection["key"] = key;
			nameValueCollection["email"] = email;
			nameValueCollection["hwid"] = value;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000074D8 File Offset: 0x000056D8
		public void forgot(string username, string email)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "forgot";
			nameValueCollection["username"] = username;
			nameValueCollection["email"] = email;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(data);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007564 File Offset: 0x00005764
		public void login(string username, string pass)
		{
			this.CheckInit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "login";
			nameValueCollection["username"] = username;
			nameValueCollection["pass"] = pass;
			nameValueCollection["hwid"] = value;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007620 File Offset: 0x00005820
		public void logout()
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "logout";
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(data);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007694 File Offset: 0x00005894
		public void web_login()
		{
			this.CheckInit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			HttpListener httpListener;
			HttpListenerRequest request;
			HttpListenerResponse httpListenerResponse;
			for (;;)
			{
				httpListener = new HttpListener();
				string text = "handshake";
				text = "http://localhost:1337/" + text + "/";
				httpListener.Prefixes.Add(text);
				httpListener.Start();
				HttpListenerContext context = httpListener.GetContext();
				request = context.Request;
				httpListenerResponse = context.Response;
				httpListenerResponse.AddHeader("Access-Control-Allow-Methods", "GET, POST");
				httpListenerResponse.AddHeader("Access-Control-Allow-Origin", "*");
				httpListenerResponse.AddHeader("Via", "hugzho's big brain");
				httpListenerResponse.AddHeader("Location", "your kernel ;)");
				httpListenerResponse.AddHeader("Retry-After", "never lmao");
				httpListenerResponse.Headers.Add("Server", "\r\n\r\n");
				if (!(request.HttpMethod == "OPTIONS"))
				{
					break;
				}
				httpListenerResponse.StatusCode = 200;
				Thread.Sleep(1);
				httpListener.Stop();
			}
			httpListener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;
			httpListener.UnsafeConnectionNtlmAuthentication = true;
			httpListener.IgnoreWriteExceptions = true;
			string text2 = request.RawUrl.Replace("/handshake?user=", "").Replace("&token=", " ");
			string value2 = text2.Split(Array.Empty<char>())[0];
			string value3 = text2.Split(new char[]
			{
				' '
			})[1];
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "login";
			nameValueCollection["username"] = value2;
			nameValueCollection["token"] = value3;
			nameValueCollection["hwid"] = value;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			bool flag = true;
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
				httpListenerResponse.StatusCode = 420;
				httpListenerResponse.StatusDescription = "SHEESH";
			}
			else
			{
				Console.WriteLine(response_structure.message);
				httpListenerResponse.StatusCode = 200;
				httpListenerResponse.StatusDescription = response_structure.message;
				flag = false;
			}
			byte[] bytes = Encoding.UTF8.GetBytes("Whats up?");
			httpListenerResponse.ContentLength64 = (long)bytes.Length;
			httpListenerResponse.OutputStream.Write(bytes, 0, bytes.Length);
			Thread.Sleep(1);
			httpListener.Stop();
			if (!flag)
			{
				Environment.Exit(0);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007920 File Offset: 0x00005B20
		public void button(string button)
		{
			this.CheckInit();
			HttpListener httpListener = new HttpListener();
			string uriPrefix = "http://localhost:1337/" + button + "/";
			httpListener.Prefixes.Add(uriPrefix);
			httpListener.Start();
			HttpListenerContext context = httpListener.GetContext();
			HttpListenerRequest request = context.Request;
			HttpListenerResponse httpListenerResponse = context.Response;
			httpListenerResponse.AddHeader("Access-Control-Allow-Methods", "GET, POST");
			httpListenerResponse.AddHeader("Access-Control-Allow-Origin", "*");
			httpListenerResponse.AddHeader("Via", "hugzho's big brain");
			httpListenerResponse.AddHeader("Location", "your kernel ;)");
			httpListenerResponse.AddHeader("Retry-After", "never lmao");
			httpListenerResponse.Headers.Add("Server", "\r\n\r\n");
			httpListenerResponse.StatusCode = 420;
			httpListenerResponse.StatusDescription = "SHEESH";
			httpListener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;
			httpListener.UnsafeConnectionNtlmAuthentication = true;
			httpListener.IgnoreWriteExceptions = true;
			httpListener.Stop();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007A04 File Offset: 0x00005C04
		public void upgrade(string username, string key)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "upgrade";
			nameValueCollection["username"] = username;
			nameValueCollection["key"] = key;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			response_structure.success = false;
			this.load_response_struct(response_structure);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007A98 File Offset: 0x00005C98
		public void license(string key)
		{
			this.CheckInit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "license";
			nameValueCollection["key"] = key;
			nameValueCollection["hwid"] = value;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007B48 File Offset: 0x00005D48
		public void check()
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "check";
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(data);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007BBC File Offset: 0x00005DBC
		public void setvar(string var, string data)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "setvar";
			nameValueCollection["var"] = var;
			nameValueCollection["data"] = data;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure data2 = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(data2);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007C48 File Offset: 0x00005E48
		public string getvar(string var)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "getvar";
			nameValueCollection["var"] = var;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.response;
			}
			return null;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public void ban(string reason = null)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "ban";
			nameValueCollection["reason"] = reason;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(data);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007D58 File Offset: 0x00005F58
		public string var(string varid)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "var";
			nameValueCollection["varid"] = varid;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.message;
			}
			return null;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public List<api.users> fetchOnline()
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "fetchOnline";
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.users;
			}
			return null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007E6C File Offset: 0x0000606C
		public void fetchStats()
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "fetchStats";
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_app_data(response_structure.appinfo);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007EF4 File Offset: 0x000060F4
		public List<api.msg> chatget(string channelname)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "chatget";
			nameValueCollection["channel"] = channelname;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.messages;
			}
			return null;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007F84 File Offset: 0x00006184
		public bool chatsend(string msg, string channelname)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "chatsend";
			nameValueCollection["message"] = msg;
			nameValueCollection["channel"] = channelname;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000801C File Offset: 0x0000621C
		public bool checkblack()
		{
			this.CheckInit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "checkblacklist";
			nameValueCollection["hwid"] = value;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000080B8 File Offset: 0x000062B8
		public string webhook(string webid, string param, string body = "", string conttype = "")
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "webhook";
			nameValueCollection["webid"] = webid;
			nameValueCollection["params"] = param;
			nameValueCollection["body"] = body;
			nameValueCollection["conttype"] = conttype;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.response;
			}
			return null;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00008170 File Offset: 0x00006370
		public byte[] download(string fileid)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "file";
			nameValueCollection["fileid"] = fileid;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return encryption.str_to_byte_arr(response_structure.contents);
			}
			return null;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00008208 File Offset: 0x00006408
		public void log(string message)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "log";
			nameValueCollection["pcuser"] = Environment.UserName;
			nameValueCollection["message"] = message;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			api.req(nameValueCollection);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00008284 File Offset: 0x00006484
		public void changeUsername(string username)
		{
			this.CheckInit();
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "changeUsername";
			nameValueCollection["newUsername"] = username;
			nameValueCollection["sessionid"] = api.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req(nameValueCollection);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(data);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008304 File Offset: 0x00006504
		public static string checksum(string filename)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(filename))
				{
					result = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
				}
			}
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00008374 File Offset: 0x00006574
		public static void LogEvent(string content)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "KeyAuth", "debug", fileNameWithoutExtension);
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			string path2 = string.Format("{0:MMM_dd_yyyy}_logs.txt", DateTime.Now);
			string text = Path.Combine(path, path2);
			try
			{
				JObject jobject = JsonConvert.DeserializeObject<JObject>(content);
				api.RedactField(jobject, "sessionid");
				api.RedactField(jobject, "ownerid");
				api.RedactField(jobject, "app");
				api.RedactField(jobject, "secret");
				api.RedactField(jobject, "version");
				api.RedactField(jobject, "fileid");
				api.RedactField(jobject, "webhooks");
				api.RedactField(jobject, "nonce");
				string arg = jobject.ToString(Formatting.None, Array.Empty<JsonConverter>());
				using (StreamWriter streamWriter = File.AppendText(text))
				{
					streamWriter.WriteLine(string.Format("[{0}] [{1}] {2}", DateTime.Now, AppDomain.CurrentDomain.FriendlyName, arg));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error logging data: " + ex.Message);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000084B8 File Offset: 0x000066B8
		private static void RedactField(JObject jsonObject, string fieldName)
		{
			JToken jtoken;
			if (jsonObject.TryGetValue(fieldName, out jtoken))
			{
				jsonObject[fieldName] = "REDACTED";
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000084E4 File Offset: 0x000066E4
		public static void error(string message)
		{
			string path = "Logs";
			string text = Path.Combine(path, "ErrorLogs.txt");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			if (!File.Exists(text))
			{
				using (File.Create(text))
				{
					File.AppendAllText(text, DateTime.Now.ToString() + " > This is the start of your error logs file");
				}
			}
			File.AppendAllText(text, DateTime.Now.ToString() + " > " + message + Environment.NewLine);
			Process.Start(new ProcessStartInfo("cmd.exe", "/c start cmd /C \"color b && title Error && echo " + message + " && timeout /t 5\"")
			{
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false
			});
			Environment.Exit(0);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000085C0 File Offset: 0x000067C0
		private static string req(NameValueCollection post_data)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Proxy = null;
					ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(api.assertSSL));
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.2/", post_data);
					stopwatch.Stop();
					api.responseTime = stopwatch.ElapsedMilliseconds;
					ServicePointManager.ServerCertificateValidationCallback = ((object <p0>, X509Certificate <p1>, X509Chain <p2>, SslPolicyErrors <p3>) => true);
					api.sigCheck(Encoding.UTF8.GetString(bytes), webClient.ResponseHeaders["signature"], post_data.Get(0));
					api.LogEvent(Encoding.Default.GetString(bytes) + "\n");
					result = Encoding.Default.GetString(bytes);
				}
			}
			catch (WebException ex)
			{
				if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
				{
					api.error("You're connecting too fast to loader, slow down.");
					api.LogEvent("You're connecting too fast to loader, slow down.");
					Environment.Exit(0);
					result = "";
				}
				else
				{
					api.error("Connection failure. Please try again, or contact us for help.");
					api.LogEvent("Connection failure. Please try again, or contact us for help.");
					Environment.Exit(0);
					result = "";
				}
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00008718 File Offset: 0x00006918
		private static bool assertSSL(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if ((!certificate.Issuer.Contains("Google Trust Services") && !certificate.Issuer.Contains("Let's Encrypt")) || sslPolicyErrors != SslPolicyErrors.None)
			{
				api.error("SSL assertion fail, make sure you're not debugging Network. Disable internet firewall on router if possible. & echo: & echo If not, ask the developer of the program to use custom domains to fix this.");
				api.LogEvent("SSL assertion fail, make sure you're not debugging Network. Disable internet firewall on router if possible. If not, ask the developer of the program to use custom domains to fix this.");
				return false;
			}
			return true;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00008758 File Offset: 0x00006958
		private static void sigCheck(string resp, string signature, string type)
		{
			if (type == "log" || type == "file")
			{
				return;
			}
			try
			{
				if (!encryption.CheckStringsFixedTime(encryption.HashHMAC((type == "init") ? api.enckey.Substring(17, 64) : api.enckey, resp), signature))
				{
					api.error("Signature checksum failed. Request was tampered with or session ended most likely. & echo: & echo Response: " + resp);
					api.LogEvent(resp + "\n");
					Environment.Exit(0);
				}
			}
			catch
			{
				api.error("Signature checksum failed. Request was tampered with or session ended most likely. & echo: & echo Response: " + resp);
				api.LogEvent(resp + "\n");
				Environment.Exit(0);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008814 File Offset: 0x00006A14
		private void load_app_data(api.app_data_structure data)
		{
			this.app_data.numUsers = data.numUsers;
			this.app_data.numOnlineUsers = data.numOnlineUsers;
			this.app_data.numKeys = data.numKeys;
			this.app_data.version = data.version;
			this.app_data.customerPanelLink = data.customerPanelLink;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008878 File Offset: 0x00006A78
		private void load_user_data(api.user_data_structure data)
		{
			this.user_data.username = data.username;
			this.user_data.ip = data.ip;
			this.user_data.hwid = data.hwid;
			this.user_data.createdate = data.createdate;
			this.user_data.lastlogin = data.lastlogin;
			this.user_data.subscriptions = data.subscriptions;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000088EB File Offset: 0x00006AEB
		private void load_response_struct(api.response_structure data)
		{
			this.response.success = data.success;
			this.response.message = data.message;
		}

		// Token: 0x04000159 RID: 345
		public string name;

		// Token: 0x0400015A RID: 346
		public string ownerid;

		// Token: 0x0400015B RID: 347
		public string secret;

		// Token: 0x0400015C RID: 348
		public string version;

		// Token: 0x0400015D RID: 349
		public string path;

		// Token: 0x0400015E RID: 350
		public static long responseTime;

		// Token: 0x0400015F RID: 351
		private static string sessionid;

		// Token: 0x04000160 RID: 352
		private static string enckey;

		// Token: 0x04000161 RID: 353
		private bool initialized;

		// Token: 0x04000162 RID: 354
		public api.app_data_class app_data = new api.app_data_class();

		// Token: 0x04000163 RID: 355
		public api.user_data_class user_data = new api.user_data_class();

		// Token: 0x04000164 RID: 356
		public api.response_class response = new api.response_class();

		// Token: 0x04000165 RID: 357
		private json_wrapper response_decoder = new json_wrapper(new api.response_structure());

		// Token: 0x02000041 RID: 65
		[DataContract]
		private class response_structure
		{
			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000157 RID: 343 RVA: 0x0000C38E File Offset: 0x0000A58E
			// (set) Token: 0x06000158 RID: 344 RVA: 0x0000C396 File Offset: 0x0000A596
			[DataMember]
			public bool success { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000159 RID: 345 RVA: 0x0000C39F File Offset: 0x0000A59F
			// (set) Token: 0x0600015A RID: 346 RVA: 0x0000C3A7 File Offset: 0x0000A5A7
			[DataMember]
			public bool newSession { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600015B RID: 347 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
			// (set) Token: 0x0600015C RID: 348 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
			[DataMember]
			public string sessionid { get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600015D RID: 349 RVA: 0x0000C3C1 File Offset: 0x0000A5C1
			// (set) Token: 0x0600015E RID: 350 RVA: 0x0000C3C9 File Offset: 0x0000A5C9
			[DataMember]
			public string contents { get; set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600015F RID: 351 RVA: 0x0000C3D2 File Offset: 0x0000A5D2
			// (set) Token: 0x06000160 RID: 352 RVA: 0x0000C3DA File Offset: 0x0000A5DA
			[DataMember]
			public string response { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000161 RID: 353 RVA: 0x0000C3E3 File Offset: 0x0000A5E3
			// (set) Token: 0x06000162 RID: 354 RVA: 0x0000C3EB File Offset: 0x0000A5EB
			[DataMember]
			public string message { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000163 RID: 355 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
			// (set) Token: 0x06000164 RID: 356 RVA: 0x0000C3FC File Offset: 0x0000A5FC
			[DataMember]
			public string download { get; set; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000165 RID: 357 RVA: 0x0000C405 File Offset: 0x0000A605
			// (set) Token: 0x06000166 RID: 358 RVA: 0x0000C40D File Offset: 0x0000A60D
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.user_data_structure info { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000167 RID: 359 RVA: 0x0000C416 File Offset: 0x0000A616
			// (set) Token: 0x06000168 RID: 360 RVA: 0x0000C41E File Offset: 0x0000A61E
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.app_data_structure appinfo { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000169 RID: 361 RVA: 0x0000C427 File Offset: 0x0000A627
			// (set) Token: 0x0600016A RID: 362 RVA: 0x0000C42F File Offset: 0x0000A62F
			[DataMember]
			public List<api.msg> messages { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600016B RID: 363 RVA: 0x0000C438 File Offset: 0x0000A638
			// (set) Token: 0x0600016C RID: 364 RVA: 0x0000C440 File Offset: 0x0000A640
			[DataMember]
			public List<api.users> users { get; set; }
		}

		// Token: 0x02000042 RID: 66
		public class msg
		{
			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600016E RID: 366 RVA: 0x0000C451 File Offset: 0x0000A651
			// (set) Token: 0x0600016F RID: 367 RVA: 0x0000C459 File Offset: 0x0000A659
			public string message { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000170 RID: 368 RVA: 0x0000C462 File Offset: 0x0000A662
			// (set) Token: 0x06000171 RID: 369 RVA: 0x0000C46A File Offset: 0x0000A66A
			public string author { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000172 RID: 370 RVA: 0x0000C473 File Offset: 0x0000A673
			// (set) Token: 0x06000173 RID: 371 RVA: 0x0000C47B File Offset: 0x0000A67B
			public string timestamp { get; set; }
		}

		// Token: 0x02000043 RID: 67
		public class users
		{
			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000175 RID: 373 RVA: 0x0000C48C File Offset: 0x0000A68C
			// (set) Token: 0x06000176 RID: 374 RVA: 0x0000C494 File Offset: 0x0000A694
			public string credential { get; set; }
		}

		// Token: 0x02000044 RID: 68
		[DataContract]
		private class user_data_structure
		{
			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000178 RID: 376 RVA: 0x0000C4A5 File Offset: 0x0000A6A5
			// (set) Token: 0x06000179 RID: 377 RVA: 0x0000C4AD File Offset: 0x0000A6AD
			[DataMember]
			public string username { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x0600017A RID: 378 RVA: 0x0000C4B6 File Offset: 0x0000A6B6
			// (set) Token: 0x0600017B RID: 379 RVA: 0x0000C4BE File Offset: 0x0000A6BE
			[DataMember]
			public string ip { get; set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x0600017C RID: 380 RVA: 0x0000C4C7 File Offset: 0x0000A6C7
			// (set) Token: 0x0600017D RID: 381 RVA: 0x0000C4CF File Offset: 0x0000A6CF
			[DataMember]
			public string hwid { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600017E RID: 382 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
			// (set) Token: 0x0600017F RID: 383 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
			[DataMember]
			public string createdate { get; set; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000180 RID: 384 RVA: 0x0000C4E9 File Offset: 0x0000A6E9
			// (set) Token: 0x06000181 RID: 385 RVA: 0x0000C4F1 File Offset: 0x0000A6F1
			[DataMember]
			public string lastlogin { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000182 RID: 386 RVA: 0x0000C4FA File Offset: 0x0000A6FA
			// (set) Token: 0x06000183 RID: 387 RVA: 0x0000C502 File Offset: 0x0000A702
			[DataMember]
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x02000045 RID: 69
		[DataContract]
		private class app_data_structure
		{
			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000185 RID: 389 RVA: 0x0000C513 File Offset: 0x0000A713
			// (set) Token: 0x06000186 RID: 390 RVA: 0x0000C51B File Offset: 0x0000A71B
			[DataMember]
			public string numUsers { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000187 RID: 391 RVA: 0x0000C524 File Offset: 0x0000A724
			// (set) Token: 0x06000188 RID: 392 RVA: 0x0000C52C File Offset: 0x0000A72C
			[DataMember]
			public string numOnlineUsers { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000189 RID: 393 RVA: 0x0000C535 File Offset: 0x0000A735
			// (set) Token: 0x0600018A RID: 394 RVA: 0x0000C53D File Offset: 0x0000A73D
			[DataMember]
			public string numKeys { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600018B RID: 395 RVA: 0x0000C546 File Offset: 0x0000A746
			// (set) Token: 0x0600018C RID: 396 RVA: 0x0000C54E File Offset: 0x0000A74E
			[DataMember]
			public string version { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600018D RID: 397 RVA: 0x0000C557 File Offset: 0x0000A757
			// (set) Token: 0x0600018E RID: 398 RVA: 0x0000C55F File Offset: 0x0000A75F
			[DataMember]
			public string customerPanelLink { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600018F RID: 399 RVA: 0x0000C568 File Offset: 0x0000A768
			// (set) Token: 0x06000190 RID: 400 RVA: 0x0000C570 File Offset: 0x0000A770
			[DataMember]
			public string downloadLink { get; set; }
		}

		// Token: 0x02000046 RID: 70
		public class app_data_class
		{
			// Token: 0x17000026 RID: 38
			// (get) Token: 0x06000192 RID: 402 RVA: 0x0000C581 File Offset: 0x0000A781
			// (set) Token: 0x06000193 RID: 403 RVA: 0x0000C589 File Offset: 0x0000A789
			public string numUsers { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000194 RID: 404 RVA: 0x0000C592 File Offset: 0x0000A792
			// (set) Token: 0x06000195 RID: 405 RVA: 0x0000C59A File Offset: 0x0000A79A
			public string numOnlineUsers { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000196 RID: 406 RVA: 0x0000C5A3 File Offset: 0x0000A7A3
			// (set) Token: 0x06000197 RID: 407 RVA: 0x0000C5AB File Offset: 0x0000A7AB
			public string numKeys { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x06000198 RID: 408 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
			// (set) Token: 0x06000199 RID: 409 RVA: 0x0000C5BC File Offset: 0x0000A7BC
			public string version { get; set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600019A RID: 410 RVA: 0x0000C5C5 File Offset: 0x0000A7C5
			// (set) Token: 0x0600019B RID: 411 RVA: 0x0000C5CD File Offset: 0x0000A7CD
			public string customerPanelLink { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x0600019C RID: 412 RVA: 0x0000C5D6 File Offset: 0x0000A7D6
			// (set) Token: 0x0600019D RID: 413 RVA: 0x0000C5DE File Offset: 0x0000A7DE
			public string downloadLink { get; set; }
		}

		// Token: 0x02000047 RID: 71
		public class user_data_class
		{
			// Token: 0x1700002C RID: 44
			// (get) Token: 0x0600019F RID: 415 RVA: 0x0000C5EF File Offset: 0x0000A7EF
			// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000C5F7 File Offset: 0x0000A7F7
			public string username { get; set; }

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000C600 File Offset: 0x0000A800
			// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000C608 File Offset: 0x0000A808
			public string ip { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000C611 File Offset: 0x0000A811
			// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000C619 File Offset: 0x0000A819
			public string hwid { get; set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000C622 File Offset: 0x0000A822
			// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000C62A File Offset: 0x0000A82A
			public string createdate { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000C633 File Offset: 0x0000A833
			// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000C63B File Offset: 0x0000A83B
			public string lastlogin { get; set; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000C644 File Offset: 0x0000A844
			// (set) Token: 0x060001AA RID: 426 RVA: 0x0000C64C File Offset: 0x0000A84C
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x02000048 RID: 72
		public class Data
		{
			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060001AC RID: 428 RVA: 0x0000C65D File Offset: 0x0000A85D
			// (set) Token: 0x060001AD RID: 429 RVA: 0x0000C665 File Offset: 0x0000A865
			public string subscription { get; set; }

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060001AE RID: 430 RVA: 0x0000C66E File Offset: 0x0000A86E
			// (set) Token: 0x060001AF RID: 431 RVA: 0x0000C676 File Offset: 0x0000A876
			public string expiry { get; set; }

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000C67F File Offset: 0x0000A87F
			// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000C687 File Offset: 0x0000A887
			public string timeleft { get; set; }
		}

		// Token: 0x02000049 RID: 73
		public class response_class
		{
			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000C698 File Offset: 0x0000A898
			// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
			public bool success { get; set; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000C6A9 File Offset: 0x0000A8A9
			// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000C6B1 File Offset: 0x0000A8B1
			public string message { get; set; }
		}
	}
}
