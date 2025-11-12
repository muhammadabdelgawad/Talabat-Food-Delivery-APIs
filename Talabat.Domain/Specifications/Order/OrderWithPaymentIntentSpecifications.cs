namespace Talabat.Domain.Specifications.Order
{
    public class OrderWithPaymentIntentSpecifications :BaseSpecifications<Entities.Orders.Order, int>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId)
         :base(order => order.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
