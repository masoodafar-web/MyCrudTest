namespace CrudTestMicroservice.Domain.Events;
public class DeleteCustomerEvent : BaseEvent
{
    public DeleteCustomerEvent(Customer item)
    {
    }
    public Customer Item { get;  }
}
