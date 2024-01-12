using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private TrainCarriage train;

    public void StartRun(float speed)
    {
        train.StartRun(speed);
    }

    public void ResetRun()
    {
        train.ReBegin();
    }
}
