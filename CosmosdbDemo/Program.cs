using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CosmosdbDemo
{
    public class Program
    {
        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = "https://cosmosdbdemoaz204.documents.azure.com:443/";
        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = "DKZ27v9FXNm7cL7pRK8NTbu35SxPpJGFHQ3ct6Fe4TYjdCaUIg9yYS6un7SwvJ7jHxD24NTtRlNepDaw6SSosg==";
        // The Cosmos client instance
        private CosmosClient cosmosClient;
        // The database we will create
        private Database database;
        // The container we will create.
        private Container container;
        // The name of the database and container we will create
        private string databaseId = "myDemoDatabase";
        private string containerId = "myDemoContainer";

        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Beginning operations...\n");
                Program p = new Program();
                await p.CosmosDemoAsync();

                // Runs the CreateDatabaseAsync method
                await p.CreateDatabaseAsync();

                // Run the CreateContainerAsync method
                await p.CreateContainerAsync();
            }
            catch (CosmosException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
        }

        public async Task CosmosDemoAsync()
        {
            // Create a new instance of the Cosmos Client
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        }

        private async Task CreateDatabaseAsync()
        {
            // Create a new database
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);
        }

        private async Task CreateContainerAsync()
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
            Console.WriteLine("Created Container: {0}\n", this.container.Id);
        }
    }
}
