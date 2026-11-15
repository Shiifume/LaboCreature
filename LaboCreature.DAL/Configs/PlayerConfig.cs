using LaboCreature.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Configs
{
    public class PlayerConfig : IEntityTypeConfiguration<PlayerEntity>
    {
        public void Configure(EntityTypeBuilder<PlayerEntity> builder)
        {
            builder.ToTable("Player");

            builder.HasKey(p => p.Id)
                   .HasName("PK_Player")
                   .IsClustered();

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Member)
                   .WithOne(m => m.Player)
                   .HasForeignKey<PlayerEntity>(p => p.MemberId)
                   .HasConstraintName("FK_Player_Member")
                   .IsRequired();
        }
    }
}
