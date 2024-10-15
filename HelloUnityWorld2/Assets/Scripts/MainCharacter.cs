using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public float movingSpeed;
    public float rotationSpeed;
    public float damage;
    public float life;

    // Start is called before the first frame update
    void Start()
    {
        print("moving speed: " + movingSpeed);
        print("rotating speed: " + rotationSpeed);
        print("damage: " + damage);
        print("life: " + life);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
