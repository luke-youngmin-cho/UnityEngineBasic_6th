using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageAmplifier : Tower
{
    [SerializeField] private float amplifyRate;
    private Dictionary<Node, IBuff<OffenceTower>> _buffPairs
        = new Dictionary<Node, IBuff<OffenceTower>>();


    private void OnTowerBuilt(Tower tower)
    {
        if (tower is OffenceTower)
        {
            BuffAmplifyingDamage<OffenceTower> tmp = new BuffAmplifyingDamage<OffenceTower>(amplifyRate);
            ((OffenceTower)tower).buffManager.ActiveBuff(tmp);
            _buffPairs.Add(tower.node, tmp);
        }
    }

    private void Start()
    {
        Collider[] cols = DetectTargets();
        BuffAmplifyingDamage<OffenceTower> buff;

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].TryGetComponent(out Node node))
            {
                node.onTowerBuilt += OnTowerBuilt;

                if (node.towerBuilt is OffenceTower)
                {
                    buff = new BuffAmplifyingDamage<OffenceTower>(amplifyRate);
                    ((OffenceTower)node.towerBuilt).buffManager.ActiveBuff(buff);
                    _buffPairs.Add(node, buff);
                }
            }
        }
    }

    private void OnDisable()
    {
        Collider[] cols = DetectTargets();

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].TryGetComponent(out Node node))
            {
                node.onTowerBuilt -= OnTowerBuilt;

                if (node.towerBuilt is OffenceTower)
                {
                    ((OffenceTower)node.towerBuilt).buffManager.DeactiveBuff(_buffPairs[node]);
                    _buffPairs.Remove(node);
                }
            }
        }
        _buffPairs.Clear();
    }
}
