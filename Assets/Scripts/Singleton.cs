using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Primero comenzamos creando una clase generica, la cual nos permita heredar de ella
// y convertir cualquier clase en singleton.
// La T representa un generico, el cual es "cualquier cosa", pero al utilizar where : MonoBehaviour, solo podremos 
// utilizar clases que hereden de MonoBehaviour, y al ser asi, como Singleton hereda de MonoBehaviour, podemos pasar
// cualquier clase que herede de singleton.
// Por ejemplo:
// PlayerController : Singleton<PlayerController>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Luego deberemos crear una propiedad para la instancia estatica, la cual representará una variable de tipo T, en el caso del ejemplo,
    // representará un PlayerController, y al ser public get y private set, podremos acceder a el desde afuera pero solo podremos establecerlo
    // desde este script.
    public static T Instance { get; private set; }

    // Luego, en el caso de que queramos que este objeto no se destruya entre escenas, podemos crear una propiedad abstracta
    // la cual deberemos implementar si o si en los hijos para establecer si se mantendrá (true) o si se destruirá al cambiar (false).
    // Utilizando en el hijo: protected override bool Persistent = true; (o false) 
    protected abstract bool Persistent { get; }

    // Luego, dentro de la función Awake, estableceremos lo siguiente:
    // Primero, que sea protected virtual para que en el caso de necesitar utilizar la función desde un hijo,
    // podremos hacerle override y llamar a base.Awake();
    protected virtual void Awake()
    {
        // Luego detectearemos si ya existe una instancia, y si es asi, destruiremos este objeto y no seguiremos ejecutando la función,
        // ya que solo puede haber una instancia de singleton y utilizar la función Destroy, destruirá el objeto en el proximo frame, lo 
        // que hará que se ejecute igualmente el codigo.
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Luego estableceremos que la instancia del singleton será el objeto que está ejecutando el script.
        Instance = this as T;

        // Y si establecimos que Persistent es true, haremos que no se destruya entre escenas, pero para que esto funcione correctamente?
        // deberemos establecer que el padre de su transform sea null, ya que no se puede ejecutar la función DontDestroyOnLoad 
        // si el objeto no se encuentra en la raiz de la jerarquia.
        if (Persistent)
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }
}