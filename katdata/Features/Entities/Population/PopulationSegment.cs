using System.ComponentModel.DataAnnotations;
using katdata.Features.Entities.Utils;
using Microsoft.EntityFrameworkCore;

namespace katdata.Features.Entities.Population;

public abstract class PopulationSegment
{
    //[Key]
    //public Guid Id { get; init; }

    public required Guid SegmentId { get; init; } // Link to Settlement

    public abstract PopulationSegmentType Type { get; }

    // List of buckets: Each bucket represents an age range (e.g., 0–6 months)
    public List<long> Buckets { get; set; } = new List<long>();

    public long GetTotal() => Buckets.Sum();
}

[Owned]
public sealed class NewBorn : PopulationSegment
{
    public override PopulationSegmentType Type => PopulationSegmentType.NewBorn;

    public long ProcessTurn(Children childrenSegment)
    {
        // Shift populations between buckets
        for (int i = Buckets.Count - 1; i > 0; i--)
        {
            Buckets[i] = Buckets[i - 1];
        }

        // Add new newborns to the first bucket
        Buckets[0] = GetNewBornCount(); // Replace with your logic for new births

        // Move the last bucket to the Children segment
        long countToMove = Buckets.Last();
        childrenSegment.Buckets[0] += countToMove;
        Buckets[^1] = 0; // Reset the last bucket

        return countToMove;
    }

    public long ReturnBucktes(int group)
    {
        return this.Buckets[group];
    }

    private long GetNewBornCount()
    {
        return 20;
    }
}

[Owned]
public sealed class Children : PopulationSegment
{
    public override PopulationSegmentType Type => PopulationSegmentType.Children;

    public long ProcessTurn(YoungAdults youngAdultsSegment)
    {
        // Shift populations between buckets
        for (int i = Buckets.Count - 1; i > 0; i--)
        {
            Buckets[i] = Buckets[i - 1];
        }

        // Move the last bucket to the YoungAdults segment
        long countToMove = Buckets.Last();
        youngAdultsSegment.Buckets[0] += countToMove;
        Buckets[^1] = 0; // Reset the last bucket

        return countToMove;
    }

    public long ReturnBucktes(int group)
    {
        return this.Buckets[group];
    }
}

[Owned]
public sealed class YoungAdults : PopulationSegment
{
    public override PopulationSegmentType Type => PopulationSegmentType.YoungAdults;

    public long ProcessTurn(WorkingAdults workingAdultsSegment)
    {
        // Shift populations between buckets
        for (int i = Buckets.Count - 1; i > 0; i--)
        {
            Buckets[i] = Buckets[i - 1];
        }

        // Move the last bucket to the WorkingAdults segment
        long countToMove = Buckets.Last();
        workingAdultsSegment.Buckets[0] += countToMove;
        Buckets[^1] = 0; // Reset the last bucket

        return countToMove;
    }
}

[Owned]
public sealed class WorkingAdults : PopulationSegment
{
    public override PopulationSegmentType Type => PopulationSegmentType.Workforce;

    public long ProcessTurn(Retired retiredSegment)
    {
        // Shift populations between buckets
        for (int i = Buckets.Count - 1; i > 0; i--)
        {
            Buckets[i] = Buckets[i - 1];
        }

        // Move the last bucket to the Retired segment
        long countToMove = Buckets.Last();
        retiredSegment.Buckets[0] += countToMove;
        Buckets[^1] = 0; // Reset the last bucket

        return countToMove;
    }
}

[Owned]
public sealed class Retired : PopulationSegment
{
    public override PopulationSegmentType Type => PopulationSegmentType.Retired;

    public void ProcessTurn()
    {
        // Shift populations between buckets
        for (int i = Buckets.Count - 1; i > 0; i--)
        {
            Buckets[i] = Buckets[i - 1];
        }

        // Reset the first bucket (no new individuals enter the Retired segment)
        Buckets[0] = 0;
    }
}

