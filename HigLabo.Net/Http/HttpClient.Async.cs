using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HigLabo.Net
{
    public partial class HttpClient
    {
        protected Task<TResult> AsyncCall<TResult>(Action<Action<TResult>> action)
        {
            var source = new TaskCompletionSource<TResult>();
            action(res => source.SetResult(res));
            return source.Task;
        }
        protected Task<TResult> AsyncCall<T, TResult>(Action<T, Action<TResult>> action, T command)
        {
            var source = new TaskCompletionSource<TResult>();
            action(command, res => source.SetResult(res));
            return source.Task;
        }
        protected Task<TResult> AsyncCall<TResult>(HttpRequestCommand command, Func<HttpResponse, TResult> func)
        {
            var source = new TaskCompletionSource<TResult>();
            this.GetHttpWebResponse(command
                , res => source.SetResult(func(new HttpResponse(res, this.ResponseEncoding)))
                , e => source.SetException(e.Exception));
            return source.Task;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<HttpWebResponse> GetHttpWebResponseAsync(String url)
        {
            return this.GetHttpWebResponseAsync(new HttpRequestCommand(url));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<HttpWebResponse> GetHttpWebResponseAsync(String url, HttpBodyFormUrlEncodedData data)
        {
            var cm = new HttpRequestCommand(url);
            cm.SetBodyStream(data);
            return this.GetHttpWebResponseAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<HttpWebResponse> GetHttpWebResponseAsync(String url, Byte[] data)
        {
            var cm = new HttpRequestCommand(url);
            cm.SetBodyStream(data);
            return this.GetHttpWebResponseAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Task<HttpWebResponse> GetHttpWebResponseAsync(String url, Stream stream)
        {
            var cm = new HttpRequestCommand(url);
            cm.SetBodyStream(stream);
            return this.GetHttpWebResponseAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task<HttpWebResponse> GetHttpWebResponseAsync(HttpRequestCommand command)
        {
            return AsyncCall<HttpRequestCommand, HttpWebResponse>(this.GetHttpWebResponse, command);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<HttpResponse> GetResponseAsync(String url)
        {
            var cm = new HttpRequestCommand(url);
            return this.GetResponseAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<HttpResponse> GetResponseAsync(String url, HttpBodyFormUrlEncodedData data)
        {
            var cm = new HttpRequestCommand(url);
            cm.SetBodyStream(data);
            return this.GetResponseAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<HttpResponse> GetResponseAsync(String url, Byte[] data)
        {
            var cm = new HttpRequestCommand(url);
            cm.SetBodyStream(data);
            return this.GetResponseAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Task<HttpResponse> GetResponseAsync(String url, Stream stream)
        {
            var cm = new HttpRequestCommand(url);
            cm.SetBodyStream(stream);
            return this.GetResponseAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task<HttpResponse> GetResponseAsync(HttpRequestCommand command)
        {
            return AsyncCall<HttpRequestCommand, HttpResponse>(this.GetResponse, command);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<String> GetBodyTextAsync(String url)
        {
            var cm = new HttpRequestCommand(url);
            return this.GetBodyTextAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<String> GetBodyTextAsync(String url, HttpBodyFormUrlEncodedData data)
        {
            var cm = new HttpRequestCommand(url);
            cm.SetBodyStream(data);
            return this.GetBodyTextAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<String> GetBodyTextAsync(String url, Byte[] data)
        {
            var cm = new HttpRequestCommand(url);
            cm.SetBodyStream(data);
            return this.GetBodyTextAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Task<String> GetBodyTextAsync(String url, Stream stream)
        {
            var cm = new HttpRequestCommand(url);
            cm.SetBodyStream(stream);
            return this.GetBodyTextAsync(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task<String> GetBodyTextAsync(HttpRequestCommand command)
        {
            return AsyncCall<HttpRequestCommand, String>(this.GetBodyText, command);
        }
    }
}
