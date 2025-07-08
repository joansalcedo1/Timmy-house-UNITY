using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
///<summary>
///Esta clase sirve para controlar toda la logica del movimiento del personaje
///ID string generated is "F:N.personaje.idRoles
///</summary>
public class personaje : MonoBehaviour
{


    // Start is called before the first frame update
    ///<summary>
    ///el atributo velocidadMovimiento es la velocidad con la que el personaje se va a mover
    ///ID string generated is "F:N.personaje.velocidadMovimiento
    ///</summary>
    public float velocidadMovimiento = 5f;
    ///<summary>
    ///el atributo velocidadRotacion es la velocidad con la que el personaje se va a girar hacia los lados
    ///ID string generated is "F:N.personaje.velocidadRotacion
    ///</summary>
    public float velocidadRotacion = 200.0f;
    ///<summary>
    ///el atributo anim es el animator Controller
    ///ID string generated is "F:N.personaje.anim
    ///</summary>
    private Animator anim;
    ///<summary>
    ///Los atributos x,y son las direcciones a las que el personaje se mueve en el espacio 
    ///ID string generated is "F:N.personaje.anim
    ///</summary>
    public float x, y;
    ///<summary>
    ///el atributo rb es el rigidbody principal del personaje
    ///ID string generated is "F:N.personaje.x"
    ///ID string generated is "F:N.personaje.y"
    ///</summary>
    public Rigidbody rb;
    ///<summary>
    ///el atributo fuerzaSalto es la fuerza que se le agrega al personaje hacía arriba y así salte 
    ///ID string generated is "F:N.personaje.fuerzaSalto"
    ///</summary>
    public float fuerzaSalto = 8f;
    ///<summary>
    ///el atributo puedoSaltar es el que se encarga de saber si el personaje esta en el piso 
    ///ID string generated is "F:N.personaje.puedoSaltar"
    ///
    ///</summary>
    public bool puedoSaltar;
    ///<summary>
    ///el atributo puedoAgacharme es el que controla en que momento el personaje agacharse
    ///ID string generated is "F:N.personaje.puedoAgacharme"
    ///</summary>
    public bool puedoAgacharme;
    ///<summary>
    ///el atributo colParado es el collider que tiene el personaje cuando esta parado
    ///ID string generated is "F:N.personaje.colParado"
    ///</summary>
    public CapsuleCollider colParado;
    ///<summary>
    ///el atributo colAgachado es el collider que tiene el personaje cuando esta agachado
    ///ID string generated is "F:N.personaje.colAgachado"
    ///</summary>
    public CapsuleCollider colAgachado;
    /// <summary>
    /// el atributo cabeza es un gameObject al cual esta atado el codigo LogicaCabeza, el cual sirve
    ///para que el personaje se agache
    /// ID string generated is "F:N.personaje.cabeza"
    ///</summary>
    public GameObject cabeza;
    ///<summary>
    ///el atributo logicaCabeza es la instancia del codigo LogicaCabeza
    ///ID string generated is "F:N.personaje.logicaCabeza"
    ///</summary>
    public LogicaCabeza logicaCabeza;
    ///<summary>
    ///el atributo estoyAgachado es un boolean que sirve para determinar en que momento el
    ///personaje esta agachado
    ///ID string generated is "F:N.personaje.estoyAgachado"
    ///</summary>
    public bool estoyAgachado;
    ///<summary>
    ///el atributo velocidadInicial es un atributo que guarda la informacion de la velocidad
    ///con la que el personaje inicia 
    ///ID string generated is "F:N.personaje.velocidadIncial"
    ///</summary>
    public float velocidadIncial;
    ///<summary>
    ///el atributo velocidadAgachado es la velocidad que tiene el jugadorEstando Agachado
    ///ID string generated is "F:N.personaje.colAgachado"
    ///</summary>
    public float velocidadAgachado;
    ///<summary>
    ///el atributo velocidadCorrer es la velocidad que tiene el jugador en el momento que esta corriendo
    ///ID string generated is "F:N.personaje.velocidadCorrer"
    ///</summary>
    public int velocidadCorrer;
    ///<summary>
    ///el atributo luces es un gameObject el cual controla todas las luces del edificio de la primera
    ///ID string generated is "F:N.personaje.luces"
    ///escena 
    ///</summary>
    public GameObject luces;
    ///<summary>
    ///el atributo lucesPrendidas es un boolean el cual indica cuando las luces estan prendidas
    ///ID string generated is "F:N.personaje.lucesPrendidas"
    ///</summary>
    bool lucesPrendidas;

    void Start()
    {
        lucesPrendidas=true;
        anim = GetComponent<Animator>();
        puedoSaltar = false;
        puedoAgacharme = false;
        
        velocidadIncial = velocidadMovimiento;
        velocidadAgachado = velocidadMovimiento * 0.5f;

    }

    private void FixedUpdate()
    {

        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

    }
    // Update is called once per frame
    void Update()
    {
        ApagarLuces();
        
        //logica para correr
        if (Input.GetKey(KeyCode.LeftShift) && !estoyAgachado && puedoSaltar)
        {
            Invoke("velocidadPacorrer", 0.25f);
            if (y > 0)
            {
                anim.SetBool("correr", true);
            }
            else
            {
                anim.SetBool("correr", false);
            }


        }
        else
        {
            anim.SetBool("correr", false);
            if (estoyAgachado)
            {
                velocidadMovimiento = velocidadAgachado;
            }
            else if (puedoSaltar)
            {
                velocidadMovimiento = velocidadIncial;
            }
        }

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
       
        anim.SetFloat("velX", x);
        anim.SetFloat("velY", y);


        //logica par saltar
        if (puedoSaltar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("salte", true);
                rb.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);


            }
            anim.SetBool("tocoSuelo", true);
        }
        else
        {
            estoyCayendo();

        }

        //logica para hacer el emote
        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.SetBool("bailar", true);
            Invoke("dejardeBailar", 1f);
        }



        //logica para agacharse
        if (Input.GetKey(KeyCode.LeftControl))
        {
            puedoAgacharme = true;
            if (puedoAgacharme)
            {
                colAgachado.enabled = true;
                colParado.enabled = false;
                cabeza.SetActive(true);
                estoyAgachado = true;

                anim.SetBool("agacharse", true);

            }
        }
        else
        {
            if (logicaCabeza.contadorDeColision <= 0)
            {
                puedoAgacharme = false;
                anim.SetBool("agacharse", false);


                colParado.enabled = true;
                cabeza.SetActive(false);
                colAgachado.enabled = false;
                estoyAgachado = false;

            }
        }


    }

    public void velocidadPacorrer()
    {
        velocidadMovimiento = velocidadCorrer;
    }
    public void dejardeBailar()
    {
        anim.SetBool("bailar", false);
    }
    public void estoyCayendo()
    {
        anim.SetBool("tocoSuelo", false);
        anim.SetBool("salte", false);
    }
    public void ApagarLuces()
    {
        if (lucesPrendidas)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lucesPrendidas = false;
                luces.SetActive(false);

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lucesPrendidas = true;
                luces.SetActive(true);
            }
        }
      }
}

