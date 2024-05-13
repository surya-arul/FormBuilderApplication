using System;
using System.Collections.Generic;
using FormBuilderSharedService.Models;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderSharedService.DbContexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblControl> TblControls { get; set; }

    public virtual DbSet<TblInput> TblInputs { get; set; }

    public virtual DbSet<TblSurvey> TblSurveys { get; set; }

    public virtual DbSet<TblUserDatum> TblUserData { get; set; }

    public virtual DbSet<TblUserSubmitDetail> TblUserSubmitDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblControl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblControls_Id");

            entity.ToTable("tblControls");

            entity.Property(e => e.DivClassName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InputClassName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InputType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InternalName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LabelClassName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OptionData).IsUnicode(false);
            entity.Property(e => e.Placeholder)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblInput>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblInputs_Id");

            entity.ToTable("tblInputs");

            entity.HasOne(d => d.Control).WithMany(p => p.TblInputs)
                .HasForeignKey(d => d.ControlId)
                .HasConstraintName("FK_tblInputs_ControlId");

            entity.HasOne(d => d.Survey).WithMany(p => p.TblInputs)
                .HasForeignKey(d => d.SurveyId)
                .HasConstraintName("FK_tblInputs_SurveyId");
        });

        modelBuilder.Entity<TblSurvey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblSurveys_Id");

            entity.ToTable("tblSurveys");

            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUserDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblUserData_Id");

            entity.ToTable("tblUserData");

            entity.Property(e => e.Label).IsUnicode(false);
            entity.Property(e => e.Value).IsUnicode(false);

            entity.HasOne(d => d.UserSubmitDetails).WithMany(p => p.TblUserData)
                .HasForeignKey(d => d.UserSubmitDetailsId)
                .HasConstraintName("FK_tblUserData_UserSubmitDetailsId");
        });

        modelBuilder.Entity<TblUserSubmitDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblUserSubmitDetails_Id");

            entity.ToTable("tblUserSubmitDetails");

            entity.Property(e => e.DateCreatedBy).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Survey).WithMany(p => p.TblUserSubmitDetails)
                .HasForeignKey(d => d.SurveyId)
                .HasConstraintName("FK_tblUserSubmitDetails_SurveyId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}