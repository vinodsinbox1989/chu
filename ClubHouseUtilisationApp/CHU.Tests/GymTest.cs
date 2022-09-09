using CHUService;
using CHUService.Facilities;

namespace CHU.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GymTest()
        {
            ConcreteFactilityFactory factory = new ConcreteFactilityFactory();
            string type = "Gym";
            string userName = "Vinod";

            IBaseFacility instance = factory.GetFactilityInstance(!String.IsNullOrEmpty(type) ? type : "Pool", userName);            

            //Act
            var actual = instance.Book();

            //Assert
            Assert.IsTrue(actual);
        }
    }
}