using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location; // Added System.Device.dll reference.

namespace PhotoCapture.Models
{
    public class Photo
    {
        public Photo()
        {
            this.DataAndTime = DateTime.Now;
        }
        public string Filename { get; set; }
        public Byte[] Content { get; set; }
        public string Type { get; set; }
        //public GPSLocation GPS { get; set; }
        public GeoCoordinate GPS { get; set; }
        public DateTime DataAndTime { get; set; }
        public String AttachedClient { get; set; }
        public List<Client> ClientList { get; set; }
        //public string ImageSource { get{
        //    string mimeType = this.Type;
        //    string base64 = Convert.ToBase64String("1");
        //    return string.Format("data:{0};base64,{1}", mimeType, base64);
        //} }
    }

    public class GPSLocation
    {
        //public string NorthSouth { get; set; }
        public  string Longitude { get; set; }
        public string Latitude { get; set; }
        //public string EastWest { get; set; }

    }

    public class FileUpload
    {
        public HttpPostedFileBase File { get; set; }
        
    }

    public class Client
    {
        //public Client()
        //{
        //    this.GPS = new GPSLocation();
        //}
        public string Name { get; set; }
        public GeoCoordinate GPS { get; set; }

    }

}