using Gugutoyer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.Data.Context.MySQL.EntityConfiguration
{
    public class PalavraConfiguration : IEntityTypeConfiguration<Palavra>
    {
        public void Configure(EntityTypeBuilder<Palavra> builder)
        {
            builder.ToTable("palavras");
            builder.HasKey(p => p.Id).HasName("palavra_id");
            builder.Property(p => p.Id).HasColumnName("palavra_id").IsRequired();
            builder.Property(p => p.Verbete).HasColumnName("palavra").HasMaxLength(100).IsRequired();
            builder.Property(p => p.Usado).HasColumnName("usado").HasConversion<int>().IsRequired();
        }
    }
}
