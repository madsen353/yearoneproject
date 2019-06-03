namespace Funhall2.Classes
{
    public interface ICustomer
    {
        string BookingId { get; set; }
        int CusId { get; set; }
        string Email { get; set; }
        string Name { get; set; }
        bool Segway { get; set; }
        bool Subscription { get; set; }
        int TotalAmountOfPoints { get; set; }

        int GetTotalAmountOfPoints();
    }
}