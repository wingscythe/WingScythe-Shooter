using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Recall: MonoBehaviour
{
 
    public float maxDuration;

    public float saveInterval;

    public float recallSpeed;

    public List<Vector3> positions;

    public List<Quaternion> rotations; 

    private bool recalling;

    private float saveStatsTimer;

    private float maxStatsStored;

    private PlayerMovement controller;

    private SphereCollider col;

    private Rigidbody rb;

    public TrailRenderer trail;
    public float trailtime; 
    void Start()
    {

        controller = GetComponent<PlayerMovement>();

        trail = GetComponent<TrailRenderer>();
        col = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();

        trail.startColor = Color.red;
        trailtime = maxDuration;
        maxStatsStored = maxDuration / saveInterval;
    }

    void Update()
    {
        if (!recalling)
        {
  
            if (Input.GetKeyDown(KeyCode.Mouse1) && positions.Count > 0 && rotations.Count > 0)
            {

                recalling = true;

                controller.enabled = false;
                col.enabled = false;
                rb.isKinematic = true;
            }

            //Handling our saving timer
            if (saveStatsTimer > 0)
            {
                saveStatsTimer -= Time.deltaTime;
            }
            else
            {
                trail.time = trailtime;
                StoreStats(); 
            }
        }
        else
        {
          
            if (positions.Count > 0 && rotations.Count > 0)
            {
                
                transform.position = Vector3.Lerp(transform.position, positions[0], recallSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotations[0], recallSpeed * Time.deltaTime);
          
                float dist = Vector3.Distance(transform.position, positions[0]);
                if (dist < 0.25f)
                {
                    SetStats();
                    GetComponent<TrailRenderer>().time = positions.Count * saveInterval / recallSpeed;
                }
            }
            else 
            {

                recalling = false;

                controller.enabled = true;
                col.enabled = true;
                rb.isKinematic = false;
            }

          
        }
    }

    void SetStats()
    {
        
        transform.position = positions[0];
        transform.rotation = rotations[0];
       
        positions.RemoveAt(0);
        rotations.RemoveAt(0);
    }

    void StoreStats()
    {
        
        saveStatsTimer = saveInterval;
        positions.Insert(0, transform.position);
        rotations.Insert(0, transform.rotation);
       
        if (positions.Count > maxStatsStored && rotations.Count > maxStatsStored)
        {
            positions.RemoveAt(positions.Count - 1);
            rotations.RemoveAt(rotations.Count - 1);
        }

    }
}