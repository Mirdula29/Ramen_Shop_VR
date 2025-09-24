using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{

    public float hight = 0.2f;
    public float speed = 2f;
    

    private Vector3 position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float y = position.y + Mathf.Sin(Time.time * speed) * (hight/4);
        transform.position = new Vector3(transform.position.x, y, position.z);
    }
}
