using RPG.DataModels;
using RPG.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Tests
{
    public class Test_SetGoldButton : MonoBehaviour
    {
        [SerializeField] private Gold gold;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => DataModelManager.instance.Get<GoldDataModel>().SetData(gold));
        }
    }
}
