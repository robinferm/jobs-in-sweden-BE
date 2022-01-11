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
        // GET api/joblistings/all/1
        [HttpGet("all/{page}")]
        public async Task<SearchResult> Get(int page) =>
             await _JobListingsService.GetAllAsync(page);

        // Get one joblisting by id
        // GET api/joblistings/61b3c689b19574f058ecd951
        [HttpGet("{id}")]
        public async Task<JobListing> GetById(string id) =>
        await _JobListingsService.GetByIdAsync(id);

        // Get joblistings by multiple ids
        // GET api/joblistings/?ids=61b3c689b19574f058ecd496&ids=61b3c689b19574f058ecdd45&ids=61b3c689b19574f058ecd951
        [HttpGet]
        public async Task<List<JobListing>> GetByIds([FromQuery] string[] ids) =>
            await _JobListingsService.GetByIdsAsync(ids);

        // Get the total number of joblistings
        // GET api/joblistings/count
        [HttpGet("count")]
        public async Task<long> GetCount() =>
            await _JobListingsService.GetCount();

        [HttpGet("searchhistory")]
        public async Task<List<SearchEntry>> GetSearchHistory() =>
            await _JobListingsService.GetSearchHistory();

        // Get joblistings with searchstring
        // GET api/joblistings/search/javascript/1
        [HttpGet("search/{searchstring}/{page}")]
        public async Task<SearchResult> GetByDescription(string searchstring, int page) =>
            await _JobListingsService.GetByDescriptionAsync(searchstring, page);

        [HttpGet("employerCount")]
        public async Task<object> GetEmployerCount() =>
            await _JobListingsService.GetEmployerCount();

        // Get api/joblistings/wordcount
        [HttpGet("wordcount")]
        public async Task<List<WordCount>> GetWordCount() =>
            await _JobListingsService.GetWordCount();

        // Get statistics for a searchstring or for all joblistings
        // GET api/joblistings/statistics
        // GEt api/joblistings/statistics/Saab
        [HttpGet("statistics/{searchstring?}")]
        public async Task<Statistics> GetStatistics(string searchstring = null) =>
            await _JobListingsService.GetStatistics(searchstring);
    }
}
