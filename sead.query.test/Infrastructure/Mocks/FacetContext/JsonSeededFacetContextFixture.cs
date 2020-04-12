﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SeadQueryTest.Infrastructure
{
    using ItemsDictionary = Dictionary<Type, IEnumerable<object>>;

    public class JsonSeededFacetContextFixture : IDisposable
    {
        /// <summary>
        /// Reads Json Facet Schema entities and stores them in a dictionary
        /// </summary>

        private Lazy<ItemsDictionary> LazyItems;
        public ItemsDictionary Items => LazyItems.Value;
        public string Folder { get; }
        public ICollection<Type> Types { get; }

        public JsonSeededFacetContextFixture()
        {
            Folder = ScaffoldUtility.JsonDataFolder();
            Types = ScaffoldUtility.GetModelTypes();
            LazyItems = new Lazy<ItemsDictionary>(Load);
        }

        //public JsonSeededFacetContextFixture(string folder, ICollection<Type> types) : this()
        //{
        //    Folder = folder;
        //    Types = types;
        //}

        protected ItemsDictionary Load()
        {
            // ... initialize data in the test database ...
            Console.WriteLine("INFO: JsonSeededFacetContextFixture");
            var reader = new JsonReaderService(new IgnoreJsonAttributesResolver());
            var items = new ItemsDictionary();
            foreach (var type in Types) {
                var entities = reader.Deserialize(type, Folder).ToArray();
                items.Add(type, entities);
            }
            return items;
        }

        public void Dispose()
        {
            // ... clean up test data...
        }

    }

    [CollectionDefinition("JsonSeededFacetContext")]
    public class JsonCollectionFixture : ICollectionFixture<JsonSeededFacetContextFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}
