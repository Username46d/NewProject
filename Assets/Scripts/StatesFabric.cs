using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatesFabric
{
    public static IPlayerStates NewState(ItemStatesTypes itemStatesType, GameObject player, int lastMove, GameObject item = null)
    {
        return itemStatesType switch
        {
            ItemStatesTypes.Normal => new BaseState(player, lastMove),
            ItemStatesTypes.JumpObject => new JumpState(player, item, lastMove),
            _ => new BaseState(player, lastMove)
        };
    }
}
