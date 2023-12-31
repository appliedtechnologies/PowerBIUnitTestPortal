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
    [Migration("20220909114532_AddName+Ergebnis3")]
    partial class AddNameErgebnis3
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

                    b.Property<string>("DatasetPbId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("DatasetPbId");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Name");

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
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("MS Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Tenant", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.UnitTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DAX")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("DAX");

                    b.Property<string>("ErwartetesErgebnis")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Erwartetes Ergebnis");

                    b.Property<string>("LetztesErgebnis")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValue("Error")
                        .HasColumnName("Letztes Ergebnis");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Name");

                    b.Property<string>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValue("Error")
                        .HasColumnName("Letzter Test");

                    b.Property<int>("UserStory")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserStory");

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
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Lastname")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("MsId")
                        .HasMaxLength(36)
                        .IsUnicode(false)
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
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Beschreibung")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Beschreibung");

                    b.Property<int>("TabularModel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TabularModel");

                    b.ToTable("UserStory", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.Workspace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Name");

                    b.Property<string>("WorkspacePbId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("WorkspacePbId");

                    b.HasKey("Id");

                    b.ToTable("Workspace", (string)null);
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TabularModel", b =>
                {
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.Workspace", "WorkspaceNavigation")
                        .WithMany("TabularModels")
                        .HasForeignKey("Workspace")
                        .IsRequired();

                    b.Navigation("WorkspaceNavigation");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.UnitTest", b =>
                {
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.UserStory", "UserStoryNavigation")
                        .WithMany("UnitTests")
                        .HasForeignKey("UserStory")
                        .IsRequired();

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
                    b.HasOne("at.PowerBIUnitTest.Portal.Data.Models.TabularModel", "TabularModelNavigation")
                        .WithMany("UserStories")
                        .HasForeignKey("TabularModel")
                        .IsRequired();

                    b.Navigation("TabularModelNavigation");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.TabularModel", b =>
                {
                    b.Navigation("UserStories");
                });

            modelBuilder.Entity("at.PowerBIUnitTest.Portal.Data.Models.Tenant", b =>
                {
                    b.Navigation("Users");
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
