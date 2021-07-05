using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTableTest.WindowsAzureStorage
{
	public class TestTableEntity : TableEntity
	{
		public double Value { get; set; }
	}
}
