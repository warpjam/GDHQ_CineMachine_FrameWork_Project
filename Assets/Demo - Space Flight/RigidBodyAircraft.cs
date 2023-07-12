using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RigidBodyAircraft : MonoBehaviour
{
    public float torque = 5000f;
    public float thrust = 100f;
    public float glideDrag;
    private float glide;
    private Rigidbody rb;

    // Model Animation
    GameObject modelObject;

    // Health
    public float health = 300f;
    public Image healthUI;
    private float maxHealth;

    public GameObject explosion;
    public ParticleSystem explosionPart;

    // Back Light
    private GameObject exhaustLight;
    private Light exLt;

    private Vector2 cursorPos;

    void Start()
    {
        glide = 0f;
        rb = GetComponent<Rigidbody>();
        exhaustLight = this.gameObject.transform.GetChild(3).gameObject;
        exLt = exhaustLight.GetComponent<Light>();
        maxHealth = health;

        modelObject = GameObject.Find("WaveForm");
    }

    void Update() 
    {
        // NEW // =====================================
        cursorPos = CursorPosition();
        YawRoll();
    }

    void FixedUpdate() 
    {
        float roll = Input.GetAxis("Horizontal");
        float pitch = cursorPos.y; // CHANGED - Formerly: Input.GetAxis("Vertical");
        float yaw = cursorPos.x;
        float throttle = Input.GetAxis("Vertical"); // CHANGE - Formerly a bool, space key
        float intensitySmooth = 2f;

        rb.AddRelativeTorque(Vector3.back * torque * roll * 5f);
        rb.AddRelativeTorque(Vector3.right * torque * pitch * 5f);

        // NEW // ======================================
        rb.AddRelativeTorque(Vector3.up * torque * yaw * 5f);

// Rigidbody Force
        if(throttle > 0f) 
        {
            rb.AddRelativeForce(Vector3.forward * thrust);
            glide = thrust;
            exLt.intensity = Mathf.Lerp(exLt.intensity,100f,Time.deltaTime * intensitySmooth);

        }
        else
        {   
            rb.AddRelativeForce(Vector3.forward * glide);
            glide *= glideDrag;
            exLt.intensity = Mathf.Lerp(exLt.intensity,15f,Time.deltaTime * intensitySmooth);

        }

        
    }

    public void damage (float damageAmount)
    {
        health -= damageAmount;
        healthUI.fillAmount = health/maxHealth;

        if(health <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(explosionPart, transform.position, Quaternion.identity);  
            Destroy(gameObject);
            SceneManager.LoadScene("Menu");
        }
    }

    // NEW // ==============================
    private Vector2 CursorPosition()
    {
        Vector2 cursorPosition = Input.mousePosition;
        cursorPosition.x -= Screen.width/2;
        cursorPosition.y -= Screen.height/2;

        float deadZone = 0.05f;
        float cursorX = cursorPosition.x / (Screen.width / 2f);
        float cursorY = cursorPosition.y / (Screen.height / 2f);

        if(Mathf.Abs(cursorX) < deadZone) cursorX = 0;
        if(Mathf.Abs(cursorY) < deadZone) cursorY = 0;
        
        return new Vector2(cursorX, -cursorY);
    }

    private Vector3 CursorAngle()
    {
        Vector2 cursorAdjust = new Vector2(cursorPos.x, cursorPos.y * .25f);
        float angle = Vector2.SignedAngle(cursorAdjust, Vector2.down);
        if(cursorAdjust.x == 0) return new Vector3(0,0,0);
        return new Vector3(0, 0, angle / 4f);
    }

    private void YawRoll()
    {
        Vector3 targetRollValue = new Vector3(0, 180, Mathf.Clamp(-CursorAngle().z, -45, 45));

        modelObject.transform.localRotation = Quaternion.Euler(targetRollValue);
    }
}