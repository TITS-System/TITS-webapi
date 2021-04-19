﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(TitsDbContext))]
    [Migration("20210419115116_AddOrderAddress")]
    partial class AddOrderAddress
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Models.Db.Account.WorkerAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("LastLatLngId")
                        .HasColumnType("bigint");

                    b.Property<long?>("LastTokenSessionId")
                        .HasColumnType("bigint");

                    b.Property<long?>("LastWorkerSessionId")
                        .HasColumnType("bigint");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<long?>("MainRestaurantId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LastTokenSessionId");

                    b.HasIndex("LastWorkerSessionId");

                    b.HasIndex("MainRestaurantId");

                    b.ToTable("WorkerAccounts");
                });

            modelBuilder.Entity("Models.Db.Account.WorkerAccountToRole", b =>
                {
                    b.Property<long>("WorkerRoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("WorkerAccountId")
                        .HasColumnType("bigint");

                    b.HasKey("WorkerRoleId", "WorkerAccountId");

                    b.HasIndex("WorkerAccountId");

                    b.ToTable("WorkerAccountToRoles");
                });

            modelBuilder.Entity("Models.Db.Account.WorkerRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("TitleEn")
                        .HasColumnType("text");

                    b.Property<string>("TitleRu")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WorkerRoles");
                });

            modelBuilder.Entity("Models.Db.Delivery", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CourierAccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long>("Status")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CourierAccountId");

                    b.HasIndex("OrderId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("Models.Db.LatLng", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("DeliveryId")
                        .HasColumnType("bigint");

                    b.Property<float>("Lat")
                        .HasColumnType("real");

                    b.Property<float>("Lng")
                        .HasColumnType("real");

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long?>("RestaurantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("OrderId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("LatLngs");
                });

            modelBuilder.Entity("Models.Db.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AddressAdditional")
                        .HasColumnType("text");

                    b.Property<string>("AddressString")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("DestinationLatLngId")
                        .HasColumnType("bigint");

                    b.Property<long>("RestaurantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DestinationLatLngId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Models.Db.Restaurant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("LocationLatLngId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LocationLatLngId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Models.Db.Sessions.TokenSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<long>("WorkerAccountId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WorkerAccountId");

                    b.ToTable("TokenSessions");
                });

            modelBuilder.Entity("Models.Db.WorkerSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CloseDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OpenDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("RestaurantId")
                        .HasColumnType("bigint");

                    b.Property<long>("WorkerAccountId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("WorkerAccountId");

                    b.ToTable("WorkerSessions");
                });

            modelBuilder.Entity("Models.Db.Account.WorkerAccount", b =>
                {
                    b.HasOne("Models.Db.Sessions.TokenSession", "LastTokenSession")
                        .WithMany()
                        .HasForeignKey("LastTokenSessionId");

                    b.HasOne("Models.Db.WorkerSession", "LastWorkerSession")
                        .WithMany()
                        .HasForeignKey("LastWorkerSessionId");

                    b.HasOne("Models.Db.Restaurant", "MainRestaurant")
                        .WithMany()
                        .HasForeignKey("MainRestaurantId");

                    b.Navigation("LastTokenSession");

                    b.Navigation("LastWorkerSession");

                    b.Navigation("MainRestaurant");
                });

            modelBuilder.Entity("Models.Db.Account.WorkerAccountToRole", b =>
                {
                    b.HasOne("Models.Db.Account.WorkerAccount", "WorkerAccount")
                        .WithMany("WorkerRoles")
                        .HasForeignKey("WorkerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.Account.WorkerRole", "WorkerRole")
                        .WithMany("Users")
                        .HasForeignKey("WorkerRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkerAccount");

                    b.Navigation("WorkerRole");
                });

            modelBuilder.Entity("Models.Db.Delivery", b =>
                {
                    b.HasOne("Models.Db.Account.WorkerAccount", "CourierAccount")
                        .WithMany("Deliveries")
                        .HasForeignKey("CourierAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.Order", "Order")
                        .WithMany("Deliveries")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourierAccount");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Models.Db.LatLng", b =>
                {
                    b.HasOne("Models.Db.Delivery", "Delivery")
                        .WithMany("LatLngs")
                        .HasForeignKey("DeliveryId");

                    b.HasOne("Models.Db.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("Models.Db.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Delivery");

                    b.Navigation("Order");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Models.Db.Order", b =>
                {
                    b.HasOne("Models.Db.LatLng", "DestinationLatLng")
                        .WithMany()
                        .HasForeignKey("DestinationLatLngId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.Restaurant", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DestinationLatLng");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Models.Db.Restaurant", b =>
                {
                    b.HasOne("Models.Db.LatLng", "LocationLatLng")
                        .WithMany()
                        .HasForeignKey("LocationLatLngId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocationLatLng");
                });

            modelBuilder.Entity("Models.Db.Sessions.TokenSession", b =>
                {
                    b.HasOne("Models.Db.Account.WorkerAccount", "WorkerAccount")
                        .WithMany()
                        .HasForeignKey("WorkerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkerAccount");
                });

            modelBuilder.Entity("Models.Db.WorkerSession", b =>
                {
                    b.HasOne("Models.Db.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.Account.WorkerAccount", "WorkerAccount")
                        .WithMany()
                        .HasForeignKey("WorkerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("WorkerAccount");
                });

            modelBuilder.Entity("Models.Db.Account.WorkerAccount", b =>
                {
                    b.Navigation("Deliveries");

                    b.Navigation("WorkerRoles");
                });

            modelBuilder.Entity("Models.Db.Account.WorkerRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Models.Db.Delivery", b =>
                {
                    b.Navigation("LatLngs");
                });

            modelBuilder.Entity("Models.Db.Order", b =>
                {
                    b.Navigation("Deliveries");
                });

            modelBuilder.Entity("Models.Db.Restaurant", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
