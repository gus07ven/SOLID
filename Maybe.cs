﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncapApp
{
    public class Maybe<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> values;   // Value of readonly?
        public Maybe()
        {
            this.values = new T[0];
        }
        public Maybe(T value)
        {
            this.values = new[] { value };
        }
        public IEnumerator<T> GetEnumerator()
        {
            return this.values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
