using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInput _playerInput;
    Vector3 _lastValue;
    float speed = 200;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.wKey.isPressed)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime);
        }
        else if (Keyboard.current.sKey.isPressed == true)
        {
            transform.Translate(transform.forward * speed * -1 * Time.deltaTime);
        }
        else if (Keyboard.current.aKey.isPressed == true)
        {
            transform.Rotate(Vector3.up * -90 * Time.deltaTime);
        }
        else if (Keyboard.current.dKey.isPressed == true)
        {
            transform.Rotate(Vector3.up * 90 * Time.deltaTime);
        }

    }
    
}
