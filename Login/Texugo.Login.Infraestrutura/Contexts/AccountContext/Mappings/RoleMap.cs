using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Texugo.Login.Core.Contexts.AccountContext.Entities;

namespace Texugo.Login.Infraestrutura.Contexts.AccountContext.Mappings;

public class RoleMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired(true);
        
        
    }
}