using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;
    public ICameraStates states;
    [SerializeField] private Transform target;

    private void Awake()
    {
        states = new FollowCamera(gameObject, target);
        Instance = this;
    }
    private void Update()
    {
        states.Update();
    }
    public void ChangeStates(CameraStatesType cameraStates, GameObject camera, Transform player)
    {
        StatesFabric.NewCameraState(cameraStates, camera, player);
    }
}

public interface ICameraStates
{
    public void Enter();
    public void Update();
}
public class FollowCamera : ICameraStates
{
    GameObject camera;
    CameraMovement cameraMovement;
    Transform target;
    float standartZoom = 10f;
    public FollowCamera(GameObject tCamera, Transform player) { (camera, cameraMovement, target) = (tCamera, tCamera.GetComponent<CameraMovement>(), player); Enter(); }
    public void Enter()
    {
        camera.GetComponent<Camera>().orthographicSize = 5f;
    }
    public void Update()
    {
        camera.transform.position = target.position + new Vector3(0, 0, -10f);
    }
}
public class StaticCamera : ICameraStates
{
    GameObject camera;
    CameraMovement cameraMovement;
    Transform target;
    float distance = 2f;

    public StaticCamera(GameObject tCamera, Transform player) { (camera, cameraMovement, target) = (tCamera, tCamera.GetComponent<CameraMovement>(), player); }
    public void Enter()
    {

    }
    public void Update()
    {

    }
}
public class ExpandingCamera : ICameraStates
{
    GameObject camera;
    CameraMovement cameraMovement;
    Transform target;
    float distance = 2f;

    public ExpandingCamera(GameObject tCamera, Transform player) { (camera, cameraMovement, target) = (tCamera, tCamera.GetComponent<CameraMovement>(), player); }
    public void Enter()
    {

    }
    public void Update()
    {
        float distanceX = Mathf.Max(2f, Mathf.Abs(camera.transform.position.x - target.position.x) + 0.1f);
        float distanceY = Mathf.Max(2f, Mathf.Abs(camera.transform.position.y - target.position.y) + 0.1f);
        camera.GetComponent<Camera>().orthographicSize = Mathf.Max(distanceX, distanceY);
    }
}