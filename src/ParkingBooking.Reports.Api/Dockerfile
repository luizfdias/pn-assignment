FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ParkingBooking.Reports.Api/ParkingBooking.Reports.Api.csproj", "ParkingBooking.Reports.Api/"]
RUN dotnet restore "ParkingBooking.Reports.Api/ParkingBooking.Reports.Api.csproj"
COPY . .
WORKDIR "/src/ParkingBooking.Reports.Api"
RUN dotnet build "ParkingBooking.Reports.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParkingBooking.Reports.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParkingBooking.Reports.Api.dll"]