using LiveGraph.UI;
using LiveGraph.UI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.TestHost;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class PageControllerTest
    {
		private readonly IWebHostBuilder _builder;

		public PageControllerTest()
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
		public async Task IndexGet()
		{
			using (var server = new TestServer(_builder))
			{
				var client = server.CreateClient();
				var result = await client.GetAsync("/Page?1");

				Assert.NotNull(result);
			}
		}

		[Fact]
		public async Task IndexPost()
		{
			using (var server = new TestServer(_builder))
			{
				var client = server.CreateClient();
				var page = new Page()
				{Id  = 1 };
				var json = JsonConvert.SerializeObject(page);
				var result = await client.PostAsync("/Page", new StringContent(json, Encoding.UTF8, "application/json"));
				
				Assert.NotNull(result);
			}
		}

		[Fact]
		public async Task Create()
		{
			using (var server = new TestServer(_builder))
			{
				var client = server.CreateClient();
				var result = await client.GetAsync("/Create");

				Assert.NotNull(result);
			}
		}

		[Fact]
		public async Task GetAll()
		{
			using (var server = new TestServer(_builder))
			{
				var client = server.CreateClient();
				var result = await client.GetAsync("/GetAll");

				Assert.NotNull(result);
			}
		}
	}
}
