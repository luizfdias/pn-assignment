using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ParkingBooking.Booking.Api;
using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.IntegrationTests.API.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace ParkingBooking.Booking.IntegrationTests.API
{
    public class ParkingBookingControllerTests
    {
        private readonly HttpClient _client;

        public ParkingBookingControllerTests()
        {
            _client = ClientFactory.Create();
        }

        [Fact]
        public async void BookParking_WhenBookingParking_ShouldReturnResultAsExpected()
        {
            var response = await _client.PostAsync("api/v1/parkingBooking", CreateContent());

            response.StatusCode.Should().Be(StatusCodes.Status202Accepted);
        }

        private static StringContent CreateContent()
        {
            return new StringContent("{\r\n\t\"garageId\": \"7f23692b-894c-4cde-9883-0d98e227507b\",\r\n\t\"licensePlate\": \"ab1234\",\r\n\t\"from\": \"2020-01-01T00:00:00Z\",\r\n\t\"to\": \"2020-01-01T01:00:00Z\"\r\n}", Encoding.UTF8, "application/json");
        }
    }
}
