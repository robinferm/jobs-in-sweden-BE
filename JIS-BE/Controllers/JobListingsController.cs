using JIS_BE.Models;
using JIS_BE.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JIS_BE.Controllers
{
    [EnableCors("AllowEverything")]
    [ApiController]
    [Route("api/[controller]")]
    public class JobListingsController : Controller
    {

        private readonly JobListingsService _JobListingsService;

        public JobListingsController(JobListingsService JobListingsService) =>
            _JobListingsService = JobListingsService;

        // Get the latest joblistings without searchstring
        // GET api/joblistings/1
        [HttpGet("{page}")]
        public async Task<SearchResult> Get(int page) =>
             await _JobListingsService.GetAsync(page);

        // Get the total number of joblistings
        // GET api/joblistings/count
        [HttpGet("count")]
        public async Task<long> GetCount() =>
            await _JobListingsService.GetCount();

        // Get joblistings with searchstring
        // GET api/joblistings/javascript/1
        [HttpGet("{searchstring}/{page}")]
        public async Task<SearchResult> GetByDescription(string searchstring, int page) =>
           await _JobListingsService.GetByDescriptionAsync(searchstring, page);

        // Get api/joblistings/statistics
        [HttpGet("statistics")]
        public async Task<List<Statistics>> GetStatistics() =>
            await _JobListingsService.GetStatistics();
    }
}
