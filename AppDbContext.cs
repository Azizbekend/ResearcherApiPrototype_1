using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1.Models;

namespace ResearcherApiPrototype_1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<ControlBlockInfo> ControlBlocks { get; set; }
        public DbSet<HardwareInfo> Hardwares { get; set; }
        public DbSet<NodeInfo> Nodes { get; set; }
        public DbSet<NodeIndicates> NodesIndicates { get; set; }

        public DbSet<HardwareCharacteristic> Characteristics { get; set; }
        public DbSet<StaticObjectInfo> StaticObjectInfos { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<HardwareCharacteristic>(entity =>
        //    {
        //        entity.HasKey(x => x.Id);
        //        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        //        entity.HasOne(h => h.Hardware)
        //        .WithMany(cb => cb.Characteristics)
        //        .HasForeignKey(h => h.HardwareId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    });
        //    modelBuilder.Entity<HardwareInfo>(entity =>
        //    {
        //        entity.HasKey(x => x.Id);
        //        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        //        entity.HasOne(h => h.ControlBlock)
        //        .WithMany(cb => cb.HardwareInfo)
        //        .HasForeignKey(h => h.ControlBlockId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    });
        //    modelBuilder.Entity<NodeIndicates>(entity =>
        //        {
        //            entity.HasKey(x => x.Id);
        //            entity.Property(x => x.Indicates).HasMaxLength(100).IsRequired();
        //            entity.Property(ts => ts.TimeStamp).HasColumnType("timestamp");
        //            entity.HasOne(n => n.NodeInfo)
        //            .WithMany(ni => ni.NodeIndicates)
        //            .HasForeignKey(n => n.NodeInfoId)
        //            .OnDelete(DeleteBehavior.Cascade);
        //        });                 

        //    modelBuilder.Entity<NodeInfo>(entity =>
        //    {
        //        entity.HasKey(x=>x.Id);
        //        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        //        entity.HasOne(h => h.HardwareInfo)
        //        .WithMany(cb => cb.NodeInfos)
        //        .HasForeignKey(h => h.HardwareId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    });
        //    modelBuilder.Entity<ControlBlockInfo>(entity =>
        //    {
        //        entity.HasKey(x => x.Id);
        //        entity.HasOne(info => info.StaticObjectInfo)
        //        .WithMany(cb => cb.ControlBlocks)
        //        .HasForeignKey(fk => fk.StaticObjectInfoId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    });
            
        

    }
}
