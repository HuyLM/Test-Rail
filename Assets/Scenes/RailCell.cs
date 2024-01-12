using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RailCell : MonoBehaviour
{
    public abstract Vector3[] GetPoints(RailCell inputCell);
    public abstract RailCell GetNextRail(RailCell inputCell);
    public virtual void OnMoveToTarget(TrainCarriage trainCarriage, RailCell inputCell)
    {

    }
}
