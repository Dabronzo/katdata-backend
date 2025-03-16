using katdata.Features.Entities;
using katdata.Features.Entities.Population;
using katdata.Features.Entities.Urban;
using Microsoft.EntityFrameworkCore;

namespace katdata.Tools
{
    public sealed class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Province> Provinces { get; set; }

        //public DbSet<Settlement> Settlements { get; set; }

        //public DbSet<SettlementPopulation> Populations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Province>(entity =>
            {
                entity.OwnsMany(p => p.Settlements, settlement =>
                {
                    settlement.WithOwner(s => s.Province).HasForeignKey(p => p.ProvinceId);

                    // Configure settlement => Population (one-to-one)
                    settlement.OwnsOne(s => s.Population, population =>
                    {
                        population.Property(p => p.Id).HasColumnName("Id");

                        // Configure Segments:
                        // NewBorns
                        population.OwnsOne(p => p.NewBornPopulation, newBorn =>
                        {
                            newBorn.Property(n => n.SegmentId).HasColumnName("SettlementId");
                            newBorn.Property(n => n.Buckets).HasColumnName("NewBornBuckets");
                        });

                        // Configure ChildrenPopulation
                        population.OwnsOne(p => p.ChildrenPopulation, children =>
                        {
                            children.Property(c => c.SegmentId).HasColumnName("ChildrenSegmentId");
                            children.Property(c => c.Buckets).HasColumnName("ChildrenBuckets");
                        });

                        // Configure YoungAdults
                        population.OwnsOne(p => p.YoungAdults, youngAdults =>
                        {
                            youngAdults.Property(y => y.SegmentId).HasColumnName("YoungAdultsSegmentId");
                            youngAdults.Property(y => y.Buckets).HasColumnName("YoungAdultsBuckets");
                        });

                        // Configure WorkingAdults
                        population.OwnsOne(p => p.WorkingAdults, workingAdults =>
                        {
                            workingAdults.Property(w => w.SegmentId).HasColumnName("WorkingAdultsSegmentId");
                            workingAdults.Property(w => w.Buckets).HasColumnName("WorkingAdultsBuckets");
                        });

                        // Configure Retired
                        population.OwnsOne(p => p.Retired, retired =>
                        {
                            retired.Property(r => r.SegmentId).HasColumnName("RetiredSegmentId");
                            retired.Property(r => r.Buckets).HasColumnName("RetiredBuckets");
                        });

                    });
                });
            });
            //modelBuilder.Entity<Province>()
            //.HasMany(p => p.Settlements)
            //.WithOne(s => s.Province)
            //.HasForeignKey(s => s.ProvinceId);

            //// Configure Settlement → Population (one-to-one)
            //modelBuilder.Entity<Settlement>()
            //    .HasOne(s => s.Population)
            //    .WithOne(p => p.Settlement)
            //    .HasForeignKey<SettlementPopulation>(p => p.SettlementId);



            //modelBuilder.Entity<SettlementPopulation>(entity =>
            //{
            //    // Configure NewBornPopulation
            //    entity.OwnsOne(p => p.NewBornPopulation, newborn =>
            //    {
            //        newborn.Property(n => n.SegmentId).HasColumnName("NewBornId");
            //        newborn.Property(n => n.Buckets).HasColumnName("NewBornBuckets");
            //    });

            //    // Configure ChildrenPopulation
            //    entity.OwnsOne(p => p.ChildrenPopulation, children =>
            //    {
            //        children.Property(c => c.SegmentId).HasColumnName("ChildrenId");
            //        children.Property(c => c.Buckets).HasColumnName("ChildrenBuckets");
            //    });

            //    // Configure YoungAdults
            //    entity.OwnsOne(p => p.YoungAdults, youngAdults =>
            //    {
            //        youngAdults.Property(y => y.SegmentId).HasColumnName("YoungAdultsId");
            //        youngAdults.Property(y => y.Buckets).HasColumnName("YoungAdultsBuckets");
            //    });

            //    // Configure WorkingAdults
            //    entity.OwnsOne(p => p.WorkingAdults, workingAdults =>
            //    {
            //        workingAdults.Property(w => w.SegmentId).HasColumnName("WorkingAdultsId");
            //        workingAdults.Property(w => w.Buckets).HasColumnName("WorkingAdultsBuckets");
            //    });

            //    // Configure Retired
            //    entity.OwnsOne(p => p.Retired, retired =>
            //    {
            //        retired.Property(r => r.SegmentId).HasColumnName("RetiredId");
            //        retired.Property(r => r.Buckets).HasColumnName("RetiredBuckets");
            //    });
            //});
        }

    }
}
