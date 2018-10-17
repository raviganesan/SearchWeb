using SearchWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchWeb.Controllers
{
    public class HomeController : Controller
    {
     
        public ActionResult Index()
        {
            var data = GetSettings();                     
            

            return View(data);
        }

        public ActionResult FilterSettings(string searchString = "")
        {
            var data = GetSettings();

            if(!string.IsNullOrWhiteSpace(searchString))
            data = data.FindAll(d => d.GroupName.Equals(searchString, StringComparison.OrdinalIgnoreCase) || d.SettingName.Equals(searchString, StringComparison.OrdinalIgnoreCase) || d.SettingValue.Equals(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            return PartialView("Settings", data);

        }

        public ActionResult Autocomplete(string searchString)
        {
            var data = GetSettings().Select(s => s.GroupName);

            var filteredItems = data.Where(
                item => item.Equals(searchString, StringComparison.InvariantCultureIgnoreCase)
                ).Distinct();
            //     var filteredItems = data.Where(
            //item => item.GroupName.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
            //);

           // var data1 = GetSettings().SelectMany(a => a.GroupName.Equals(searchString, StringComparison.OrdinalIgnoreCase) );


            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<Setting> GetSettings()
        {
            var settings = new List<Setting>();

            settings.Add(new Setting { GroupName = "HSM", SettingName = "Public", SettingValue = "Ravi" });
            settings.Add(new Setting { GroupName = "HSM", SettingName = "Private", SettingValue = "CW48" });
            settings.Add(new Setting { GroupName = "Config", SettingName = "IpAddress", SettingValue = "localhost" });
            settings.Add(new Setting { GroupName = "HSM", SettingName = "Public", SettingValue = "Ravi" });
            settings.Add(new Setting { GroupName = "Server", SettingName = "BETA", SettingValue = "SPSAPIDEVVH1" });

            return settings;
        }
    }
}