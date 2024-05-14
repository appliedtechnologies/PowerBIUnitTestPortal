using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class PortalDbContext : DbContext
    {
        public PortalDbContext()
        {
        }

        public PortalDbContext(DbContextOptions<PortalDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TabularModel> TabularModels { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<TestRun> TestRuns { get; set; }
        public virtual DbSet<TestRunCollection> TestRunCollections { get; set; }
        public virtual DbSet<UnitTest> UnitTests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserStory> UserStories { get; set; }
        public virtual DbSet<Workspace> Workspaces { get; set; }

        public Guid MsIdCurrentUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TabularModel>(entity =>
            {
                entity.ToTable("TabularModel");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Id");

                entity.Property(e => e.UniqueIdentifier)
                 .HasDefaultValueSql("NEWID()")
                 .IsRequired()
                 .HasColumnName("Unique Identifier");

                entity.Property(e => e.MsId)
                 .IsRequired()
                 .HasColumnName("Ms Id");

                entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");

                entity.HasOne(d => d.WorkspaceNavigation)
                 .WithMany(p => p.TabularModels)
                 .HasForeignKey(d => d.Workspace)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenant");

                entity.Property(e => e.MsId)
                    .IsRequired()
                    .HasColumnName("MS Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TestRun>(entity =>
            {
                entity.ToTable("TestRun");

                entity.Property(e => e.UniqueIdentifier)
                 .HasDefaultValueSql("NEWID()")
                 .IsRequired()
                 .HasColumnName("Unique Identifier");

                entity.Property(e => e.WasPassed)
                 .IsRequired()
                 .HasColumnName("WasPassed");

                entity.Property(e => e.TimeStamp)
                 .IsRequired()
                 .HasColumnName("TimeStamp");

                entity.Property(e => e.Result)
                 .HasMaxLength(255)
                 .HasColumnName("Result");

                entity.Property(e => e.ExpectedResult)
                 .IsRequired()
                 .HasMaxLength(255)
                 .HasColumnName("Expected Result");

                entity.Property(e => e.JsonResponse)
                 .HasColumnName("Json Repsonse");

                entity.Property(e => e.ExecutedSuccessfully)
                 .HasColumnName("Executed Successfully");

                entity.HasOne(d => d.UnitTestNavigation)
                 .WithMany(p => p.TestRuns)
                 .HasForeignKey(d => d.UnitTest)
                 .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.TestRunCollectionNavigation)
                 .WithMany(p => p.TestRuns)
                 .HasForeignKey(d => d.TestRunCollection)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .IsRequired(false);

                entity.HasOne(d => d.CreatedByNavigation)
                 .WithMany(p => p.TestRunCreatedByNavigations)
                 .HasForeignKey(d => d.CreatedBy)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_TestRun_Created_By");

                entity.HasOne(d => d.ModifiedByNavigation)
                 .WithMany(p => p.TestRunModifiedByNavigations)
                 .HasForeignKey(d => d.ModifiedBy)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_TestRun_Modified_By");

            });

            modelBuilder.Entity<TestRunCollection>(entity =>
            {
                entity.ToTable("TestRunCollection");

                entity.Property(e => e.UniqueIdentifier)
                 .HasDefaultValueSql("NEWID()")
                 .IsRequired()
                 .HasColumnName("Unique Identifier");

                entity.Property(e => e.WasPassed)
                 .IsRequired()
                 .HasMaxLength(255)
                 .IsUnicode(false)
                 .HasColumnName("Result");

                entity.Property(e => e.TimeStamp)
                 .IsRequired()
                 .HasColumnName("TimeStamp");

                entity.HasOne(d => d.CreatedByNavigation)
                 .WithMany(p => p.TestRunCollectionCreatedByNavigations)
                 .HasForeignKey(d => d.CreatedBy)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_TestRunCollection_Created_By");

                entity.HasOne(d => d.ModifiedByNavigation)
                 .WithMany(p => p.TestRunCollectionModifiedByNavigations)
                 .HasForeignKey(d => d.ModifiedBy)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_TestRunCollection_Modified_By");
            });

            modelBuilder.Entity<UnitTest>(entity =>
            {
                entity.ToTable("UnitTest");

                entity.HasIndex(p => new { p.Name, p.UserStory })
                 .IsUnique();

                entity.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(255)
                 .HasColumnName("Name");

                entity.Property(e => e.UniqueIdentifier)
                 .HasDefaultValueSql("NEWID()")
                 .IsRequired()
                 .HasColumnName("Unique Identifier");

                entity.Property(e => e.ExpectedResult)
                 .IsRequired()
                 .HasMaxLength(255)
                 .HasColumnName("Expected Result");

                entity.Property(e => e.DateTimeFormat)
                 .HasMaxLength(255)
                 .HasColumnName("DateTimeFormat");

                entity.Property(e => e.DecimalPlaces)
                 .HasMaxLength(255)
                 .HasColumnName("DecimalPlaces");

                entity.Property(e => e.FloatSeparators)
                 .HasMaxLength(255)
                 .HasColumnName("FloatSeparators");

                entity.Property(e => e.DAX)
                 .IsRequired()
                 .HasMaxLength(3000)
                 .HasColumnName("DAX")
                 .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8")
                 .IsUnicode();

                entity.HasOne(d => d.UserStoryNavigation)
                 .WithMany(p => p.UnitTests)
                 .HasForeignKey(d => d.UserStory)
                 .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.CreatedByNavigation)
                 .WithMany(p => p.UnitTestCreatedByNavigations)
                 .HasForeignKey(d => d.CreatedBy)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_UnitTest_Created_By")
                 .IsRequired(false);

                entity.HasOne(d => d.ModifiedByNavigation)
                 .WithMany(p => p.UnitTestModifiedByNavigations)
                 .HasForeignKey(d => d.ModifiedBy)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_UnitTest_Modified_By")
                 .IsRequired(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255);

                entity.Property(e => e.MsId)
                    .IsRequired()
                    .HasColumnName("MS Id");

                entity.HasOne(d => d.TenantNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Tenant)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UserStory>(entity =>
            {
                entity.ToTable("UserStory");

                entity.Property(e => e.UniqueIdentifier)
                 .HasDefaultValueSql("NEWID()")
                .IsRequired()
                .HasColumnName("Unique Identifier");

                entity.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(255)
                 .HasColumnName("Name");

                entity.HasOne(d => d.TabularModelNavigation)
                 .WithMany(p => p.UserStories)
                 .HasForeignKey(d => d.TabularModel)
                 .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.CreatedByNavigation)
                 .WithMany(p => p.UserStoryCreatedByNavigations)
                 .HasForeignKey(d => d.CreatedBy)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_UserStory_Created_By")
                 .IsRequired(false);

                entity.HasOne(d => d.ModifiedByNavigation)
                 .WithMany(p => p.UserStoryModifiedByNavigations)
                 .HasForeignKey(d => d.ModifiedBy)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_UserStory_Modified_By")
                 .IsRequired(false);
            });

            modelBuilder.Entity<Workspace>(entity =>
            {
                entity.ToTable("Workspace");

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("Name");

                entity.Property(e => e.UniqueIdentifier)
                 .HasDefaultValueSql("NEWID()")
                 .IsRequired()
                 .HasColumnName("Unique Identifier");

                entity.Property(e => e.MsId)
                .IsRequired()
                .HasColumnName("Ms Id");

                entity.Property(e => e.IsVisible)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("Is Visible");

                entity.HasOne(d => d.TenantNavigation)
                 .WithMany(p => p.Workspaces)
                 .HasForeignKey(d => d.Tenant)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Workspace_Tenant")
                 .IsRequired(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected virtual void OnBeforeSaving(Guid msIdCurrentUser = default)
        {
            this.ChangeTracker.DetectChanges();

            var added = this.ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added && e.Entity is ITrackCreated)
                        .Select(e => e.Entity);

            var modified = this.ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Modified && e.Entity is ITrackModified)
                        .Select(e => e.Entity);

            if (added.Count() > 0 || modified.Count() > 0)
            {
                User currentUser;
                if (msIdCurrentUser != default)
                {
                    currentUser = this.Users.First(e => e.MsId == msIdCurrentUser);

                }
                else
                {
                    currentUser = this.Users.First(e => e.MsId == this.MsIdCurrentUser);
                }
                foreach (ITrackCreated entity in added)
                {
                    entity.CreatedOn = DateTime.Now;
                    entity.CreatedBy = currentUser.Id;
                }

                foreach (ITrackModified entity in modified.Union(added.Where(e => e is ITrackModified).Cast<ITrackModified>()))
                {
                    entity.ModifiedOn = DateTime.Now;
                    entity.ModifiedBy = currentUser.Id;
                }

            }
        }

        public override int SaveChanges()
        {
            this.OnBeforeSaving();
            return base.SaveChanges();
        }
        public int SaveChanges(Guid msIdCurrentUser)
        {
            this.OnBeforeSaving(msIdCurrentUser);
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            this.OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public Task<int> SaveChangesAsync(Guid msIdCurrentUser, bool acceptAllChangesOnSuccess = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            this.OnBeforeSaving(msIdCurrentUser);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
