using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Riot.Data.Entities;

namespace Riot.Data.Context;

public partial class RiotDbContext : DbContext
{
    public RiotDbContext()
    {
    }

    public RiotDbContext(DbContextOptions<RiotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Matches__3214EC07799C6248");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Region).HasMaxLength(10);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Players__3214EC07EF323093");

            entity.Property(e => e.LastLogin).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Mmr).HasDefaultValue(1000);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.CurrentMatch).WithMany(p => p.Players)
                .HasForeignKey(d => d.CurrentMatchId)
                .HasConstraintName("FK_Players_Matches");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
