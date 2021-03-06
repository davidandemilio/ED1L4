﻿using System;
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
    public class PaisController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
        // GET: Pais
        public ActionResult Index()
        {
            return View(db.DiccionarioListados.Values.ToList());
        }

        // GET: Pais/Details/5
        public ActionResult Details(string id)
        {
            Pais pais = db.DiccionarioListados[id];
           
            return View(pais);
        }


        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
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
                        Pais y = JsonConvert.DeserializeObject<Pais>(x);
                        db.DiccionarioListados.Add(y.nombre, y);
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
        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pais/Edit/5
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


        public ActionResult Deletefa(int id,string pais)
        {
            db.DiccionarioListados[pais].faltantes.Remove(id);
            return RedirectToAction("Details", new { id = pais });
        }

        public ActionResult Deleteca(int id, string pais)
        {
            db.DiccionarioListados[pais].cambios.Remove(id);
            return RedirectToAction("Details", new { id = pais });
        }
        // GET: Pais/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pais/Delete/5
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
