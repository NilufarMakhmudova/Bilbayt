using System.Collections.Generic;

namespace Bilbayt.Infrastructure.AppSettings
{
    public class CosmosDbSettings
    {
        /// <summary>
        ///     Live Cosmos DB account URL
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        ///     Key - The primary key for the Azure DocumentDB account.
        /// </summary>
        public string PrimaryKey { get; set; }
        /// <summary>
        ///     Database name
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        ///     List of containers in the database
        /// </summary>
        public List<ContainerInfo> Containers { get; set; }

    }
    public class ContainerInfo
    {
        /// <summary>
        ///     Container Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     Container partition Key
        /// </summary>
        public string PartitionKey { get; set; }
    }

}
