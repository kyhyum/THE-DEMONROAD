using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemPoolSize
{
    public ItemSO item;
    public int poolSize;
}

public class ItemDropManager : MonoBehaviour
{

    public static ItemDropManager Instance;

    public List<ItemPoolSize> itemPoolSizes; // 각 아이템과 그에 대응하는 풀 크기
    private Dictionary<ItemSO, List<GameObject>> itemPools; // 각 아이템에 대응하는 풀

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 풀 초기화
        InitializeObjectPools();
    }

    void InitializeObjectPools()
    {
        // 딕셔너리 초기화
        itemPools = new Dictionary<ItemSO, List<GameObject>>();

        // 각 아이템에 대응하는 풀 생성
        foreach (var kvp in itemPoolSizes)
        {
            ItemSO item = kvp.item;
            int poolSize = kvp.poolSize;

            List<GameObject> itemPool = new List<GameObject>();

            // 풀에 아이템 미리 생성하여 추가
            for (int i = 0; i < poolSize; i++)
            {
                GameObject newItem = CreateItem(item);
                newItem.SetActive(false);
                itemPool.Add(newItem);
            }

            // 딕셔너리에 추가
            itemPools.Add(item, itemPool);
        }
    }

    public void SpawnItem(ItemSO Item, Vector3 position)
    {
        List<GameObject> itemPool = itemPools[Item];

        // 비활성화된 아이템 중에서 찾아 사용
        for (int i = 0; i < itemPool.Count; i++)
        {
            if (!itemPool[i].activeInHierarchy)
            {
                // 아이템 초기화 및 활성화
                //InitializeItem(itemPool[i], Item);
                itemPool.RemoveAt(itemPool.Count - 1);
                itemPool[i].SetActive(true);

                // 생성된 아이템의 위치를 랜덤으로 지정 (여기서는 예시로 (0, 0, 0)으로 설정)
                itemPool[i].transform.position = position;

                break; // 아이템을 찾았으면 루프 종료
            }
        }
    }

    public void ReturnItem(GameObject gameObject, ItemSO item)
    {
        gameObject.gameObject.SetActive(false);
        itemPools[item].Add(gameObject);
    }

    void InitializeItem(GameObject item, ItemSO itemData)
    {
        // 아이템 데이터로 아이템 초기화
        // TODO : 필요하다면?
    }

    GameObject CreateItem(ItemSO itemData)
    {
        // 드랍 가능한 아이템 중에서 선택한 아이템을 생성
        GameObject newItem = itemData.CreateItem();

        // 생성된 아이템을 비활성화 상태로 설정
        newItem.SetActive(false);

        return newItem;
    }
}