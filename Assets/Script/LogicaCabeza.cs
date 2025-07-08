using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
///Esta clase sirv para saber si el personaje tiene algún objeto encima
///mediante un contador de colisones
///ID string generated is "F:N.LogicaCabeza"
///</summary>
public class LogicaCabeza : MonoBehaviour
{
    ///<summary>
    ///El atributo contadorDeColision permite saber si el personaje tiene algún objeto encima
    ///cuando tiene un objeto encima es 1 y cuando no es 0 
    ///ID string generated is "F:N.LogicaCabeza.contadorDeColision"
    ///</summary>
    public int contadorDeColision = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        contadorDeColision=contadorDeColision+1;
        Debug.Log(contadorDeColision);
    }
    private void OnTriggerExit(Collider other)
    {
        contadorDeColision = contadorDeColision -1;
        Debug.Log(contadorDeColision);
    }
}
