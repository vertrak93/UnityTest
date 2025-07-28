using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject followTarget;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private float cameraSpeed = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    // void Update()
    // {
    //     targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, this.transform.position.z);
    //     this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    // }

    void LateUpdate()
    {
        if (followTarget == null) return;

        targetPosition = new Vector3(
            followTarget.transform.position.x,
            followTarget.transform.position.y,
            transform.position.z // mantiene el Z original
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            cameraSpeed * Time.fixedDeltaTime
        );
    }

}
