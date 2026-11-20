using LaboCreature.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LaboCreature.DAL
{
    public class CreatureDBContext : DbContext
    {
        public CreatureDBContext(DbContextOptions options) : base(options) { }

        public DbSet<MemberEntity> Members { get; set; }
        public DbSet<PlayerEntity> Players { get; set; }
        public DbSet<RaceEntity> Races { get; set; }
        public DbSet<CreatureEntity> Creatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
