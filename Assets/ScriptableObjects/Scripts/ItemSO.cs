using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Create/Item", order = 0)]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string itemName { get; private set; }
    [field: SerializeField] public string description { get; private set; }
    [field: SerializeField] public ItemType type { get; private set; }
    [field: SerializeField] public Rank rank { get; private set; }
    [field: SerializeField] public Sprite icon { get; private set; }
}
