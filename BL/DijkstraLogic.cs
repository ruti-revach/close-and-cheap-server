using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.Windows.Documents;
using System.Collections;
using GoogleMaps.LocationServices;
namespace close_and_cheap.BL
{
    public class DijkstraLogic
    {

        internal static string key = "AIzaSyDEPtSp3Za3ekttM_2ueZyiqoSCCO-9BUc";
  
        List<string> storesCategories = new List<string>();


        //פונקציה שמוצאת בעלי עסק קרובים למיקום המשתמש

        public static List<string> findBusinessOners(string location, string kategory)
        {
 
            //{המרת שם של עיר לקואורדינאטות
            var locationService = new GoogleLocationService("AIzaSyDEPtSp3Za3ekttM_2ueZyiqoSCCO-9BUc");// הכנסת המפתח לאיפיאי של גוגל מפס
            var point = locationService.GetLatLongFromAddress(location);
            var latitude = point.Latitude;//קו הרוחב
            var longitude = point.Longitude;//קו האורך
                                            //}המרת שם של עיר לקואורדינאטות


            string url = "https://maps.googleapis.com/maps/api/place/textsearch/xml?query=" + kategory + "&location=" + latitude + "," + longitude + "&radius=2000&region=il&key=AIzaSyDEPtSp3Za3ekttM_2ueZyiqoSCCO-9BUc";
            //הקישור לחיפוש בעלי העסקים, בתוספת שם עצם אותו אני מחפשת מיקום (קווי אורך ורוחב) ומפתח לאיפיאי
            List<string> allNames = new List<string>();
            WebClient wc = new WebClient();
            try
            {
                string geoCodeInfo = wc.DownloadString(url);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(geoCodeInfo);

                XmlNodeList namesNode = xmlDoc.GetElementsByTagName("name");


                foreach (XmlNode item in namesNode)
                {
                    string myName = item.InnerText;
                    allNames.Add(myName);
                }

                return allNames;
            }
            catch (Exception)
            {
                return allNames;
            }
        }

        //יצירת מסלול
        public List<string> GetMaslul(string location, List<string> storesCategories)
        {
            List<string> maslulToReturn = new List<string>();
            maslulToReturn.Add(location);

            foreach (var storeCategory in storesCategories)
            {
                List<string> storePointersList = findBusinessOners(location, storeCategory);
                string locationNearestStore = GetNearestStore(location, storePointersList);
                maslulToReturn.Add(locationNearestStore);
                location = locationNearestStore;
            }

            return maslulToReturn;
        }


        //הכנסת האיבר הראשון
        public string GetNearestStore(string startingPoint, List<string> storesPointers)
        {
            try
            {
                double minDistance = 0;
                string pointerOfNearestStore = null;

                foreach (var storesPointer in storesPointers)
                {
                    double internalDistance = DistanceInMinutes(startingPoint, storesPointer);
                    if (minDistance == 0 || internalDistance < minDistance)
                    {
                        minDistance = internalDistance;
                        pointerOfNearestStore = storesPointer;
                    }

                }

                return pointerOfNearestStore;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public double DistanceInMinutes(string fPoint, string sPoint)
        {
            string uri = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins="
                          + fPoint + " &destinations=" + sPoint + " &mode=driving&units=imperial&sensor=false&key=" + DijkstraLogic.key + "AIzaSyDEPtSp3Za3ekttM_2ueZyiqoSCCO-9BUc";
            WebClient wc = new WebClient();
            try
            {
                string geoCodeInfo = wc.DownloadString(uri);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(geoCodeInfo);

                string duration = xmlDoc.DocumentElement.SelectSingleNode("//DistanceMatrixResponse/row/element/duration/value").InnerText;
                return (Convert.ToDouble(duration) / 60);//.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("error on DistanceInMinutes:" + ex.Message);
            }
            //return TravelingTime

        }
    }
}
