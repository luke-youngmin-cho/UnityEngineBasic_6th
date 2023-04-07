using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isTowerExist => towerBuilt;
    public Tower towerBuilt;
    private Renderer _renderer;
    private Material _origin;
    [SerializeField] private Material _buildAvailable;
    [SerializeField] private Material _buildNotAvailable;

    public void DestroyTowerHere()
    {
        Destroy(towerBuilt.gameObject);
        towerBuilt = null;
    }

    public bool TryBuildTowerHere(TowerType type, int upgradeLevel, out Tower tower)
    {
        tower = null;

        if (isTowerExist)
        {
            Debug.Log("해당 위치에는 타워가 이미 존재 하므로 건설할 수 없습니다.");
            return false;
        }

        if (TowerInfoAssets.instance.TryGetTowerInfo(type, upgradeLevel, out TowerInfo info))
        {
            tower = Instantiate(info.prefab,
                                transform.position,
                                Quaternion.identity);
            tower.node = this;
            towerBuilt = tower;
            return true;
        }

        return false;
    }

    public bool TryBuildTowerHere(TowerInfo info, out Tower tower)
    {
        tower = null;

        if (isTowerExist)
        {
            Debug.Log("해당 위치에는 타워가 이미 존재 하므로 건설할 수 없습니다.");
            return false;
        }

        tower = Instantiate(info.prefab,
                                transform.position,
                                Quaternion.identity);
        tower.node = this;
        towerBuilt = tower;
        return true;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _origin = _renderer.sharedMaterial;
    }

    private void OnMouseEnter()
    {
        if (isTowerExist)
            _renderer.material = _buildNotAvailable;
        else
            _renderer.material = _buildAvailable;
    }

    private void OnMouseExit()
    {
        _renderer.material = _origin;
    }
}
