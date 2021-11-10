﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Panda.Entity;

namespace Panda.Entity.Migrations
{
    [DbContext(typeof(PandaContext))]
    partial class PandaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Panda.Entity.DataModels.Accounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastLoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LockedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LoginFailCount")
                        .HasColumnType("int");

                    b.Property<string>("Passwd")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.AuditLogs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("AuditType")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Stack")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.Categorys", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CategoryDesc")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsShow")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Pid")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.DicDatas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DicKey")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DicValue")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("DicDatas");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.FriendlyLinkRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("LinksId")
                        .HasColumnType("int");

                    b.Property<string>("UA")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("LinksId");

                    b.ToTable("FriendlyLinkRecords");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.FriendlyLinks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SiteName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SiteUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FriendlyLinks");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.Pages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.PostTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostCount")
                        .HasColumnType("int");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.Posts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("AllowComment")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CustomLink")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CustomLink");

                    b.HasIndex("UpdateTime");

                    b.HasIndex("Id", "Status");

                    b.HasIndex("Text", "Title")
                        .HasAnnotation("MySql:FullTextIndex", true)
                        .HasAnnotation("MySql:FullTextParser", "ngram");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.PostsCategoryRelations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("PostsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.HasIndex("PostsId");

                    b.ToTable("ArticleCategoryRelations");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.TagsRelation", b =>
                {
                    b.Property<int>("PostsId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasIndex("PostsId");

                    b.HasIndex("TypeId");

                    b.ToTable("TagsRelations");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.FriendlyLinkRecord", b =>
                {
                    b.HasOne("Panda.Entity.DataModels.FriendlyLinks", "Links")
                        .WithMany("Records")
                        .HasForeignKey("LinksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Links");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.Posts", b =>
                {
                    b.HasOne("Panda.Entity.DataModels.Accounts", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.PostsCategoryRelations", b =>
                {
                    b.HasOne("Panda.Entity.DataModels.Categorys", "Categories")
                        .WithMany("ArticleCategoryRelations")
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Panda.Entity.DataModels.Posts", "Posts")
                        .WithMany("ArticleCategoryRelations")
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categories");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.TagsRelation", b =>
                {
                    b.HasOne("Panda.Entity.DataModels.Posts", "Posts")
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Panda.Entity.DataModels.PostTags", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Posts");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.Categorys", b =>
                {
                    b.Navigation("ArticleCategoryRelations");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.FriendlyLinks", b =>
                {
                    b.Navigation("Records");
                });

            modelBuilder.Entity("Panda.Entity.DataModels.Posts", b =>
                {
                    b.Navigation("ArticleCategoryRelations");
                });
#pragma warning restore 612, 618
        }
    }
}
