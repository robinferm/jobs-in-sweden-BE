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
        private readonly IMongoCollection<WordCount> _statisticsCollection;
        private readonly IMongoCollection<SearchEntry> _searchHistoryCollection;
        private readonly IMongoCollection<object> _employerCountCollection;

        public JobListingsService(IOptions<JISDatabaseSettings> jisDatabaseSettings)
        {
            var mongoClient = new MongoClient(jisDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(jisDatabaseSettings.Value.DatabaseName);

            _jobListingsCollection = mongoDatabase.GetCollection<JobListing>(jisDatabaseSettings.Value.CollectionName);
            _statisticsCollection = mongoDatabase.GetCollection<WordCount>(jisDatabaseSettings.Value.StatisticsCollection);
            _searchHistoryCollection = mongoDatabase.GetCollection<SearchEntry>(jisDatabaseSettings.Value.SearchHistoryCollection);
            _employerCountCollection = mongoDatabase.GetCollection<object>(jisDatabaseSettings.Value.EmployerCountCollection);
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

        public async Task<List<SearchEntry>> GetSearchHistory() =>
            await _searchHistoryCollection.Find(_ => true).ToListAsync();

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
            // Log searchstring to db
            var searchEntry = new SearchEntry()
            {
                Searchstring = searchstring,
            };

            if (page == 1) { await _searchHistoryCollection.InsertOneAsync(searchEntry); }

            var total = await _jobListingsCollection.EstimatedDocumentCountAsync();
            var pageSize = 5;
            var data = await _jobListingsCollection.Find(x => x.description.text.Contains(searchstring)).SortByDescending(x => x.publication_date).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();

            var result = new SearchResult()
            {
                Data = data,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = total,
            };

            return result;

        }

        public async Task<Statistics> GetStatistics(string searchstring)
        {
            List<JobListing> joblistings;
            if (searchstring == null)
            {
                joblistings = await _jobListingsCollection.Find(_ => true).ToListAsync();
            }
            else
            {
                joblistings = await _jobListingsCollection.Find(x => x.description.text.Contains(searchstring)).ToListAsync();
            }
            var date = joblistings.GroupBy(x => x.publication_date.Date, (key, values) => new { Date = key, Count = values.Count() });
            var employer = joblistings.GroupBy(x => x.employer.name, (key, values) => new { Employer = key, Count = values.Count() });
            var statistics = new Statistics()
            {
                Date = date,
                Employer = employer
            };
            return statistics;
        }

        public async Task<object> GetEmployerCount() =>
        await  _employerCountCollection.Find(_ => true).ToListAsync();

        public async Task<List<WordCount>> GetWordCount()
        {
            var statistics = await _statisticsCollection.Find(_ => true).Limit(20).ToListAsync();
            return statistics;
        }

        // Count all documents
        public async Task<long> GetCount() =>
            await _jobListingsCollection.EstimatedDocumentCountAsync();
    }
}
