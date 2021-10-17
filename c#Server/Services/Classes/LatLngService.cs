using Google.Maps;
using Google.Maps.Geocoding;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Classes
{
    public class LatLngService: ILatLngService
    {
        
        string key = "AIzaSyDYzomy-RA69Y1A5iCXtZ4-H-IzMc4iYyk";///מפתח לגוגל מאפס
        int travelTime;


        //מחזיר אוביקט מסוג כתובת
        public void FindDistance(ref double lat,ref double lng,string from)//מקבל כתובות כלשהן
        {
            GoogleSigned.AssignAllServices(new GoogleSigned(key));//שליחת מפתח חשבון גוגל לאפשור השירות
            var request = new GeocodingRequest();//יצירת אובקיט מסוג בקשת כתובת
            request.Address = from;//קובעים את הכתובת לכתובת שהתקבלה
            var response = new GeocodingService().GetResponse(request);//מפעילים את בקשת השירות וקבלת בתגובה 
            if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)//בודק את ססטוס התגובה האם הצליח 
            {
                var result = response.Results.First();
                //מציג את הכתובת בנקוגות אורך ורוחב
                lat = result.Geometry.Location.Latitude;
                lng = result.Geometry.Location.Longitude;
            }
            else//אם הבקשה נדחתה או לא הצליחה
            {
                lat = 0;
                lng = 0;
            }

        }
    }
}
