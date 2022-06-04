using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.Spawning;

public class UIController : NetworkBehaviour
{
    [SerializeField] private SettingsPopUp settingsPopUp;
    private GameObject player;
    private RelativeMovement playerControls;
    private GameObject camera;
    private CameraFollow cameraFollow;
    private GameObject weapon;
    private Weapon shooting;
    private bool escPressed;


    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = !Screen.fullScreen;
        settingsPopUp.Close();
        EnableCursor(false);
        escPressed = false;
    }

    void EnableCursor(bool enable)
    {
        Cursor.lockState = (enable ? CursorLockMode.None : CursorLockMode.Locked);
        Cursor.visible = enable;
    }

    // Update is called once per frame
    void Update()
    {
        NetworkObject netPlayer = NetworkSpawnManager.GetLocalPlayerObject();

        if (Input.GetKeyDown(KeyCode.Escape) && netPlayer != null)
        {
            player = netPlayer.gameObject;
            //player = GameObject.FindGameObjectWithTag("Player");
            playerControls = player.GetComponent<RelativeMovement>();
            camera = GameObject.FindGameObjectWithTag("CamPivot");
            cameraFollow = camera.GetComponent<CameraFollow>();
            weapon = GameObject.FindGameObjectWithTag("Weapon");
            shooting = weapon.GetComponent<Weapon>();

            if (Input.GetKeyDown(KeyCode.Escape) && !settingsPopUp.isActiveAndEnabled && escPressed == false)
            {
                cameraFollow.enabled = false;
                playerControls.enabled = false;
                shooting.enabled = false;
                EnableCursor(true);
                escPressed = true;
                Debug.Log("case1");
            }

            else if(Input.GetKeyDown(KeyCode.Escape) && !settingsPopUp.isActiveAndEnabled)
            {
                cameraFollow.enabled = true;
                playerControls.enabled = true;
                shooting.enabled = true;
                EnableCursor(false);
                cameraFollow.enabled = true;
                escPressed = false;
                Debug.Log("case2");
            }
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !settingsPopUp.isActiveAndEnabled && escPressed == false)
        {
            EnableCursor(true);
            escPressed = true;
            Debug.Log("case3");
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnableCursor(false);
            escPressed = false;
            Debug.Log("case4");
        }
       
    }

    public void OnOpenSettings()
    {
        Debug.Log("open settings");
    }

    public void OnPointerDownOpen()
    {
        EnableCursor(true);
        settingsPopUp.Open();
    }

    public void OnPointerDownClose()
    {
        EnableCursor(false);
        settingsPopUp.Close();
    }



}
