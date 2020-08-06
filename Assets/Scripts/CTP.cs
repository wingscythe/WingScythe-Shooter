using System;
using UnityEngine;

public class CTP : MonoBehaviour
{
    public float CatTime = 0;
    public float DogTime = 0;
    public int catCount = 0;
    public int dogCount = 0;
    public bool cat = false;
    public bool dog = false;
    public int captureTime = 60;
    // Start is called before the first frame update
    void Start()
    {
        checkColliders();
        Debug.Log(" cats: " + catCount + " dogs : " + dogCount);
    }

    // Update is called once per frame
    void Update()
    {
        //get colliders 
        //check if dog > cat colliders, if so call update
        updateTime();
        
        Debug.Log(dog);
        Debug.Log(DogTime);
        Debug.Log(CatTime);
    }

    void checkColliders()
    {
        Collider[] colliders = Physics.OverlapBox(this.transform.position, transform.localScale);
        for(int i = 0; i< colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Dog" || colliders[i].gameObject.tag == "Cat")
            {
                if (colliders[i].gameObject.tag == "Dog") dogCount++;
                else if (colliders[i].gameObject.tag == "Cat") catCount++;
            }
        }
    }
    void setCat()
    {
        cat = true;
    }

    void setDog()
    {
        dog = true;
    }

    void updateTime()
    {
        if (cat)
        {
            if (DogTime > CatTime && dogCount > catCount) DogTime -= Time.deltaTime;
            else CatTime += Time.deltaTime;
        }

        else if (dog)
        {
            if (CatTime > DogTime && catCount > dogCount) CatTime -= Time.deltaTime;
            else DogTime += Time.deltaTime;
        }
    }

    
}
