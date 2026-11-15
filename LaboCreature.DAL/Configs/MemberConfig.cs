using LaboCreature.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Configs
{
    public class MemberConfig : IEntityTypeConfiguration<MemberEntity>
    {
        public void Configure(EntityTypeBuilder<MemberEntity> builder)
        { 
            builder.ToTable("Member");

            builder.HasKey(m => m.Id)
                   .HasName("PK_Member")
                   .IsClustered();

            builder.Property(m => m.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.Email)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(m => m.Username)
                   .HasMaxLength(50)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(m => m.PasswordHash)
                   .HasMaxLength(255)
                   .IsRequired();
        }
    }
}
