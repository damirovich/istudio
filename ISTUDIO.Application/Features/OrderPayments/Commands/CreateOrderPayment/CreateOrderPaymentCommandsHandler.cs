namespace ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;

using ISTUDIO.Domain.EntityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

using ResModel = Result;
public class CreateOrderPaymentCommandsHandler : IRequestHandler<CreateOrderPaymentCommands, ResModel>
{
    private readonly IAppDbContext _appDbContext;

    public CreateOrderPaymentCommandsHandler(IAppDbContext appDbContext)
        => _appDbContext = appDbContext;

    public async Task<ResModel> Handle(CreateOrderPaymentCommands command, CancellationToken cancellationToken)
    {
        await using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var orderPayment = new OrderPaymentEntity
            {
                OrderId = command.OrderId,
                PaymentMethodId = command.PaymentMethodId,
                Amount = command.Amount,
                PaymentDate = DateTime.Now,
                Status = command.StatusPayment
            };

            var order = await _appDbContext.Orders
                .FirstOrDefaultAsync(c => c.Id == command.OrderId, cancellationToken);

            if (order != null)
            {
                var orderStatus = await _appDbContext.OrderStatus
                    .FirstOrDefaultAsync(s => s.NameEng == command.StatusPayment, cancellationToken);

                if (orderStatus != null)
                {
                    order.Status = orderStatus;
                    _appDbContext.Orders.Update(order);

                    var orderStatusHistory = new OrderStatusHistoryEntity
                    {
                        OrderId = command.OrderId,
                        Status = command.StatusPayment,
                        ChangeDate = DateTime.Now
                    };

                    await _appDbContext.OrderStatusHistories.AddAsync(orderStatusHistory, cancellationToken);
                }
            }

            int TranChashId = 0;
            

            var userCashback = await _appDbContext.UserCashbacks
                .FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellationToken);

            if (command.CreditBonusAmount > 0)
            {
                TranChashId = await ApplyCashback(command.UserId, command.OrderId, (decimal)-command.CreditBonusAmount, "Credit", userCashback, cancellationToken);
            }

            if (command.DebitBonusAmount > 0)
            {
                TranChashId = await ApplyCashback(command.UserId, command.OrderId, (decimal)command.DebitBonusAmount, "Debit", userCashback, cancellationToken);
            }
            orderPayment.TransactionId = TranChashId.ToString();
            _appDbContext.OrderPayments.Add(orderPayment);

            await _appDbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return ResModel.Failure(new[] { ex.Message });
        }
    }

    //Метод для транзакции по бонусам 
    private async Task<int> ApplyCashback(string userId, int orderId, decimal amount, string transactionType, UserCashbackEntity userCashback, CancellationToken cancellationToken)
    {
        var cashbackTransaction = new CashbackTransactionEntity
        {
            UserId = userId,
            OrderId = orderId,
            Amount = Math.Abs(amount),
            TransactionType = transactionType,
            CreatedAt = DateTime.Now
        };

        await _appDbContext.CashbackTransactions.AddAsync(cashbackTransaction, cancellationToken);

        if (userCashback != null)
        {
            userCashback.Amount += amount;
            userCashback.ExpirationDate = userCashback.ExpirationDate.AddMonths(1);
            _appDbContext.UserCashbacks.Update(userCashback);
        }
        else
        {
            var newUserCashback = new UserCashbackEntity
            {
                UserId = userId,
                Amount = amount,
                CreatedAt = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(3),
                Status = "Active",
                
            };

            await _appDbContext.UserCashbacks.AddAsync(newUserCashback, cancellationToken);
        }
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return cashbackTransaction.Id;
    }
}
