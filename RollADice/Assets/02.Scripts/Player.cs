using UnityEngine;

public class Player : MonoBehaviour
{
    public void Move(Vector3 target)
    {
        transform.Translate(target, Space.World);
    }
}