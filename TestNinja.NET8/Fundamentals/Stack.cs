using System;
using System.Collections.Generic;

namespace TestNinja.NET8.Fundamentals
{
    public class Stack<T>
    {
        private readonly List<T> _list = new List<T>();

        public int Count => _list.Count;

        // set T to be nullable - in .NET 8 a warning occurs if it isn't nullable and
        // null is passed as an argument
        public void Push(T? obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            
            _list.Add(obj);
        }

        public T Pop()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException();

            var result = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);

            return result; 
        }


        public T Peek()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException();

            return _list[_list.Count - 1];
        }
    }
}