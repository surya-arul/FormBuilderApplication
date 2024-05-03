using System;
using System.Collections.Generic;
using FormBuilderMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderMVC.DbContexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblControl> TblControls { get; set; }

    public virtual DbSet<TblInput> TblInputs { get; set; }

    public virtual DbSet<TblSurvey> TblSurveys { get; set; }

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

            entity.HasOne(d => d.Survey).WithMany(p => p.TblInputs)
                .HasForeignKey(d => d.SurveyId)
                .HasConstraintName("FK_tblInputs_SurveyId");
        });

        modelBuilder.Entity<TblSurvey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblSurveys_Id");

            entity.ToTable("tblSurveys");

            entity.Property(e => e.FormAction)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FormMethod)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
