using Microsoft.EntityFrameworkCore;
using Seisankanri.Model.Entities;

namespace Seisankanri.Model
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<MasterCode>? MasterCodes { get; set; }
        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Role>? Roles { get; set; }
        public virtual DbSet<Permission>? Permissions { get; set; }
        public virtual DbSet<Function>? Functions { get; set; }
        public virtual DbSet<Resource>? Resources { get; set; }
        public virtual DbSet<Machine>? Machines { get; set; }
        public virtual DbSet<Category>? Categories { get; set; }
        public virtual DbSet<Calendar>? Calendars { get; set; }
        public virtual DbSet<Company>? Companies { get; set; }
        public virtual DbSet<SalePlan>? SalePlans { get; set; }
        public virtual DbSet<Item>? Items { get; set; }
        public virtual DbSet<ItemEdi>? ItemEdis { get; set; }
        public virtual DbSet<ItemPrice>? ItemPrices { get; set; }
        public virtual DbSet<ItemBuyPrices>? ItemBuyPrices { get; set; }
        public virtual DbSet<ItemSalePrices>? ItemSalePrices { get; set; }
        public virtual DbSet<Classification>? Classifications { get; set; }
        public virtual DbSet<ReceiveOrder>? ReceiveOrders { get; set; }
        public virtual DbSet<ReceiveOrderDt>? ReceiveOrderDts { get; set; }
        public virtual DbSet<Seq>? Seqs { get; set; }
        public virtual DbSet<SystemSettings>? SystemSettings { get; set; }
        public virtual DbSet<ProductPlanDay>? ProductPlanDays { get; set; }
        public virtual DbSet<ProductPlanMonth>? ProductPlanMonths { get; set; }
        public virtual DbSet<Inventory>? Inventories { get; set; }
        public virtual DbSet<Warehouses>? Warehouses { get; set; }
        public virtual DbSet<Manufactory>? Manufactories { get; set; }
        public virtual DbSet<LogAction>? LogAction { get; set; }
        public virtual DbSet<LogException>? LogException { get; set; }
        public virtual DbSet<InventoryTotal>? InventoryTotals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);    
        }
    }
}
