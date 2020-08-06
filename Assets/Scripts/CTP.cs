using System;
using System.Text.RegularExpressions;
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
        setBool();
        updateTime();

        Debug.Log(dog);
        Debug.Log(DogTime);
        Debug.Log(CatTime);
    }

    void checkColliders()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale);
        int i = 0;
        while(i < colliders.Length)
        {
            if (colliders[i].gameObject.tag == "Dog" || colliders[i].gameObject.tag == "Cat")
            {
                Debug.Log(colliders[i].gameObject.name);
                if (colliders[i].gameObject.tag == "Dog") dogCount++;
                else if (colliders[i].gameObject.tag == "Cat") catCount++;
            }
            i++;
        }
    }
    void setBool()
    {
        if (dogCount > catCount)
        {
            dog = true;
            cat = false;
        }
        else if (catCount > dogCount)
        {
            cat = true;
            dog = false;
        }
    }
    void updateTime()
    {
        if (cat)
        {
            if (DogTime > CatTime && DogTime != 0) DogTime -= Time.deltaTime;
            else CatTime += Time.deltaTime;
        }

        else if (dog)
        {
            if (CatTime > DogTime && CatTime != 0) CatTime -= Time.deltaTime;
            else DogTime += Time.deltaTime;
        }
    }

    void win()
    {
        if (DogTime == captureTime) Debug.Log("Win");
        else if (CatTime == captureTime) Debug.Log("Win");

    }

    
}
