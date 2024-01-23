using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FPCamera : MonoBehaviour
{
    public float horizontalSpeed = 1f;
    [SerializeField] public static int MaxHealth = 100;
    public static int CurrentHealth;
    public float verticalSpeed = 1f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    public float restartDelay = 2f;
    public Text gameOverText;
    private Camera cam;
    void Start()
    {
        CurrentHealth = MaxHealth;
        cam = Camera.main;
        gameOverText.text = "";
    }

    void Update()
    {
        // Cursor lock
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //Camera movement
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);

        if (CurrentHealth<=0)
        {
            gameOverText.text = "Game Over";
            Invoke("Restart",restartDelay);
            UIManager.score = 0;
        }

    }

    void Restart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("LevelDesign");
    }
}

