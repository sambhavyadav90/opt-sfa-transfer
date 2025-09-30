using Microsoft.EntityFrameworkCore;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;


namespace OptSfa.Migration.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ClientMaster> DbClientMaster => Set<ClientMaster>();
    public DbSet<EmployeeMaster> employeeMasters => Set<EmployeeMaster>();
    public DbSet<HeadquarterMaster> headquarterMasters => Set<HeadquarterMaster>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ClientMaster>(eb =>
        {
            eb.HasKey(e => e.client_id);
            eb.Property(e => e.client_name).HasMaxLength(200).IsRequired();
        });
        modelBuilder.Entity<EmployeeMaster>(eb =>
        {
            eb.HasKey(e => e.empId);
            eb.Property(e => e.name).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<HeadquarterMaster>(eb =>
        {
            eb.HasKey(e => e.districtId);
            eb.Property(e => e.district).HasMaxLength(200).IsRequired();
        });
        modelBuilder.Entity<EmployeeTargetViewModel>().HasNoKey();
        base.OnModelCreating(modelBuilder);
    }
}
