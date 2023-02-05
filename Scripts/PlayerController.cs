using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public static PlayerController activePlayer;
    public static float mouseSensitivity;
    [SerializeField] GameObject cameraHolder;
    public float speed;
    public float jumpSpeed;
    public float sprintSpeed;
    float verticalLookRotation;
    private CharacterController _charController;
    private float ySpeed;
    private float originalStepOffset;
    public AudioSource audio_footstep;
    public AudioSource audio_runfootstep;
    bool walk = false;
    bool sprint = false;
    bool jump = false;
    public float stamina;
    public float dist_enemy;
    float obr_dist_enemy;
    public float dist_enemy2;
    float obr_dist_enemy2;
    public GameObject enemy;
    public GameObject enemy2;
    public Image dark;
    Color dark_color;
    bool root_cd;
    public float root_cd_time;
    public NavMeshAgent enemy_nav;
    public Events1 event1;
    public float hp1_plank;
    public float hp2_plank;
    public float hp3_plank;
    public AudioSource hit_plank;
    public AudioSource des_plank;
    public GameObject cam;
    public Events3 events3;
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        originalStepOffset = _charController.stepOffset;
        dark_color = dark.color;
    }
    void Awake() 
    {
        activePlayer = this;
        if (PlayerPrefs.HasKey("Sensitivity")) 
        {
            mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");
        }
    }
    void Update()
    {
        Look();
        Move();
        Dist();
        if(root_cd)
        {
            root_cd_time += Time.deltaTime;
            speed = 2f;
            sprint = false;
        }
        if(root_cd_time > 1f)
        {
            root_cd = false;
            root_cd_time = 0;
        }
        RaycastHit hit;
        if (Physics.Raycast(cameraHolder.transform.position, cameraHolder.transform.forward, out hit, 5f)) {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (hit.transform.tag == "plank1") {
                    hp1_plank -= 1;
                    hit_plank.Play();
                    if(hp1_plank < 0)
                    {
                        des_plank.Play();
                        Destroy(hit.collider.gameObject);
                    }
                }
                if (hit.transform.tag == "plank2") {
                    hp2_plank -= 1;
                    hit_plank.Play();
                    if(hp2_plank < 0)
                    {
                        des_plank.Play();
                        Destroy(hit.collider.gameObject);
                    }
                }
                if (hit.transform.tag== "plank3") {
                    hp3_plank -= 1;
                    hit_plank.Play();
                    if(hp3_plank < 0)
                    {
                        des_plank.Play();
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    void Look()
    {
        UnityEngine.Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraHolder.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;
        movementDirection = Vector3.ClampMagnitude(movementDirection, speed);
        movementDirection = transform.TransformDirection(movementDirection);

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        _charController.Move(velocity * Time.deltaTime);
        if(verticalInput>=1f || horizontalInput>=1f || verticalInput<=-1f || horizontalInput<=-1f)
        {
            walk = true;
            if(!audio_footstep.isPlaying && walk==true && sprint==false)
            {
                audio_footstep.Play();
            }
            if(!audio_runfootstep.isPlaying && walk==true && sprint==true)
            {
                audio_runfootstep.Play();
            }
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0f)
            {
                speed = sprintSpeed;
                stamina -= Time.deltaTime;
                sprint = true;
            }else{
                speed = 5.0f;
                sprint = false;
            }
        }else{
            walk = false;
            stamina += Time.deltaTime;
        }
        if(stamina >= 5f)
        {
            stamina = 5f;
        }
        if(stamina <= 0f)
        {
            stamina = 0f;
            jump = false;
            sprint = false;
        }
    }
    void Dist()
    {
        dist_enemy = Vector3.Distance(enemy.transform.position, transform.position);
        dist_enemy2 = Vector3.Distance(enemy2.transform.position, transform.position);
        if(dist_enemy < 25f)
        {
            dark_color = dark.color;
            obr_dist_enemy = 1f - (dist_enemy / 25f);
            dark_color.a = obr_dist_enemy;
            dark.color = dark_color;
        }else{
            dark_color.a -= Time.deltaTime;
        }
        if(dist_enemy2 < 25f)
        {
            dark_color = dark.color;
            obr_dist_enemy2 = 1f - (dist_enemy2 / 25f);
            dark_color.a = obr_dist_enemy2;
            dark.color = dark_color;
        }else{
            dark_color.a -= Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="root")
        {
            root_cd = true;
            if(event1.stop == true)
            {
                enemy_nav.SetDestination(transform.position);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="root")
        {
            root_cd = true;
            if(event1.stop == true)
            {
                enemy_nav.SetDestination(gameObject.transform.position);
            }
        }
    }
}
    
