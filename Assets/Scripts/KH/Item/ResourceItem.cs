using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : Item, IStackable
{
    private int count;

    public void Add(int n)
    {
        count += n;
    }

    public void Sub(int n)
    {
        count -= n;
    }

    public int Get()
    {
        return count;
    }
}
