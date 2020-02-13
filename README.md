# Parking booking

This is a solution developed in ASP.NET Core with functionalities for parking booking.

## Getting Started

To get started in a development environment, it's only necessary to clone this repository, open the solution and start the following projects:

- ParkingBooking.Booking.Api.csproj
- ParkingBooking.Worker.csproj

### Prerequisites

.NET CORE 3 or above
Internet connection and access to azure cloud

### Configuration

All the configurations are already set in the appsettings.json.

## How to use the Parking Booking Api

The API has 1 entry point for the parking booking as following:

The provided {CorrelationId} must be the same between all callings. It will be used as the correlation identification of the whole process to check the diff and obtain the results.

First step is provide the data to be Analyzed. The field 'content' must be filled with a Base64 encoded value.

Here is a sample request to left and right resources:

```
  POST HOST/api/v1/ParkingBooking
  {
    "garageId": "b8766e1b-2b3f-43fd-b31c-e0667df469d0",
    "licensePlate": "ab1234",
    "from": "2020-01-10T12:00:00Z",
    "to": "2020-01-10T13:00:00Z"
  }
```

Here is a sample success response to both endpoints:

```
  HTTP 202 Accepted
```

## Built With
API:
* [ASP.NET CORE](https://www.asp.net/core/overview/aspnet-vnext)
* [AutoMapper](https://automapper.org/) 
* [Fluent Validation](https://fluentvalidation.net/)

Tests:
* [AutoFixture](https://github.com/AutoFixture/AutoFixture) 
* [NSubstitute](https://github.com/nsubstitute/NSubstitute) 
* [XUnit](https://github.com/xunit/xunit) 
* [FluentAssertions](https://github.com/fluentassertions/fluentassertions) 

## Authors

* **Luiz Fernando Dias Rezende** [LinkedIn](https://www.linkedin.com/in/lrezende-dev/)
