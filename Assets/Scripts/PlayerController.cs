using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.01f;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5F)
        {
            this.transform.Translate(new Vector3(Input.GetAxisRaw(horizontal) * speed * Time.deltaTime,0,0));
        }

        if (Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5F)
        {
            this.transform.Translate(new Vector3(0, Input.GetAxisRaw(vertical) * speed * Time.deltaTime, 0));
        }
    }
}
