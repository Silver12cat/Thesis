using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    private float speed = 30.0f;
    private float m_MovX;
    private float m_MovY;
    private Vector3 m_moveHorizontal;
    private Vector3 m_movVertical;
    private Vector3 m_velocity;
    private Rigidbody m_Rigid;
    private float m_yRot;
    private float m_xRot;
    private Vector3 m_rotation;
    private Vector3 m_cameraRotation;
    private float m_lookSensitivity = 3.0f;
    private bool m_cursorIsLocked = true;
    static public bool injured;
    static public string crashSite = " ";
    Scene scene;
    public GameObject car;

    public Camera m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        m_Rigid = GetComponent<Rigidbody>();
        LockCursor();
        if (!string.Equals(scene.name,"The road"))
        {
            speed = 30f;
            transform.position = GameManager.Instance.SpawnLocation;
            transform.Rotate(GameManager.Instance.SpawnRotation);
        }
        else
        {
            speed = 80f;
        }
        if (string.Equals(scene.name, crashSite))
        {
            car.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

        m_MovX = Input.GetAxis("Horizontal");
        m_MovY = Input.GetAxis("Vertical");

        m_moveHorizontal = transform.right * m_MovX;
        m_movVertical = transform.forward * m_MovY;

        m_velocity = (m_moveHorizontal + m_movVertical).normalized * speed;

        //mouse movement
        m_yRot = Input.GetAxisRaw("Mouse X");
        m_rotation = new Vector3(0, m_yRot, 0) * m_lookSensitivity;

        //apply camera rotation
        m_xRot = Input.GetAxis("Mouse Y");
        m_cameraRotation = new Vector3(m_xRot, 0, 0) * m_lookSensitivity;

        //player movement
        if (m_velocity != Vector3.zero)
        {
            m_Rigid.MovePosition(m_Rigid.position + m_velocity * Time.fixedDeltaTime);
        }

        //rotate players's camera
        if (m_rotation != Vector3.zero)
        {
            m_Rigid.MoveRotation(m_Rigid.rotation * Quaternion.Euler(m_rotation));

        }

        //making camera rotate like FPS and not like a plane
        if (m_Camera != null)
        {
            m_Camera.transform.Rotate(-m_cameraRotation);
        }

        //InternalLockUpdate();

    }


    //controls the locking and unlocking of the mouse
   /*private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }
        if (m_cursorIsLocked)
        {
            UnlockCursor();
        }
        else if (!m_cursorIsLocked)
        {
            LockCursor();
        }
    }
    */
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetInjured(bool injury)
    {
        if (injury)
        {
            injured = true;
        }
        else
        {
            injured = false;
        }
    }

    public bool GetInjured()
    {
        return injured;
    }

    public void DisableMovement()
    {
        speed = 0f;
        m_lookSensitivity = 0f;
    }

    public void EnableMovement(float s, float look)
    {
        speed = s;
        m_lookSensitivity = look;
    }

    public void SetCrashSite(string site)
    {
        crashSite = site;
    }
}
