using katdata.Features.Entities;
using katdata.Features.Entities.Population;
using katdata.Features.Entities.Urban;
using katdata.Tools;

namespace katdata.Services
{
    public sealed class GameSetUp(
        Repository<Province, Guid> provinceRepo
        )
    {

        public async Task SetUp()
        {
            var provinceId = Guid.NewGuid();
            var settlementId = Guid.NewGuid();


            var population = new SettlementPopulation()
            {
                Id = settlementId,
                NewBornPopulation = SetNewBornPopulation(),
                ChildrenPopulation = SetChildrenPopulation(),
                YoungAdults = SetYoungAdultsPopulation(),
                WorkingAdults = SetWorkingAdultsPopulation(),
                Retired = SetRetiredPopulation(),
            };

            var settlement = new Settlement()
            {
                Name = "Vila Bosta",
                ProvinceId = provinceId,
                Population = population,
                Type = Features.Entities.Utils.SettlementType.Village,
            };

            var province = new Province()
            {
                ProvinceId = provinceId,
                ProvinceName = "Banana Republic",
                Settlements = [settlement],
            };

            await provinceRepo.SaveAsync(province);

        }

        private static NewBorn SetNewBornPopulation()
        {
            var newBorns = new NewBorn
            {
                SegmentId = Guid.NewGuid(), // Link to a settlement or region
                Buckets = new List<long> { 0, 0, 0, 0 } // 4 buckets for 0–24 months
            };

            // Add some initial newborns
            newBorns.Buckets[1] = 100; // 100 newborns aged 0–6 months
            return newBorns;
        }

        private static Children SetChildrenPopulation()
        {
            var children = new Children
            {
                SegmentId = Guid.NewGuid(), // Link to a settlement or region
                Buckets = new List<long>(new long[20]) // 20 buckets for 24–144 months
            };
            // Add some initial children
            children.Buckets[0] = 500; // 500 children aged 24–30 months
            return children;
        }

        private static YoungAdults SetYoungAdultsPopulation()
        {
            var youngAdults = new YoungAdults
            {
                SegmentId = Guid.NewGuid(), // Link to a settlement or region
                Buckets = new List<long>(new long[12]) // 12 buckets for 144–216 months
            };

            // Add some initial young adults
            youngAdults.Buckets[0] = 300; // 300 young adults aged 144–150 months
            return youngAdults;
        }

        public static WorkingAdults SetWorkingAdultsPopulation()
        {
            var workingAdults = new WorkingAdults
            {
                SegmentId = Guid.NewGuid(), // Link to a settlement or region
                Buckets = new List<long>(new long[84]) // 84 buckets for 216–720 months
            };

            // Add some initial working adults
            workingAdults.Buckets[0] = 1000; // 1000 working adults aged 216–222 months         
            return workingAdults;
        }

        public static Retired SetRetiredPopulation()
        {
            var retired = new Retired
            {
                SegmentId = Guid.NewGuid(), // Link to a settlement or region
                Buckets = new List<long>(new long[60]) // 60 buckets for 720–1080 months
            };

            // Add some initial retired individuals
            retired.Buckets[0] = 200; // 200 retired individuals aged 720–726 months
            return retired;
        }


    }
}
