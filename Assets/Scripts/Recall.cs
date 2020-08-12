using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Jobs;

public class Recall: MonoBehaviour
{
 
    public float maxDuration;

    public float saveInterval;

    public float recallSpeed;

    public List<Vector3> positions;

    public List<Quaternion> rotations; 

    public bool recalling;

    private float saveStatsTimer;

    private float maxStatsStored;

    private PlayerMovement controller;

    private SphereCollider col;

    private Rigidbody rb;

    public TrailRenderer trail;
    public float trailtime; 
    public void Start()
    {

        controller = GetComponent<PlayerMovement>();

        trail = GetComponent<TrailRenderer>();
        col = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();

        trailtime = maxDuration;
        maxStatsStored = maxDuration / saveInterval;
    }

    public void Update()
    {
        if (!recalling)
        {
  
            if (this.GetComponent<GunScript>().rewind == true && positions.Count > 0 && rotations.Count > 0)
            {

                recalling = true;

                controller.enabled = false;
                //col.enabled = false;
                //rb.isKinematic = true;
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
                this.GetComponent<GunScript>().rewind = false;
                controller.enabled = true;
                //col.enabled = true;
                // rb.isKinematic = false;
            }

          
        }
    }

    public void SetStats()
    {
        
        transform.position = positions[0];
        transform.rotation = rotations[0];
       
        positions.RemoveAt(0);
        rotations.RemoveAt(0);
    }

    public void StoreStats()
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