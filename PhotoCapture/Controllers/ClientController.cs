using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoCapture.Models;

namespace PhotoCapture.Controllers
{
    public class ClientController : Controller
    {
        private Manager m = new Manager();
        // GET: /Client/
        public ActionResult Index()
        {
            var ClientList = new List<Client>();
            if (Session["Clients"] == null)
            {
                ClientList = (List<Client>)m.InitClientList();
                Session["Clients"] = ClientList;
            }
            else
            {
                ClientList = (List<Client>)Session["Clients"];
            }

            return View(ClientList);
        }

        //
        // GET: /Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Client/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Client/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
