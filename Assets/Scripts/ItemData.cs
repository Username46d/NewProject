using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public ItemStates state;
    [SerializeField] public ItemStatesTypes itemStatesType;
    public void Start(){state = ItemStates.Idle;}
}

//public class MovementBlock : MonoBehaviour
//{ 

//    void 
//}

public enum ItemStatesTypes{
    Normal,
    JumpObject
}
public enum ItemStates
{
    Idle,
    Throw,
    ProcessEq,
    Equipped
}
