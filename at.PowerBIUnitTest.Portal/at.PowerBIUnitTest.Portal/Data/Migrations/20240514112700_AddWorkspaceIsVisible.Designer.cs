﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using at.PowerBIUnitTest.Portal.Data.Models;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    [DbContext(typeof(PortalDbContext))]
    [Migration("20240514112700_AddWorkspaceIsVisible")]
    partial class AddWorkspaceIsVisible
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TabularModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("MsId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Ms Id");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.Property<Guid>("UniqueIdentifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Unique Identifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("Workspace")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Workspace");

                    b.ToTable("TabularModel", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("MsId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("MS Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Tenant", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TestRun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ExecutedSuccessfully")
                        .HasColumnType("bit")
                        .HasColumnName("Executed Successfully");

                    b.Property<string>("ExpectedResult")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Expected Result");

                    b.Property<string>("JsonResponse")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Json Repsonse");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Result")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Result");

                    b.Property<int>("TestRunCollection")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("TimeStamp");

                    b.Property<Guid>("UniqueIdentifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Unique Identifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("UnitTest")
                        .HasColumnType("int");

                    b.Property<bool>("WasPassed")
                        .HasColumnType("bit")
                        .HasColumnName("WasPassed");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("TestRunCollection");

                    b.HasIndex("UnitTest");

                    b.ToTable("TestRun", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TestRunCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("TimeStamp");

                    b.Property<Guid>("UniqueIdentifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Unique Identifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<bool>("WasPassed")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("bit")
                        .HasColumnName("Result");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("TestRunCollection", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.UnitTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DAX")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(3000)")
                        .HasColumnName("DAX")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<string>("DateTimeFormat")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("DateTimeFormat");

                    b.Property<string>("DecimalPlaces")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("DecimalPlaces");

                    b.Property<string>("ExpectedResult")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Expected Result");

                    b.Property<string>("FloatSeparators")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("FloatSeparators");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.Property<string>("ResultType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UniqueIdentifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Unique Identifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("UserStory")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("UserStory");

                    b.HasIndex("Name", "UserStory")
                        .IsUnique();

                    b.ToTable("UnitTest", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Lastname")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("MsId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("MS Id");

                    b.Property<int>("Tenant")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Tenant");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.UserStory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.Property<int>("TabularModel")
                        .HasColumnType("int");

                    b.Property<Guid>("UniqueIdentifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Unique Identifier")
                        .HasDefaultValueSql("NEWID()");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("TabularModel");

                    b.ToTable("UserStory", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.Workspace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsVisible")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Is Visible");

                    b.Property<Guid>("MsId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Ms Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.Property<int?>("Tenant")
                        .HasColumnType("int");

                    b.Property<Guid>("UniqueIdentifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Unique Identifier")
                        .HasDefaultValueSql("NEWID()");

                    b.HasKey("Id");

                    b.HasIndex("Tenant");

                    b.ToTable("Workspace", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TabularModel", b =>
                {
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.Workspace", "WorkspaceNavigation")
                        .WithMany("TabularModels")
                        .HasForeignKey("Workspace")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkspaceNavigation");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TestRun", b =>
                {
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.User", "CreatedByNavigation")
                        .WithMany("TestRunCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_TestRun_Created_By");

                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.User", "ModifiedByNavigation")
                        .WithMany("TestRunModifiedByNavigations")
                        .HasForeignKey("ModifiedBy")
                        .HasConstraintName("FK_TestRun_Modified_By");

                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.TestRunCollection", "TestRunCollectionNavigation")
                        .WithMany("TestRuns")
                        .HasForeignKey("TestRunCollection");

                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.UnitTest", "UnitTestNavigation")
                        .WithMany("TestRuns")
                        .HasForeignKey("UnitTest")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("ModifiedByNavigation");

                    b.Navigation("TestRunCollectionNavigation");

                    b.Navigation("UnitTestNavigation");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TestRunCollection", b =>
                {
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.User", "CreatedByNavigation")
                        .WithMany("TestRunCollectionCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_TestRunCollection_Created_By");

                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.User", "ModifiedByNavigation")
                        .WithMany("TestRunCollectionModifiedByNavigations")
                        .HasForeignKey("ModifiedBy")
                        .HasConstraintName("FK_TestRunCollection_Modified_By");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("ModifiedByNavigation");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.UnitTest", b =>
                {
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.User", "CreatedByNavigation")
                        .WithMany("UnitTestCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_UnitTest_Created_By");

                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.User", "ModifiedByNavigation")
                        .WithMany("UnitTestModifiedByNavigations")
                        .HasForeignKey("ModifiedBy")
                        .HasConstraintName("FK_UnitTest_Modified_By");

                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.UserStory", "UserStoryNavigation")
                        .WithMany("UnitTests")
                        .HasForeignKey("UserStory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("ModifiedByNavigation");

                    b.Navigation("UserStoryNavigation");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.User", b =>
                {
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.Tenant", "TenantNavigation")
                        .WithMany("Users")
                        .HasForeignKey("Tenant")
                        .IsRequired();

                    b.Navigation("TenantNavigation");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.UserStory", b =>
                {
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.User", "CreatedByNavigation")
                        .WithMany("UserStoryCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_UserStory_Created_By");

                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.User", "ModifiedByNavigation")
                        .WithMany("UserStoryModifiedByNavigations")
                        .HasForeignKey("ModifiedBy")
                        .HasConstraintName("FK_UserStory_Modified_By");

                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.TabularModel", "TabularModelNavigation")
                        .WithMany("UserStories")
                        .HasForeignKey("TabularModel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("ModifiedByNavigation");

                    b.Navigation("TabularModelNavigation");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.Workspace", b =>
                {
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.Tenant", "TenantNavigation")
                        .WithMany("Workspaces")
                        .HasForeignKey("Tenant")
                        .HasConstraintName("FK_Workspace_Tenant");

                    b.Navigation("TenantNavigation");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TabularModel", b =>
                {
                    b.Navigation("UserStories");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.Tenant", b =>
                {
                    b.Navigation("Users");

                    b.Navigation("Workspaces");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TestRunCollection", b =>
                {
                    b.Navigation("TestRuns");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.UnitTest", b =>
                {
                    b.Navigation("TestRuns");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.User", b =>
                {
                    b.Navigation("TestRunCollectionCreatedByNavigations");

                    b.Navigation("TestRunCollectionModifiedByNavigations");

                    b.Navigation("TestRunCreatedByNavigations");

                    b.Navigation("TestRunModifiedByNavigations");

                    b.Navigation("UnitTestCreatedByNavigations");

                    b.Navigation("UnitTestModifiedByNavigations");

                    b.Navigation("UserStoryCreatedByNavigations");

                    b.Navigation("UserStoryModifiedByNavigations");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.UserStory", b =>
                {
                    b.Navigation("UnitTests");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.Workspace", b =>
                {
                    b.Navigation("TabularModels");
                });
#pragma warning restore 612, 618
        }
    }
}
