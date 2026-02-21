using ExpenseTracker.API.Common;

namespace ExpenseTracker.API.Interfaces;
using ExpenseTracker.API.Dtos;

public interface IUserService
{
    Task<Result> RegisterAsync(RegisterRequest request);
}