using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Projectile projectile; //inspector에서 Projectile prefab을 삽입

    private void Update()
    {
       if( Input.GetKey(KeyCode.A))
            {
            Debug.Log("zz");
            Shoot();
        }
            
    }
    public void Shoot()
    {


        Instantiate(projectile);
        projectile.SetSpecialMode(true);
        projectile.SetSpeed(20);
        //Projectile aa =  Instantiate(projectile);
        //aa.SetSpeed(20);
        //aa.SetSpecialMode(true);
        projectile.Get();
        //Debug.Log(projectile.Get()  );
        
    }
}