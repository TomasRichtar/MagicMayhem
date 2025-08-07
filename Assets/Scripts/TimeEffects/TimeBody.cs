using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    ////[SerializeField] private bool isRewinding = false;
    ////[SerializeField] private int RewindSpeed = 2;
    ////[SerializeField][Tooltip("In seconds")] private float rewindLimit = 5;

    ////private List<RewindData> pointsInTime = new List<RewindData>();

    ////private Rigidbody rigBody;

    ////private void Awake()
    ////{
    ////    rigBody = GetComponent<Rigidbody>();
    ////}
    ////// Start is called before the first frame update
    ////void Start()
    ////{
        
    ////}

    ////// Update is called once per frame
    ////void Update()
    ////{
    ////    if (Input.GetKeyDown(KeyCode.Tab))
    ////    {
    ////        StartRewind();
    ////    }
    ////    if (Input.GetKeyUp(KeyCode.Tab))
    ////    {  
    ////        StopRewind(); 
    ////    }
    ////}

    ////private void FixedUpdate()
    ////{
    ////    if (isRewinding)
    ////    {
    ////        Rewind();
    ////    }
    ////    else
    ////    {
    ////        Record();
    ////    }
    ////}
    ////public void Record()
    ////{
    ////    if (pointsInTime.Count > Mathf.Round(rewindLimit / Time.fixedDeltaTime))
    ////    {
    ////        pointsInTime.RemoveAt(pointsInTime.Count - 1);
    ////    }
    ////    RewindData pointInTime = new RewindData(transform.position, transform.rotation, rigBody.velocity);
    ////    pointsInTime.Insert(0, pointInTime);
    ////}

    ////public void Rewind()
    ////{
    ////    if (pointsInTime.Count == 0) return;

    ////    for (int i = 0; i < RewindSpeed && pointsInTime.Count > 0; i++)
    ////    {
    ////        RewindData pointInTime = pointsInTime[0];
    ////        transform.position = pointInTime.Position;
    ////        transform.rotation = pointInTime.Rotation;

    ////        if (rigBody != null)
    ////        {
    ////            rigBody.velocity = pointInTime.Velocity;
    ////        }

    ////        pointsInTime.RemoveAt(0);
    ////    }

    ////    //PointInTime pointInTime = pointsInTime[0];
    ////    //transform.position = pointInTime.Position;
    ////    //transform.rotation = pointInTime.Rotation;
    ////    //if (rigBody!= null)
    ////    //{
    ////    //    rigBody.velocity = pointInTime.Velocity;
    ////    //}

    ////    //pointsInTime.RemoveAt(0);
    ////}

    ////public void StartRewind()
    ////{
    ////    isRewinding = true;
    ////    if (rigBody)
    ////    {
    ////        rigBody.isKinematic = true;
    ////    }
    ////}

    ////public void StopRewind()
    ////{
    ////    isRewinding = false; 
    ////    if (rigBody)
    ////    {
    ////        rigBody.isKinematic = false;

    ////        if (pointsInTime.Count > 0)
    ////        {
    ////            rigBody.velocity = pointsInTime[0].Velocity;
    ////        }
    ////    }
    ////}
}
