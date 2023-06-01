using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RPG.GameSystems;
using RPG.GameElements;
using static UnityEngine.Rendering.DebugUI;

namespace RPG.UI 
{
    public class PlayerHPUI : UIMonoBehaviour
    {
        [SerializeField] private Slider _hpBar;
        [SerializeField] private TMP_Text _hpValue;

        protected override void Init()
        {
            base.Init();
            MainSystem.instance.StartCoroutine(E_Init());
        }

        private IEnumerator E_Init()
        {
            yield return new WaitUntil(() => GameManager.instance.mine != null);
            PlayerController player = GameManager.instance.mine;
            _hpBar.minValue = player.hpMin;
            _hpBar.maxValue = player.hpMax;
            _hpBar.value = player.hp;
            _hpValue.text = ((int)player.hp).ToString();
            player.onHpChanged += (value =>
            {
                _hpBar.value = value;
                _hpValue.text = ((int)value).ToString();
            });
        }
    }
}