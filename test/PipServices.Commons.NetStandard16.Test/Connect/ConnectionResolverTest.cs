using PipServices.Commons.Config;
using PipServices.Commons.Connect;
using PipServices.Commons.Errors;
using PipServices.Commons.Refer;
using System;
using System.Linq;
using Xunit;

namespace PipServices.Commons.Test.Connect
{
    public sealed class ConnectionResolverTest
    {
        private static readonly ConfigParams RestConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", 3000
        );

        private ConnectionResolver _connectionResolver;

        public ConnectionResolverTest()
        {
            _connectionResolver = new ConnectionResolver(RestConfig);
            _connectionResolver.SetReferences(new References());
        }

        [Fact]
        public void TestConfigure()
        {
            var config = _connectionResolver.GetAll().FirstOrDefault();
            Assert.Equal(config.Get("protocol"), "http");
            Assert.Equal(config.Get("host"), "localhost");
            Assert.Equal(config.Get("port"), "3000");
        }

        [Fact]
        public void TestRegister()
        {
            var connectionParams = new ConnectionParams();
            _connectionResolver.RegisterAsync("correlationId", connectionParams).Wait();
            var configList = _connectionResolver.GetAll();

            Assert.Equal(configList.Count(), 1);

            connectionParams.DiscoveryKey = "Discovery key value";
            _connectionResolver.RegisterAsync("correlationId", connectionParams).Wait();
            configList = _connectionResolver.GetAll();

            Assert.Equal(configList.Count(), 2);

            _connectionResolver.RegisterAsync("correlationId", connectionParams).Wait();
            configList = _connectionResolver.GetAll();
            var configFirst = configList.FirstOrDefault();
            var configLast = configList.LastOrDefault();

            Assert.Equal(configList.Count(), 3);
            Assert.Equal(configFirst.Get("protocol"), "http");
            Assert.Equal(configFirst.Get("host"), "localhost");
            Assert.Equal(configFirst.Get("port"), "3000");
            Assert.Equal(configLast.Get("discovery_key"), "Discovery key value");
        }

        [Fact]
        public void TestResolve()
        {
            var connectionParams = _connectionResolver.ResolveAsync("correlationId").Result;

            Assert.Equal(connectionParams.Get("protocol"), "http");
            Assert.Equal(connectionParams.Get("host"), "localhost");
            Assert.Equal(connectionParams.Get("port"), "3000");

            var restConfigDiscovery = ConfigParams.FromTuples(
                "connection.protocol", "http",
                "connection.host", "localhost",
                "connection.port", 3000,
                "connection.discovery_key", "Discovery key value"
            );

            IReferences references = new References();
            _connectionResolver = new ConnectionResolver(restConfigDiscovery, references);
            try
            {
                _connectionResolver.ResolveAsync("correlationId").Wait();
            }
            catch (Exception ex)
            {
                Assert.IsType<ConfigException>(ex.InnerException);
            }
        }
    }
}
