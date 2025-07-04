using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : GAgent
{
    protected override void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("CollectResource", 3, false);
        goals.Add(s1, 5);
    }
}
