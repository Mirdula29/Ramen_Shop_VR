using UnityEngine;
using UnityEngine.UIElements;

public class arrow : MonoBehaviour
{
    public float rotation = 100f;
    public float hight = 0.5f;
    public float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotation * Time.deltaTime);

        //float y = position.y + Mathf.Sin(Time.time * speed) * hight;
        //transform.position = new Vector3(transform.position.x, y, position.z);
    }
}
