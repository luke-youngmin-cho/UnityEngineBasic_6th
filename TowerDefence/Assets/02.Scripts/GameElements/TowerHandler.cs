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
    public void ShowPreview(TowerInfo towerInfo)
    {
        MeshFilter[] originalFilters = towerInfo.prefab.GetComponentsInChildren<MeshFilter>();

        for (int i = 0; i < originalFilters.Length; i++)
        {
            _meshFilters[i].transform.localPosition = originalFilters[i].transform.localPosition;
            _meshFilters[i].sharedMesh = originalFilters[i].sharedMesh;
            _meshRenderers[i].sharedMaterial = _previewMaterial;
        }
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        _meshFilters = GetComponentsInChildren<MeshFilter>();   
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _nodeMask))
        {
            transform.position = new Vector3(hit.collider.transform.position.x,
                                             hit.point.y,
                                             hit.collider.transform.position.z);
        }
        else
        {
            transform.position = Vector3.one * 5000.0f;
        }

        if (Input.GetMouseButtonDown(1))
            gameObject.SetActive(false);
    }
}