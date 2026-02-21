using ExpenseTracker.API.Common;
using ExpenseTracker.API.Data;
using ExpenseTracker.API.Dtos;
using ExpenseTracker.API.Entities;
using ExpenseTracker.API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.API.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        ApplicationDbContext context,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> RegisterAsync(RegisterRequest request)
    {
        var normalizedEmail = request.Email.ToLower().Trim();
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (existingUser != null)
            return Result.Failure("Email already exists.");

        var tempUser = new User
        {
            Id = Guid.NewGuid(),
            Email = normalizedEmail,
            PasswordHash = "", // temporary, will override below
            CreatedAt = DateTime.UtcNow
        };

        var passwordHash = _passwordHasher.HashPassword(tempUser, request.Password);

        var user = new User
        {
            Id = tempUser.Id,
            Email = normalizedEmail,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(user, request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}