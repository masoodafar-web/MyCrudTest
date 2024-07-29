namespace CrudTestMicroservice.Domain.Events;
public class UpdateCustomerEvent : BaseEvent
{
    public UpdateCustomerEvent(Customer item)
    {
    }
    public Customer Item { get;  }
}
