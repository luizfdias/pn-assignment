using AutoFixture.Xunit2;
using Xunit;
using Xunit.Sdk;

namespace ParkingBooking.Booking.UnitTests.AutoData
{
    public class AutoNSubstituteInlineDataAttribute : InlineAutoDataAttribute
    {
        public AutoNSubstituteInlineDataAttribute(params object[] values) 
            : base(new DataAttribute[] { new AutoNSubstituteDataAttribute(), new InlineDataAttribute(values) })
        {}
    }
}
