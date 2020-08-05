using UnityEngine;

public class CTP : MonoBehaviour
{
    public GameObject point;
    public float CatTime = 0;
    public float DogTime = 0;
    public bool cat = false;
    public bool dog = false;
    public int captureTime = 60;
    // Start is called before the first frame update
    void Start()
    {
        
      

    }

    // Update is called once per frame
    void Update()
    {
        //get colliders 
        //check if dog > cat colliders, if so call update

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "cat") setCat();
        else if (collision.gameObject.tag == "dog") setDog();
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
            if (DogTime > CatTime) DogTime -= Time.deltaTime;
            else CatTime += Time.deltaTime;
        }

        else if (dog)
        {
            if (CatTime > DogTime) CatTime -= Time.deltaTime;
            else DogTime += Time.deltaTime;
        }
    }

    
}
