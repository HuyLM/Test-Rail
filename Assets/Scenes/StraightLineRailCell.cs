using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineRailCell : RailCell
{
    public InOutPutRail[] InOuts;


    public override Vector3[] GetPoints(RailCell inputCell)
    {
        Vector3[] points = null;

        foreach(var inout in InOuts)
        {
            if(inputCell == inout.In_Cell)
            {
                points = inout.Out_NextPoints;
                break;
            }
        }
        if(points == null)
        {
            Debug.LogError("GetPoints: null");
            return null;
        }
            
        Vector3[] worldPoints = new Vector3[points.Length];
        for(int i = 0; i < worldPoints.Length; ++i)
        {
            worldPoints[i] = transform.TransformPoint(points[i]);
        }
        return worldPoints;
    }

    public override RailCell GetNextRail(RailCell inputCell)
    {
        foreach (var inout in InOuts)
        {
            if (inputCell == inout.In_Cell)
            {
                return inout.Out_Cell;
            }
        }
        Debug.LogError("GetNextRail: wrong input cell");
        return null;
    }

 
}
[System.Serializable]
public class InOutPutRail
{
    public RailCell In_Cell;
    public Vector3[] Out_NextPoints;
    public RailCell Out_Cell;
}
