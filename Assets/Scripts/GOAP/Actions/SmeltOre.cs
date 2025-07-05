using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmeltOre : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        if (agentStates.HasState("HasRawIron"))
        {
            int value = agentStates.GetValue("HasRawIron");
            agentStates.RemoveState("HasRawIron");
            agentStates.ModifyState("HasIron", value);
        } else if (agentStates.HasState("HasRawGold"))
        {
            int value = agentStates.GetValue("HasRawGold");
            agentStates.RemoveState("HasRawGold");
            agentStates.ModifyState("HasGold", value);
        }
        
        return true;
    }
}
