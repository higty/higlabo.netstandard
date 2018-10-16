using System;
using System.Collections.Generic;
using System.Text;

namespace HigLabo.Mime
{
    public class InvalidMailMessage : MailMessage
    {
        public String ParseText { get; private set; }

        public InvalidMailMessage(MimeMessage message, String parseText)
            : base(message.Headers)
        {
            this.RawData = message.RawData;
            this.ParseText = parseText;
        }
        public override string ToString()
        {
            return this.ParseText + Environment.NewLine + Environment.NewLine + this.GetRawText();
        }
    }
}
