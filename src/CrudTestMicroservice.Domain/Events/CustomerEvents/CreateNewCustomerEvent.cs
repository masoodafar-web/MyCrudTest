namespace CrudTestMicroservice.Domain.Events;
public class CreateNewCustomerEvent : BaseEvent
{
    public CreateNewCustomerEvent(Customer item)
    {
    }
    public Customer Item { get;  }
}
