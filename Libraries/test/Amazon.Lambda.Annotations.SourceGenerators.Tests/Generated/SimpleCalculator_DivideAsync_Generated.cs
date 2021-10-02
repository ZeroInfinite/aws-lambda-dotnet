﻿using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace TestServerlessApp
{
    public class SimpleCalculator_DivideAsync_Generated
    {
        private readonly ServiceProvider serviceProvider;

        public SimpleCalculator_DivideAsync_Generated()
        {
            var services = new ServiceCollection();

            // By default, Lambda function class is added to the service container using the scoped lifetime
            // because web dependencies are normally scoped to the client request. To use a different lifetime,
            // specify the lifetime in Startup.ConfigureServices(IServiceCollection) method.
            services.AddScoped<TestServerlessApp.SimpleCalculator>();
            var startup = new TestServerlessApp.Startup();
            startup.ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public async System.Threading.Tasks.Task<APIGatewayProxyResponse> DivideAsync(APIGatewayProxyRequest request, ILambdaContext _context)
        {
            // Create a scope for every request,
            // this allows creating scoped dependencies without creating a scope manually.
            using var scope = serviceProvider.CreateScope();

            var simpleCalculator = scope.ServiceProvider.GetRequiredService<TestServerlessApp.SimpleCalculator>();
            var response = await simpleCalculator.DivideAsync();
            var body = System.Text.Json.JsonSerializer.Serialize(response);

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = body,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "text/plain" }
                }
            };
        }
    }
}