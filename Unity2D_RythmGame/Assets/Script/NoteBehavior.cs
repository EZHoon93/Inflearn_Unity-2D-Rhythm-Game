using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{

    public int noteType;
    private GameManager.judges judge;
    public float speed;
    private KeyCode keyCode;

    void Start()
    {
        if (noteType == 1) keyCode = KeyCode.D;
        else if (noteType == 2) keyCode = KeyCode.F;
        else if (noteType == 3) keyCode = KeyCode.J;
        else if (noteType == 4) keyCode = KeyCode.K;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * GameManager.instance.noteSpeed);

        if (Input.GetKey(keyCode))
        {
            Debug.Log(judge);
            if (judge != GameManager.judges.NONE) Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bad Line")
        {
            judge = GameManager.judges.BAD;
        }
        else if (collision.gameObject.tag == "Good Line")
        {
            judge = GameManager.judges.GOOD;
        }
        else if (collision.gameObject.tag == "Perfect Line")
        {
            judge = GameManager.judges.PERFECT;
        }
        else if (collision.gameObject.tag == "Miss Line")
        {
            judge = GameManager.judges.MISS;
            Destroy(gameObject);
        }
        Debug.Log(judge);


    }
}
