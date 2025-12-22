using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Common.Security;
using LifeQuestAPI.Domain.Entities;
using LifeQuestAPI.Domain.Entities.Identity;
using LifeQuestAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LifeQuestAPI.Persistence.Contexts.SeedData;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<LifeQuestDbContext>();

        context.Database.EnsureCreated();

        var egitim = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Eğitim");
        if (egitim == null)
        {
            egitim = new Category { Name = "Eğitim", Description = "Akademik ve kişisel gelişim", IconPath = "education.png" };
            context.Categories.Add(egitim);
        }

        var saglik = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Sağlık");
        if (saglik == null)
        {
            saglik = new Category { Name = "Sağlık", Description = "Fiziksel ve zihinsel sağlık", IconPath = "health.png" };
            context.Categories.Add(saglik);
        }

        var kariyer = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Kariyer");
        if (kariyer == null)
        {
            kariyer = new Category { Name = "Kariyer", Description = "İş hayatı ve profesyonel hedefler", IconPath = "career.png" };
            context.Categories.Add(kariyer);
        }

        var sosyal = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Sosyal Sorumluluk");
        if (sosyal == null)
        {
            sosyal = new Category { Name = "Sosyal Sorumluluk", Description = "Topluma fayda ve gönüllülük", IconPath = "social.png" };
            context.Categories.Add(sosyal);
        }

        await context.SaveChangesAsync();

        if (!await context.Badges.AnyAsync())
        {
            var badges = new List<Badge>
            {
                new Badge { Name = "Yeni Başlayan", Description = "İlk adımı attın! (Level 1)", IconPath = "rookie.png", RequiredLevel = 1, CategoryId = null },
                new Badge { Name = "Hırslı", Description = "Artık durdurulamazsın! (Level 2)", IconPath = "ambitious.png", RequiredLevel = 2, CategoryId = null }
            };
            await context.Badges.AddRangeAsync(badges);
            await context.SaveChangesAsync();
        }

        if (!await context.AppTasks.AnyAsync())
        {
            var tasks = new List<AppTask>
            {
                new AppTask { Title = "Kitap Oku", Description = "En az 20 sayfa kitap oku.", CategoryId = egitim.Id, BaseXP = 50, MinLevel = 1, Difficulty = DifficultyLevel.Easy, IsActive = true },
                new AppTask { Title = "Makale İncele", Description = "Alanında bir adet akademik makale oku.", CategoryId = egitim.Id, BaseXP = 100, MinLevel = 2, Difficulty = DifficultyLevel.Medium, IsActive = true },
                new AppTask { Title = "Su İç", Description = "Günde 2 litre su hedefini tamamla.", CategoryId = saglik.Id, BaseXP = 20, MinLevel = 1, Difficulty = DifficultyLevel.Easy, IsActive = true },
                new AppTask { Title = "Yürüyüş Yap", Description = "5000 adım at.", CategoryId = saglik.Id, BaseXP = 80, MinLevel = 1, Difficulty = DifficultyLevel.Medium, IsActive = true },
                new AppTask { Title = "Sokak Hayvanlarını Besle", Description = "Bir kap mama veya su bırak.", CategoryId = sosyal.Id, BaseXP = 150, MinLevel = 1, Difficulty = DifficultyLevel.Medium, IsActive = true }
            };
            await context.AppTasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
        }

        var adminRole = await context.AppRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
        if (adminRole == null)
        {
            adminRole = new AppRole { Id = Guid.NewGuid(), Name = "Admin"};
            context.AppRoles.Add(adminRole);
        }

        var userRole = await context.AppRoles.FirstOrDefaultAsync(r => r.Name == "User");
        if (userRole == null)
        {
            userRole = new AppRole { Id = Guid.NewGuid(), Name = "User"};
            context.AppRoles.Add(userRole);
        }

        await context.SaveChangesAsync();

        if (!await context.AppUsers.AnyAsync(u => u.Email == "admin@lifequest.com"))
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("Admin123!", out passwordHash, out passwordSalt);

            var adminUser = new AppUser
            {
                Id = Guid.NewGuid(),
                Name = "System",
                Surname = "Admin",
                UserName = "system_admin",
                Email = "admin@lifequest.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                TotalXP = 99999,
                GeneralLevel = 99,
                AppRoleId = adminRole.Id,
            };

            await context.AppUsers.AddAsync(adminUser);
            await context.SaveChangesAsync();
        }
    }
}