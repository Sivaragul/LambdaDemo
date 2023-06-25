using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace LambdaDemo
{
    public class UserProvider : IUserProvider
    {
        private readonly IAmazonDynamoDB dynamoDB;

        public UserProvider(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;
        }
        public async Task<flightdetail[]> GetFlightdetails()
        {
            var result = await dynamoDB.ScanAsync(new ScanRequest
            {
                TableName = "flight"
            });
            if (result != null && result.Items != null)
            {
                var flightdetails = new List<flightdetail>();
                foreach (var item in result.Items)
                {
                    item.TryGetValue("Guid", out var Guid);
                    item.TryGetValue("flightid", out var flightid);
                    item.TryGetValue("dep_dest", out var dep_dest);
                    item.TryGetValue("arr_dest", out var arr_dest);
                    item.TryGetValue("dep_date", out var dep_date);
                    item.TryGetValue("arr_date", out var arr_date);

                    flightdetails.Add(new flightdetail
                    {
                        flightid = flightid?.S,
                        dep_dest = dep_dest?.S,
                        arr_dest = arr_dest?.S,


                    });
                }
                return flightdetails.ToArray();
            }
            return Array.Empty<flightdetail>();
        }
    }
}
