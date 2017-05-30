using LiveGraph.UI.Controllers;
using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using LiveGraph.UI;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace LiveGraph.UnitTest
{
	public class HomeControllerTest
	{
		private readonly TestServer _server;
		private readonly HttpClient _client;
		public HomeControllerTest()
		{
			var path = PlatformServices.Default.Application.ApplicationBasePath;
			var setDir = Path.GetFullPath(Path.Combine(path, "..\\..\\..\\..\\LiveGraph.UI"));

			var builder = new WebHostBuilder()
	.UseContentRoot(setDir)
	.ConfigureLogging(factory =>
	{
		factory.AddConsole();
	})
	.UseStartup<Startup>()
	.ConfigureServices(services =>
	{
		services.Configure((RazorViewEngineOptions options) =>
		{
			var previous = options.CompilationCallback;
			options.CompilationCallback = (context) =>
			{
				previous?.Invoke(context);

				var assembly = typeof(Startup).GetTypeInfo().Assembly;
				var assemblies = assembly.GetReferencedAssemblies().Select(x => MetadataReference.CreateFromFile(Assembly.Load(x).Location))
				.ToList();
				assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("mscorlib")).Location));
				assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Private.Corelib")).Location));
				assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Razor")).Location));
				assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime.CompilerServices")).Location));
				context.Compilation = context.Compilation.AddReferences(assemblies);
			};
		});
	});

			_server = new TestServer(builder);


			_client = _server.CreateClient();

		}


		[Fact]
		public async Task ReturnHelloWorld()
		{
			var response = await _client.GetAsync("/");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal("Hello World!",
				responseString);

		}
	}
}
