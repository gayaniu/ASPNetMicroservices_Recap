using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString")); //comming from appsettings.json
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName")); //comming from appsettings.json

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));  //comming from appsettings.json
            CatalogContextSeed.SeedData(Products); // to use the data returned from SeedData() if no data avaiable in database
        }

        public IMongoCollection<Product> Products { get; }
    }
}
