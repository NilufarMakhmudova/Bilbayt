using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bilbayt.Core.Entities;
using Bilbayt.Core.Interfaces.Persistence;
using Bilbayt.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Bilbayt.Infrastructure.CosmosDbData.Repository
{
    public class AppUserRepository : CosmosDbRepository<AppUser>, IAppUserRepository
    {
        /// <summary>
        ///     CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "bilbaytdbcontainer";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "shoppinglist:783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(AppUser entity) => $"{entity.UserName}:{Guid.NewGuid()}";

        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);

        public AppUserRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }

        // Use Cosmos DB Parameterized Query to avoid SQL Injection.
        // Get by Category is also an example of single partition read, where get by title will be a cross partition read
        public async Task<IEnumerable<AppUser>> GetUsersAsyncByUsername(string username)
        {
            List<AppUser> results = new List<AppUser>();
            string query = @$"SELECT * FROM c WHERE c.userName = @username";

            QueryDefinition queryDefinition = new QueryDefinition(query)
                                                    .WithParameter("@username", username);
            string queryString = queryDefinition.ToString();

            IEnumerable<AppUser> entities = await this.GetItemsAsync(queryString);
            
            results.AddRange(entities);
            return results;
        }
    }
}
