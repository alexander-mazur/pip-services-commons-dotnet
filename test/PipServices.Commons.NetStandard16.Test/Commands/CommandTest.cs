﻿using PipServices.Commons.Errors;
using PipServices.Commons.Run;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PipServices.Commons.Commands
{
    public sealed class CommandTest
    {
        private class CommandExec : IExecutable
        {
            public Task<object> ExecuteAsync(string correlationId, Parameters args)
            {
                if (correlationId == "wrongId")
                    throw new ApplicationException(null, null, null, "Test error");

                return Task.FromResult((object)0);
            }
        }

        [Fact]
        public void TestGetName()
        {
            var command = new Command("name", null, new CommandExec());
            Assert.Equal("name", command.Name);
        }

        [Fact]
        public void testExecute()
        {
            var command = new Command("name", null, new CommandExec());

            Dictionary<int, object> map = new Dictionary<int, object>();
            map.Add(8, "title 8");
            map.Add(11, "title 11");
            Parameters param = new Parameters(map);

            Assert.Equal(command.ExecuteAsync("a", param).Result, 0);

            //try
            //{
            //    var result = command.ExecuteAsync("wrongId", param).Result;
            //}
            //catch (ApplicationException e)
            //{
            //    Assert.Equal("Test error", e.Message);
            //}
        }

    }
}
