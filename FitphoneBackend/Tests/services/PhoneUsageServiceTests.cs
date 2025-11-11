using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Services;
using FitPhoneBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FitphoneBackend.Tests.Services
{
    public class PhoneUsageServiceTests
    {
        private async Task<ApplicationDbContext> GetInMemoryDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique db name per test
                .Options;

            var context = new ApplicationDbContext(options);

            // Seed some test data
            context.PhoneUsages.AddRange(
                new PhoneUsage(Guid.NewGuid(), Guid.NewGuid(), 5, 120),
                new PhoneUsage(Guid.NewGuid(), Guid.NewGuid(), 3, 60)
            );

            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task GetPhoneUsageByUserIdAsync_ReturnsCorrectUsage()
        {
            var context = await GetInMemoryDbContextAsync();
            var service = new PhoneUsageService(context);

            var userId = context.PhoneUsages.First().UserId;
            var usage = await service.GetPhoneUsageByUserIdAsync(userId);

            Assert.NotNull(usage);
            Assert.Equal(userId, usage!.UserId);
        }

        [Fact]
        public async Task GetPhoneUsageByIdAsync_ReturnsCorrectUsage()
        {
            var context = await GetInMemoryDbContextAsync();
            var service = new PhoneUsageService(context);

            var id = context.PhoneUsages.First().Id;
            var usage = await service.GetPhoneUsageByIdAsync(id);

            Assert.NotNull(usage);
            Assert.Equal(id, usage!.Id);
        }

        [Fact]
        public async Task GetAllPhoneUsageAsync_ReturnsAllUsages()
        {
            var context = await GetInMemoryDbContextAsync();
            var service = new PhoneUsageService(context);

            var usages = await service.GetAllPhoneUsageAsync();

            Assert.Equal(2, usages.Count());
        }

        [Fact]
        public async Task CreatePhoneUsageAsync_AddsNewUsage()
        {
            var context = await GetInMemoryDbContextAsync();
            var service = new PhoneUsageService(context);

            var newUsage = new PhoneUsage(Guid.NewGuid(), Guid.NewGuid(), 7, 200);
            var created = await service.CreatePhoneUsageAsync(newUsage);

            var fromDb = await context.PhoneUsages.FindAsync(created.Id);

            Assert.NotNull(fromDb);
            Assert.Equal(newUsage.UserId, fromDb!.UserId);
            Assert.Equal(newUsage.UnlockCount, fromDb.UnlockCount);
        }

        [Fact]
        public async Task UpdatePhoneUsageAsync_UpdatesExistingUsage()
        {
            var context = await GetInMemoryDbContextAsync();
            var service = new PhoneUsageService(context);

            var usage = context.PhoneUsages.First();
            usage.Update(usage.UnlockCount + 1, usage.ScreenTimeMinutes + 10); // if you have a DDD Update method
            await service.UpdatePhoneUsageAsync(usage);

            var fromDb = await context.PhoneUsages.FindAsync(usage.Id);
            Assert.Equal(usage.UnlockCount, fromDb!.UnlockCount);
        }

        [Fact]
        public async Task DeletePhoneUsageAsync_RemovesUsage()
        {
            var context = await GetInMemoryDbContextAsync();
            var service = new PhoneUsageService(context);

            var usage = context.PhoneUsages.First();
            await service.DeletePhoneUsageAsync(usage.Id);

            var fromDb = await context.PhoneUsages.FindAsync(usage.Id);
            Assert.Null(fromDb);
        }
    }
}
