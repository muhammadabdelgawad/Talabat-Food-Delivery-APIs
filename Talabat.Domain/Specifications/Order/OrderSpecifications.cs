namespace Talabat.Domain.Specifications.Order
{
    public class OrderSpecifications : BaseSpecifications<Entities.Orders.Order, int>
    {
        public OrderSpecifications(string buyerEmail, int orderId) 
             : base(order => order.Id== orderId && order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
        }
        public OrderSpecifications(string buyerEmail) 
             : base(order => order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
            AddOrderByDesc(order => order.OrderDate);
        }
        
            
       private protected override void AddIncludes()
        {
            Includes.Add(o => o.DeliveryMethod!);
            Includes.Add(o => o.Items);
        }
    }
}
