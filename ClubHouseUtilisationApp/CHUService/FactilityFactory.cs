using CHUService.Facilities;

namespace CHUService
{
    public abstract class FactilityFactory
    {
        public abstract IBaseFacility GetFactilityInstance(string type);
    }
}
