using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneShop.Ordering.Application.Common.Interfaces;
using PhoneShop.Ordering.Domain.Entities;
using PhoneShop.Ordering.Infrastructure.Common;
using System.Reflection;

namespace PhoneShop.Ordering.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
    {
        await _mediator.DispatchDomainEvent(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
