using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ManualHtmlCacheClearer.Events
{
    [DataContract]
    public class HtmlCacheClearRemoteEvent
    {
        [DataMember]
        public string SiteName { get; set; }
    }
}