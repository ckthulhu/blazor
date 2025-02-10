﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RadzenApp.Model.Scan;

namespace RadzenApp.Data;

public partial class ScanContext : DbContext
{
    public ScanContext(DbContextOptions<ScanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ScanRequest> ScanRequests { get; set; }

    public virtual DbSet<Technology> Technologies { get; set; }

    public virtual DbSet<WebApp> WebApps { get; set; }

    public virtual DbSet<WebAppPerson> WebAppPeople { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ScanRequest>(entity =>
        {
            entity.ToTable("ScanRequest");

            entity.Property(e => e.ScanRequestId).HasColumnName("ScanRequestID");
            entity.Property(e => e.DeploymentLocation).HasMaxLength(50);

            entity.HasMany(d => d.Technologies).WithMany(p => p.ScanRequests)
                .UsingEntity<Dictionary<string, object>>(
                    "ScanTechnology",
                    r => r.HasOne<Technology>().WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ScanTechnology_Technology"),
                    l => l.HasOne<ScanRequest>().WithMany()
                        .HasForeignKey("ScanRequestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ScanTechnology_ScanRequest"),
                    j =>
                    {
                        j.HasKey("ScanRequestId", "TechnologyId");
                        j.ToTable("ScanTechnology");
                        j.IndexerProperty<int>("ScanRequestId").HasColumnName("ScanRequestID");
                        j.IndexerProperty<int>("TechnologyId").HasColumnName("TechnologyID");
                    });
        });

        modelBuilder.Entity<Technology>(entity =>
        {
            entity.ToTable("Technology");

            entity.Property(e => e.TechnologyId)
                .ValueGeneratedNever()
                .HasColumnName("TechnologyID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<WebApp>(entity =>
        {
            entity.ToTable("WebApp");

            entity.Property(e => e.WebAppId).HasColumnName("WebAppID");
            entity.Property(e => e.AppDescription).HasMaxLength(2000);
            entity.Property(e => e.AppName).HasMaxLength(100);
            entity.Property(e => e.AppUrl)
                .HasMaxLength(100)
                .HasColumnName("AppURL");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateTs)
                .HasColumnType("datetime")
                .HasColumnName("CreateTS");
            entity.Property(e => e.EditBy).HasMaxLength(50);
            entity.Property(e => e.EditTs)
                .HasColumnType("datetime")
                .HasColumnName("EditTS");
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<WebAppPerson>(entity =>
        {
            entity.HasKey(e => new { e.WebAppId, e.PersonNumber });

            entity.ToTable("WebAppPerson");

            entity.Property(e => e.WebAppId).HasColumnName("WebAppID");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.WebApp).WithMany(p => p.WebAppPeople)
                .HasForeignKey(d => d.WebAppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WebAppPerson_WebAppPerson");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}