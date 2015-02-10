using PhotoCapture.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Device.Location;

namespace PhotoCapture.Controllers
{
    public class HomeController : Controller
    {
        private Manager m = new Manager();
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

        public ActionResult Create()
        {
            var list = new List<Client>();
            if (Session["Clients"] == null)
            {
                list = (List<Client>)m.InitClientList();
                Session["Clients"] = list;
            }
            else
            {
                list = (List<Client>)Session["Clients"];
            }



            return View(new Models.FileUpload());
        }
        [HttpPost]
        public ActionResult Create(Models.FileUpload NewPhoto, string CurrentLocation)
        {
            if(NewPhoto.File != null){

      
                Image image = Image.FromStream(NewPhoto.File.InputStream);
               
                
                Models.Photo exifData = new Models.Photo();
                exifData.Filename = System.IO.Path.GetFileName(NewPhoto.File.FileName);

                //byte[] Content = new byte[NewPhoto.File.ContentLength];

                //NewPhoto.File.InputStream.Read(Content, 0, NewPhoto.File.ContentLength);
                

                //exifData.Content = Content;
                exifData.Type = NewPhoto.File.ContentType;

                //exifData.GPS.Longitude = m.GetLongitude(image);
                //exifData.GPS.Latitude = m.GetLatitude(image);

                string path = @"~\App_Data\UploadedImgs\";


                try
                {
                    NewPhoto.File.SaveAs(Server.MapPath(path + NewPhoto.File.FileName));

                }
                catch (Exception)
                {
                    
                    NewPhoto.File.SaveAs(Server.MapPath(path + "1"));
                }                

                if (CurrentLocation != "")
                {
                    char[] delim = { ',', ' ' };
                    string[] latLng = CurrentLocation.Split(delim);
                    exifData.GPS = new GeoCoordinate(double.Parse(latLng[0]), double.Parse(latLng[1]));
                }
                else
                {
                    try
                    {
                        exifData.GPS = new GeoCoordinate(double.Parse(m.GetLatitude(image)), double.Parse(m.GetLongitude(image)));
                    }
                    catch (FormatException)
                    {
                        ViewBag.Name = "No Geolocation Data Available";
                        return View("Create");
                    }
                }
                
                
                List<Client> tempList = (List<Client>)Session["Clients"];
                exifData.ClientList = tempList.OrderBy(x => x.GPS.GetDistanceTo(exifData.GPS)).ToList();
                Session["EXIF"] = exifData;

                

                return RedirectToAction("Details");
            }
            else
            {
                ViewBag.Name = "No File Uploaded";
                return View("Create");
            }
        }

        public ActionResult Details()
        {
            Models.Photo exif = (Models.Photo)Session["EXIF"];

            return View(exif);
        }

        [HttpPost]
        public ActionResult Details(String AttachedClient)
        {
            if (AttachedClient != null)
            {
                Models.Photo exif = (Models.Photo)Session["EXIF"];
                exif.AttachedClient = AttachedClient;
                Session["EXIF"] = exif;
                return View("View1", exif);
            }
            else
            {
                return View("Error");
            }
            
        }

        public FileResult RenderImage()
        {
            Models.Photo exif = (Models.Photo)Session["EXIF"];

            string filename = @"~\App_Data\UploadedImgs\" + exif.Filename;
            string contentType = exif.Type;
                return new FilePathResult(filename , contentType);
        }
    }
}