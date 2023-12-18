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

        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Workspace> Workspaces { get; set; }
        public virtual DbSet<TabularModel> TabularModels {get; set;}
        public virtual DbSet<UserStory> UserStories {get; set;}
        public virtual DbSet<UnitTest> UnitTests {get; set;}
        public virtual DbSet<History> Histories {get; set;}
        public virtual DbSet<TestRuns> TestRuns {get; set;}
        public Guid MsIdCurrentUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenant");

                entity.Property(e => e.MsId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("MS Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MsId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("MS Id");

                entity.HasOne(d => d.TenantNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Tenant)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Workspace>(entity =>
            {
                entity.ToTable("Workspace");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Id");

                   entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Name");

                    entity.Property(e => e.WorkspacePbId)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("WorkspacePbId");
            });

            modelBuilder.Entity<UserStory>(entity =>
            {
                entity.ToTable("UserStory");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Id");

                  entity.Property(e => e.Beschreibung)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Beschreibung"); 

                   entity.HasOne(d => d.TabularModelNavigation)
                    .WithMany(p => p.UserStories)
                    .HasForeignKey(d => d.TabularModel)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UnitTest>(entity =>
            {
                entity.ToTable("UnitTest");
                
                
                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Id");

                  entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Name"); 

                   entity.Property(e => e.ExpectedResult)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("ExpectedResult");

                   entity.Property(e => e.LastResult)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("LastResult");

                   entity.Property(e => e.Timestamp)
                   .HasDefaultValue("Error")
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("LastRun");

                   entity.Property(e => e.DateTimeFormat)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("DateTimeFormat");

                   entity.Property(e => e.DecimalPlaces)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("DecimalPlaces");

                   entity.Property(e => e.FloatSeparators)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("FloatSeparators");

                   entity.Property(e => e.DAX)
                   .IsRequired()
                   .HasMaxLength(3000)
                   .IsUnicode(false)
                   .HasColumnName("DAX")
                    .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8")
                    .IsUnicode();

                   entity.HasOne(d => d.UserStoryNavigation)
                    .WithMany(p => p.UnitTests)
                    .HasForeignKey(d => d.UserStory)
                    .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(d => d.ResultTypeNavigation)
                    .WithMany(p => p.UnitTests)
                    .HasForeignKey(d => d.ResultType)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                    entity.HasIndex(p => new {p.Name , p.UserStory}).IsUnique();
                
            });

            modelBuilder.Entity<ResultType>(entity =>
            {
                entity.ToTable("ResultType");

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Name");

                entity.HasData(new ResultType{Name = "Date"});
                entity.HasData(new ResultType{Name = "Float"});
                entity.HasData(new ResultType{Name = "Percentage"});
                entity.HasData(new ResultType{Name = "String"});
            });

            modelBuilder.Entity<TabularModel>(entity =>
            {
                entity.ToTable("TabularModel");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Id");

                  entity.Property(e => e.DatasetPbId)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("DatasetPbId"); 

                   entity.Property(e => e.Name)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Name");

                   entity.HasOne(d => d.WorkspaceNavigation)
                    .WithMany(p => p.TabularModels)
                    .HasForeignKey(d => d.Workspace)
                    .OnDelete(DeleteBehavior.Cascade);
            });


             modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Id");

                  entity.Property(e => e.Result)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Result");

                   entity.Property(e => e.TimeStamp)
                   .IsRequired()
                   .IsUnicode(false)
                   . HasColumnName("TimeStamp");

                   entity.Property(e => e.LastRun)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("LastRun");

                   entity.Property(e => e.ExpectedRun)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("ExpectedRun");

                   entity.HasOne(d => d.UnitTestNavigation)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.UnitTest)
                    .OnDelete(DeleteBehavior.Cascade);

                    
                   entity.HasOne(d => d.TestRunNavigation)
                    .WithMany(p => p.HistoriesRun)
                    .HasForeignKey(d => d.TestRun)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired(false);
                    

                    
            });

             modelBuilder.Entity<TestRuns>(entity =>
            {
                entity.ToTable("TestRuns");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Id");

                  entity.Property(e => e.Result)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Result");

                   entity.Property(e => e.TimeStamp)
                   .IsRequired()
                   .IsUnicode(false)
                   . HasColumnName("TimeStamp");

                   entity.Property(e => e.Workspace)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Workspace");

                   entity.Property(e => e.Count)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Count");

                     entity.Property(e => e.TabularModel)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("TabularModel");

                    entity.Property(e => e.UserStory)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("UserStory");

                    entity.Property(e => e.Type)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("Type");

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
