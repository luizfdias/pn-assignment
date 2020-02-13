FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ParkingBooking.Worker/ParkingBooking.Worker.csproj", "ParkingBooking.Worker/"]
RUN dotnet restore "ParkingBooking.Worker/ParkingBooking.Worker.csproj"
COPY . .
WORKDIR "/src/ParkingBooking.Worker"
RUN dotnet build "ParkingBooking.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParkingBooking.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParkingBooking.Worker.dll"]