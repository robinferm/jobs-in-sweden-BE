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


        // GET api/joblistings
        [HttpGet]
        public async Task<List<JobListing>> Get() =>
             await _JobListingsService.GetAsync();

        // GET api/joblistings/count
        [HttpGet("count")]
        public async Task<long> GetCount() =>
            await _JobListingsService.GetCount();

        // GET api/joblistings/javascript
        [HttpGet("{searchstring}/{page}")]
        public async Task<SearchResult> GetByDescription(string searchstring, int page) =>
           await _JobListingsService.GetByDescriptionAsync(searchstring, page);
    }
}
