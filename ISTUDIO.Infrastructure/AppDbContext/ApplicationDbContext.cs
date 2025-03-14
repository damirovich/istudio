﻿using ISTUDIO.Infrastructure.AppDbContext.EntityConfiguration;
using ISTUDIO.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ISTUDIO.Infrastructure.AppDbContext;

public class ApplicationDbContext : IdentityDbContext<AppUser>, IAppDbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<FamilyCustomersEntity> FamilyUsers { get; set; }
    public DbSet<CustomerImagesEntity> UserImages { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<SmsNikitaRequest> SmsNikitaRequests { get; set; }
    public DbSet<SmsNikitaResponse> SmsNikitaResponses { get; set; }
    public DbSet<SmsNikitaStatus> SmsNikitaStatuses { get; set; }
    public DbSet<CustomersEntity> Customers { get; set; }
    public DbSet<CustomerImagesEntity> CustomerImages { get; set; }
    public DbSet<ProductsEntity> Products { get; set; }
    public DbSet<DiscountEntity> Discounts { get; set; }
    public DbSet<ProductImagesEntity> ProductImages { get; set; }
    public DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<FavoriteProductsEntity> FavoriteProducts { get; set; }
    public DbSet<OrderDetailEntity> OrderDetails { get; set; }
    public DbSet<OrderAddressEntity> OrderAddresses { get; set; }
    public DbSet<BannerEntity> Banners { get; set; }
    public DbSet<OrderStatusHistoryEntity> OrderStatusHistories { get; set; }
    public DbSet<MagazineEntity> Magazines { get; set; }
    public DbSet<OrderNotificationRecipientEntity> OrderNotificationRecipients { get; set; }
    public DbSet<FreedomPayInitResEntity> FreedomPayInitRespons { get; set; }
    public DbSet<FreedomPayResultRequestEntity> FreedomPayResultRequests { get; set; }
    public DbSet<FreedomPayResultResponseEntity> FreedomPayResultResponses { get; set; }
    public DbSet<FreedomPayInitReqEntity> FreedomPayInitReq { get; set; }
    public DbSet<BakaiCheckStatusResEntity> BakaiCheckStatusResponse { get; set; }
    public DbSet<BakaiConfirmTranReqEntity> BakaiConfirmTranRequest { get; set; }
    public DbSet<BakaiConfirmTranResEntity> BakaiConfirmTranResponse { get; set; }
    public DbSet<BakaiCreateTranReqEntity> BakaiCreateTranRequest { get; set; }
    public DbSet<BakaiCreateTranResEntity> BakaiCreateTranResponse { get; set; }
    public DbSet<PaymentMethodEntity> PaymentMethods { get; set; }
    public DbSet<PaymentTypeEntity> PaymentTypes { get; set; }  
    public DbSet<OrderPaymentEntity> OrderPayments { get; set; }
    public DbSet<OrderDeliveryEntity> OrderDelivery { get; set; }
    public DbSet<AppUpdateInfoEntity> AppUpdateInfos { get; set; }
    public DbSet<CashbackTransactionEntity> CashbackTransactions { get; set; }
    public DbSet<UserCashbackEntity> UserCashbacks { get; set; }
    public DbSet<ProductCashbackEntity> ProductCashbacks { get; set; }
    public DbSet<CashbackEntity> Cashbacks { get; set; }
    public DbSet<OrderStatusEntity> OrderStatus { get; set; }
    public DbSet<StoriesEntity> Stories { get; set; }   
    public DbSet<StoryContentEntity> StoryContents { get; set; }
    public DbSet<RolePermissionEntity> RolePermissions { get; set; }
    public DbSet<PermissionEntity> Permissions { get; set; }
    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.DBInitializer();

        builder.Entity<IdentityRole>(e => e.ToTable(name: "Roles"));
        builder.Entity<IdentityUserRole<string>>(e => e.ToTable(name: "UserRoles"));
        builder.Entity<IdentityUserClaim<string>>(e => e.ToTable(name: "UserClaim"));
        builder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
        builder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"));

        builder.ApplyConfiguration(new AppUserConfiguration());
        builder.ApplyConfiguration(new CustomerImagesConfiguration());
        builder.ApplyConfiguration(new FamilyCustomerConfiguration());
        builder.ApplyConfiguration(new CategoryEntityConfiguration());
        builder.ApplyConfiguration(new DiscountEntityConfiguration());
        builder.ApplyConfiguration(new InvoiceDetailEntityConfiguration());
        builder.ApplyConfiguration(new InvoiceDetailEntityConfiguration());
        builder.ApplyConfiguration(new OrderDetailEntityConfiguration());
        builder.ApplyConfiguration(new OrderEntityConfiguration());
        builder.ApplyConfiguration(new PaymentMethodEntityConfiguration());
        builder.ApplyConfiguration(new PaymentTypeEntityConfiguration());
        builder.ApplyConfiguration(new ProducImagesEntityConfiguration());
        builder.ApplyConfiguration(new ProductsEntityConfiguration());
        builder.ApplyConfiguration(new ShoppingCartEntityConfiguration());
        builder.ApplyConfiguration(new SmsNikitaRequestConfiguration());
        builder.ApplyConfiguration(new SmsNikitaResponseConfiguration());
        builder.ApplyConfiguration(new SmsNikitaStatusConfiguration());
        builder.ApplyConfiguration(new CustomersEntityConfiguration());
        builder.ApplyConfiguration(new FavoriteProductsEntityConfiguration());
        builder.ApplyConfiguration(new OrderAddressConfiguration());
        builder.ApplyConfiguration(new BannerEntityConfiguration());
        builder.ApplyConfiguration(new OrderStatusHistoryConfiguration());
        builder.ApplyConfiguration(new MagazineEntityConfiguration());
        builder.ApplyConfiguration(new OrderNotificationRecipientConfiguration());
        builder.ApplyConfiguration(new FreedomPayInitResEntityConfiguration());
        builder.ApplyConfiguration(new FreedomPayResultRequestEntityConfiguration());
        builder.ApplyConfiguration(new FreedomPayResultResponseEntityConfiguration());
        builder.ApplyConfiguration(new FreedomPayInitReqEntityConfiguration());
        builder.ApplyConfiguration(new BakaiCheckStatusResEntityConfiguration());
        builder.ApplyConfiguration(new BakaiConfirmTranReqEntityConfiguration());
        builder.ApplyConfiguration(new BakaiConfirmTranResEntityConfiguration());
        builder.ApplyConfiguration(new BakaiCreateTranReqEntityConfiguration());
        builder.ApplyConfiguration(new BakaiCreateTranResEntityConfiguration());
        builder.ApplyConfiguration(new AppUpdateInfoEntityConfiguration());
        builder.ApplyConfiguration(new OrderPaymentEntityConfiguration());
        builder.ApplyConfiguration(new OrderDeliveryEntityConfiguration());
        builder.ApplyConfiguration(new CashbackTransactionEntityConfiguration());
        builder.ApplyConfiguration(new UserCashbackEntityConfiguration());
        builder.ApplyConfiguration(new ProductsEntityConfiguration());
        builder.ApplyConfiguration(new CashbackEntityConfiguration());
        builder.ApplyConfiguration(new OrderStatusEntityConfiguration());
        builder.ApplyConfiguration(new StoriesEntityConfiguration());
        builder.ApplyConfiguration(new StoryContentEntityConfiguration());
        builder.ApplyConfiguration(new PermissionEntityConfiguration());
        builder.ApplyConfiguration(new RolePermissionEntityConfiguration());
    }   
}
