﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Subarea> Subareas { get; set; }
 
    DbSet<Lead> Leads { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}