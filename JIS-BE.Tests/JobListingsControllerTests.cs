using FakeItEasy;
using JIS_BE.Controllers;
using JIS_BE.Models;
using JIS_BE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Xunit;

namespace JIS_BE.Tests
{
    public class JobListingsControllerTests
    {
        private readonly JISDatabaseSettings _dbsettings;
        private readonly JobListingsController _controller;
        private readonly JobListingsService _service;


        public JobListingsControllerTests()
        {
            var settings = new JISDatabaseSettings()
            {
                ConnectionString = "mongodb://mongo:27017/?authSource=JIS",
                DatabaseName = "JIS",
                CollectionName = "JobListings"
            };
            IOptions<JISDatabaseSettings> dbsettings = Options.Create(settings);

            _service = new JobListingsService(dbsettings);
            _controller = new JobListingsController(_service);
        }

        // Move integration testing to a separate project
        //[Fact]
        //public async Task Get_Returns_OK()
        //{
        //    var settings = new JISDatabaseSettings()
        //    {
        //        ConnectionString = "mongodb://localhost:27017/?authSource=JIS",
        //        DatabaseName = "JIS",
        //        CollectionName = "JobListings"
        //    };
        //    var o = A.Fake<IOptions<JISDatabaseSettings>>(x => x.WithArgumentsForConstructor(() => new JISDatabaseSettings("mongodb://localhost:27017/?authSource=JIS", "JIS", "JobListings")));
        //    var test = A.Fake<JISDatabaseSettings>();
        //    test.ConnectionString = "mongodb://localhost:27017/?authSource=JIS";
        //    test.DatabaseName = "JIS";
        //    test.CollectionName = "JobListings";

        //    var d = Options.Create(test);
        //    var asd = A.Fake<IOptions<JISDatabaseSettings>>(x => x.WithArgumentsForConstructor(() => Options.Create(settings)));
        //    var s = A.Fake<JobListingsService>(x => x.WithArgumentsForConstructor(() => ));
        //    var c = A.Fake<JobListingsController>();
        //    var res = await c.Get("123");


        //    //var res = await _controller.Get("123");

        //    Assert.IsType<OkObjectResult>(res);
        //}
    }
}
