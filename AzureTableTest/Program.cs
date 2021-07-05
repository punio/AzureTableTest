using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AzureTableTest
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var directory = Path.GetDirectoryName(typeof(Program).Assembly.Location);
			var builder = new ConfigurationBuilder()
				.SetBasePath(directory)
				.AddJsonFile("appsettings.json", false, true);
			var configuration = builder.Build();


			await WindowsAzureStorage.TestTableService.SetTestData(configuration.GetConnectionString("StorageConnection"));

			try
			{
				await AzureDataTables.TestTableService.GetTestData(configuration.GetConnectionString("StorageConnection"));
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			}

			try
			{
				AzureDataTables.TestTableService.GonyoGonyo();
				await AzureDataTables.TestTableService.GetTestData(configuration.GetConnectionString("StorageConnection"));
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			}
		}
	}
}
