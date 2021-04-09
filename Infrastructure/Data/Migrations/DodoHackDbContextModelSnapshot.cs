﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(TitsDbContext))]
    partial class DodoHackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Models.Db.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("LastWorkSessionId")
                        .HasColumnType("bigint");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<long>("MainWorkPointId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LastWorkSessionId");

                    b.HasIndex("MainWorkPointId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Models.Db.AccountRole", b =>
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

                    b.ToTable("AccountRoles");
                });

            modelBuilder.Entity("Models.Db.AccountSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountSessions");
                });

            modelBuilder.Entity("Models.Db.AccountToRole", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.HasKey("RoleId", "AccountId");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountToRoles");
                });

            modelBuilder.Entity("Models.Db.LatLng", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<float>("Lat")
                        .HasColumnType("real");

                    b.Property<float>("Lng")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("LatLngs");
                });

            modelBuilder.Entity("Models.Db.MenuIngredient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("MenuIngredients");
                });

            modelBuilder.Entity("Models.Db.MenuProduct", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<long>("ProductCategoryId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductPackId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.HasIndex("ProductPackId");

                    b.ToTable("MenuProducts");
                });

            modelBuilder.Entity("Models.Db.MenuProductPack", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MenuProductPacks");
                });

            modelBuilder.Entity("Models.Db.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Models.Db.OrderIngredient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderIngredients");
                });

            modelBuilder.Entity("Models.Db.OrderProduct", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<long>("ProductCategoryId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductPackId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.HasIndex("ProductPackId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("Models.Db.OrderProductPack", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderProductPacks");
                });

            modelBuilder.Entity("Models.Db.ProductCategory", b =>
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

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Models.Db.ScheduledWorkSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("WorkPointId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("WorkPointId");

                    b.ToTable("ScheduledWorkSessions");
                });

            modelBuilder.Entity("Models.Db.WorkPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("Id");

                    b.ToTable("WorkPoints");
                });

            modelBuilder.Entity("Models.Db.WorkSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CloseDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OpenDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("ScheduledWorkSessionId")
                        .HasColumnType("bigint");

                    b.Property<long>("WorkPointId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ScheduledWorkSessionId");

                    b.HasIndex("WorkPointId");

                    b.ToTable("WorkSessions");
                });

            modelBuilder.Entity("Models.Db.WorkSessionPause", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("WorkSessionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WorkSessionId");

                    b.ToTable("WorkSessionPauses");
                });

            modelBuilder.Entity("Models.Db.Account", b =>
                {
                    b.HasOne("Models.Db.WorkSession", "LastWorkSession")
                        .WithMany()
                        .HasForeignKey("LastWorkSessionId");

                    b.HasOne("Models.Db.WorkPoint", "MainWorkPoint")
                        .WithMany("AssignedAccounts")
                        .HasForeignKey("MainWorkPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LastWorkSession");

                    b.Navigation("MainWorkPoint");
                });

            modelBuilder.Entity("Models.Db.AccountSession", b =>
                {
                    b.HasOne("Models.Db.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Models.Db.AccountToRole", b =>
                {
                    b.HasOne("Models.Db.Account", "Account")
                        .WithMany("Roles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.AccountRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Models.Db.LatLng", b =>
                {
                    b.HasOne("Models.Db.Account", "Account")
                        .WithMany("LatLngs")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Models.Db.MenuIngredient", b =>
                {
                    b.HasOne("Models.Db.MenuProduct", "MenuProduct")
                        .WithMany("Ingredients")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuProduct");
                });

            modelBuilder.Entity("Models.Db.MenuProduct", b =>
                {
                    b.HasOne("Models.Db.ProductCategory", "ProductCategory")
                        .WithMany("ProductTemplates")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.MenuProductPack", "MenuProductPack")
                        .WithMany("Products")
                        .HasForeignKey("ProductPackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuProductPack");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Models.Db.Order", b =>
                {
                    b.HasOne("Models.Db.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Models.Db.OrderIngredient", b =>
                {
                    b.HasOne("Models.Db.OrderProduct", "Product")
                        .WithMany("Ingredients")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Models.Db.OrderProduct", b =>
                {
                    b.HasOne("Models.Db.ProductCategory", "ProductCategory")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.OrderProductPack", "ProductPack")
                        .WithMany("Products")
                        .HasForeignKey("ProductPackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");

                    b.Navigation("ProductPack");
                });

            modelBuilder.Entity("Models.Db.OrderProductPack", b =>
                {
                    b.HasOne("Models.Db.Order", "Order")
                        .WithMany("ProductPacks")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Models.Db.ScheduledWorkSession", b =>
                {
                    b.HasOne("Models.Db.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.WorkPoint", "WorkPoint")
                        .WithMany()
                        .HasForeignKey("WorkPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("WorkPoint");
                });

            modelBuilder.Entity("Models.Db.WorkSession", b =>
                {
                    b.HasOne("Models.Db.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Db.ScheduledWorkSession", "ScheduledWorkSession")
                        .WithMany("OpenedWorkSessions")
                        .HasForeignKey("ScheduledWorkSessionId");

                    b.HasOne("Models.Db.WorkPoint", "WorkPoint")
                        .WithMany("WorkSessions")
                        .HasForeignKey("WorkPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("ScheduledWorkSession");

                    b.Navigation("WorkPoint");
                });

            modelBuilder.Entity("Models.Db.WorkSessionPause", b =>
                {
                    b.HasOne("Models.Db.WorkSession", "WorkSession")
                        .WithMany("WorkSessionPauses")
                        .HasForeignKey("WorkSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkSession");
                });

            modelBuilder.Entity("Models.Db.Account", b =>
                {
                    b.Navigation("LatLngs");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Models.Db.AccountRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Models.Db.MenuProduct", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("Models.Db.MenuProductPack", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Models.Db.Order", b =>
                {
                    b.Navigation("ProductPacks");
                });

            modelBuilder.Entity("Models.Db.OrderProduct", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("Models.Db.OrderProductPack", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Models.Db.ProductCategory", b =>
                {
                    b.Navigation("OrderProducts");

                    b.Navigation("ProductTemplates");
                });

            modelBuilder.Entity("Models.Db.ScheduledWorkSession", b =>
                {
                    b.Navigation("OpenedWorkSessions");
                });

            modelBuilder.Entity("Models.Db.WorkPoint", b =>
                {
                    b.Navigation("AssignedAccounts");

                    b.Navigation("WorkSessions");
                });

            modelBuilder.Entity("Models.Db.WorkSession", b =>
                {
                    b.Navigation("WorkSessionPauses");
                });
#pragma warning restore 612, 618
        }
    }
}
