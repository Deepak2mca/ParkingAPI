using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using ParkingService.Models;

namespace ParkingService.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {

        [HttpGet]
        public string Get()
        {           
            List<Location> aList = MongoDBHelper.GetEntityList<Location>();
            var json = JsonConvert.SerializeObject(aList);
            return json;
        }
        [HttpGet]
        public string GetByLocationName(string locname)
        {
            List<Location> location = MongoDBHelper.GetCollection<Location>().FindAllAs<Location>().Where<Location>(sb => sb.name == locname).ToList<Location>();
            var json = JsonConvert.SerializeObject(location);
            return json;
        }
        [HttpGet]
        public string GetByLatLong(Loc loc)
        {            
            List<Location> location = MongoDBHelper.GetCollection<Location>().FindAllAs<Location>().Where<Location>(sb => sb.loc.coordinates[0] == loc.coordinates[0] && sb.loc.coordinates[1] == loc.coordinates[1]).ToList<Location>();
            var json = JsonConvert.SerializeObject(location);
            return json;
        }
        [HttpPut]
        public int AddLocation()
        {
            System.Collections.Generic.List<double> items2 = new System.Collections.Generic.List<double> { 1.23222, 4.56222 };
            Location documnt = new Location
            {
                _id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                name = "Parking Spaces,Dadar",
                tspaces = 200,
                aspaces = 70,
                loc = new Loc() { type = "point", coordinates = items2 }
            };
            MongoDBHelper.InsertEntity<Location>(documnt);
            return 1;
        }
    }
}
