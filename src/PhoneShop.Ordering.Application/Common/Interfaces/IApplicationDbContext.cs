using Microsoft.EntityFrameworkCore;
using PhoneShop.Ordering.Domain.Entities;

namespace PhoneShop.Ordering.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; set; }

    Task<int> SaveChangeAsync(CancellationToken cancellationToken);
}
