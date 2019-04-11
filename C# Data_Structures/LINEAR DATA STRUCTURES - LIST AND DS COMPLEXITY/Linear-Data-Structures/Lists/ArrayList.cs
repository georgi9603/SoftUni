using System;

public class ArrayList<T>
{
    private const int startingCount = 2;
    public T[] arr;

    //TODO create constructor that initialize array with specific Count < 
    public ArrayList()
    {
        arr = new T[startingCount];

    }

    public int Count
    {
        get; set;
    }

    public T this[int index]
    {
        get
        {
            if (index >= Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return this.arr[index];
        }

        set
        {
            if (index >= Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.arr[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count >= this.arr.Length)
        {
            this.Resize();
        }
        arr[this.Count++] = item;
    }

    private void Resize()
    {
        T[] copyOfArray = new T[this.arr.Length * 2];
        for (int i = 0; i < arr.Length; i++)
        {
            copyOfArray[i] = arr[i];
        }
        arr = copyOfArray;
    }

    public T RemoveAt(int index)
    {
        if (index >= Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        T element = arr[index];
        arr[index] = default(T);
        this.Shift(index);
        this.Count--;

        if (this.Count <= arr.Length / 4)
        {
            this.Shrink();
        }
        return element;
    }

    private void Shrink()
    {
        T[] shrinkedCopyOfArray = new T[arr.Length / 2];
        for (int i = 0; i < this.Count; i++)
        {
            shrinkedCopyOfArray[i] = arr[i];
        }
        arr = shrinkedCopyOfArray;
    }

    private void Shift(int index)
    {
        for (int i = index; i < this.Count; i++)
        {
            arr[i] = arr[i + 1];
        }
    }
}