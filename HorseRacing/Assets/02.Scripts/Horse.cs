using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    public bool doMove;
    [SerializeField] private float _speed = 2.0f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _stability = 0.7f;

    
    private void FixedUpdate()
    {
        if (doMove)
            Move();
    }

    /// <summary>
    /// 고정 프레임당 한번 호출
    /// 거리변화량 = 속도 * 시간변화량 = (Vector3.forward * randomSpeed) * Time.fixedDeltaTime
    /// </summary>
    private void Move()
    {
        float randomSpeed = _speed * Random.Range(_stability, 1.0f);
        transform.Translate(Vector3.forward * randomSpeed * Time.fixedDeltaTime);
    }
}
