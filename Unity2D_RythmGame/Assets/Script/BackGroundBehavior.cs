using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundBehavior : MonoBehaviour
{

    public GameObject gameBackground;
    private SpriteRenderer gameBackgroundSrpiteRender;
    // Start is called before the first frame update
    void Start()
    {
        gameBackgroundSrpiteRender = gameBackground.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut(gameBackgroundSrpiteRender,0.005f)  );
    }

    IEnumerator FadeOut(SpriteRenderer spriteRenderer, float amount)
    {
        Color color = spriteRenderer.color;

        while (color.a > 0.0f)
        {
            color.a -= amount;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(amount);
        }
    }
}
