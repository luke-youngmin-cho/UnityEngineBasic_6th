using UnityEngine;

/// <summary>
/// 맵의 기본 칸 베이스 클래스. 
/// </summary>
public abstract class Tile : MonoBehaviour
{
    public int index; // 몇번째 칸인지에 대한 인덱스

    /// <summary>
    /// 플레이어가 해당 칸에 도착했을때 호출할 함수
    /// </summary>
    public virtual void OnHere()
    {
        Debug.Log($"[Tile] : {index} 번째 칸 도착!");
    }
}