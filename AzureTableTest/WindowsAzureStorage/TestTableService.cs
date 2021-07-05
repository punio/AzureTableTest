using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTableTest.WindowsAzureStorage
{
	class TestTableService
	{
		public static async Task SetTestData(string connectionString)
		{
			var storageAccount = CloudStorageAccount.Parse(connectionString);
			var tableClient = storageAccount.CreateCloudTableClient();
			var table = tableClient.GetTableReference("AzureTestTable");
			await table.CreateIfNotExistsAsync();

			await table.ExecuteAsync(TableOperation.InsertOrReplace(new TestTableEntity
			{
				PartitionKey = "Test1",
				RowKey = "NormalValue",
				Value = 1.23
			}));
			await table.ExecuteAsync(TableOperation.InsertOrReplace(new TestTableEntity
			{
				PartitionKey = "Test2",
				RowKey = "NaN",
				Value = double.NaN
			}));
			await table.ExecuteAsync(TableOperation.InsertOrReplace(new TestTableEntity
			{
				PartitionKey = "Test3",
				RowKey = "PositiveInfinity",
				Value = double.PositiveInfinity
			}));
			await table.ExecuteAsync(TableOperation.InsertOrReplace(new TestTableEntity
			{
				PartitionKey = "Test4",
				RowKey = "NegativeInfinity",
				Value = double.NegativeInfinity
			}));
		}
	}
}
