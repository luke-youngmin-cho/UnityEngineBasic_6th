using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterFire : MonoBehaviour
{
    public static FighterFire instance;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletReloadTime = 0.2f;
    [SerializeField] private float _bombReloadTime = 1.0f;
    private float _bulletReloadTimer;
    private float _bombReloadTimer;
    public int bombNum
    {
        get
        {
            return _bombNum;
        }
        set
        {
            if (value > _bombNumMax)
                value = _bombNumMax;

            if (_bombNum == value)
                return;

            _bombNum = value;
            OnBombNumChanged?.Invoke(value);
        }
    }
    private int _bombNum;
    [SerializeField] int _bombNumInit = 3;
    [SerializeField] int _bombNumMax = 8;
    public event Action<int> OnBombNumChanged;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        bombNum = _bombNumInit;
    }

    private void Update()
    {
        FireBullet();
        FireBomb();
    }

    private void FireBullet()
    {
        if (_bulletReloadTimer <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //GameObject go = new GameObject();
                //go.AddComponent<Transform>();
                //go.AddComponent<CapsuleCollider>();
                //go.AddComponent<Bullet>();
                Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
                _bulletReloadTimer = _bulletReloadTime;
            }
        }
        else
        {
            _bulletReloadTimer -= Time.deltaTime;
        }
    }

    private void FireBomb()
    {

        if (_bombNum > 0)
        {
            if (_bombReloadTimer <= 0)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    Instantiate(_bombPrefab, _firePoint.position, Quaternion.identity);
                    bombNum--;
                    _bombReloadTimer = _bombReloadTime;
                }
            }
            else
            {
                _bombReloadTimer -= Time.deltaTime;
            }
        }
    }
}
