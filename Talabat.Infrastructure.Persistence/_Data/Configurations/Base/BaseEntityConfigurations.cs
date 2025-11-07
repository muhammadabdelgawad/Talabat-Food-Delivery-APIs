namespace Talabat.Infrastructure.Persistence.Data.Configurations.Base
{
  // [DbContextType(typeof(StoreDbContext))]
    public class BaseEntityConfigurations<TEntity,Tkey> : IEntityTypeConfiguration<TEntity>
        where TEntity: BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
           builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
