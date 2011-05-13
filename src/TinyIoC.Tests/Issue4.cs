using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinyIoC.Tests.TestData;
using TinyIoC.Tests.TestData.NestedInterfaceDependencies;

namespace TinyIoC.Tests
{
    [TestClass]
    public class Issue4
    {
        [TestMethod]
        public void Resolve_InterfacesAcrossInChildContainer_Resolves()
        {
            var container = UtilityMethods.GetContainer();

            container.Register<IService2, Service2>().AsMultiInstance();

            container.Register<IService4, Service4>().AsMultiInstance();

            container.Register<IService5, Service5>().AsMultiInstance();

            var child = container.GetChildContainer();

            var nestedService = new Service3();
            child.Register<IService3>(nestedService);


            var service5 = child.Resolve<IService5>();

            Assert.IsNotNull(service5.Service4);

            Assert.AreSame(nestedService, service5.Service4.Service2.Service3);
        }
    }
}