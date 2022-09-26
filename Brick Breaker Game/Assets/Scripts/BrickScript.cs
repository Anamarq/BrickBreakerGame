using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points;  //Puntos de ese bloque
    public int hitsToBreak; //Cuantos golpes hay que dar para romperlo
    public Sprite hitSprite;
    public Sprite hit2Sprite;
    public Sprite hit3Sprite;
    public Sprite hit4Sprite;
    private BallScript ballScr;

    public void BreakBrick()
    {
        if (hitsToBreak <= 5){  // Si es 6 es que es irrompible
            hitsToBreak--;
            if (hitsToBreak == 1)
                GetComponent<SpriteRenderer>().sprite = hitSprite;
            else if (hitsToBreak == 2)
                GetComponent<SpriteRenderer>().sprite = hit2Sprite;
            else if (hitsToBreak == 3)
                GetComponent<SpriteRenderer>().sprite = hit3Sprite;
            else if (hitsToBreak == 4)
                GetComponent<SpriteRenderer>().sprite = hit4Sprite;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("shot"))
        {
            //ballScr = GameObject.FindGameObjectsWithTag("Ball")[0].GetComponent<BallScript>();
            //ballScr.colisionBrick(other);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
    */
}
