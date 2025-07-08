using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
///Esta clase sirve para controlar el salto del personaje 
///mediante un boolean y su propio colisionador
///ID string generated is "F:N.LogicaPies"
///</summary>
public class logicaPies : MonoBehaviour
{
    ///<summary>
    ///El atributo personaje es una instancia del codigo personaje,
    ///se instancia porque un colisionador atado a el gameObject es quien detecta si el personaje esta en el piso
    ///y despues cambia el atributo que se encuentra en el codigo personaje a false o true
    ///ID string generated is "F:N.LogicaPies.personaje"
    ///</summary>
    public personaje personaje;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        personaje.puedoSaltar = true;
    }
    private void OnTriggerExit(Collider other)
    {
        personaje.puedoSaltar = false;
    }
}
