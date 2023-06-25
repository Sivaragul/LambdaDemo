

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace LambdaDemo
{
    public class DemoFunction
    {

        public async Task<APIGatewayProxyResponse> DemoFunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string name = "No Name";
            if(request.QueryStringParameters!=null && request.QueryStringParameters.ContainsKey("name"))
            {
                name = request.QueryStringParameters["name"];
            }


            var userProvider = new UserProvider(new AmazonDynamoDBClient());
            var flightdetails = await userProvider.GetFlightdetailsAsync();
            
            context.Logger.Log($"Got name:{name}");
            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(flightdetails)
            };
        }
    }
}
