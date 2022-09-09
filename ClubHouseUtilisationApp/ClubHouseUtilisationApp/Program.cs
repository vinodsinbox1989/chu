
using CHUService;
using CHUService.Facilities;

ConcreteFactilityFactory factory = new ConcreteFactilityFactory();

Console.WriteLine("Enter the user name whom booking is to be done (Vinod, UserA, UserB, UserC): ");
var userName = Console.ReadLine();

userName = !String.IsNullOrEmpty(userName) ? userName : "Vinod";

Console.WriteLine($"Booking for the user {userName}: ");

BookFacility(factory, userName);

Console.ReadKey();

static void BookFacility(ConcreteFactilityFactory factory, string userName)
{
    Console.WriteLine("Enter your Facility Type (Pool, KidsPlayArea, Gym, Library): ");

    var type = Console.ReadLine();

    IBaseFacility instance = factory.GetFactilityInstance(!String.IsNullOrEmpty(type) ? type : "Gym", userName);
    instance.Book();

    Console.WriteLine("Do you wish book another facility (Y/N): ");
    var yesNo = Console.ReadLine();
    switch (yesNo)
    {
        case "Y":
            BookFacility(factory, userName);
            break;
        case "N":
            Console.WriteLine("Thank you booking.");
            break;
        default:
            Console.WriteLine("Invalid input.");
            break;
    }
}