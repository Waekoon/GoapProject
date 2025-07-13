using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveAtStash : GAction
{
    public override void Awake()
    {
        base.Awake();
        actionName = "RetrieveAtStash";
        duration = 1;
        targetName = "Stash";

        preconditions.Add("Iron", 1);
    }

    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        if (agentStates.HasState("HasIron"))
        {
            int value = agentStates.GetValue("HasIron");
            GWorld.Instance.GetWorld().ModifyState("Iron", value);
            agentStates.RemoveState("HasIron");
        } else if (agentStates.HasState("HasGold"))
        {
            int value = agentStates.GetValue("HasGold");
            GWorld.Instance.GetWorld().ModifyState("Gold", value);
            agentStates.RemoveState("HasGold");
        } else if (agentStates.HasState("HasEmerald"))
        {
            int value = agentStates.GetValue("HasEmerald");
            GWorld.Instance.GetWorld().ModifyState("Emerald", value);
            agentStates.RemoveState("HasEmerald");
        }

        return true;
    }
}
