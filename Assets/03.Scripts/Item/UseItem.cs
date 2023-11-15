using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : Item, IStackable, IUsable
{
    private int count;

    public UseItem(ItemSO itemSO) : base(itemSO)
    {
    }

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

    public void Use()
    {

    }
}
