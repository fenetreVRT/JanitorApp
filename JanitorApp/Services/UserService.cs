using JanitorApp.Context;
using JanitorApp.Models;
using JanitorApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JanitorApp.Services;


internal class UserService
{
    private readonly DataContext _context = new();

    public async Task<UserEntity> SaveAsync(UserRegistration userRegistration)
    {
        var userEntity = new UserEntity
        {
            FirstName = userRegistration.FirstName,
            LastName = userRegistration.LastName,
            Email = userRegistration.Email,
            PhoneNumber = userRegistration.PhoneNumber,
        };

        await _context.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        return userEntity;
    }

    public async Task<UserEntity> GetByEmailAsync(string email)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        if (userEntity == null)
            return null!;

        return userEntity;
    }


}

