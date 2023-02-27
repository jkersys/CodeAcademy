﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UTP_Web_API.Database;

#nullable disable

namespace UTPWebAPI.Migrations
{
    [DbContext(typeof(UtpContext))]
    [Migration("20230112124650_ChangedInvestiationRelationshipsTest")]
    partial class ChangedInvestiationRelationshipsTest
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("AdministrativeInspectionInvestigator", b =>
                {
                    b.Property<int>("AdministrativeInspectionsAdministrativeInspectionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InvestigatorsInvestigatorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AdministrativeInspectionsAdministrativeInspectionId", "InvestigatorsInvestigatorId");

                    b.HasIndex("InvestigatorsInvestigatorId");

                    b.ToTable("AdministrativeInspectionInvestigator");
                });

            modelBuilder.Entity("InvestigationInvestigator", b =>
                {
                    b.Property<int>("InvestigationsInvestigationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InvestigatorsInvestigatorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InvestigationsInvestigationId", "InvestigatorsInvestigatorId");

                    b.HasIndex("InvestigatorsInvestigatorId");

                    b.ToTable("InvestigationInvestigator");
                });

            modelBuilder.Entity("UTP_Web_API.Models.AdministrativeInspection", b =>
                {
                    b.Property<int>("AdministrativeInspectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConclusionId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.HasKey("AdministrativeInspectionId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ConclusionId");

                    b.ToTable("AdministrativeInspection");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyAdress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CompanyEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CompanyPhone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CompanyRegistrationNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("CompanyId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Complain", b =>
                {
                    b.Property<int>("ComplainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyInformation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ConclusionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("InvestigatorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocalUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ComplainId");

                    b.HasIndex("ConclusionId");

                    b.HasIndex("InvestigatorId");

                    b.HasIndex("LocalUserId");

                    b.ToTable("Complain");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Conclusion", b =>
                {
                    b.Property<int>("ConclusionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Decision")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ConclusionId");

                    b.ToTable("Conclusion");

                    b.HasData(
                        new
                        {
                            ConclusionId = 1,
                            Decision = "Skundas atmestas"
                        },
                        new
                        {
                            ConclusionId = 2,
                            Decision = "Skundas priimtas"
                        },
                        new
                        {
                            ConclusionId = 3,
                            Decision = "Pažeidimų nenustatyta"
                        },
                        new
                        {
                            ConclusionId = 4,
                            Decision = "Nutraukta dėl mažareikšmiškumo"
                        },
                        new
                        {
                            ConclusionId = 5,
                            Decision = "Nustatyti pažeidimai, byla perduota komisijai"
                        });
                });

            modelBuilder.Entity("UTP_Web_API.Models.Investigation", b =>
                {
                    b.Property<int>("InvestigationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ConclusionId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("InvestigatorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LegalBase")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Penalty")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("InvestigationId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ConclusionId");

                    b.ToTable("Investigation");
                });

            modelBuilder.Entity("UTP_Web_API.Models.InvestigationStage", b =>
                {
                    b.Property<int>("InvestigationStageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AdministrativeInspectionId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ComplainId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InvestigationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.HasKey("InvestigationStageId");

                    b.HasIndex("AdministrativeInspectionId");

                    b.HasIndex("ComplainId");

                    b.HasIndex("InvestigationId");

                    b.ToTable("InvestigationStage");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Investigator", b =>
                {
                    b.Property<int>("InvestigatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CabinetNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CertificationId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LocalUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WorkAdress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("InvestigatorId");

                    b.HasIndex("LocalUserId")
                        .IsUnique();

                    b.ToTable("Investigator");
                });

            modelBuilder.Entity("UTP_Web_API.Models.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<long>("PhoneNumber")
                        .HasMaxLength(30)
                        .HasColumnType("INTEGER");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("LocalUser");
                });

            modelBuilder.Entity("AdministrativeInspectionInvestigator", b =>
                {
                    b.HasOne("UTP_Web_API.Models.AdministrativeInspection", null)
                        .WithMany()
                        .HasForeignKey("AdministrativeInspectionsAdministrativeInspectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UTP_Web_API.Models.Investigator", null)
                        .WithMany()
                        .HasForeignKey("InvestigatorsInvestigatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InvestigationInvestigator", b =>
                {
                    b.HasOne("UTP_Web_API.Models.Investigation", null)
                        .WithMany()
                        .HasForeignKey("InvestigationsInvestigationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UTP_Web_API.Models.Investigator", null)
                        .WithMany()
                        .HasForeignKey("InvestigatorsInvestigatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UTP_Web_API.Models.AdministrativeInspection", b =>
                {
                    b.HasOne("UTP_Web_API.Models.Company", "Company")
                        .WithMany("AdministrativeInspections")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UTP_Web_API.Models.Conclusion", "Conclusion")
                        .WithMany("AdministrativeInspections")
                        .HasForeignKey("ConclusionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Conclusion");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Complain", b =>
                {
                    b.HasOne("UTP_Web_API.Models.Conclusion", "Conclusion")
                        .WithMany("Complains")
                        .HasForeignKey("ConclusionId");

                    b.HasOne("UTP_Web_API.Models.Investigator", "Investigator")
                        .WithMany("Complains")
                        .HasForeignKey("InvestigatorId");

                    b.HasOne("UTP_Web_API.Models.LocalUser", "LocalUser")
                        .WithMany("Complains")
                        .HasForeignKey("LocalUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conclusion");

                    b.Navigation("Investigator");

                    b.Navigation("LocalUser");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Investigation", b =>
                {
                    b.HasOne("UTP_Web_API.Models.Company", "Company")
                        .WithMany("Investigations")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UTP_Web_API.Models.Conclusion", "Conclusion")
                        .WithMany("Investigations")
                        .HasForeignKey("ConclusionId");

                    b.Navigation("Company");

                    b.Navigation("Conclusion");
                });

            modelBuilder.Entity("UTP_Web_API.Models.InvestigationStage", b =>
                {
                    b.HasOne("UTP_Web_API.Models.AdministrativeInspection", null)
                        .WithMany("InvestigationStages")
                        .HasForeignKey("AdministrativeInspectionId");

                    b.HasOne("UTP_Web_API.Models.Complain", "Complain")
                        .WithMany("Stages")
                        .HasForeignKey("ComplainId");

                    b.HasOne("UTP_Web_API.Models.Investigation", null)
                        .WithMany("Stages")
                        .HasForeignKey("InvestigationId");

                    b.Navigation("Complain");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Investigator", b =>
                {
                    b.HasOne("UTP_Web_API.Models.LocalUser", "LocalUser")
                        .WithOne("Investigator")
                        .HasForeignKey("UTP_Web_API.Models.Investigator", "LocalUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocalUser");
                });

            modelBuilder.Entity("UTP_Web_API.Models.AdministrativeInspection", b =>
                {
                    b.Navigation("InvestigationStages");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Company", b =>
                {
                    b.Navigation("AdministrativeInspections");

                    b.Navigation("Investigations");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Complain", b =>
                {
                    b.Navigation("Stages");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Conclusion", b =>
                {
                    b.Navigation("AdministrativeInspections");

                    b.Navigation("Complains");

                    b.Navigation("Investigations");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Investigation", b =>
                {
                    b.Navigation("Stages");
                });

            modelBuilder.Entity("UTP_Web_API.Models.Investigator", b =>
                {
                    b.Navigation("Complains");
                });

            modelBuilder.Entity("UTP_Web_API.Models.LocalUser", b =>
                {
                    b.Navigation("Complains");

                    b.Navigation("Investigator")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
