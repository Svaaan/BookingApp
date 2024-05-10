﻿// <auto-generated />
using System;
using Booking.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Booking.Api.Migrations
{
    [DbContext(typeof(CinemaDbContext))]
    [Migration("20240509202227_Merge")]
    partial class Merge
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Booking.Api.Entities.Booker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BookingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("bookers");
                });

            modelBuilder.Entity("Booking.Api.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("company");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Adress = "Testgatan 123b",
                            CompanyName = "TestCompany",
                            Country = "Sverige",
                            Email = "Test@mail.com"
                        });
                });

            modelBuilder.Entity("Booking.Api.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgeRestriction")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxShows")
                        .HasColumnType("int");

                    b.Property<int>("Minutes")
                        .HasColumnType("int");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("Subtitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("movies");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            AgeRestriction = 15,
                            Description = "The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son",
                            Director = "Francis Ford Coppola",
                            Genre = "Crime",
                            Hours = 2,
                            Language = "English",
                            MaxShows = 5,
                            Minutes = 55,
                            ReleaseYear = 1972,
                            Subtitle = "Swedish",
                            Title = "The Godfather"
                        });
                });

            modelBuilder.Entity("Booking.Api.Entities.MovieTheatre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("movieTheatres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Adress = "Biogatan 12a",
                            CompanyId = 1,
                            Name = "TestTheatre"
                        });
                });

            modelBuilder.Entity("Booking.Api.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookedSeats")
                        .HasColumnType("int");

                    b.Property<int>("BookerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookerId");

                    b.HasIndex("ShowId");

                    b.ToTable("reservations");
                });

            modelBuilder.Entity("Booking.Api.Entities.Salon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<int>("MovieTheatreId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieTheatreId");

                    b.ToTable("salons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableSeats = 30,
                            MovieTheatreId = 1,
                            Name = "Salon 1",
                            Status = 0
                        });
                });

            modelBuilder.Entity("Booking.Api.Entities.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerSeat")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("VAT")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("SalonId");

                    b.ToTable("shows");
                });

            modelBuilder.Entity("Booking.Api.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = 1,
                            Email = "john@example.com",
                            HashedPassword = "BizPDsh69n2K6oTCB0//QzdkPxJsgvX0A29+SL9UVqY=",
                            LastName = "Doe",
                            Name = "John",
                            Password = "password",
                            Role = "Admin",
                            Salt = new byte[] { 222, 68, 180, 109, 146, 55, 206, 161, 246, 169, 1, 76, 123, 173, 32, 40, 73, 108, 3, 182, 168, 207, 163, 52, 162, 133, 220, 197, 134, 209, 107, 95 }
                        },
                        new
                        {
                            Id = 2,
                            CompanyId = 1,
                            Email = "tess@example.com",
                            HashedPassword = "BizPDsh69n2K6oTCB0//QzdkPxJsgvX0A29+SL9UVqY=",
                            LastName = "Doe",
                            Name = "Tess",
                            Password = "password",
                            Role = "User",
                            Salt = new byte[] { 222, 68, 180, 109, 146, 55, 206, 161, 246, 169, 1, 76, 123, 173, 32, 40, 73, 108, 3, 182, 168, 207, 163, 52, 162, 133, 220, 197, 134, 209, 107, 95 }
                        },
                        new
                        {
                            Id = 3,
                            CompanyId = 1,
                            Email = "Richard@example.com",
                            HashedPassword = "BizPDsh69n2K6oTCB0//QzdkPxJsgvX0A29+SL9UVqY=",
                            LastName = "Doe",
                            Name = "Richard",
                            Password = "password",
                            Role = "Manager",
                            Salt = new byte[] { 222, 68, 180, 109, 146, 55, 206, 161, 246, 169, 1, 76, 123, 173, 32, 40, 73, 108, 3, 182, 168, 207, 163, 52, 162, 133, 220, 197, 134, 209, 107, 95 }
                        });
                });

            modelBuilder.Entity("Booking.Api.Entities.MovieTheatre", b =>
                {
                    b.HasOne("Booking.Api.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Booking.Api.Entities.Reservation", b =>
                {
                    b.HasOne("Booking.Api.Entities.Booker", "Booker")
                        .WithMany()
                        .HasForeignKey("BookerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Booking.Api.Entities.Show", "Show")
                        .WithMany("Reservations")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booker");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Booking.Api.Entities.Salon", b =>
                {
                    b.HasOne("Booking.Api.Entities.MovieTheatre", "MovieTheatre")
                        .WithMany()
                        .HasForeignKey("MovieTheatreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieTheatre");
                });

            modelBuilder.Entity("Booking.Api.Entities.Show", b =>
                {
                    b.HasOne("Booking.Api.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Booking.Api.Entities.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("Booking.Api.Entities.User", b =>
                {
                    b.HasOne("Booking.Api.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Booking.Api.Entities.Show", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
