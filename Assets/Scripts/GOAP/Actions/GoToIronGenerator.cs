using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToIronGenerator : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        agentStates.ModifyState("HasRawIron", 1);
        cost += Random.Range(0, 0.1f);
        return true;
    }
}
