using PhoneShop.Ordering.Domain.Common;

namespace PhoneShop.Ordering.Application.Common.Interfaces;

public interface IEmailService
{
    Task<bool> SendMailAsync(Email email);
}
