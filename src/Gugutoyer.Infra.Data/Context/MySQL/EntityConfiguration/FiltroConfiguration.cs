using Gugutoyer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Gugutoyer.Infra.Data.Context.MySQL.EntityConfiguration
{
    public class FiltroConfiguration : IEntityTypeConfiguration<Filtro>
    {
        public void Configure(EntityTypeBuilder<Filtro> builder)
        {
            builder.ToTable("filtro");
            builder.HasKey(f => f.Id).HasName("filtro_id");
            builder.Property(f => f.Id).HasColumnName("filtro_id");
            builder.HasOne(f => f.Palavra).WithOne().HasForeignKey<Palavra>(p => p.Id);
            builder.Property(f => f.Id).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
