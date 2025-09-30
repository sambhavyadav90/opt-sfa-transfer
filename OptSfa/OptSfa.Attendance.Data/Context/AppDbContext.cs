using Microsoft.EntityFrameworkCore;
using OptSfa.Attendance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptSfa.Attendance.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ClientMaster> DbClientMaster => Set<ClientMaster>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ClientMaster>(eb =>
        {
            eb.HasKey(e => e.client_id);
            eb.Property(e => e.client_name).HasMaxLength(200).IsRequired();
        });
    }
}
