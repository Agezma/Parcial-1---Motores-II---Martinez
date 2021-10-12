using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject prefabBullet;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(prefabBullet);
            bullet.transform.position = transform.position;
            bullet.transform.forward = transform.forward;
        }
    }
}
