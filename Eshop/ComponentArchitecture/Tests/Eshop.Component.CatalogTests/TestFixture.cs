﻿using Eshop.Component.Catalog;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Component.CatalogTests
{
	public class TestFixture
	{
		public IServiceCollection Services; 
		
		public TestFixture()
		{
			var services = new ServiceCollection();

			// Loading ALL handlers (probably not ideal for unit tests)
			services.AddMediatR(typeof(Setup).Assembly);

			Services = services;
		}
	}
}