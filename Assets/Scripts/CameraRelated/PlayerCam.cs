using System.Collections;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform PlayerModel;

    float xRotation;
    float yRotation;

    private IEnumerator sway; 


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCameraSway(0.5f);
    }

    private void Update()
    {
        //Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        PlayerModel.rotation = Quaternion.Euler(0, yRotation, 0);


    }


    private bool finishedLerp;
    private IEnumerator LerpCam(Vector3 pos,Quaternion rot, int frames)
    {
        finishedLerp = false;
        for (int i = 0; i < frames; i++) {
        
            yield return new WaitForSeconds(0.05f);
            transform.position = Vector3.Lerp(transform.position, pos, i / frames);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, i / frames);
        }
        finishedLerp = true;
    }
    public void StartCameraSway(float Intensity)
    {
        sway = SwayCamera(Intensity);
        StartCoroutine(sway);
    }

    private IEnumerator SwayCamera(float Intensity)
    {
        while (true)
        {
            float swayDir = UnityEngine.Random.Range(0, 2);

            //SwayRight// 
            if (swayDir == 0)
            {
                print("swayRight");
                StartCoroutine(LerpCam(transform.position + transform.right * 2, transform.rotation * new Quaternion(0, 0, Intensity * 20, 0), 50));
                yield return new WaitUntil(() => finishedLerp == true);
                print("finished");
                StartCoroutine(LerpCam(transform.position - transform.right * -2, transform.rotation * new Quaternion(0, 0, Intensity * -20, 0), 50));
                yield return new WaitUntil( () => finishedLerp == true);
            }
            else
            {
                print("swayLeft");
                StartCoroutine(LerpCam(transform.position + transform.right * -2, transform.rotation * new Quaternion(0, 0, Intensity * -20, 0), 50));
                yield return new WaitUntil(() => finishedLerp == true);
                StartCoroutine(LerpCam(transform.position - transform.right * 2, transform.rotation * new Quaternion(0, 0, Intensity * 20, 0), 50));
                yield return new WaitUntil(() => finishedLerp == true);
            }
        }
    }

    public void StopCameraSway()
    {
        StopCoroutine(sway);
    }
}
