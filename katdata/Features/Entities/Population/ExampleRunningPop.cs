namespace katdata.Features.Entities.Population;

//public sealed class ExampleRunningPop
//{

//    public void ProcessPopulation()
//    {
//        var newBorns = new NewBorn
//        {
//            Id = Guid.NewGuid(),
//            SegmentId = Guid.NewGuid(), // Link to a settlement or region
//            Buckets = new List<long> { 0, 0, 0, 0 } // 4 buckets for 0–24 months
//        };

//        var children = new Children
//        {
//            Id = Guid.NewGuid(),
//            SegmentId = Guid.NewGuid(), // Link to a settlement or region
//            Buckets = new List<long>(new long[20]) // 20 buckets for 24–144 months
//        };

//        var youngAdults = new YoungAdults
//        {
//            Id = Guid.NewGuid(),
//            SegmentId = Guid.NewGuid(), // Link to a settlement or region
//            Buckets = new List<long>(new long[12]) // 12 buckets for 144–216 months
//        };

//        var workingAdults = new WorkingAdults
//        {
//            Id = Guid.NewGuid(),
//            SegmentId = Guid.NewGuid(), // Link to a settlement or region
//            Buckets = new List<long>(new long[84]) // 84 buckets for 216–720 months
//        };

//        var retired = new Retired
//        {
//            Id = Guid.NewGuid(),
//            SegmentId = Guid.NewGuid(), // Link to a settlement or region
//            Buckets = new List<long>(new long[60]) // 60 buckets for 720–1080 months
//        };

//        // Add some initial newborns
//        newBorns.Buckets[1] = 100; // 100 newborns aged 0–6 months

//        // Add some initial children
//        children.Buckets[0] = 500; // 500 children aged 24–30 months

//        // Add some initial young adults
//        youngAdults.Buckets[0] = 300; // 300 young adults aged 144–150 months

//        // Add some initial working adults
//        workingAdults.Buckets[0] = 1000; // 1000 working adults aged 216–222 months

//        // Add some initial retired individuals
//        retired.Buckets[0] = 200; // 200 retired individuals aged 720–726 months

//        var segments = new List<PopulationSegment> { newBorns, children, youngAdults, workingAdults, retired };

//        // Simulate 12 turns (1 year)
//        for (int turn = 1; turn <= 12; turn++)
//        {
//            Console.WriteLine($"--- Turn {turn} ---");
//            Console.WriteLine($"Before process the turn");
//            Console.WriteLine($"NewBorn Buckets 0: {newBorns.ReturnBucktes(0)}");
//            Console.WriteLine($"NewBorn Buckets 1: {newBorns.ReturnBucktes(1)}");
//            Console.WriteLine($"NewBorn Buckets 2: {newBorns.ReturnBucktes(2)}");
//            Console.WriteLine($"NewBorn Buckets 3: {newBorns.ReturnBucktes(3)}");
//            Console.WriteLine($"Children Buckets 0: {children.ReturnBucktes(0)}");
//            Console.WriteLine($"Children Buckets 1: {children.ReturnBucktes(1)}");
//            Console.WriteLine($"Children Buckets 2: {children.ReturnBucktes(2)}");
//            Console.WriteLine($"Children Buckets 3: {children.ReturnBucktes(3)}");
//            Console.WriteLine($"Children Buckets 4: {children.ReturnBucktes(4)}");

//            //Console.WriteLine($"Total NewBorns: {newBorns.GetTotal()}");
//            //Console.WriteLine($"Total Children: {children.GetTotal()}");

//            //Console.WriteLine($"TotalYoung Adults: {youngAdults.GetTotal()}");
//            //Console.WriteLine($"Total Workadults: {workingAdults.GetTotal()}");
//            //Console.WriteLine($"Total Retired: {retired.GetTotal()}");

//            // Process the turn
//            long newChildren = newBorns.ProcessTurn(children);
//            long newYoungAdults = children.ProcessTurn(youngAdults);
//            long newWorkingAdults = youngAdults.ProcessTurn(workingAdults);
//            long newRetired = workingAdults.ProcessTurn(retired);
//            retired.ProcessTurn();

//            // Output the results
//            Console.WriteLine($"New Children: {newChildren}");
//            Console.WriteLine($"New Young Adults: {newYoungAdults}");
//            Console.WriteLine($"New Working Adults: {newWorkingAdults}");
//            Console.WriteLine($"New Retired: {newRetired}");
//            Console.WriteLine();

//            // Use population data for game mechanics
//            UpdateGameMechanics(newBorns, children, youngAdults, workingAdults, retired);
//        }
//    }

//    public void UpdateGameMechanics(NewBorn newBorns, Children children, YoungAdults youngAdults, WorkingAdults workingAdults, Retired retired)
//    {
//        // Example: Calculate total workforce (YoungAdults + WorkingAdults)
//        long workforce = workingAdults.GetTotal();

//        // Example: Calculate housing demand
//        long totalPopulation = newBorns.GetTotal() + children.GetTotal() + youngAdults.GetTotal() + workingAdults.GetTotal() + retired.GetTotal();
//        long housingDemand = totalPopulation / 4; // Assume 1 house per 4 people

//        // Example: Calculate education demand (Children)
//        long educationDemand = children.GetTotal();

//        // Example: Calculate retirement benefits cost
//        long retirementCost = retired.GetTotal() * 100; // Assume $100 per retired person

//        // Output the results
        
//        Console.WriteLine($"Children Buckets 0: {children.ReturnBucktes(0)}");
//        Console.WriteLine($"Children Buckets 1: {children.ReturnBucktes(1)}");
//        Console.WriteLine($"Children Buckets 2: {children.ReturnBucktes(2)}");
//        Console.WriteLine($"Children Buckets 3: {children.ReturnBucktes(3)}");
//        Console.WriteLine($"Children Buckets 4: {children.ReturnBucktes(4)}");
//        Console.WriteLine($"Total Workadults: {workforce}");
//        Console.WriteLine($"TotalYoung Adults: {youngAdults.GetTotal()}");
//        Console.WriteLine($"Total Children: {children.GetTotal()}");
//        Console.WriteLine($"Total Retired: {retired.GetTotal()}");
//        Console.WriteLine($"Housing Demand: {housingDemand}");
//        Console.WriteLine($"Education Demand: {educationDemand}");
//        Console.WriteLine($"Retirement Cost: {retirementCost}");
//        Console.WriteLine();
//    }
//}
