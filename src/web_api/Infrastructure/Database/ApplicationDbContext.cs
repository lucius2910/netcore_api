using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
        public virtual DbSet<Item>? Items { get; set; }
        public virtual DbSet<ItemPrice>? ItemPrices { get; set; }
        public virtual DbSet<ItemBuyPrices>? ItemBuyPrices { get; set; }
        public virtual DbSet<ItemSalePrices>? ItemSalePrices { get; set; }
        public virtual DbSet<Classification>? Classifications { get; set; }
        public virtual DbSet<ReceiveOrder>? ReceiveOrders { get; set; }
        public virtual DbSet<Warehouses>? Warehouses { get; set; }
        public virtual DbSet<SalePlan>? SalePlans { get; set; }

        public virtual DbSet<LogAction>? LogAction { get; set; }
        public virtual DbSet<LogException>? LogException { get; set; }

    }
}
