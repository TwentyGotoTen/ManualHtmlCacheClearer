using ManualHtmlCacheClearer.Events;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Diagnostics.PerformanceCounters;
using Sitecore.Events;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManualHtmlCacheClearer.Commands
{
    public class Clear : Command
    {
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull((object)context, "context");
            Assert.IsNotNull((object)context.Parameters, "parameters");
            Assert.IsTrue(context.Parameters.Count > 0, "parameters collection cannot be empty");

            if (context.Items.Length > 0)
                return;

            string siteName = context.Parameters["site"];
            if (string.IsNullOrWhiteSpace(siteName))
                return;

            Event.RaiseEvent("htmlcache:clear", siteName);

            HtmlCacheClearRemoteEvent ev = new HtmlCacheClearRemoteEvent() { SiteName = siteName };
            Sitecore.Eventing.EventManager.QueueEvent<HtmlCacheClearRemoteEvent>(ev);
        }
    }
}


