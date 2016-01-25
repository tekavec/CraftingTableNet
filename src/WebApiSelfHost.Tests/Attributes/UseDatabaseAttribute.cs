using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace WebApiSelfHost.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UseDatabaseAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {
            new ServerBootstrap().CreateDatabase();
        }

        public void AfterTest(ITest test)
        {
            new ServerBootstrap().DropDatabase();
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}