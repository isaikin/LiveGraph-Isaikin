using LiveGraph.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.TestHost;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class VisualizationGraphControllerTest
    {
		private readonly IWebHostBuilder _builder;

		public VisualizationGraphControllerTest()
		{
			var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\LiveGraph.UI"));
			_builder = new WebHostBuilder()
				.UseContentRoot(path)
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
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Threading.Tasks")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Dynamic.Runtime")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Razor.Runtime")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Razor")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Razor")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Html.Abstractions")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Text.Encodings.Web")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.ApplicationInsights.AspNetCore")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Security.Principal")).Location));
							assemblies.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Identity.EntityFrameworkCore")).Location));

							context.Compilation = context.Compilation.AddReferences(assemblies);
						};
					});
				});
		}
		[Fact]
		public async Task Index()
		{
			using (var server = new TestServer(_builder))
			{
				var client = server.CreateClient();
				var result = await client.GetAsync("/VisualizationGraph");

				Assert.NotNull(result);
			}
		}
	}
}
