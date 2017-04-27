using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.EmbeddedResource;
using PCLStorage;
using System.Diagnostics;
using System;

namespace TestMath.Net
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new TestMath_NetPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		public async Task ReadCSVFile()
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			try
			{
				string filename = "Logs.csv";

				IFile file = await rootFolder.CreateFileAsync(filename, CreationCollisionOption.FailIfExists);
				await file.ReadAllTextAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error message: ", ex.Message);
			}

			ExistenceCheckResult exist = await rootFolder.CheckExistsAsync("Logs.csv");
		}
	}
}
