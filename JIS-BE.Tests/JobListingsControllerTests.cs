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
        [Fact]
        public async Task Get_Returns_OK()
        {
        var res = await _controller.Get("123");

            Assert.IsType<OkObjectResult>(res);
        }
    }
}
