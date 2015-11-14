using Sitecore.Diagnostics;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.XmlControls;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore;

namespace ManualHtmlCacheClearer.Client.Galleries
{
    public class HtmlCachesGallery  : BaseForm
    {
        protected GalleryMenu Options;
        protected Scrollbox sbCaches;

        protected void Invoke(Message message, bool closeGallery)
        {
            Assert.ArgumentNotNull((object)message, "message");
            SheerResponse.Eval("scForm.getParentForm().invoke(\"" + StringUtil.EscapeBackslash(message.ToString()) + "\")");
            if (!closeGallery)
                return;
            SheerResponse.Eval("scForm.getParentForm().Content.closeGallery(scForm.browser.getFrameElement().id)");
        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull((object)e, "e");
            base.OnLoad(e);

            if (Sitecore.Context.ClientPage.IsEvent)
                return;

            int num = 1;
            try
            {
                var siteItems = GetSiteItems();
                if (siteItems == null)
                    return;

                foreach(Item item in siteItems)
                {
                    AddOption(item);          
                }
            }
            catch (Exception ex)
            {
                Sitecore.Context.ClientPage.AddControl(sbCaches, new Literal("Something went wrong. Check the logs"));
                Log.Error("ManualHTMLCacheClear module gallely failed to generate site list.", (object)ex);
            }
        }

        private IEnumerable<Item> GetSiteItems()
        {
            var parent = Sitecore.Context.Database.GetItem("DUMMY GUID");
            if (parent == null)
                return null;

            var validItems = parent.Children.Where(i => i.TemplateID == new ID("DUMMY GUID"));     
       
            return validItems.Any() ? validItems : null;
        }

        private void AddOption(Item item)
        {
            XmlControl option = ControlFactory.GetControl("Gallery.HtmlCaches.Option") as XmlControl;
            Assert.IsNotNull(option, "XML Control - Gallery.HtmlCaches.Option");

            var siteName = item["Site Name"];

            option["Header"] = (object)string.Format("<b>{0}</b><br /><b>{1}</b>)", siteName);
            option["Click"] = (object)string.Format("htmlcache:clear(site={0})", siteName);

            Sitecore.Context.ClientPage.AddControl(sbCaches, option);
        }
    }
}