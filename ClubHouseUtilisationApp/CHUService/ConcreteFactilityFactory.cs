using CHUService.Facilities;

namespace CHUService
{
    public class ConcreteFactilityFactory : FactilityFactory
    {

        public override IBaseFacility GetFactilityInstance(string facilityType = "Pool")
        {
            switch (facilityType)
            {
                case "Pool":
                    return new PoolFacility();
                case "Gym":
                    return new GymFacility();
                case "KidsPlayArea":
                    return new KidsPlayAreaFacility();
                case "Library":
                    return new LibraryFacility();
                default:
                    throw new ApplicationException($"Facility {facilityType} cannot be created");
            }
        }
    }
}
