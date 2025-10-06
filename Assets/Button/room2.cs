using UnityEngine;
using UnityEngine.InputSystem;

public class room2 : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void Update()
    {
        // Sol tık basıldıysa: bir objeye tıklayıp tıklamadığını kontrol et
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - (Vector3)mousePos;
            }
        }

        // Sol tık bırakıldıysa sürüklemeyi bırak
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        // Sürükleme aktifse konumu güncelle
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = mousePos + (Vector2)offset;
        }
    }
}
