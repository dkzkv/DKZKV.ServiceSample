using System.Data;
using DKZKV.BookStore.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DKZKV.BookStore.Application.Commands.Extensions;

public class TransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionalBehavior<TRequest, TResponse>> _logger;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public TransactionalBehavior(IUnitOfWorkFactory unitOfWorkFactory, ILogger<TransactionalBehavior<TRequest, TResponse>> logger)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (request is not ITransactionalCommand) return await next();

        using var unitOfWork = await _unitOfWorkFactory.CreateAsync(IsolationLevel.ReadCommitted, cancellationToken);

        try
        {
            var response = await next();
            await unitOfWork.CommitAsync(cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogCritical(e, "Un expected error, while providing transactional command");
            throw;
        }
    }
}