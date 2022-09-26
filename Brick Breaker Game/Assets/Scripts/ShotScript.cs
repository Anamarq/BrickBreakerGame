using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public float speed;
    private BallScript ballScr;
    //private AudioSource audioShot;
    // Start is called before the first frame update
    void Start()
    {
       // audioShot = GetComponent<AudioSource>();
        //audioShot.Play();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f, 1) * Time.deltaTime * speed); //Se mueve en y cada frame
        /*if (transform.position.y > 5f)  //Si se te escapa el powerUp y pasa ese margen
        {
            Destroy(gameObject); //Lo elimina
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D other)  //SE activa si choca con other, en este no es un trigger
    {
        
        if (other.transform.CompareTag("Brick")) //Si choca con un brick el disparo
        {
            ballScr = GameObject.FindGameObjectsWithTag("Ball")[0].GetComponent<BallScript>(); //Busca el BallScript de una bola
            ballScr.colisionBrick(other); //LLama a la función para que haga lo mismo q la bola si choca con un brick
            Destroy(gameObject);
        }
        
        if (other.transform.CompareTag("TopEdge"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ball"))
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>() );
        }
        
    }
}
