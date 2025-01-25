using GameZone.Models;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Configuration
{
    public class GameDeviceConfig : IEntityTypeConfiguration<GameDevice>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GameDevice> builder)
        => builder.HasKey(e => new { e.GameId, e.DeviceId });
        
    }
}
