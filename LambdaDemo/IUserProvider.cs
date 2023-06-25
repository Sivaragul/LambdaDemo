namespace LambdaDemo
{
    public interface IUserProvider
    {
        Task<flightdetail[]> GetFlightdetails();
    }
}