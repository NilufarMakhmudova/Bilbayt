using System;
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
        ///     e.g. "email:783dfe25-7ece-4f0b-885e-c0ea72135942"
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
    }
}
