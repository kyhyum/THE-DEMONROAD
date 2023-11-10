using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// State에 들어갔을 때 실행된다.
    /// </summary>
    public void Enter();

    /// <summary>
    /// State를 나갈 때 실행된다.
    /// </summary>
    public void Exit();

    /// <summary>
    /// State용 Update 함수이다.
    /// </summary>
    public void Update();

    /// <summary>
    /// State용 LateUpdate 함수이다
    /// </summary>
    public void LateUpdate();

    /// <summary>
    /// State용 물리적 update 함수이다. 
    /// </summary>
    public void PhysicsUpdate();
}
