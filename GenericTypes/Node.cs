using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericTypes
{
    public class Node<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }

        public Node(string key, T value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
