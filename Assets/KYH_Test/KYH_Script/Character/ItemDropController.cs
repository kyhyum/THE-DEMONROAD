using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    List<int> itemPercent = new List<int>();
    List<ItemSO> item = new List<ItemSO>();

    public void DropItem()
    {
        int totalWeight = itemPercent.Sum();
        int randomValue = Random.Range(0, totalWeight); // 0부터 총 가중치까지의 랜덤 값 생성

        int cumulativeWeight = 0;
        for (int i = 0; i < itemPercent.Count; i++)
        {
            cumulativeWeight += itemPercent[i];
            if (randomValue < cumulativeWeight)
            {
                // 해당 아이템을 반환하는 코드
            }
        }
    }

    
}
