using LaboCreature.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboCreature.DAL.Configs
{
    public class RaceConfig : IEntityTypeConfiguration<RaceEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RaceEntity> builder)
        {
            builder.ToTable("Race");

            builder.HasKey(r => r.Id)
                   .HasName("PK_Race")
                   .IsClustered();

            builder.Property(r => r.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(r => r.FrontSprite)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.HasData(
                new RaceEntity()
                {
                    Id = 1,
                    Name = "Water Thingy",
                    FrontSprite = "/images/waterthingy.png"
                },
                new RaceEntity()
                {
                    Id = 2,
                    Name = "Common Fox",
                    FrontSprite = "/images/commonfox.png"
                },
                new RaceEntity()
                {
                    Id = 3,
                    Name = "Fire Chicken",
                    FrontSprite = "/images/firechicken.png"
                },
                new RaceEntity()
                {
                    Id = 4,
                    Name = "Purple Draco",
                    FrontSprite = "/images/purpledraco.png"
                },
                new RaceEntity()
                {
                    Id = 5,
                    Name = "Weird Penguin",
                    FrontSprite = "/images/weirdpenguin.png"
                });
        }
    }
}
