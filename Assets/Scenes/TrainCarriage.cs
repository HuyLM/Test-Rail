using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.DemiLib;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Plugins.Core.PathCore;
using System.Linq;

public class TrainCarriage : MonoBehaviour
{
    public float speed;
    //public PathType PathType;
    //public PathMode pathMode;
    public float lockahead;
    [SerializeField] private RailCell StartRail;



    [Header("Private")]
    [SerializeField] private RailCell CurRail;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private Vector3 startScale;

    [SerializeField] private List<Vector3> prePoints;
    [SerializeField] public RailCell PreRail;
    [SerializeField] public RailCell NexttRail;
    TweenerCore<Vector3, Path, PathOptions> moveTween;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;

        CurRail = StartRail;
        PreRail = StartRail;
    }


    public void StartRun()
    {
        List<Vector3> points = CurRail.GetPoints(PreRail).ToList();
        prePoints = points.ToList();
        //prePoints = CurRail.GetPoints(PreRail).ToList();

        // transform.DOPath(points, speed, PathType, pathMode, 10).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(lockahead, Vector3.left).OnComplete(NextCell); //GotoWaypoint
        int targetIndex = points.Count;
        NexttRail = CurRail.GetNextRail(PreRail);
        points.AddRange(NexttRail.GetPoints(CurRail));

        moveTween =  transform.DOPath(points.ToArray(), speed, PathType.CatmullRom, PathMode.Full3D, 10).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(lockahead, Vector3.left).OnWaypointChange((int index)=> {
            if (index == targetIndex)
            {
                moveTween.Kill();
                NextCell();
            }
        });

    }

    private void NextCell()
    {
        PreRail = CurRail;
        CurRail = NexttRail;
        NexttRail = CurRail.GetNextRail(PreRail);

        List<Vector3> points = new List<Vector3>();
        if(prePoints.Count > 0)
        {
            points.AddRange(prePoints);
        }
        points.AddRange(CurRail.GetPoints(PreRail));
        int targetIndex = points.Count;
        points.AddRange(NexttRail.GetPoints(CurRail));

        moveTween = transform.DOPath(points.ToArray(), speed, PathType.CatmullRom, PathMode.Full3D, 10).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(lockahead, Vector3.left).OnWaypointChange((int index) => {
            if (index == targetIndex)
            {
                moveTween.Kill();
                CurRail.OnMoveToTarget(this);
                NextCell();
            }
        });
        if(prePoints.Count > 0)
        {
            moveTween.GotoWaypoint(prePoints.Count, true);
        }

        prePoints = CurRail.GetPoints(PreRail).ToList();
    }

    public void ReBegin()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startScale;

        PreRail = StartRail;
        CurRail = StartRail;
    }
    private bool stop = false;
    public void TeleportTo(Transform newPosition)
    {
        transform.position = newPosition.position;
        transform.rotation = newPosition.localRotation;
        prePoints.Clear();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartRun();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            ReBegin();
        }
    }
}
