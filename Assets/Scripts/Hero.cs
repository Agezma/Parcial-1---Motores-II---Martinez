using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : NPC
{
    public int level;
    public float XPtoLevelUp;
    public float ammountOfExperience;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

        if (ammountOfExperience >= XPtoLevelUp)
        {
            ammountOfExperience -= XPtoLevelUp;
            level++;
            Debug.Log("LEVEL UP");
        }
    }
}
        