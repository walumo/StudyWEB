﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudyWEB.Models;

namespace StudyWEB.Migrations
{
    [DbContext(typeof(StudyDiaryContext))]
    [Migration("20220721075453_nullfix")]
    partial class nullfix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Latin1_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudyWEB.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Note1")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Note");

                    b.Property<int>("TaskId")
                        .HasColumnType("int")
                        .HasColumnName("Task_Id");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("StudyWEB.Models.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Task_Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("TaskDeadline")
                        .HasColumnType("datetime");

                    b.Property<string>("TaskDescription")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("TaskDone")
                        .HasColumnType("bit");

                    b.Property<int?>("TaskPriority")
                        .HasColumnType("int");

                    b.Property<string>("TaskTitle")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TopicId")
                        .HasColumnType("int")
                        .HasColumnName("Topic_Id");

                    b.HasKey("TaskId");

                    b.HasIndex("TopicId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("StudyWEB.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Topic_ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("TopicCompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TopicDescription")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("TopicEstimatedTimeToMaster")
                        .HasColumnType("float");

                    b.Property<bool>("TopicInProgress")
                        .HasColumnType("bit");

                    b.Property<string>("TopicSource")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("TopicStartLearningDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("TopicTimeSpent")
                        .HasColumnType("float");

                    b.Property<string>("TopicTitle")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("TopicId");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("StudyWEB.Models.Note", b =>
                {
                    b.HasOne("StudyWEB.Models.Task", "Task")
                        .WithMany("Notes")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("FK_Note_Task")
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("StudyWEB.Models.Task", b =>
                {
                    b.HasOne("StudyWEB.Models.Topic", "Topic")
                        .WithMany("Tasks")
                        .HasForeignKey("TopicId")
                        .HasConstraintName("FK_Task_Topic")
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("StudyWEB.Models.Task", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("StudyWEB.Models.Topic", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}