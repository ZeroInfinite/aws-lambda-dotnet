using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace TestServerlessApp
{
    public class Greeter_SayHello_Generated
    {
        private readonly ServiceProvider serviceProvider;

        public Greeter_SayHello_Generated()
        {
            var services = new ServiceCollection();

            // By default, Lambda function class is added to the service container using the scoped lifetime
            // because web dependencies are normally scoped to the client request. To use a different lifetime,
            // specify the lifetime in Startup.ConfigureServices(IServiceCollection) method.
            services.AddScoped<TestServerlessApp.Greeter>();
            serviceProvider = services.BuildServiceProvider();
        }

        public APIGatewayProxyResponse SayHello(APIGatewayProxyRequest request, ILambdaContext _context)
        {
            // Create a scope for every request,
            // this allows creating scoped dependencies without creating a scope manually.
            using var scope = serviceProvider.CreateScope();

            var greeter = scope.ServiceProvider.GetRequiredService<TestServerlessApp.Greeter>();
            greeter.SayHello();
            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
            };
        }
    }
}