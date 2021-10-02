using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using System.Runtime.InteropServices;
using TestServerlessApp.Services;
using System.Numerics;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TestServerlessApp
{
    public class SimpleCalculator
    {
        private readonly ISimpleCalculatorService _simpleCalculatorService;

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public SimpleCalculator(ISimpleCalculatorService simpleCalculatorService)
        {
            this._simpleCalculatorService = simpleCalculatorService;
        }

        [LambdaFunction]
        public int Add()
        {
            return _simpleCalculatorService.Add(4, 2);
        }

        [LambdaFunction]
        public APIGatewayProxyResponse Subtract()
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = _simpleCalculatorService.Subtract(4, 2).ToString()
            };
        }

        [LambdaFunction]
        public string Multiply()
        {
            return _simpleCalculatorService.Multiply(4, 2).ToString();
        }

        [LambdaFunction]
        public async Task<int> DivideAsync()
        {
            return await Task.FromResult(_simpleCalculatorService.Divide(4, 2));
        }
    }
}
