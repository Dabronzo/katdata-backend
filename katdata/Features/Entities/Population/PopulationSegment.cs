using System.ComponentModel.DataAnnotations;
using katdata.Features.Entities.Urban;

namespace katdata.Features.Entities.Population
{
    public abstract class PopulationSegment
    {
        [Key]
        public Guid Id { get; init; }

        public required Guid SettlementId { get; init; } // Link to Settlement

        public required int LifeCicle { get; set; }

        public abstract PopulationSegmentType Type { get; }

        public required long Total { get; set; }

    }

    public sealed class Children : PopulationSegment
    {
        public override PopulationSegmentType Type => PopulationSegmentType.Children;

        public required long Student {  get; init; }

        public required long Uneducated {  get; init; }

    }

    public sealed class AdultStudents : PopulationSegment
    {
        public override PopulationSegmentType Type => PopulationSegmentType.Studend;

        public required long BasicEducation { get; set; }

        public required long AdvancedEducation { get; set; }

        public required long HigherEducation { get; set; }

    }

    public sealed class AdultsWorking : PopulationSegment
    {
        public override PopulationSegmentType Type => PopulationSegmentType.Workforce;

        public required long LowLevel { get; set; } // Can only do basic labor

        public required long BasicLevel { get; set; } // Low-skill workers

        public required long Tecnical { get; set; } // High-skill workers

        public required long HighLevel { get; set; } // Engineers, researchers, doctors
    }

    public sealed class Retired : PopulationSegment
    {
        public override PopulationSegmentType Type => PopulationSegmentType.Retired;

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
        public void ProcessTurn(Settlement settlement)
        {
            ProcessGrowth(settlement);
            ProcessEducation(settlement);
            ProcessWorkforce(settlement);
            ProcessRetirement(settlement);
        }

        private void ProcessGrowth(Settlement settlement)
        {
            //var growthRate = settlement.GrowthIndicator.Health * 0.01m; // Example scaling
            //var newBirths = (long)(settlement.TotalPopulation * growthRate);
            settlement.Children.Total += 10;
        }

        private void ProcessEducation(Settlement settlement)
        {
            var studentsMovingUp = (long)(settlement.Children.Student * 0.1); // Example: 10% move up per turn
            settlement.Children.Total -= studentsMovingUp;
            settlement.AdultStudens.BasicEducation += studentsMovingUp;
        }

        private void ProcessWorkforce(Settlement settlement)
        {
            var graduates = (long)(settlement.AdultStudens.BasicEducation * 0.2); // Example: 20% enter workforce
            settlement.AdultStudens.BasicEducation -= graduates;
            settlement.AdultStudens.AdvancedEducation += graduates;
        }

        private void ProcessRetirement(Settlement settlement)
        {
            var retiringWorkers = (long)(settlement.Workforce.LowLevel * 0.05); // Example: 5% retire per turn
            settlement.Workforce.LowLevel -= retiringWorkers;
            settlement.Retired.Total += retiringWorkers;
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
        Children,
        Studend,
        Workforce,
        Retired,
    }
}
