using JIS_BE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
        private readonly IMongoCollection<Statistics> _statisticsCollection;

        public JobListingsService(IOptions<JISDatabaseSettings> jisDatabaseSettings)
        {
            var mongoClient = new MongoClient(jisDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(jisDatabaseSettings.Value.DatabaseName);

            _jobListingsCollection = mongoDatabase.GetCollection<JobListing>(jisDatabaseSettings.Value.CollectionName);
            _statisticsCollection = mongoDatabase.GetCollection<Statistics>(jisDatabaseSettings.Value.StatisticsCollection);
        }

        // Get all, using limit for now
        public async Task<SearchResult> GetAllAsync(int page)
        {
            var total = await _jobListingsCollection.EstimatedDocumentCountAsync();
            var pageSize = 5;
            var data = await _jobListingsCollection.Find(_ => true).SortByDescending(x => x.publication_date).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();

            var result = new SearchResult()
            {
                Data = data,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = total
            };

            return result;
        }

        // Get by id
        public async Task<JobListing> GetByIdAsync(string id) =>
            await _jobListingsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Get by list of ids
        public async Task<List<JobListing>> GetByIdsAsync(string[] ids)
        {
            //var filterDef = new FilterDefinitionBuilder<JobListing>();
            //var filter = filterDef.In("_id", ids);
            //return await _jobListingsCollection.Find(filter).ToListAsync();
            return await _jobListingsCollection.Find(x => ids.Contains(x.Id)).ToListAsync();
        }

        // Get by searchstring in description
        public async Task<SearchResult> GetByDescriptionAsync(string searchstring, int page)
        {
            var total = await _jobListingsCollection.EstimatedDocumentCountAsync();
            var pageSize = 5;
            var data = await _jobListingsCollection.Find(x => x.description.text.Contains(searchstring)).SortByDescending(x => x.publication_date).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();

            var result = new SearchResult()
            {
                Data = data,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = total
            };

            return result;

        }

        public async Task<List<Statistics>> GetStatistics()
        {
            var statistics = await _statisticsCollection.Find(_ => true).Limit(20).ToListAsync();
            return statistics;
        }

        // Count all documents
        public async Task<long> GetCount() =>
            await _jobListingsCollection.EstimatedDocumentCountAsync();
    }
}
