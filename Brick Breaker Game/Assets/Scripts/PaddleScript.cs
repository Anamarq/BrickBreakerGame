using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public float speed; //cambiar veloc
    public float rightScreenEdge;
    public float leftScreenEdge;
    public GameManager gm;
    public GameObject ball;  //Para el power up de cambiar la veloc de la bola
    public Transform shot;
    public int timeShot;
    private AudioSource audioShot;
    public float timeWhenCauchInvencibleBall;
    public float timeWhenCauchBar;
    public GameObject BarPowerUp;
    public Sprite invencibleSprite;
    public Sprite normalBallSprite;
    private GameObject[] irrBrick; 
    private bool onInvencible; //Indica que está en on el  power up invencible ball
    // Start is called before the first frame update
    void Start()
    {
        timeShot = 0;
        audioShot= GetComponent<AudioSource>();
        BarPowerUp.SetActive(false);
        irrBrick = GameObject.FindGameObjectsWithTag("IrrompibleBrick");  //LLena un array con los irrompible bricks
        onInvencible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return; ///Lo que hace es saltarse todo lo que iba a hacer después del if
        }
        float horizontal = Input.GetAxis("Horizontal"); //Da un valor 0 si no pulso nada, y positivo si pulso D o -> (mod en Edit-Proyect Settings-Input)
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed); //Mover el paddle en dirección x.Vector2.right da 1 en dirección x y 0 en y (2D)
        //Al mult por horizontal Da neg si izq, o pos si derecha, y solo en horiz pq en vert es 0. DeltaTime es para tiempo real y no por frames, util por si cambia plataforma

        if (transform.position.x < leftScreenEdge) //Si pasa el límite izquierdo
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);//Se queda fijo en esa pos, como que choca. La y se deja igual
        }

        if(transform.position.x > rightScreenEdge) //Si pasa el límite derecho
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);//Se queda fijo en esa pos, como que choca. La y se deja igual
        }


        //Para power up
        if (( (BarPowerUp.activeSelf ==true) && (Time.time - timeWhenCauchBar) > 5))//Si han padsado 5 segundos desde q se cogió el power up
        {
            BarPowerUp.SetActive(false);
        }
        if ((onInvencible == true) && (Time.time - timeWhenCauchInvencibleBall) > 5) //Si han padsado 5 segundos desde q se cogió el power up
        {
            GameObject[] ballpos = GameObject.FindGameObjectsWithTag("Ball");  //LLena un array con las balls
            for (int i = 0; i < ballpos.Length; ++i)
            {
                ballpos[i].GetComponent<BallScript>().ballInvencible = false; //Activa el ballinvencible de cada ball
                ballpos[i].GetComponent<SpriteRenderer>().sprite = normalBallSprite; //Cambia el sprite a bola invencible
            }
            
            for (int i = 0; i < irrBrick.Length; ++i)
            {
                irrBrick[i].GetComponent<BoxCollider2D>().isTrigger = false; //para que ya no traspase los bricks
            }
            onInvencible = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("hit "+other.name);
         if (other.CompareTag("Ball"))
        {
            other.GetComponent<Rigidbody2D>().velocity *= 1.01f; //Aumenta velocidad a la bola
        }

        if (other.CompareTag("ExtraLive"))
        {
            gm.UpdateLives(1); //Aporta una vida
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("FastBall"))
        {
            GameObject[] ballpos = GameObject.FindGameObjectsWithTag("Ball");  //LLena un array con las balls
            for (int i = 0; i < ballpos.Length; ++i)
            {
                ballpos[i].GetComponent<Rigidbody2D>().velocity *= 1.5f; //Aumenta velocidad a la bola
            }

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("SlowBall"))
        {
            GameObject[] ballpos = GameObject.FindGameObjectsWithTag("Ball");  //LLena un array con las balls
            for (int i = 0; i < ballpos.Length; ++i)
            {
                ballpos[i].GetComponent<Rigidbody2D>().velocity *= 0.7f; //Disminuye velocidad a la bola
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("SlowPaddle"))
        {
            speed *= 0.7f;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("FastPaddle"))
        {
            speed *= 1.5f;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("2Balls"))
        {
            GameObject [] ballpos = GameObject.FindGameObjectsWithTag("Ball");  //LLena un array con las balls
            for (int i = 0; i < ballpos.Length; ++i)
            {
                GameObject newBall = Instantiate(ball, new Vector3(ballpos[i].transform.position.x + 0.1f, ballpos[i].transform.position.y+0.1f, transform.position.z), ballpos[i].transform.rotation); //Hace aparecer el power up en ese bloque.
                newBall.GetComponent<Rigidbody2D>().velocity = ballpos[i].GetComponent<Rigidbody2D>().velocity;
                newBall.GetComponent<Rigidbody2D>().AddForce(Vector2.up);
                newBall.GetComponent<BallScript>().inPlay = true;
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Transport"))
        {
            GameObject[] ballpos = GameObject.FindGameObjectsWithTag("Ball");  //LLena un array con las balls
            for (int i = 0; i < ballpos.Length; ++i)
            {
                int randomChanceX = Random.Range(-18, 18); //Crea un valor aleatorio 
                int randomChanceY = Random.Range(-9, 12); //Crea un valor aleatorio 
                ballpos[i].transform.position = new Vector2(randomChanceX/3, randomChanceY/3);

            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Invencible"))
        {
            onInvencible = true;
            timeWhenCauchInvencibleBall = Time.time; 
            GameObject[] ballpos = GameObject.FindGameObjectsWithTag("Ball");  //LLena un array con las balls
            for (int i = 0; i < ballpos.Length; ++i)
            {
                ballpos[i].GetComponent<BallScript>().ballInvencible = true; //Activa el ballinvencible de cada ball
                ballpos[i].GetComponent<SpriteRenderer>().sprite = invencibleSprite; //Cambia el sprite a bola invencible
            }
            for (int i = 0; i < irrBrick.Length; ++i)
            {
                irrBrick[i].GetComponent<BoxCollider2D>().isTrigger = true; //para que traspase los bricks
            }

            Destroy(other.gameObject);
            //Sigue en el script Ball, cuando choca con brick se mantiene la veloc
            //Sigue en el update de este script
        }
        else if (other.CompareTag("Gun"))
        {
            if(timeShot==0) //Controla si ya hay alguno disparando, en cuyo caso no sería 0
                InvokeRepeating("shotGun", 0.2f, 1f); //SE dispara cada q segundo, se invoca la función cada 1 seg


            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Bar"))
        {

            BarPowerUp.SetActive(true);
            timeWhenCauchBar = Time.time;
            Destroy(other.gameObject);
            //Sigue en el update de este script
        }



    }
    void shotGun()
    {
        ++timeShot;
        Instantiate(shot, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), transform.rotation); //Hace aparecer el power up en ese bloque.
        Instantiate(shot, new Vector3(transform.position.x +1, transform.position.y, transform.position.z), transform.rotation); //Hace aparecer el power up en ese bloque.
        audioShot.Play();

        if (timeShot > 10)  //Al llegar a 10 ya deja de disparar
        {
            CancelInvoke("shotGun"); //Deja de invocar la llamada
            timeShot = 0; //Vuelve a 0 por si se coge otro gun
            
        }
    }
}
