using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class BlinkingFullBasket : PunBehaviour 
{

    public bool isBlinking;
    public Image image;
    public float delay;
    private float timer;

    public bool blink;
    public Sprite img;

    void Start()
    {
        image = GetComponent<Image>();
        blink = false;
    }

	void Update () 
    {
        if (isBlinking)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                timer = 0;
                if (blink)
                {
                    Debug.Log("blink");
                    blink = false;
                    image.transform.localScale = new Vector3(0, 0, 0);
                    //image.sprite = null;
                }
                else
                {
                    Debug.Log("no blink");
                    blink = true;
                    image.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    //image.sprite = img;
                }
            }
        }
	}
}
