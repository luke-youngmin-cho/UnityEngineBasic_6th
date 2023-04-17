using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    public static TowerHandler instance;
    [SerializeField] private Material _previewMaterial;
    private MeshFilter[] _meshFilters;
    private MeshRenderer[] _meshRenderers;
    [SerializeField] private LayerMask _nodeMask;
    private RaycastHit _hit;
    private TowerInfo _selected;

    public void ShowPreview(TowerInfo towerInfo)
    {
        MeshFilter[] originalFilters = towerInfo.prefab.GetComponentsInChildren<MeshFilter>();

        for (int i = 0; i < originalFilters.Length; i++)
        {
            _meshFilters[i].transform.localPosition = originalFilters[i].transform.localPosition;
            _meshFilters[i].sharedMesh = originalFilters[i].sharedMesh;
            _meshRenderers[i].sharedMaterial = _previewMaterial;
        }

        for (int i = _meshFilters.Length - 1; i >= originalFilters.Length; i--)
        {
            _meshFilters[i].sharedMesh = null;
            _meshRenderers[i].sharedMaterial = null;
        }

        transform.localScale = towerInfo.prefab.transform.localScale;
        _selected = towerInfo;
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        instance = this;
        _meshFilters = GetComponentsInChildren<MeshFilter>();   
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _nodeMask))
        {
            transform.position = new Vector3(_hit.collider.transform.position.x,
                                             _hit.point.y,
                                             _hit.collider.transform.position.z);
        }
        else
        {
            transform.position = Vector3.one * 5000.0f;
        }


        if (Input.GetMouseButtonDown(0))
            OnLeftClick();
        if (Input.GetMouseButtonDown(1))
            gameObject.SetActive(false);
    }

    private void OnLeftClick()
    {
        if (_selected != null &&
            _hit.collider != null)
        {
            if (Player.instance.money < _selected.buildPrice)
            {
                Debug.Log($"[TowerHandler] : 타워 건설에 실패했습니다. 잔액이 부족합니다.");
                return;
            }

            if (_hit.collider.GetComponent<Node>().TryBuildTowerHere(_selected.type, _selected.upgradeLevel, out Tower tower))
            {
                Player.instance.money -= _selected.buildPrice;
                Debug.Log($"[TowerHandler] : {_selected.type}{_selected.upgradeLevel} 타워 건설 완료.");
            }
        }
    }
}