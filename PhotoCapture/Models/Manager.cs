using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Device.Location;

namespace PhotoCapture.Controllers
{
    public class Manager
    {
        public IEnumerable<PhotoCapture.Models.Client> InitClientList()
        {
            var ClientList = new List<PhotoCapture.Models.Client>();


            ClientList.Add(new PhotoCapture.Models.Client()
            {
                Name = "John",
                GPS = new GeoCoordinate( 44.0076782222222, -79.4275970277778 )
            });

            ClientList.Add(new PhotoCapture.Models.Client()
            {
                Name = "Mary",
                GPS = new GeoCoordinate(44.0176782222222, -79.4175970277778)
            });

            ClientList.Add(new PhotoCapture.Models.Client()
            {
                Name = "Paul",
                GPS = new GeoCoordinate( 44.0076782222222, -79.4375970277778 )
            });

            ClientList.Add(new PhotoCapture.Models.Client()
            {
                Name = "James",
                GPS = new GeoCoordinate( 44.0076782222222, -79.4475970277778 )
            });

            return ClientList;
        }
        public string GetLatitude(Image photo){
            
            string latitude = "";

            try
            {       
                PropertyItem lt = photo.GetPropertyItem(2);

                latitude = this.ParseLocation(lt);

                PropertyItem ltRef = photo.GetPropertyItem(1);
                if (this.ParseRef(ltRef))
                {
                    latitude = "-" + latitude;
                }
            }
            catch (Exception e)
            {

                latitude = e.Message;
            }

            return latitude;
            
        }


        public string GetLongitude(Image photo)
        {
            string longitude = "";

            try
            {
                PropertyItem lg = photo.GetPropertyItem(4);
                
                longitude = this.ParseLocation(lg);

                PropertyItem lgRef = photo.GetPropertyItem(3);
                if (this.ParseRef(lgRef))
                {
                    longitude = "-" + longitude;
                }

            }
            catch (Exception e)
            {

                longitude = e.Message;
            }

            return longitude;
        }
        public string ParseLocation(PropertyItem pi)
        {
            double deg = BitConverter.ToUInt32(pi.Value, 0);
            uint deg_div = BitConverter.ToUInt32(pi.Value, 4);

            double min = BitConverter.ToUInt32(pi.Value, 8);
            uint min_div = BitConverter.ToUInt32(pi.Value, 12);

            double mmm = BitConverter.ToUInt32(pi.Value, 16);
            uint mmm_div = BitConverter.ToUInt32(pi.Value, 20);

            double m = 0;
            if (deg_div != 0 || deg != 0)
            {
                m = (deg / deg_div);
            }

            if (min_div != 0 || min != 0)
            {
                m = m + (min / min_div) / 60;
            }

            if (mmm_div != 0 || mmm != 0)
            {
                m = m + (mmm / mmm_div / 3600);
            }

            return m.ToString();
        }

        public bool ParseRef(PropertyItem pi)
        {
            string LocationRef = System.Text.Encoding.ASCII.GetString(pi.Value, 0, pi.Len - 1);
            if (LocationRef == "W" || LocationRef == "S")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}