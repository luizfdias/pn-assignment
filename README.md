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

In order to book a parking, it is necessary to make a request as following:

```
  POST HOST/api/v1/ParkingBooking
  {
    "garageId": "b8766e1b-2b3f-43fd-b31c-e0667df469d0",
    "licensePlate": "ab1234",
    "from": "2020-01-10T12:00:00Z",
    "to": "2020-01-10T13:00:00Z"
  }
```

If the request was accepted, it will return the response:

```
  HTTP 202 Accepted
```

The booking process is running asynchronous. Currently the result of the booking is only being logged, for tests purposes. Next you can see a simplified view of the booking flow process:

![booking flow](https://raw.githubusercontent.com/luizfdias/pn-assignment/master/assets/booking-flow-1.png)

## Built With
API:
* [ASP.NET CORE](https://www.asp.net/core/overview/aspnet-vnext)
* [AutoMapper](https://automapper.org/) 
* [Fluent Validation](https://fluentvalidation.net/)
* [Azure service bus](https://azure.microsoft.com/en-us/services/service-bus/)

Tests:
* [AutoFixture](https://github.com/AutoFixture/AutoFixture) 
* [NSubstitute](https://github.com/nsubstitute/NSubstitute) 
* [XUnit](https://github.com/xunit/xunit) 
* [FluentAssertions](https://github.com/fluentassertions/fluentassertions) 

## Authors

* **Luiz Fernando Dias Rezende** [LinkedIn](https://www.linkedin.com/in/lrezende-dev/)
