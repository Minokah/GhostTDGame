using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameplayCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera overheadCamera;
    public CinemachineFreeLook pivotCamera;
    public GameObject pivotObject;

    public float pivotMoveRate = 10;
    public bool enabled = true;
    public bool overhead = true;
    float pivotY;

    // Start is called before the first frame update
    void Start()
    {
        pivotY = pivotObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // overhead cam? we don't care
        if (!enabled || overhead) return;

        // if holding shift, pan horizontally faster
        if (Input.GetKey(KeyCode.LeftShift)) pivotMoveRate = 30;
        else pivotMoveRate = 10;

        // move camera around
        Vector3 newPosition = pivotObject.transform.position;
        if (Input.GetKey(KeyCode.W)) newPosition.x -= pivotMoveRate * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S)) newPosition.x += pivotMoveRate * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) newPosition.z -= pivotMoveRate * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D)) newPosition.z += pivotMoveRate * Time.deltaTime;

        // Bind camera to playable area
        if (newPosition.z < 10) newPosition.z = 10;
        if (newPosition.z > 80) newPosition.z = 80;
        if (newPosition.x < -30) newPosition.x = -30;
        if (newPosition.x > 30) newPosition.x = 30;
        
        pivotObject.transform.position = newPosition;

        // hold right click to pan the camera vertically
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            pivotCamera.m_YAxis.m_MaxSpeed = 4;
            ClampCamera();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            pivotCamera.m_YAxis.m_MaxSpeed = 0;
        }
    }

    public void EnableCams()
    {
        enabled = true;
        SetOverhead(overhead);
    }

    public void DisableCams()
    {
        enabled = false;
        overheadCamera.gameObject.SetActive(false);
        pivotCamera.gameObject.SetActive(false);
    }

    public void SetOverhead(bool state) {
        overhead = state;
        overheadCamera.gameObject.SetActive(overhead);
        pivotCamera.gameObject.SetActive(!overhead);
        if (!overhead) ClampCamera();
    }

    public void ToggleOverhead() {
        SetOverhead(!overhead);
    }

    private void ClampCamera()
    {
        float yClamp = pivotCamera.m_YAxis.Value;
        if (yClamp <= 0)
        {
            yClamp = 0.001f;
            pivotCamera.m_YAxis.Value = 0.001f;
        }
        if (yClamp > 0.5f)
        {
            yClamp = 0.5f;
            pivotCamera.m_YAxis.Value = 0.5f;
        }
        pivotObject.transform.position = new Vector3(pivotObject.transform.position.x, pivotY + (40 * yClamp), pivotObject.transform.position.z);
    }
}
