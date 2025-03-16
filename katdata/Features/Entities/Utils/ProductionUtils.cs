namespace katdata.Features.Entities.Models;

public sealed record FacilityInventoryUnit
{
    public required ResourceName Material { get; init; }

    public required long Quantity { get; set; }
}

public sealed record MonthlyProfitRecord
{
    public int Month { get; init; }
    public decimal Income { get; init; }
    public decimal Expenses { get; init; }
    public decimal NetProfit { get; init; }
}
