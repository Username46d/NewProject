using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatesFabric
{
    public static IPlayerStates NewState(ItemStatesTypes itemStatesType, GameObject player, GameObject item = null)
    {
        return itemStatesType switch
        {
            ItemStatesTypes.Normal => new BaseState(player),
            ItemStatesTypes.JumpObject => new JumpState(player, item),
            _ => new BaseState(player)
        };
    }
}
