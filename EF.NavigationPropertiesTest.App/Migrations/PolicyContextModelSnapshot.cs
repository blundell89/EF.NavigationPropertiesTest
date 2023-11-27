﻿// <auto-generated />
using System;
using EF.NavigationPropertiesTest.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EF.NavigationPropertiesTest.App.Migrations
{
    [DbContext(typeof(PolicyContext))]
    partial class PolicyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EF.NavigationPropertiesTest.App.Policy", b =>
                {
                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PolicyId");

                    b.ToTable("Policies", (string)null);
                });

            modelBuilder.Entity("EF.NavigationPropertiesTest.App.PolicyEvent", b =>
                {
                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Event")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PolicyId", "UpdatedAt");

                    b.ToTable("PolicyEvents", (string)null);
                });

            modelBuilder.Entity("EF.NavigationPropertiesTest.App.PolicyEvent", b =>
                {
                    b.HasOne("EF.NavigationPropertiesTest.App.Policy", "Policy")
                        .WithMany("Events")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Policy");
                });

            modelBuilder.Entity("EF.NavigationPropertiesTest.App.Policy", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
