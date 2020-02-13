using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ParkingBooking.Booking.Application.ApiModels;
using ParkingBooking.Booking.Domain.Commands;

namespace ParkingBooking.Booking.Api.Modules
{
    public static class AutoMapperModuleExtensions
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(ctx =>
            {
                return AutoMapperConfiguration.Create().CreateMapper();
            });
        }
    }

    public static class AutoMapperConfiguration
    {
        public static IConfigurationProvider Create()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<BookParkingRequest, BookParkingCommand>();
            });

            mappingConfig.AssertConfigurationIsValid();

            return mappingConfig;
        }
    }
}
