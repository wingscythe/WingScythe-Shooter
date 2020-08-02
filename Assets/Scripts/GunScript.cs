using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform barrel;
    public float range = 0f;
    public float damage = 1f;
    public bool rewind = false;
    public float fireRate = 0.25f;
    public float shotSpd = 0.07f;
    private Camera fpsCam;
    private WaitForSeconds shotDuration;
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
 
    // Start is called before the first frame update
    void Start()
    {
        shotDuration = new WaitForSeconds(shotSpd);
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fpsCam && Input.GetButtonDown("Fire1") && Time.time>nextFire){
            nextFire = fireRate + Time.time;
            StartCoroutine(ShotEffect());
            StartCoroutine("Fire");
        }

        if (fpsCam && Input.GetButtonDown("Fire2"))
        {
            Rewind();
        }
    }

    IEnumerator Fire(){
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hit;

        laserLine.SetPosition(0,barrel.position);

        Ray ray = new Ray(rayOrigin, fpsCam.transform.forward);

        if(Physics.Raycast(ray, out hit, range)){
            laserLine.SetPosition(1,hit.point);
            if(hit.collider.tag == "Enemy"){
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.health -= damage;
            }
        }else{
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * range));
        }

        Debug.DrawRay(barrel.position, transform.forward * range, Color.green);
        yield return null;
    }

    void Rewind()
    {
        RaycastHit hit;
        rewind = true; 
        Ray ray = new Ray(barrel.position, transform.forward);
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.tag == "Enemy")
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                
            }
        }

        Debug.DrawRay(barrel.position, transform.forward * range, Color.green);
    }

    private IEnumerator ShotEffect(){
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
