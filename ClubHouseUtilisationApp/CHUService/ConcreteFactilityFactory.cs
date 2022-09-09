using CHUService.Facilities;

namespace CHUService
{
    public class ConcreteFactilityFactory : FactilityFactory
    {

        public override IBaseFacility GetFactilityInstance(string facilityType, string userName)
        {
            switch (facilityType)
            {
                case "Pool":
                    return new PoolFacility(facilityType, userName);
                case "Gym":
                    return new GymFacility(facilityType, userName);
                case "KidsPlayArea":
                    return new KidsPlayAreaFacility(facilityType, userName);
                case "Library":
                    return new LibraryFacility(facilityType, userName);
                default:
                    throw new ApplicationException($"Facility {facilityType} cannot be created");
            }
        }
    }
}
