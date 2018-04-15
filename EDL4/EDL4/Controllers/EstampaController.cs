using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using EDL4.DBContest;
using EDL4.Models;
namespace EDL4.Controllers
{

    public class EstampaController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
        // GET: Estampa
        public ActionResult Index()
        {
            return View(db.estampasdirectas.Values.ToList());
        }

        // GET: Estampa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Estampa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estampa/Create
        [HttpPost]
        public ActionResult carga(HttpPostedFileBase postedFile)
        {





            if (postedFile != null)
            {


                string filepath = string.Empty;

                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filepath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filepath);

                string csvData = System.IO.File.ReadAllText(filepath);


       

                try
                {
                    JObject json = JObject.Parse(csvData);

                    foreach (JProperty property in json.Properties())
                    {

                        string x = property.Value.ToString();
                        Estampa y = JsonConvert.DeserializeObject<Estampa>(x);

                        db.estampasdirectas.Add(y.pais_no, y);



                    }



                    ViewBag.Message = "Cargado Exitosamente";


                }
                catch (Exception)
                {
                    ViewBag.Message = "Dato erroneo.";
                }

            }

            return RedirectToAction("Index");
        }

        // GET: Estampa/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estampa estampabuscada = db.estampasdirectas[id];

            if (estampabuscada == null)
            {

                return HttpNotFound();
            }
            return View(estampabuscada);
        }

        // POST: Estampa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pais_no,se_tiene")]Estampa estampa)
        {
            try
            {
                // TODO: Add update logic here
                Estampa estampabuscada = db.estampasdirectas[estampa.pais_no];
                if (estampabuscada == null)
                {
                    return HttpNotFound();
                }
                estampabuscada.pais_no = estampa.pais_no;
                estampabuscada.se_tiene = estampa.se_tiene;

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Estampa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estampa/Delete/5
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

