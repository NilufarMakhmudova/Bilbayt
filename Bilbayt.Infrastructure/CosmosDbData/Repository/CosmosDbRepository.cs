using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Bilbayt.Core.Entities;
using Bilbayt.Core.Interfaces.Persistence;
using Bilbayt.Core.Specifications.Base;
using Bilbayt.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Bilbayt.Infrastructure.CosmosDbData.Repository
{
    public abstract class CosmosDbRepository<T> : IRepository<T>, IContainerContext<T> where T : BaseEntity
    {
        /// <summary>
        ///     Name of the CosmosDB container
        /// </summary>
        public abstract string ContainerName { get; }

        /// <summary>
        ///     Generate id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract string GenerateId(T entity);

        /// <summary>
        ///     Resolve the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public abstract PartitionKey ResolvePartitionKey(string entityId);

        
        /// <summary>
        ///     Cosmos DB factory
        /// </summary>
        private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;

        /// <summary>
        ///     Cosmos DB container
        /// </summary>
        private readonly Container _container;
      

        public CosmosDbRepository(ICosmosDbContainerFactory cosmosDbContainerFactory)
        {
            this._cosmosDbContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(ICosmosDbContainerFactory));
            this._container = this._cosmosDbContainerFactory.GetContainer(ContainerName)._container;
        }

        public async Task AddItemAsync(T item)
        {
            item.Id = GenerateId(item);
            await _container.CreateItemAsync<T>(item, ResolvePartitionKey(item.Id));
        }

        public async Task UpdateItemAsync(string id, T item)
        {
            // Update
            await this._container.UpsertItemAsync<T>(item, ResolvePartitionKey(id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<T>(id, ResolvePartitionKey(id));
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<T>(id, ResolvePartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        // Search data using SQL query string
        // This shows how to use SQL string to read data from Cosmos DB for demonstration purpose.
        // For production, try to use safer alternatives like Parameterized Query and LINQ if possible.
        // Using string can expose SQL Injection vulnerability, e.g. select * from c where c.id=1 OR 1=1. 
        // String can also be hard to work with due to special characters and spaces when advanced querying like search and pagination is required.
        public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
        {
            var resultSetIterator = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            var results = new List<T>();
            while (resultSetIterator.HasMoreResults)
            {
                var response = await resultSetIterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        /// <inheritdoc cref="IRepository{T}.GetItemsAsync(Ardalis.Specification.ISpecification{T})"/>
        public async Task<IEnumerable<T>> GetItemsAsync(ISpecification<T> specification)
        {
            var queryable = ApplySpecification(specification);
            var iterator = queryable.ToFeedIterator<T>();

            var results = new List<T>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        /// <inheritdoc cref="IRepository{T}.GetItemsCountAsync(ISpecification{T})"/>
        public async Task<int> GetItemsCountAsync(ISpecification<T> specification)
        {
            var queryable = ApplySpecification(specification);
            return await queryable.CountAsync();
        }

      
        /// <summary>
        ///     Evaluate specification and return IQueryable
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            var evaluator = new CosmosDbSpecificationEvaluator<T>();
            return evaluator.GetQuery(_container.GetItemLinqQueryable<T>(), specification);
        }

    }
}
