using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MVCDemo.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            ImageBusinessLayer imageBusinessLayer = new ImageBusinessLayer();
            List<Image> images = imageBusinessLayer.Images.ToList();
            return View(images);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ImageBusinessLayer imageBusinessLayer =
                   new ImageBusinessLayer();
            Image image =
                   imageBusinessLayer.Images.Single(emp => emp.UserId == id);

            return View(image);
        }

        [HttpPost]
        public ActionResult Create(Image image)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(image.imagefile.FileName);
                string extension = Path.GetExtension(image.imagefile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                image.imagepath = "~/pictures/" + filename;
                filename = Path.Combine(Server.MapPath("~/pictures"), filename);
                image.imagefile.SaveAs(filename);

                // Retrieve form data using form collection
                ImageBusinessLayer imageBusinessLayer =
                    new ImageBusinessLayer();

                imageBusinessLayer.AddImage(image);
                return RedirectToAction("Index");
            }
            return View(image);

        }

        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                ImageBusinessLayer imageBusinessLayer =
                    new ImageBusinessLayer();
                imageBusinessLayer.SaveImage(image);

                return RedirectToAction("Index");
            }
            return View(image);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            ImageBusinessLayer imageBusinessLayer =
                new ImageBusinessLayer();
            imageBusinessLayer.DeleteImage(id);
            return RedirectToAction("Index");
        }
        public ActionResult ImageView()
        {
            ImageBusinessLayer imageBusinessLayer = new ImageBusinessLayer();
            List<Image> images = imageBusinessLayer.Images.ToList();
            return View(images);
            
        }

    }
}