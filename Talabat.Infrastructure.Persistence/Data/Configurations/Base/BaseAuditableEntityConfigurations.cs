using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Talabat.Infrastructure.Persistence.Data.Configurations.Base
{
    public class BaseAuditableEntityConfigurations<TEntity,TKey> :BaseEntityConfigurations<TEntity, TKey>
        where TEntity :BaseEntity<TKey>
        where TKey :IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
