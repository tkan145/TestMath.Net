using System.Reflection;
using Xamarin.Forms;
using System;
using System.IO;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

using System.Diagnostics;
using PCLStorage;
using Plugin.EmbeddedResource;
using System.Threading.Tasks;


using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;

using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using MathNet.Numerics;



namespace TestMath.Net
{
	public partial class TestMath_NetPage : ContentPage
	{
		public TestMath_NetPage()
		{
			InitializeComponent();

			// Test embeddedResource
			var rootFolder = FileSystem.Current.LocalStorage;
			var file = ResourceLoader.GetEmbeddedResourceStream(Assembly.Load(new AssemblyName(assmName)), csvFile);
			Debug.WriteLine(file.Length);

			// Xamarin default solution
			var assem = typeof(TestMath_NetPage).GetTypeInfo().Assembly;
			Stream stream = assem.GetManifestResourceStream("TestMath.Net.Logs.csv");

			// Test CSV Helper
			//var reader = new System.IO.StreamReader(stream);
			var reader = new System.IO.StreamReader(file);

			var csv = new CsvHelper.CsvParser(reader);
			Debug.WriteLine(csv.Read());


			//Test CSV reading all records

			var csvAll = new CsvHelper.CsvReader(reader);
			csvAll.Configuration.HasHeaderRecord = false;
			//csvAll.Read();
			//var testField = csvAll.GetField<double>(0);

			var records = csvAll.GetRecords<double>().ToList();
			//Debug.WriteLine(csv.FieldCount);
			Debug.WriteLine(records.Count());

			//Matrix<float> csvMatrix = Test.DelimitedReader.ReadStream<float>(stream, false, ",");
			//Debug.WriteLine("csvMatrix", csvMatrix);


			// Test create Math.NET matrix with CSV Helper













			// Test Math.NET matrix

			// Generate a dense matrix with 500 rows and 500 column
			// FIlled with random numbers 
			var m = Matrix<double>.Build.Random(500, 500);
			var v = Vector<double>.Build.Random(500);
			var y = m.Solve(v);
			Debug.WriteLine(y.Count);

			// Shorter version
			var M = Matrix<double>.Build;
			var V = Vector<double>.Build;

			var x = M.Random(500, 500);
			var z = V.Random(500);

		}

		//public static Matrix<TDataType> Read<TDataType>(TextReader reader, bool sparse = false, string delimiter = @"\s", bool hasHeaders = false, IFormatProvider formatProvider = null)
		//	where TDataType : struct, IEquatable<TDataType>, IFormattable
		//{
		//	var max = -1;
		//	var csv = new CsvHelper.CsvParser(reader);
		//	//var parse = CreateParser<TDataType>(formatProvider);
		//	var matrix = sparse ? Matrix<TDataType>.Build.Sparse(3, max) : Matrix<TDataType>.Build.Dense(3, max);
		//	var storage = matrix.Storage;

		//	//while(csv.Read())
		//	var row = csv.Read();
		//	for (var j = 0; j < row.Length; j++)
		//	{
		//		// strip off quotes (TODO: can we replace this with trimming?)
		//		var value = row[j].Replace("'", string.Empty).Replace("\"", string.Empty);
		//		storage.At(i, j, parse(value));
		//	}

		//	return matrix;
		//}


		public IFormatProvider FormatProvider { get; set; }

		static Func<string, T> CreateParser<T>(IFormatProvider formatProvider)
		{
			if (typeof(T) == typeof(double))
			{
				return number => (T)(object)double.Parse(number, NumberStyles.Any, formatProvider);
			}
			if (typeof(T) == typeof(float))
			{
				return number => (T)(object)float.Parse(number, NumberStyles.Any, CultureInfo.InvariantCulture);
			}
			if (typeof(T) == typeof(System.Numerics.Complex))
			{
				return number => (T)(object)number.ToComplex(formatProvider);
			}
			if (typeof(T) == typeof(Complex32))
			{
				return number => (T)(object)number.ToComplex32(formatProvider);
			}
			throw new NotSupportedException();
		}


		private const string csvFile = "Logs.csv";
		private const string assmName = "TestMath.Net";
	}

}
