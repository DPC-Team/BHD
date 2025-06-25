using BHD.Application.Common.Interfaces.Services;

namespace BHD.Infrastructure.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}
