using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleSpawner : MonoBehaviour
{
    public GameObject squarePrefab;
    public GameObject smallRectPrefab;
    public GameObject largeRectPrefab;

    private GameObject currentDragging;
    private Vector3 offset;
    private bool isDragging = false;

    void Update()
    {
        // Spawn kare örneği (örnek: Q tuşuna basınca)
        if (Keyboard.current.qKey.wasPressedThisFrame)
            SpawnPiece(squarePrefab);

        // Spawn küçük dikdörtgen (W tuşuna basınca)
        if (Keyboard.current.wKey.wasPressedThisFrame)
            SpawnPiece(smallRectPrefab);

        // Spawn büyük dikdörtgen (E tuşuna basınca)
        if (Keyboard.current.eKey.wasPressedThisFrame)
            SpawnPiece(largeRectPrefab);

        // Sürükleme işlemi
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                currentDragging = hit.collider.gameObject;
                offset = currentDragging.transform.position - (Vector3)mousePos;
                isDragging = true;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
            currentDragging = null;
        }

        if (isDragging && currentDragging != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            currentDragging.transform.position = mousePos + (Vector2)offset;
        }
    }

    void SpawnPiece(GameObject prefab)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        GameObject piece = Instantiate(prefab, mousePos, Quaternion.identity);

        // Rigidbody Kinematic olmalı ki sahnede sabit kalsın, düşmesin
        Rigidbody2D rb = piece.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
