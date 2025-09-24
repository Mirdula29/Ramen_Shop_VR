using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{

    public float hight = 0.08f;
    public float speed = 2f;

    private Vector3 basePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        basePosition = transform.position;
    }


    /// <summary>
    /// Call to change where the arrow floats (x,z base and center y).
    /// Also immediately places the arrow at that base position.
    /// </summary>
    public void SetBasePosition(Vector3 pos)
    {
        basePosition = pos;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        float y = basePosition.y + Mathf.Sin(Time.time * speed) * hight;
        transform.position = new Vector3(basePosition.x, y, basePosition.z);
    }
}
