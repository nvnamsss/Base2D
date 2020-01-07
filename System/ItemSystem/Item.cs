﻿using Base2D.System.DamageSystem;
using Base2D.System.UnitSystem;
using UnityEngine;

namespace Base2D.System.ItemSystem
{
    public class Item : MonoBehaviour, IAttribute
    {
        public Attribute Attribute { get; set; }
        public ModificationInfos Modification { get ;set; }
        public string itemName;
        public string description;
        public ItemType itemType;

        public Sprite icon;
        public bool instaUse = false;

        public virtual void Use(){

        }
    }
}
