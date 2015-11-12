using Sitecore.Configuration;
using Sitecore.Diagnostics.PerformanceCounters;
using Sitecore.Events;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManualHtmlCacheClearer.Events
{
    public class HtmlCacheEventHandler
    {
        protected void OnCacheClear(object sender, EventArgs args)
        {
            string siteName = Event.ExtractParameter(args, 0) as string;
            if (siteName == null)
                return;

            if (string.IsNullOrEmpty(siteName))
                return;

            SiteInfo siteInfo = Factory.GetSiteInfo(siteName);
            if (siteInfo == null)
                return;

            siteInfo.HtmlCache.Clear();
            JobsCount.TasksHtmlCacheClearings.Increment(1L);
        }
    }
}


