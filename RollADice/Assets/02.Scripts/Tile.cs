using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public int index;

    public virtual void OnHere()
    {
        Debug.Log($"[Tile] : {index} 번째 칸 도착!");
    }
}