namespace Funhall2.Classes
{
    public interface ICustomerActivity
    {
        IActivity Activity { get; set; }
        ICustomer Customer { get; set; }
        string Name { get; }
        int Points { get; set; }

        string ToString();
    }
}