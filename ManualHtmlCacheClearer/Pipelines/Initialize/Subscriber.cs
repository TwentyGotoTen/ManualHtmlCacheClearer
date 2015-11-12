using ManualHtmlCacheClearer.Events;
using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManualHtmlCacheClearer.Pipelines.Initialize
{
    public class Subscriber
    {
        public virtual void InitializeFromPipeline(PipelineArgs args)
        {
            var action = new Action<HtmlCacheClearRemoteEvent>(RaiseRemoteEvent);
            Sitecore.Eventing.EventManager.Subscribe<HtmlCacheClearRemoteEvent>(action);
        }
        private void RaiseRemoteEvent(HtmlCacheClearRemoteEvent myEvent)
        {
            Sitecore.Events.Event.RaiseEvent("htmlcache:clear:remote", new object[] { myEvent.SiteName });
        }
    }
}