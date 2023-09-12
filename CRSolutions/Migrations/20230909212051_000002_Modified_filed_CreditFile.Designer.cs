﻿// <auto-generated />
using System;
using CRSolutions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRSolutions.Migrations
{
    [DbContext(typeof(CRSolutionsDBContext))]
    [Migration("20230909212051_000002_Modified_filed_CreditFile")]
    partial class _000002_Modified_filed_CreditFile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CRSolutions.Models.Candidate", b =>
                {
                    b.Property<Guid>("IdCantidate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AudioFile")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("VARCHAR(600)");

                    b.Property<string>("BlackList")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CURP")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR(300)");

                    b.Property<string>("CreditFile")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("VARCHAR(600)");

                    b.Property<string>("EvaluatedPosition")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR(300)");

                    b.Property<DateTime>("EvaluationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR(300)");

                    b.Property<Guid>("IdCompany")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IdRiskScore")
                        .HasColumnType("int");

                    b.Property<int>("IdTypeTest")
                        .HasColumnType("int");

                    b.Property<Guid?>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RecordEvaluation")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR(300)");

                    b.Property<string>("ReportFile")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("VARCHAR(600)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("IdCantidate");

                    b.HasIndex("IdCompany");

                    b.HasIndex("IdUser");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("CRSolutions.Models.Company", b =>
                {
                    b.Property<Guid>("IdCompany")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR(300)");

                    b.Property<string>("CompanyDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR(500)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR(300)");

                    b.Property<bool>("Credits")
                        .HasColumnType("bit");

                    b.Property<string>("RFC")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("IdCompany");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CRSolutions.Models.Role", b =>
                {
                    b.Property<Guid>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR(500)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("IdRol");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CRSolutions.Models.User", b =>
                {
                    b.Property<Guid>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR(300)");

                    b.Property<Guid>("IdCompany")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdRol")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("IdUser");

                    b.HasIndex("IdCompany");

                    b.HasIndex("IdRol");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CRSolutions.Models.Candidate", b =>
                {
                    b.HasOne("CRSolutions.Models.Company", "Company")
                        .WithMany("Candidates")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRSolutions.Models.User", "User")
                        .WithMany("Candidates")
                        .HasForeignKey("IdUser");

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRSolutions.Models.User", b =>
                {
                    b.HasOne("CRSolutions.Models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRSolutions.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CRSolutions.Models.Company", b =>
                {
                    b.Navigation("Candidates");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CRSolutions.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CRSolutions.Models.User", b =>
                {
                    b.Navigation("Candidates");
                });
#pragma warning restore 612, 618
        }
    }
}
