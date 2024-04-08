namespace WebShop.Testing.DummyClasses
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Core.Data;
    using Core.Models.Identity;
    public static class UserManagerMockSetup
    {
        public static Mock<UserManager<ApplicationUser>> ConfigureMock(this Mock<UserManager<ApplicationUser>> mock, ApplicationDbContext context)
        {
            mock.Setup(m => m.Users)
                .Returns(context.Users);

            mock.Setup(m => m.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .Returns<ApplicationUser>((user) => GetRoles(user, context));

            mock.Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns<ApplicationUser, string>((user, role) => AddToRole(user, role, context));

            mock.Setup(m => m.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns<ApplicationUser, string>((user, role) => RemoveFromRole(user, role, context));

            return mock;
        }

        private static async Task<IList<string>> GetRoles(ApplicationUser user, ApplicationDbContext context)
        {
            var roles = await context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

            return roles;
        }

        private static async Task<IdentityResult> AddToRole(ApplicationUser user, string role,
            ApplicationDbContext context)
        {
            var roleObj = context.Roles.FirstOrDefault(r => r.Name == role);
            var result = IdentityResult.Success;

            if (roleObj == null)
            {
                result = IdentityResult.Failed(new[]
                {
                    new IdentityError()
                    {
                        Description = "Invalid role."
                    }
                });
            }

            if (!result.Succeeded)
            {
                return result;
            }

            await context.UserRoles.AddAsync(new()
            {
                UserId = user.Id,
                RoleId = roleObj.Id
            });
            await context.SaveChangesAsync();

            return result;
        }

        private static async Task<IdentityResult> RemoveFromRole(ApplicationUser user, string role, ApplicationDbContext context)
        {
            var result = IdentityResult.Success;
            var roleObj = await context.Roles.FirstOrDefaultAsync(r => r.Name == role);
            if (roleObj == null)
            {
                result = IdentityResult.Failed(new[]
                {
                    new IdentityError()
                    {
                        Description = "Invalid role"
                    }
                });
            }

            if (!result.Succeeded)
            {
                return result;
            }

            var roleId = roleObj.Id;
            var userRole =
                await context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == roleId);

            if (userRole == null)
            {
                throw new InvalidOperationException("Role not set to user");
            }

            context.Remove(userRole);
            await context.SaveChangesAsync();
            return result;
        }
    }
}
