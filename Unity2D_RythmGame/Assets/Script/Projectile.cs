using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private bool isSpecialMode;

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
    public void SetSpecialMode(bool mode)
    {
        this.isSpecialMode = mode;
        Debug.Log(isSpecialMode);
    }

    public void Get()
    {
        Debug.Log(isSpecialMode);
    }
}