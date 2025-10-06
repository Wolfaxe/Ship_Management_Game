using UnityEngine;
using UnityEngine.InputSystem;

public class DragCloneOnStart : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    [Header("Clone Settings")]
    public int maxCloneCount = 5;       // toplam kaç klon oluşturulabilir
    public GameObject clonePrefab;      // sürüklenecek klon prefabı

    private int cloneCounter = 0;
    private GameObject currentClone;

    void Start()
    {

       Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (clonePrefab == null)
            clonePrefab = gameObject; // prefab yoksa kendini kullan
    }

    void Update()
    {
        // Sol tıkla tıklama kontrolü
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - (Vector3)mousePos;

                // Klon oluştur
                if (cloneCounter < maxCloneCount)
                {
                    currentClone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
                    
                    // Klonun tekrar kendini klonlamaması için scripti devre dışı bırak
                    var script = currentClone.GetComponent<DragCloneOnStart>();
                    if (script != null)
                        script.enabled = false;

                    cloneCounter++;
                }
            }
        }

        // Sol tık bırakıldığında sürüklemeyi bırak
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
            currentClone = null; // sürükleme bittiğinde referansı temizle
        }

        // Sürükleme aktifse klonun pozisyonunu güncelle
        if (isDragging && currentClone != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            currentClone.transform.position = mousePos + (Vector2)offset;
        }
    }
}
