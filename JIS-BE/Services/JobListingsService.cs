using JIS_BE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JIS_BE.Services
{
    public class JobListingsService
    {
        private readonly IMongoCollection<JobListing> _jobListingsCollection;

        public JobListingsService(IOptions<JISDatabaseSettings> jisDatabaseSettings)
        {
            var mongoClient = new MongoClient(jisDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(jisDatabaseSettings.Value.DatabaseName);

            _jobListingsCollection = mongoDatabase.GetCollection<JobListing>(
                jisDatabaseSettings.Value.CollectionName);
        }

        // Get all, using limit for now
        public async Task<List<JobListing>> GetAsync() =>
        await _jobListingsCollection.Find(_ => true).Limit(5).ToListAsync();

        // Get by id
        public async Task<JobListing> GetAsync(string id) =>
            await _jobListingsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Get by searchstring in description
        public async Task<List<JobListing>> GetByDescriptionAsync(string searchstring) =>
        await _jobListingsCollection.Find(x => x.description.text.Contains(searchstring)).ToListAsync();

        //public async Task CreateAsync(JobListing newJobListing) =>
        //    await _jobListingsCollection.InsertOneAsync(newJobListing);

        //public async Task UpdateAsync(string id, JobListing updatedJobListing) =>
        //    await _jobListingsCollection.ReplaceOneAsync(x => x.Id == id, updatedJobListing);

        //public async Task RemoveAsync(string id) =>
        //    await _jobListingsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
