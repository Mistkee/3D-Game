using UnityEditor;
using UnityEngine;
public class FPCamera : MonoBehaviour
{
    public float horizontalSpeed = 1f;
    [SerializeField] public static int MaxHealth = 100;
    public static int CurrentHealth;
    public float verticalSpeed = 1f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private bool MenuIsOn;
    private Camera cam;

    void Start()
    {
        CurrentHealth = MaxHealth;
        cam = Camera.main;
        MenuIsOn = false;
    }

    void Update()
    {
        // Cursor lock
        if (Input.GetKeyDown(KeyCode.Escape) && !MenuIsOn)
        {
            Cursor.lockState = CursorLockMode.None;
            MenuIsOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && MenuIsOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
            MenuIsOn = false;
        }

        //Camera movement
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);

    }
}

