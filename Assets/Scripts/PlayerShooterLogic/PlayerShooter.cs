using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShooter : MonoBehaviour
{
    public GameObject Bullet;
    public Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            GameObject bullet = Instantiate (Bullet);
            Bullet.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
            Bullet.transform.forward = playerCamera.transform.forward;
        }   
    }
}
