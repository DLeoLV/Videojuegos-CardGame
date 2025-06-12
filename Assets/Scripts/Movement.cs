using UnityEngine;
public class Movement : MonoBehaviour
{
    public void Left()
    {
        transform.position += Vector3.left * 3f;
    }

    public void Right()
    {
        transform.position += Vector3.right * 3f;
    }
}