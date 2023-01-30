using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterFire : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _reloadTime = 0.2f;
    private float _reloadTimer;

    private void Update()
    {
        if (_reloadTimer <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //GameObject go = new GameObject();
                //go.AddComponent<Transform>();
                //go.AddComponent<CapsuleCollider>();
                //go.AddComponent<Bullet>();
                Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
                _reloadTimer = _reloadTime;
            }
        }
        else
        {
            _reloadTimer -= Time.deltaTime;
        }
    }
}
