namespace ATC.Abstractions.BillingSystem.TariffPlan
{
    public interface ITariffPlan
    {
        decimal GetPrice(int time);
    }
}
