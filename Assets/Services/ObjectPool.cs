using System;
using System.Collections.Generic;

public class ObjectPool<T>
{
    private readonly Queue<T> _objects;
    private readonly Func<T> _objectGenerator;

    public ObjectPool(Func<T> objectGenerator)
    {
        _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
        _objects = new Queue<T>();
    }

    public T Get() => _objects.Count > 0 ? _objects.Dequeue() : _objectGenerator();

    public void Return(T item) => _objects.Enqueue(item);
}