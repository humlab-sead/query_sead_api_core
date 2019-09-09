﻿using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SeadQueryCore;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using SeadQueryCore.QueryBuilder;
using System.Diagnostics;

namespace SeadQueryTest.FacetsConfig
{
    [TestClass]
    public class QueryBuilderTests
    {
        private fixtures.FacetConfigGenerator fixture;
        private static IContainer container;
        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [ClassInitialize()]
        public static void InitializeClass(TestContext context)
        {
            container = new TestDependencyService().Register();
        }

        [TestInitialize()]
        public void Initialize() {
            fixture = new fixtures.FacetConfigGenerator();
        }


    }
}
