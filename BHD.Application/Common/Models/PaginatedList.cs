using AutoMapper;
using BHD.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BHD.Application.Common.Models
{
    public class PaginatedList<T> : BasePaginatedList<T>
    {
        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize) : base(items, count, pageNumber, pageSize)
        {

        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        public static async Task<PaginatedList<T>> CreateAndMapAsync<Type>(IQueryable<Type> source, int pageNumber, int pageSize, IMapper mapper)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var data = mapper.Map<List<T>>(items);

            return new PaginatedList<T>(data, count, pageNumber, pageSize);
        }
    }
}
