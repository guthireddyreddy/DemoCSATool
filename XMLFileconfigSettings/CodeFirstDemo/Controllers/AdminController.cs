using CodeFirstDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace CodeFirstDemo.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //public ActionResult Index()
        //{
        //    return View();
        //}


        [HttpPost]

        [Route("api/[controller]")]
        public ActionResult AddEditProject(ProjectModels mdl)
        {
            if (mdl.Id > 0)
            {
                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/XML/ProjectList.xml"));
                var items = (from item in xmlDoc.Descendants("Project") select item).ToList();
                XElement selected = items.Where(p => p.Element("Id").Value == mdl.Id.ToString()).FirstOrDefault();
                selected.Remove();
                xmlDoc.Save(Server.MapPath("~/XML/ProjectList.xml"));
                xmlDoc.Element("Projects").Add(new XElement("Project", new XElement("Id", mdl.Id), new XElement("ProjectName", mdl.username), new XElement("Location", mdl.password)));
                xmlDoc.Save(Server.MapPath("~/XML/ProjectList.xml"));
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                XmlDocument oXmlDocument = new XmlDocument();
                oXmlDocument.Load(Server.MapPath("~/XML/ProjectList.xml"));
                XmlNodeList nodelist = oXmlDocument.GetElementsByTagName("Project");
                var x = oXmlDocument.GetElementsByTagName("Id");
                int Max = 0;
                foreach (XmlElement item in x)
                {
                    int EId = Convert.ToInt32(item.InnerText.ToString());
                    if (EId > Max)
                    {
                        Max = EId;
                    }
                }
                Max = Max + 1;
                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/XML/ProjectList.xml"));
                xmlDoc.Element("Projects").Add(new XElement("Project", new XElement("Id", Max), new XElement("ProjectName", mdl.username), new XElement("Location", mdl.password)));
                xmlDoc.Save(Server.MapPath("~/XML/ProjectList.xml"));
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}