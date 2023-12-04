using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : Item, IStackable, IUsable
{
    private int count;
    public event CountChangedDelegate OnCountChanged;
    public delegate void CountChangedDelegate(int count);
    public event UsedDelegate ApplyCoolTime;
    public delegate void UsedDelegate();

    public UseItem(ItemSO itemSO) : base(itemSO)
    {
        if (itemSO is UseItemSO)
        {
            UseItemSO useItemSO = (UseItemSO)itemSO;
            count = useItemSO.count;
        }
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

    public virtual void Use()
    {
        Sub(1);
        OnCountChanged?.Invoke(count);
        ApplyCoolTime?.Invoke();
    }
}
