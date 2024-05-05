using System.Collections;

namespace Larkins.CSharpKatas.BlobNumbering;

public sealed class UniqueQueue<T> : IEnumerable<T>, ICollection
{
    private readonly Queue<T> queue = new();
    private readonly HashSet<T> alreadyAdded;

    public UniqueQueue(IEqualityComparer<T>? comparer)
    {
        alreadyAdded = new HashSet<T>(comparer);
    }

    public UniqueQueue(IEnumerable<T> collection, IEqualityComparer<T>? comparer = null)
    {
        // Do this so the enumeration does not happen twice in case the enumerator behaves differently each enumeration.
        var localCopy = collection.ToList();

        queue = new Queue<T>(localCopy);
        alreadyAdded = new HashSet<T>(localCopy, comparer);
    }

    public UniqueQueue(int capacity, IEqualityComparer<T>? comparer = null)
    {
        queue = new Queue<T>(capacity);
        alreadyAdded = new HashSet<T>(comparer);
    }

    // Here are the constructors that use the default comparer.
    // By passing null in for the comparer it will just use the default one for the type.
    public UniqueQueue()
        : this(null)
    {
    }

    public int Count => queue.Count;

    bool ICollection.IsSynchronized => ((ICollection)queue).IsSynchronized;

    object ICollection.SyncRoot => ((ICollection)queue).SyncRoot;

    /// <summary>
    /// Attempts to enqueue a object, returns false if the object was ever added to the queue in the past.
    /// </summary>
    /// <param name="item">The item to enqueue.</param>
    /// <returns>True if enqueued, False otherwise.</returns>
    public bool Enqueue(T item)
    {
        if (alreadyAdded.Add(item))
        {
            queue.Enqueue(item);
            return true;
        }

        return false;
    }

    public T Dequeue() => queue.Dequeue();

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return ((IEnumerable<T>)queue).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)queue).GetEnumerator();
    }

    void ICollection.CopyTo(Array array, int index)
    {
        ((ICollection)queue).CopyTo(array, index);
    }
}
