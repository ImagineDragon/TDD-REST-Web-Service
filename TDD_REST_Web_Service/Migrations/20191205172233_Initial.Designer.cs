﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TDD_REST_Web_Service.Models;

namespace TDD_REST_Web_Service.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20191205172233_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TDD_REST_Web_Service.Models.DefaultModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Field1");

                    b.Property<int>("Field2");

                    b.Property<string>("Field3");

                    b.Property<int>("Field4");

                    b.HasKey("Id");

                    b.ToTable("DefaultModels");
                });
#pragma warning restore 612, 618
        }
    }
}