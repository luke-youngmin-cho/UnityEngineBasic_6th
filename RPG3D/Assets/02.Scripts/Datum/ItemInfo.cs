using RPG.DataStructures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.GameElements.Items;
using RPG.GameElements;

namespace RPG.Datum
{
    public abstract class ItemInfo : ScriptableObject
    {
        public ItemID id;
        public int numMax; // 한 슬롯 최대 소지가능갯수
        public Gold purchasePrice;
        public Gold sellPrice;
        public Sprite icon;
        public Mesh mesh;
        public Material material;
        public Item prefab;

        public abstract void Use();
    }
}