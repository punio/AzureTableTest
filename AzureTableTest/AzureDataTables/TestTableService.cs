using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;

namespace AzureTableTest.AzureDataTables
{
	class TestTableService
	{
		public static async Task GetTestData(string connectionString)
		{
			var tableClient = new TableClient(connectionString, "AzureTestTable");
			var queryResults = tableClient.QueryAsync<TestTableEntity>("", 1);
			await foreach (var entity in queryResults)
			{
				Console.WriteLine($"{entity.PartitionKey} , {entity.RowKey} , {entity.Value}");
			}
		}

		public static void GonyoGonyo()
		{
			var libAssembly = Assembly.GetAssembly(typeof(global::Azure.Data.Tables.TableClient));
			var type = libAssembly.GetType("Azure.Data.Tables.DictionaryTableExtensions");
			var typeActions = type.GetField("typeActions", BindingFlags.Static | BindingFlags.NonPublic);
			((Dictionary<Type, Action<PropertyInfo, object, object>>)typeActions.GetValue(null))[typeof(double)] = SetDouble;
		}
		private static void SetDouble(PropertyInfo property, object propertyValue, object result)
		{
			if (propertyValue is string s)
			{
				if (s == double.NaN.ToString(CultureInfo.InvariantCulture)) property.SetValue(result, double.NaN);
				if (s == double.PositiveInfinity.ToString(CultureInfo.InvariantCulture)) property.SetValue(result, double.PositiveInfinity);
				if (s == double.NegativeInfinity.ToString(CultureInfo.InvariantCulture)) property.SetValue(result, double.NegativeInfinity);
			}
			else { property.SetValue(result, propertyValue); }
		}
	}
}
