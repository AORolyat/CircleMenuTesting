using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerTest : MonoBehaviour
{
    public static InputManagerTest Singleton { get { return singleton; } }
    private static InputManagerTest singleton;
    
    private void Awake()
    {
        if (InputManagerTest.singleton == null)
        {
            InputManagerTest.singleton = this;
            DontDestroyOnLoad(this);
        }
        else if (InputManagerTest.singleton != this)
        {
            Destroy(gameObject);
        }
    }

    public Vector3 JStick_JDpad_Keyboard(int joystickIndex)
    {
        float horizontal = 0.0f;
        float vertical = 0.0f;

        horizontal += Input.GetAxisRaw("J_Horizontal" + joystickIndex.ToString());
        horizontal += Input.GetAxisRaw("J_DpadHorizontal" + joystickIndex.ToString());
        horizontal += Input.GetAxisRaw("Horizontal" + joystickIndex.ToString());
        horizontal = Mathf.Clamp(horizontal, -1.0f, 1.0f);


        vertical += Input.GetAxisRaw("J_Vertical" + joystickIndex.ToString());
        vertical += Input.GetAxisRaw("J_DpadVertical" + joystickIndex.ToString());
        vertical += Input.GetAxisRaw("Vertical" + joystickIndex.ToString());
        vertical = Mathf.Clamp(vertical, -1.0f, 1.0f);

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        return direction;
    }
}
