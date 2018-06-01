﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncapApp
{
    public class StoreCache
    {
        private readonly ConcurrentDictionary<int, string> cache;

        public StoreCache()
        {
            this.cache = new ConcurrentDictionary<int, string>();
        }

        public void AddOrUpdate(int id, string message)
        {
            this.cache.AddOrUpdate(id, message, (i, s) => message);
        }

        public string GetOrAdd(int id, Func<int, string> messageFactory)
        {
            return this.cache.GetOrAdd(id, messageFactory);
        }
    }
}