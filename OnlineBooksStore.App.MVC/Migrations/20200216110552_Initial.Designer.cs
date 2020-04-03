﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineBooksStore.Persistence.EF;
using OnlineBooksStore.Persistence.EF.Mvc;

namespace OnlineBooksStore.App.MVC.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200216110552_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineBooksStore.Domain.Contracts.Entities.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Authors");

                    b.Property<string>("BookCover");

                    b.Property<long?>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Language");

                    b.Property<int>("PageCount");

                    b.Property<long?>("PublisherId");

                    b.Property<decimal>("PurchasePrice");

                    b.Property<decimal>("RetailPrice");

                    b.Property<string>("Title");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("OnlineBooksStore.Domain.Contracts.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameEng");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("OnlineBooksStore.Domain.Contracts.Entities.Publisher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("OnlineBooksStore.Domain.Contracts.Entities.Book", b =>
                {
                    b.HasOne("OnlineBooksStore.Domain.Contracts.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("OnlineBooksStore.Domain.Contracts.Entities.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId");
                });
#pragma warning restore 612, 618
        }
    }
}
