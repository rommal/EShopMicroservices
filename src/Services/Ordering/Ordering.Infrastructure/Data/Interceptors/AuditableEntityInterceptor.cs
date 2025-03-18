using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context is null) return;

            foreach(var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.LastModifiedBy = entry.Entity.CreatedBy = "romanm";
                    entry.Entity.LastModified = entry.Entity.CreatedAt = DateTime.UtcNow;
                }
                
                if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntites())
                {
                    entry.Entity.LastModifiedBy = "anotheruser";
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }
    }

    public static class Extensions
    {
        public static bool HasChangedOwnedEntites(this EntityEntry entry) =>
            entry.References.Any(e => e.TargetEntry != null && e.TargetEntry.Metadata.IsOwned() && 
            (e.TargetEntry.State == EntityState.Added || e.TargetEntry.State == EntityState.Modified));
    }
}
