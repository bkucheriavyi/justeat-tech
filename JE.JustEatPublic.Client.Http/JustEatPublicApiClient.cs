namespace JE.JustEat.Public.Client
{
    public class JustEatPublicApiClient : IJustEatPublicApiClient
    {
        public IRestaurantResource RestaurantResource { get; }

        public JustEatPublicApiClient(IRestaurantResource restaurantResource)
        {
            RestaurantResource = restaurantResource;
        }
    }
}
