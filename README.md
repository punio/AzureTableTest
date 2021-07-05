# AzureTableTest

Blog : https://puni-o.hatenablog.com/entry/2021/07/05/114057

Recent Azure Table APIs can't read values like double.NaN.

This is a sample that uses Azure.Data.Tables to force double.NaN to be read.

I couldn't write it with my own ability.

# Read

Replace ```Azure.Data.Tables.DictionaryTableExtensions.typeActions[typeof(double)]``` (Azure/azure-sdk-for-net/.../DictionaryTableExtensions.cs)

This is a static dictionary, so replace Action using reflection, it will work. 

```cs
AzureDataTables.TestTableService.GonyoGonyo()
```

# Write

The write process will work rewrite the process here because an exception will occur in ```Azure/azure-sdk-for-net/.../Utf8JsonWriterExtensions.cs```.

```cs
case double d:
	if (double.IsNaN(d)) writer.WriteStringValue(double.NaN.ToString(CultureInfo.InvariantCulture));
	else if (double.IsPositiveInfinity(d)) writer.WriteStringValue(double.PositiveInfinity.ToString(CultureInfo.InvariantCulture));
	else if (double.IsNegativeInfinity(d)) writer.WriteStringValue(double.NegativeInfinity.ToString(CultureInfo.InvariantCulture));
	else writer.WriteNumberValue(d);
	break;
```

This process was part of a static method and I couldn't change it using reflection. 
