using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VixeriaEngine
{
    /// <summary>
    /// List that adds new elements to the front of the list and cycles the remaining items up the list, with a specified max amount of elements.
    /// </summary>
    /// <typeparam name="T">Can only contain one of these types: int, float, double, long, or decimal.</typeparam>
    public class AveragingList<T> : List<T>
    {
        //List<T> list;
        int maxElements;

        public AveragingList(int _maxElements)
        {
            maxElements = _maxElements;
        }

        new public void Add(T item)
        {
            base.Add(item);
            if (Count > maxElements)
                RemoveAt(0);
        }
    }

    // TODO: implement exception
    class WrongTypeInAveragingList : Exception
    {
        public WrongTypeInAveragingList() : base() { }
        public WrongTypeInAveragingList(string message) : base(message) { }
        public WrongTypeInAveragingList(string message, Exception innerException) : base(message, innerException) { }
    }
}
