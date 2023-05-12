using RPG.DataStructures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Datum
{
    [CreateAssetMenu(fileName = "new ItemInfo", menuName = "RPG/Create a new ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public ItemID id;
        public int numMax; // �� ���� �ִ� �������ɰ���
        public Gold purchasePrice;
        public Gold sellPrice;
        public Sprite icon;
        public Mesh mesh;
        public Material material;
    }
}