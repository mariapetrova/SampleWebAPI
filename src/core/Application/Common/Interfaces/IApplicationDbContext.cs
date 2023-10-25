using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Department> Departments { get; set; }
 
    DbSet<Person> Persons { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}