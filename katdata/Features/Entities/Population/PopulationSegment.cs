using System.ComponentModel.DataAnnotations;
using katdata.Features.Entities.Urban;

namespace katdata.Features.Entities.Population
{
    public abstract class PopulationSegment
    {
        [Key]
        public Guid Id { get; init; }

        public required Guid SegmentId { get; init; } // Link to Settlement

        public required int CurrentAge { get; set; } = 0;

        public abstract PopulationSegmentType Type { get; }

        public abstract int MaxAge { get; }

        protected long PopulationCount { get; set; }

        public long GetTotal() => this.PopulationCount;

        public void ProcessAge() => this.CurrentAge++;

        public void IncreasePopulation(int amount) => this.PopulationCount += amount;

        public void DecreasePopulation(int amount) => this.PopulationCount = Math.Max(0, this.PopulationCount - amount);


    }

    public sealed class NewBorn : PopulationSegment
    {
        public override PopulationSegmentType Type => PopulationSegmentType.NewBorn;

        public override int MaxAge => 12;
    }


    public sealed class Children : PopulationSegment
    {
        public override PopulationSegmentType Type => PopulationSegmentType.Children;

        public override int MaxAge => 24;

        public required decimal Education { get; set;}

        public long EducatedCount => (long)Math.Round(this.PopulationCount * this.Education);

        public long NonEducatedCount => this.PopulationCount - this.EducatedCount;

    }

    public sealed class AdultStudents : PopulationSegment
    {
        public override PopulationSegmentType Type => PopulationSegmentType.Studend;

        public required decimal Working { get; set; }

        public required decimal TecnincalEducation { get; set; }

        public required decimal HighEducation { get; set; }

        public override int MaxAge => 48;

        public long WorkingCount => (long)Math.Round(this.PopulationCount * this.Working);

        public long TecnicalSudentsCount => (long)Math.Round(this.PopulationCount * this.TecnincalEducation);

        public long HightEducationCount => (long)Math.Round(this.PopulationCount * this.HighEducation);

    }

    public sealed class AdultsWorking : PopulationSegment
    {
        public override PopulationSegmentType Type => PopulationSegmentType.Workforce;

        public required decimal BasicWorkers { get; set; } // Can only do basic labor

        public required decimal TecnicalWorkers { get; set; } // High-skill workers

        public required decimal HighLevelWorkers { get; set; } // Engineers, researchers, doctors

        public override int MaxAge => 168;

        public long BasicWorkersCount => (long)Math.Round(this.PopulationCount * this.BasicWorkers);

        public long TecnicalWorkersCount => (long)Math.Round(this.PopulationCount * this.TecnicalWorkers);

        public long HighLevelWorkersCount => (long)Math.Round(this.PopulationCount * this.HighLevelWorkers);
    }

    public sealed class Retired : PopulationSegment
    {
        public override PopulationSegmentType Type => PopulationSegmentType.Retired;

        public override int MaxAge => 48;

    }

    public sealed record GrowthIndicator
    {
        public required decimal Education { get; set; }

        public required decimal Health { get; init; }

    }

    public sealed record EducationIndicator
    {
        public required int Kindergarden { get; init; }

        public required int TecnicalSchoolVacancies { get; init; }

        public required int UniversityVacancies { get; init; }
    }

    public sealed record JobsIndicator
    {
        public required int LowSkillJobs { get; init; }

        public required int BasicSkillJobs { get; init; }

        public required int TecnicalSkillJobs { get; init; }

        public required int HighSkillJobs { get; init; }
    }

    public sealed class PopulationProcessor
    {

        private void MovingAgeingPopulation(Settlement settlement)
        {
            List<Population> segments = new()
            {

            }
        }

    }


    public sealed record PopulationEducation
    {
        public required long Uneducated { get; set; } // Can only do basic labor
        public required long BasicEducation { get; set; } // Low-skill workers
        public required long AdvancedEducation { get; set; } // High-skill workers
        public required long HigherEducation { get; set; } // Engineers, researchers, doctors

        // Total workforce by education level
        public long GetTotalEducated() => BasicEducation + AdvancedEducation + HigherEducation;

        // Graduates move up education levels each turn (based on schools & funding)
        public void GraduateStudents(long basic, long advanced, long higher)
        {
            Uneducated = Math.Max(0, Uneducated - basic);
            BasicEducation += basic;

            BasicEducation = Math.Max(0, BasicEducation - advanced);
            AdvancedEducation += advanced;

            AdvancedEducation = Math.Max(0, AdvancedEducation - higher);
            HigherEducation += higher;
        }
    }

    public sealed record PopulationEmployment
    {
        public required long TotalWorkforce { get; set; }
        public required long Employed { get; set; }
        public long Unemployed => TotalWorkforce - Employed;

        public decimal UnemploymentRate => TotalWorkforce == 0 ? 0 : (decimal)Unemployed / TotalWorkforce;

        // Update employment counts
        public void UpdateEmployment(long newJobs, long lostJobs)
        {
            Employed = Math.Max(0, Employed + newJobs - lostJobs);
        }
    }

    public sealed record PopulationGrowth
    {
        public required decimal BirthRate { get; set; } // Births per 1,000 people per turn
        public required decimal DeathRate { get; set; } // Deaths per 1,000 people per turn

        public long CalculateBirths(long totalPopulation) => (long)(totalPopulation * BirthRate / 1000);
        public long CalculateDeaths(long totalPopulation) => (long)(totalPopulation * DeathRate / 1000);
    }

    public enum PopulationSegmentType
    {
        None = 0,
        NewBorn,
        Children,
        Studend,
        Workforce,
        Retired,
    }
}
