using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringManager
{
    private static string jsonPath = "Assets/Resources/MyCharacter/";
    private static string itemJsonPath = "Assets/Resources/MyInventory/";
    private static string storageName = "Storage";
    public static string JsonPath { get { return jsonPath; } }
    public static string ItemJsonPath { get { return itemJsonPath; } }
    public static string StorageName { get { return storageName; } }
}
public enum Job
{
    WARRIOR,
    ARCHOR,
    WIZZARD
}
public enum StatType
{
    STR,
    DEX,
    INT,
    CON
}
public enum SceneType
{
    Start,
    Town,
    Dungeon,
    Loading
}
