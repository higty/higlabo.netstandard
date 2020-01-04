using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HigLabo.Core;

namespace HigLabo.Net
{
    /// <summary>
    /// 
    /// </summary>
    internal class StreamWriteContext
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HttpRequestUploadingEventArgs> Uploading;
        private Stream _TargetStream = null;
        private Int64 _StreamLength = 0;
        private Int32? _BufferSize = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetStream"></param>
        public StreamWriteContext(Stream targetStream, Int64 streamLength)
        {
            if (targetStream == null) { throw new ArgumentNullException("targetStream must not be null"); }
            _TargetStream = targetStream;
            _StreamLength = streamLength;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetStream"></param>
        /// <param name="bufferSize"></param>
        public StreamWriteContext(Stream targetStream, Int64 streamLength, Int32 bufferSize)
        {
            if (targetStream == null) { throw new ArgumentNullException("targetStream must not be null"); }
            if (bufferSize <= 0) { throw new ArgumentException("bufferSize must be larger than zero"); }
            _TargetStream = targetStream;
            _StreamLength = streamLength;
            _BufferSize = bufferSize;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void Write(Byte[] data)
        {
            MemoryStream mm = new MemoryStream(data);
            this.Write(mm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceStream"></param>
        internal void Write(Stream sourceStream)
        {
            var length = _StreamLength;
            
            if (this._BufferSize.HasValue == true)
            {
                Byte[] bb = null;
                Int64 index = 0;
                Int32 size = this._BufferSize.Value;
                Boolean isBreak = false;
                while (true)
                {
                    if (size >= length - index)
                    {
                        size = (Int32)(length - index);
                        isBreak = true;
                    }
                    bb = new Byte[size];
                    sourceStream.Read(bb, 0, size);
                    _TargetStream.Write(bb, 0, size);
                    this.OnUploading(new HttpRequestUploadingEventArgs(size, index + size));
                    if (isBreak == true) { break; }
                    index = index + size;
                }
            }
            else
            {
                sourceStream.CopyToAsync(_TargetStream).GetAwaiter().GetResult();
                this.OnUploading(new HttpRequestUploadingEventArgs(length, length));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnUploading(HttpRequestUploadingEventArgs e)
        {
            var eh = this.Uploading;
            if (eh != null)
            {
                eh(this, e);
            }
        }
    }
}
