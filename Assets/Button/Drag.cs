using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    private bool isDragging = false;
    void OnMouseDown()
    {
        isDragging = true;
    }
    void OnMouseUp()
    {
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = mousePosition;
        }
    }
}
