using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;

namespace ParkingService.Models
{
    public static class MongoDBHelper
    {
        private static string connectionString = "mongodb://deepak:M0ngo@XXX.YY.ZZZ.AAA/db01";
        public static string DatabaseName { get { return "db02"; } }       
        private static MongoDatabase _database;
        public static MongoDatabase DB
        {
            get
            {
                if (_database == null)
                {
                    //var Client = new MongoClient(connectionString);
                    MongoClient client = new MongoClient(connectionString);
                    MongoServer server = client.GetServer();
                    _database = server.GetDatabase(DatabaseName);                   
                    return _database;
                }
                return _database;
            }
        }

       

        public static MongoCollection<T> GetCollection<T>() where T : Entity
        {
            return DB.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public static List<T> GetEntityList<T>() where T : Entity
        {           
            var collection = DB.GetCollection<T>(typeof(T).Name.ToLower());            
            return collection.AsQueryable().ToList<T>();
        }

        public static void InsertEntity<T>(T entity) where T : Entity
        {
            GetCollection<T>().Save(entity);
        }
    }
}
