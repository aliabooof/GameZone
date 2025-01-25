using GameZone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameZone.Configuration
{
    public class DeciveConfig : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
           
               builder.HasData(new Device[] {
                    new Device { Id = 1, Name="PlayStation" , Icon ="bi bi-Playstation"},
                    new Device { Id = 2, Name="Xbox" , Icon ="bi bi-xbox"},
                    new Device { Id = 3, Name="Nintendo Switch" , Icon ="bi bi-nintendo-switch"},
                    new Device { Id = 4, Name="PC" , Icon ="bi bi-pc-display"},
               });

        }
    }
}
