using ISTUDIO.Domain.EntityModel;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ISTUDIO.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<FamilyCustomersEntity> FamilyUsers { get; set; }
    DbSet<CustomerImagesEntity> UserImages { get; set; }
    DbSet<SmsNikitaRequest> SmsNikitaRequests { get; set; }
    DbSet<SmsNikitaResponse> SmsNikitaResponses { get; set; }
    DbSet<SmsNikitaStatus> SmsNikitaStatuses { get; set; }
    DbSet<CategoryEntity> Categories { get; set; }
    DbSet<CustomersEntity> Customers { get; set; }
    DbSet<CustomerImagesEntity> CustomerImages { get; set; }
    DbSet<ProductsEntity> Products { get; set; }
    DbSet<DiscountEntity> Discounts { get; set; }
    DbSet<ProductImagesEntity> ProductImages { get; set; }
    DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }
    DbSet<OrderEntity> Orders { get; set; }
    DbSet<OrderDetailEntity> OrderDetails { get; set; }
    DbSet<FavoriteProductsEntity> FavoriteProducts { get; set; }
    DbSet<OrderAddressEntity> OrderAddresses { get; set; }
    DbSet<BannerEntity> Banners { get; set; }
    DbSet<OrderStatusHistoryEntity> OrderStatusHistories { get; set; }
    DbSet<MagazineEntity> Magazines { get; set; }
    DbSet<OrderNotificationRecipientEntity> OrderNotificationRecipients { get; set; }
    DbSet<FreedomPayInitResEntity> FreedomPayInitRespons { get; set; }
    DbSet<FreedomPayResultRequestEntity> FreedomPayResultRequests { get; set; }
    DbSet<FreedomPayResultResponseEntity> FreedomPayResultResponses { get; set; }
    DbSet<FreedomPayInitReqEntity> FreedomPayInitReq { get; set; }
    DbSet<BakaiCheckStatusResEntity> BakaiCheckStatusResponse { get; set; }
    DbSet<BakaiConfirmTranReqEntity> BakaiConfirmTranRequest { get; set; }
    DbSet<BakaiConfirmTranResEntity> BakaiConfirmTranResponse { get; set; }
    DbSet<BakaiCreateTranReqEntity> BakaiCreateTranRequest { get; set; }
    DbSet<BakaiCreateTranResEntity> BakaiCreateTranResponse { get; set; }
    DbSet<AppUpdateInfoEntity> AppUpdateInfos { get; set; }
    DbSet<PaymentMethodEntity> PaymentMethods { get; set; }
    DbSet<PaymentTypeEntity> PaymentTypes { get; set; }
    DbSet<OrderPaymentEntity> OrderPayments { get; set; }
    DbSet<OrderDeliveryEntity> OrderDelivery { get; set; }
    DbSet<CashbackTransactionEntity> CashbackTransactions { get; set; }
    DbSet<UserCashbackEntity> UserCashbacks { get; set; }
    DbSet<ProductCashbackEntity> ProductCashbacks { get; set; }
    DbSet<CashbackEntity> Cashbacks { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
