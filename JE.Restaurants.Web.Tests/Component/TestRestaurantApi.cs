using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using JE.JustEat.Public.Client;
using JE.JustEat.Public.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TestStack.BDDfy;

namespace JE.Restaurant.WebApi.Tests.Component
{
    [TestFixture(Category = "Component")]
    [Ignore("Fix it later, port to the 3.0 core generic Host")]
    public class TestRestaurantApi
    {
        protected WebApplicationFactory<Startup> _applicationFactory;
        protected Mock<IHttpClientFactory> _clientFactoryMock = new Mock<IHttpClientFactory>();
        private IRestaurantResource _restaurantResource;
        private Mock<IRestaurantResource> _restaurantResourceMock;
        private Temperatures _result;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _clientFactoryMock = new Mock<IHttpClientFactory>();
            _applicationFactory = TestServerFactory.CreateTestServerFactory(services =>
            {
                services.AddSingleton(_restaurantResourceMock.Object);
            });

            var appClient = _applicationFactory.CreateClient();
            appClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _clientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(appClient);

            _restaurantResource = new RestaurantResource(_clientFactoryMock.Object, new JustEat.Public.Client.Http.Configuration.JustEatPublicApiClientConfig { });
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _applicationFactory?.Dispose();
        }

        [Test]
        public void Get_restaurants_bypostcode_list()
        {
            this.Given(x => Given_restaurants_exist())
                .When(x => When_I_get_list_by_postal_code())
                .Then(x => Then_result_should_not_be_empty())
                .BDDfy();
        }

        #region Givens 
        private void Given_restaurants_exist()
        {
            _restaurantResourceMock.Setup(x => x.GetRestaurantByPostCodeAsync(It.IsAny<string>()))
                .ReturnsAsync(new Temperatures
                {
                    Restaurants = new[] {
                        new TemperaturesRestaurant {
                            Name = "Test 1",
                            RatingStars = 1.1,
                            CuisineTypes = new[] {
                                new CuisineType { Name = "Cuisine Type 1" },
                                new CuisineType { Name = "Cuisine Type 2" } } },
                        new TemperaturesRestaurant {
                                    Name = "Test 2",
                                    RatingStars = 2.2,
                                    CuisineTypes = new[] {
                                        new CuisineType { Name = "Cuisine Type 3" },
                                        new CuisineType { Name = "Cuisine Type 4" } } }
                    }
                }
                );
        }
        #endregion

        #region Whens

        private async Task When_I_get_list_by_postal_code()
        {
            _result = await _restaurantResource.GetRestaurantByPostCodeAsync("any");
        }
        #endregion

        #region Thens

        private void Then_result_should_not_be_empty()
        {
            _result.Should().NotBeNull();
            _result.Restaurants.Length.Should().Be(2);
            _result.Restaurants[1].CuisineTypes.Length.Should().Be(2);
            _result.Restaurants.Length.Should().Be(2);
            _result.Restaurants[0].RatingStars.Should().Be(3.3);
            _result.Restaurants[1].Name.Should().Be("Test 3");
        }
        #endregion
    }
}
