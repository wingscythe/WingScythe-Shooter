using System.Collections.Generic;
using UnityEngine;

public class Recall: MonoBehaviour
{
 
    public float maxDuration;

    public float saveInterval;

    public float recallSpeed;

    public List<Vector3> positions;

    private bool recalling;

    private float saveStatsTimer;

    private float maxStatsStored;

    private PlayerMovement controller;

    private SphereCollider col;

    private Rigidbody rb;

    void Start()
    {

        controller = GetComponent<PlayerMovement>();

        col = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();


        maxStatsStored = maxDuration / saveInterval;
    }

    void Update()
    {
        if (!recalling)
        {
  
            if (Input.GetKeyDown(KeyCode.Mouse1) && positions.Count > 0)
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
                StoreStats(); 
            }
        }
        else
        {
          
            if (positions.Count > 0)
            {
                
                transform.position = Vector3.Lerp(transform.position, positions[0], recallSpeed * Time.deltaTime);
          
                float dist = Vector3.Distance(transform.position, positions[0]);
                if (dist < 0.25f)
                {
                    SetStats();
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

       
        positions.RemoveAt(0);

    }

    void StoreStats()
    {
        
        saveStatsTimer = saveInterval;

        
        positions.Insert(0, transform.position);
       
        if (positions.Count > maxStatsStored)
        {
            positions.RemoveAt(positions.Count - 1);
        }

    }
}