﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.ModelsDb;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Models.Migrations
{
    [DbContext(typeof(AdvertBoardDbContext))]
    partial class AdvertBoardDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.ModelsDb.AdvertDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_creation");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expiration_date");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<Guid?>("UserDbId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserDbId");

                    b.ToTable("advert");
                });

            modelBuilder.Entity("Models.ModelsDb.UserDb", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean")
                        .HasColumnName("is_admin");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Models.ModelsDb.AdvertDb", b =>
                {
                    b.HasOne("Models.ModelsDb.UserDb", null)
                        .WithMany("AdvertCollection")
                        .HasForeignKey("UserDbId");
                });

            modelBuilder.Entity("Models.ModelsDb.UserDb", b =>
                {
                    b.Navigation("AdvertCollection");
                });
#pragma warning restore 612, 618
        }
    }
}
