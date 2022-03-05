using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D customPointer;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(customPointer, Vector2.zero, CursorMode.ForceSoftware);
    }

}
