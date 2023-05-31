using RPG.GameElements;
using RPG.GameSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.UI
{
    public class TargetHPUI : UIMonoBehaviour
    {
        [SerializeField] private Slider _hpBar;
        [SerializeField] private TMP_Text _hpValue;
        [SerializeField] private TMP_Text _name;

        protected override void Init()
        {
            base.Init();
            MainSystem.instance.StartCoroutine(E_Init());
        }

        private IEnumerator E_Init()
        {
            yield return new WaitUntil(() => GameManager.instance.mine != null);
            Player player = GameManager.instance.mine;

            player.behaviourTree.onTargetChanged += ((prev, next) =>
            {
                IDamageable damageable;
                if (prev != null &&
                    prev.TryGetComponent(out damageable))
                {
                    damageable.onHpChanged -= Refresh;
                }

                if (next != null &&
                    next.TryGetComponent(out damageable))
                {
                    _hpBar.minValue = damageable.hpMin;
                    _hpBar.maxValue = damageable.hpMax;
                    _hpBar.value = damageable.hp;
                    _hpValue.text = ((int)damageable.hp).ToString();
                    _name.text = next.name;
                    damageable.onHpChanged += Refresh;
                    ShowUnmanaged();
                }
                else
                {
                    HideUnmanaged();
                }
            });
        }

        private void Refresh (float value)
        {
            _hpBar.value = value;
            _hpValue.text = ((int) value).ToString();
        }
    }
}