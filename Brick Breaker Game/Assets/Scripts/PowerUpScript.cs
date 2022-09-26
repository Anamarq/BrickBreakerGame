using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f, -1)* Time.deltaTime*speed); //Se mueve en y (hacia abajo) cada frame
        if (transform.position.y < -7f)  //Si se te escapa el powerUp y pasa ese margen
        {
            Destroy(gameObject); //Lo elimina
        }
    }
}
