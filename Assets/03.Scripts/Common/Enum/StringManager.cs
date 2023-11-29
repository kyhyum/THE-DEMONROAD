using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringManager
{
    private static string baseObjcetPath = "BaseCharacter/";
    private static string jsonPath = "Assets/Resources/MyCharacter/";
    private static string testJsonPath = "Assets/Resources/Tester/";
    private static string itemJsonPath = "Assets/Resources/MyInventory/";
    private static string storageName = "Storage";
    private static string storagePrefab = "Storage";
    private static string testItemJsonPath = "Assets/Resources/TestInventory/";
    private static string testStorageName = "TestStorage";
    public static string JsonPath { get { return jsonPath; } }
    public static string ItemJsonPath { get { return itemJsonPath; } }
    public static string StorageName { get { return storageName; } }
    public static string TestJsonPath { get { return testJsonPath; } }
    public static string TestItemJsonPath { get { return testItemJsonPath; } }
    public static string TestStorageName { get { return testStorageName; } }
    public static string BaseObjectPath { get { return baseObjcetPath; } }
}
