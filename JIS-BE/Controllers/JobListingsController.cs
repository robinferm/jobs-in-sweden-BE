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

        [HttpGet("count")]
        public async Task<long> GetCount() =>
            await _JobListingsService.GetCount();


        // GET api/joblistings/61b3c66bb19574f058e7b2ad
        //[HttpGet("{id:length(24)}")]
        //public async Task<ActionResult<JobListing>> Get(string id)
        //{
        //    var JobListing = await _JobListingsService.GetAsync(id);

        //    if (JobListing is null)
        //    {
        //        return NotFound();
        //    }

        //    return JobListing;
        //}

        // GET api/joblistings/javascript
        [HttpGet("{searchstring}")]
        public async Task<List<JobListing>> GetByDescription(string searchstring) =>
           await _JobListingsService.GetByDescriptionAsync(searchstring);





        //[HttpPost]
        //public async Task<IActionResult> Post(JobListing newJobListing)
        //{
        //    await _JobListingsService.CreateAsync(newJobListing);

        //    return CreatedAtAction(nameof(Get), new { id = newJobListing.Id }, newJobListing);
        //}

        //[HttpPut("{id:length(24)}")]
        //public async Task<IActionResult> Update(string id, JobListing updatedJobListing)
        //{
        //    var JobListing = await _JobListingsService.GetAsync(id);

        //    if (JobListing is null)
        //    {
        //        return NotFound();
        //    }

        //    updatedJobListing.Id = JobListing.Id;

        //    await _JobListingsService.UpdateAsync(id, updatedJobListing);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var JobListing = await _JobListingsService.GetAsync(id);

        //    if (JobListing is null)
        //    {
        //        return NotFound();
        //    }

        //    await _JobListingsService.RemoveAsync(JobListing.Id);

        //    return NoContent();
        //}
    }
}
