using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    ItemSlot[] slots;
    private int[] count;

    private void Awake()
    {
        count = new int[3];
        slots = new ItemSlot[81];
    }
}
