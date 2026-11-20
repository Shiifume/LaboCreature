using LaboCreature.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaboCreature.DAL.Configs
{
    public class CreatureConfig : IEntityTypeConfiguration<CreatureEntity>
    {
        public void Configure(EntityTypeBuilder<CreatureEntity> builder)
        {
            builder.ToTable("Creature");

            builder.HasKey(c => c.Id)
                   .HasName("PK_Creature")
                   .IsClustered();

            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                   .HasMaxLength(80)
                   .IsUnicode()
                   .IsRequired();

            builder.HasOne(c => c.Player)
                   .WithMany(p => p.Creatures)
                   .HasForeignKey(c => c.PlayerId)
                   .HasConstraintName("FK_Creature_Player")
                   .IsRequired();

            builder.HasOne(c => c.Race)
                   .WithMany()
                   .HasForeignKey(c => c.RaceId)
                   .HasConstraintName("FK_Creature_Race")
                   .IsRequired();



        }
    }
}
