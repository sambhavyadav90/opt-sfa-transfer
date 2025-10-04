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
    public DbSet<TargetPercentFormula> targetPercentFormulaMasters => Set<TargetPercentFormula>();
    public DbSet<SaveDataMaster> saveDataMasters => Set<SaveDataMaster>();
    public DbSet<MappedDbColumnTran> mappedDbColumnTrans => Set<MappedDbColumnTran>();
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
        modelBuilder.Entity<TargetPercentFormula>(eb =>
        {
            eb.ToTable("target_percent_formula");
            eb.HasKey(e => e.row_id);
        });
        modelBuilder.Entity<SaveDataMaster>(eb =>
        {
            eb.ToTable("mst_savedata_main");
            eb.HasNoKey();

            eb.Property(e => e.ItemName)
                .HasColumnName("item_name")
                .HasMaxLength(50)
                .IsRequired();

            eb.Property(e => e.Mrp)
                .HasColumnName("mrp");

            eb.Property(e => e.Qty)
                .HasColumnName("qty");

            eb.Property(e => e.Total)
                .HasColumnName("total");

            eb.Property(e => e.Tax)
                .HasColumnName("tax");

            eb.Property(e => e.FinalAmount)
                .HasColumnName("final_amount");
        });
        modelBuilder.Entity<MappedDbColumnTran>().ToTable("tran_savedatacolumn_mapping").HasNoKey();
        // modelBuilder.Entity<MappedDbColumnTran>(eb =>
        //    {
        //    eb.HasNoKey();
        //    eb.ToTable("tran_savedatacolumn_mapping");
        //    eb.Property(e => e.DbColumnName)
        //    .HasColumnName("db_column_name")
        //    .HasMaxLength(255);
        //    eb.Property(e => e.DbColumnMapping)
        //        .HasColumnName("db_column_mapping")
        //        .HasMaxLength(255);

        //    eb.Property(e => e.DbColumnName)
        //        .HasColumnName("db_column_name")
        //        .HasMaxLength(255);
        //    });
       // modelBuilder.Entity<MappedDbColumnTran>().Property("tran_savedatacolumn_mapping");


        modelBuilder.Entity<EmployeeTargetViewModel>().HasNoKey();
        modelBuilder.Entity<HeadQuarterViewModel>().HasNoKey();
        base.OnModelCreating(modelBuilder);
    }
}
