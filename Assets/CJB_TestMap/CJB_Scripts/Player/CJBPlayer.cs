using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJBPlayer : MonoBehaviour
{
    public int playerProgress;

    // 플레이어의 진행 상황을 업데이트하는 메서드
    public void UpdatePlayerProgress(int progress)
    {
        playerProgress = progress;
    }
}
