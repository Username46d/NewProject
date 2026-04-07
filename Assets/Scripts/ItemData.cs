using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] public ItemStatesTypes itemStatesType;
}
public enum ItemStatesTypes{
    Normal,
    JumpObject
}