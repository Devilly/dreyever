using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsBlink : MonoBehaviour {

    private Image image;
    public Sprite[] animationSprites;

    void Start()
    {
        image = GetComponent<Image>();

        StartCoroutine(TriggerAnimation());
    }

    private IEnumerator TriggerAnimation()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(3, 15));

            StartCoroutine(Animate());
        }
    }
        private IEnumerator Animate()
    {
        float frameTime = 1f / 30;
        foreach (Sprite sprite in animationSprites)
        {
            yield return new WaitForSeconds(frameTime);
            image.sprite = sprite;
        }
    }
}
