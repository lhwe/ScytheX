using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using _PlatoInternals;

namespace Plato
{
	// Token: 0x02000012 RID: 18
	public class Boost
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00008B26 File Offset: 0x00006D26
		public Boost(long accountId)
		{
			this._accountId = accountId;
			this._hwid = Hwid.Get();
			this._httpClient = this.InitHttpClient();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008B4C File Offset: 0x00006D4C
		public Boost(long accountId, string hwid)
		{
			this._accountId = accountId;
			this._hwid = hwid;
			this._httpClient = this.InitHttpClient();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008B6E File Offset: 0x00006D6E
		private HttpClient InitHttpClient()
		{
			return new HttpClient(new HttpClientHandler
			{
				Proxy = new WebProxy()
			});
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008B85 File Offset: 0x00006D85
		public string GetLink()
		{
			return string.Format("https://gateway.platoboost.com/a/{0}?id={1}", this._accountId, this._hwid);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008BA4 File Offset: 0x00006DA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task<Boost.VerificationResult> Redeem(string key)
		{
			Boost.<Redeem>d__8 <Redeem>d__;
			<Redeem>d__.<>t__builder = AsyncTaskMethodBuilder<Boost.VerificationResult>.Create();
			<Redeem>d__.<>4__this = this;
			<Redeem>d__.key = key;
			<Redeem>d__.<>1__state = -1;
			<Redeem>d__.<>t__builder.Start<Boost.<Redeem>d__8>(ref <Redeem>d__);
			return <Redeem>d__.<>t__builder.Task;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00008BF0 File Offset: 0x00006DF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task<Boost.VerificationResult> Verify(string key)
		{
			Boost.<Verify>d__9 <Verify>d__;
			<Verify>d__.<>t__builder = AsyncTaskMethodBuilder<Boost.VerificationResult>.Create();
			<Verify>d__.<>4__this = this;
			<Verify>d__.<>1__state = -1;
			<Verify>d__.<>t__builder.Start<Boost.<Verify>d__9>(ref <Verify>d__);
			return <Verify>d__.<>t__builder.Task;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00008C34 File Offset: 0x00006E34
		[return: Nullable(new byte[]
		{
			0,
			2
		})]
		private Task<T> DeserializeJson<T>(HttpContent content)
		{
			Boost.<DeserializeJson>d__10<T> <DeserializeJson>d__;
			<DeserializeJson>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<DeserializeJson>d__.content = content;
			<DeserializeJson>d__.<>1__state = -1;
			<DeserializeJson>d__.<>t__builder.Start<Boost.<DeserializeJson>d__10<T>>(ref <DeserializeJson>d__);
			return <DeserializeJson>d__.<>t__builder.Task;
		}

		// Token: 0x04000168 RID: 360
		private readonly long _accountId;

		// Token: 0x04000169 RID: 361
		private readonly HttpClient _httpClient;

		// Token: 0x0400016A RID: 362
		private readonly string _hwid;

		// Token: 0x0200004B RID: 75
		public enum VerificationResult
		{
			// Token: 0x0400023E RID: 574
			Success,
			// Token: 0x0400023F RID: 575
			Invalid,
			// Token: 0x04000240 RID: 576
			Error
		}

		// Token: 0x0200004C RID: 76
		private class VerificationBody
		{
			// Token: 0x060001BB RID: 443 RVA: 0x0000C6D9 File Offset: 0x0000A8D9
			public VerificationBody(bool success)
			{
				this.Success = success;
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060001BC RID: 444 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
			public bool Success { get; }
		}
	}
}
