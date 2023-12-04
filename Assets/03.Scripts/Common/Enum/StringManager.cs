using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringManager
{
    private static string baseObjcetPath = "BaseCharacter/";
    private static string jsonPath = $"{Application.persistentDataPath}/MyCharacter/";
    private static string itemJsonPath = $"{Application.persistentDataPath}/MyInventory/";
    private static string storageName = "Storage";
    private static string inventoryPrefabPath = "Prefabs/UI/UI_Inventory";
    private static string stroagePrefabPath = "Prefabs/UI/UI_Storage";
    private static string skillPrefabPath = "Prefabs/UI/UI_SKill";
    private static string questLogPrefabPath = "Prefabs/UI/UI_QuestLog";
    private static string questProgressPath = "Prefabs/UI/UI_QuestProgress";
    private static string gameoverPath = "Prefabs/UI/UI_GameOver";
    public static string JsonPath { get { return jsonPath; } }
    public static string ItemJsonPath { get { return itemJsonPath; } }
    public static string StorageName { get { return storageName; } }
    public static string BaseObjectPath { get { return baseObjcetPath; } }
    public static string InventoryPrefabPath { get { return inventoryPrefabPath; } }
    public static string StroagePrefabPath { get { return stroagePrefabPath; } }
    public static string SKillPrefabPath { get { return skillPrefabPath; } }
    public static string QuestLogPrefabPath { get { return questLogPrefabPath; } }
    public static string QuestProgressPath { get { return questProgressPath; } }
    public static string GameOverPrefabPath { get { return gameoverPath; } }
}
