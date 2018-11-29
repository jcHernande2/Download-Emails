using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CentroDeCorreos.Clases;
using OpenPop.Mime;
using OpenPop.Pop3;
using System.Net.Mail;
using CentroDeCorreos.Models;
using System.Data;
using System.Text;
using PagedList;

namespace CentroDeCorreos.Controllers
{
    public class HomeController : Controller
    {
        DB DataBase = new DB();
        public ActionResult Index()
        {
            return View();
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
        public ActionResult Inbox(int page = 1)
        {
            Emails Emails = new Emails();
            Emails.DescargaCorreos(78);
            IPagedList<tblCorreosElectronicosPruebas> lstMsgInbox= DataBase.ConsultarCorreos(page, 10);
            //return PartialView("Inbox", lstMsgInbox);
            return View(lstMsgInbox);
        }  //fin de Listar
        public ActionResult Mensaje()
        {
            ViewBag.error = 0;
            ViewBag.mensaje = "";
            ViewBag.resultado = "";
            if (Request.IsAjaxRequest())
            {
               
                return PartialView("Mensaje", DataBase.GetEmail(Request.Form["MessageId"].ToString()));
            }
            return View();
        }
    }
}