using System.Data.Entity;

namespace FEPVWebApiOwinHost.Models
{
    public class ContractorContext : DbContext
    {
        public ContractorContext()
            : base("name=Beling")
        {   
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Contractor>().HasMany<Certificate>(c => c.Certificates)
        //        .WithRequired(c => c.Contractor).HasForeignKey(c => new {c.IdCard, c.Employer});
        //}

        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<ContractorBiometric> ContractorBiometrics { get; set; }
        public DbSet<ContractorQualification> ContractorQualifications { get; set; }
        public DbSet<ContractorForeigner> ContractorForeigners { get; set; }
        public DbSet<ContractorEmployeeFile> ContractorEmployeeFiles { get; set; }
    }
}
