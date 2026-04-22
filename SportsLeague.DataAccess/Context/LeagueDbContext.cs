using Microsoft.EntityFrameworkCore;
using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.DataAccess.Context
{
    public class LeagueDbContext : DbContext
    {
        public LeagueDbContext(DbContextOptions<LeagueDbContext> options) : base(options)
        {
        }

        public DbSet<Teams> Teams { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentTeam> TournamentTeams { get; set; }
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Sponsors> Sponsors { get; set; }
        public DbSet<TournamentSponsor> TournamentSponsors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Teams Configuration
            modelBuilder.Entity<Teams>(entity =>
            {
                entity.HasIndex(t => t.Name).IsUnique();
                entity.HasKey(t => t.Id);
            });

            //Player Configuration
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(80);
                entity.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(80);
                entity.Property(p => p.BirthDate)
                .IsRequired();
                entity.Property(p => p.Number)
                .IsRequired();
                entity.Property(p => p.Position)
                .IsRequired();
                entity.Property(p => p.CreatedAt)
                .IsRequired();
                entity.Property(p => p.UpdatedAt)
                .IsRequired(false);

                // Relacion 1:N con Team
                entity.HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

                // Indice unico compuesto: numero de camiseta unico por equipo
                entity.HasIndex(p => new { p.TeamId, p.Number })
                .IsUnique();
            });

            //Referee Configuration
            modelBuilder.Entity<Referee>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.FirstName).IsRequired().HasMaxLength(80);
                entity.Property(r => r.LastName).IsRequired().HasMaxLength(80);
                entity.Property(r => r.Nationality).IsRequired().HasMaxLength(80);
                entity.Property(r => r.CreatedAt).IsRequired();
                entity.Property(r => r.UpdatedAt).IsRequired(false);
            });

            //Tournament Configuration
            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(150);
                entity.Property(t => t.Season).IsRequired().HasMaxLength(20);
                entity.Property(t => t.StartDate).IsRequired();
                entity.Property(t => t.EndDate).IsRequired();
                entity.Property(t => t.Status).IsRequired();
                entity.Property(t => t.CreatedAt).IsRequired();
                entity.Property(t => t.UpdatedAt).IsRequired(false);
            });

            //TournamentTeam Configuration
            modelBuilder.Entity<TournamentTeam>(entity =>
            {
                entity.HasKey(tt => tt.Id);
                entity.Property(tt => tt.RegisteredAt).IsRequired();
                entity.Property(tt => tt.CreatedAt).IsRequired();
                entity.Property(tt => tt.UpdatedAt).IsRequired(false);

                // Relación con Tournament
                entity.HasOne(tt => tt.Tournament)
                .WithMany(t => t.TournamentTeams)
                .HasForeignKey(tt => tt.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

                // Relación con Team
                entity.HasOne(tt => tt.Team)
                .WithMany(t => t.TournamentTeams)
                .HasForeignKey(tt => tt.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

                // Índice único compuesto: un equipo solo una vez por torneo
                entity.HasIndex(tt => new { tt.TournamentId, tt.TeamId }).IsUnique();
            });

            //Sponsor Configuration
            modelBuilder.Entity<Sponsors>().HasIndex(x => x.Name).IsUnique();

            //TournamentSponsor Configuration
            modelBuilder.Entity<TournamentSponsor>(entity =>
            {
                entity.HasIndex(x => new { x.TournamentId, x.SponsorId }).IsUnique();
            });

            // Relacion para TournamentSponsor con Tournament
            modelBuilder.Entity<TournamentSponsor>(entity =>
            {
                entity.HasOne(x => x.Tournament)
                .WithMany(t => t.TournamentSponsors)
                .HasForeignKey(x => x.TournamentId);
            });

            // Relacion para TournamentSponsor con Sponsor
            modelBuilder.Entity<TournamentSponsor>()
                .HasOne(x => x.Sponsor)
                .WithMany(s => s.TournamentSponsors)
                .HasForeignKey(x => x.SponsorId);
        }
    }
}