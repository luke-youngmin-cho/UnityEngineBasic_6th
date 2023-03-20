using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpAssets : MonoBehaviour
{
    private static DamagePopUpAssets _instance;
    public static DamagePopUpAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<DamagePopUpAssets>("DamagePopUpAssets"));
                _instance.Init();
            }
            return _instance;
        }
    }

    public GameObject this[LayerMask layerMask]
        => _dictionary[layerMask];

    [SerializeField] private List<UKeyValuePair<LayerMask, GameObject>> _damagePopUps;
    private Dictionary<LayerMask, GameObject> _dictionary;

    public GameObject GetDamagePopUP(LayerMask layerMask)
    {
        return _damagePopUps.Find(x => x.key == layerMask).value;
    }

    private void Init()
    {
        _dictionary = new Dictionary<LayerMask, GameObject>();
        foreach (UKeyValuePair<LayerMask, GameObject> damagePopUp in _damagePopUps)
        {
            _dictionary.Add(damagePopUp.key, damagePopUp.value);
        }
    }
}
