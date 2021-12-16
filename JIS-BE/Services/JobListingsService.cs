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
        public async Task<SearchResult> GetByDescriptionAsync(string searchstring, int page)
        {
            var total = await _jobListingsCollection.EstimatedDocumentCountAsync();
            var pageSize = 5;
            var data = _jobListingsCollection.Find(x => x.description.text.Contains(searchstring)).Skip((page - 1) * pageSize).Limit(pageSize).ToList();

            var result = new SearchResult()
            {
                Data = data,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = total
            };

            return result;

        }

        // Count all documents
        public async Task<long> GetCount() =>
            await _jobListingsCollection.EstimatedDocumentCountAsync();
    }
    public class SearchResult
    {

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; }
        public ICollection<JobListing> Data { get; set; }
    }
}
