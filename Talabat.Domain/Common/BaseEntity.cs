#nullable disable

namespace Talabat.Domain.Common
{
    public abstract class BaseEntity <Tkey> where Tkey: IEquatable<Tkey>
    {
        public  Tkey Id { get; set; }
    }
}
