﻿// <auto-generated />
using ATMSimulation.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ATMSimulation.API.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20230921042458_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ATMSimulation.API.User", b =>
                {
                    b.Property<string>("CardNumber")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PIN")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.HasKey("CardNumber");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            CardNumber = "12345678955555",
                            Balance = 8000,
                            Name = "Mahmoud Elish",
                            PIN = "123456"
                        },
                        new
                        {
                            CardNumber = "56789012345678",
                            Balance = 2500,
                            Name = "Ali",
                            PIN = "666666"
                        },
                        new
                        {
                            CardNumber = "12345678901234",
                            Balance = 9100,
                            Name = "Radwa",
                            PIN = "000000"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}