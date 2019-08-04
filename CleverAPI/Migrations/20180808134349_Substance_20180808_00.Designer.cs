﻿// <auto-generated />
using CleverAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CleverAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180808134349_Substance_20180808_00")]
    partial class Substance_20180808_00
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("CleverAPI.Models.AirContaminant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("ApproximateSafeExposureLevel");

                    b.Property<int?>("HazardClass");

                    b.Property<decimal?>("MaximumPermissibleConcentrationDailyAverage");

                    b.Property<decimal?>("MaximumPermissibleConcentrationOneTimeMaximum");

                    b.Property<string>("Name");

                    b.Property<string>("NumberCAS");

                    b.HasKey("Id");

                    b.ToTable("AirContaminant");
                });

            modelBuilder.Entity("CleverAPI.Models.AirContaminantKK", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("ApproximateSafeExposureLevel");

                    b.Property<int?>("Code");

                    b.Property<string>("Formula");

                    b.Property<int?>("HazardClass");

                    b.Property<decimal?>("MaximumPermissibleConcentrationDailyAverage");

                    b.Property<decimal?>("MaximumPermissibleConcentrationOneTimeMaximum");

                    b.Property<string>("Name");

                    b.Property<string>("NumberCAS");

                    b.HasKey("Id");

                    b.ToTable("AirContaminantKK");
                });

            modelBuilder.Entity("CleverAPI.Models.AirContaminantRU", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("ApproximateSafeExposureLevel");

                    b.Property<int?>("Code");

                    b.Property<string>("Formula");

                    b.Property<int?>("HazardClass");

                    b.Property<decimal?>("MaximumPermissibleConcentrationDailyAverage");

                    b.Property<decimal?>("MaximumPermissibleConcentrationOneTimeMaximum");

                    b.Property<string>("Name");

                    b.Property<string>("NumberCAS");

                    b.HasKey("Id");

                    b.ToTable("AirContaminantRU");
                });

            modelBuilder.Entity("CleverAPI.Models.CATO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AreaType");

                    b.Property<string>("Code");

                    b.Property<int?>("EgovId");

                    b.Property<DateTime?>("ExpiredDateTime");

                    b.Property<string>("NameKK");

                    b.Property<string>("NameRU");

                    b.Property<int?>("Parent");

                    b.HasKey("Id");

                    b.ToTable("CATO");
                });

            modelBuilder.Entity("CleverAPI.Models.CompanyKK", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityKindKK");

                    b.Property<string>("ActivityKindRU");

                    b.Property<string>("BIN");

                    b.Property<string>("CATO");

                    b.Property<DateTime?>("DateRegister");

                    b.Property<DateTime?>("ExpiredDateTime");

                    b.Property<string>("HeadName");

                    b.Property<string>("KRP");

                    b.Property<string>("KRPNameKK");

                    b.Property<string>("KRPNameRU");

                    b.Property<string>("LegalAddress");

                    b.Property<string>("LocalityKK");

                    b.Property<string>("LocalityRU");

                    b.Property<string>("NameKK");

                    b.Property<string>("NameRU");

                    b.Property<string>("OKED");

                    b.Property<string>("OKEDSecondary");

                    b.HasKey("Id");

                    b.HasIndex("NameRU");

                    b.ToTable("CompanyKK");
                });

            modelBuilder.Entity("CleverAPI.Models.Substance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("NameEN");

                    b.Property<string>("NameKK");

                    b.Property<string>("NameRU");

                    b.HasKey("Id");

                    b.ToTable("Substance");
                });
#pragma warning restore 612, 618
        }
    }
}
