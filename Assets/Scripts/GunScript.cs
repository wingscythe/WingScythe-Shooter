using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform barrel;
    public float range = 0f;
    public float damage = 1f;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            StartCoroutine("Fire");
        }
    }

    IEnumerator Fire(){
        RaycastHit hit;
        Ray ray = new Ray(barrel.position, transform.forward);

        if(Physics.Raycast(ray, out hit, range)){
            if(hit.collider.tag == "Enemy"){
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.health -= damage;
            }
        }

        Debug.DrawRay(barrel.position, transform.forward * range, Color.green);
        yield return null;
    }
}
