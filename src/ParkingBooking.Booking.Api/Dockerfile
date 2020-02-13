FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ParkingBooking.Booking.Api/ParkingBooking.Booking.Api.csproj", "ParkingBooking.Booking.Api/"]
RUN dotnet restore "ParkingBooking.Booking.Api/ParkingBooking.Booking.Api.csproj"
COPY . .
WORKDIR "/src/ParkingBooking.Booking.Api"
RUN dotnet build "ParkingBooking.Booking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParkingBooking.Booking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParkingBooking.Booking.Api.dll"]