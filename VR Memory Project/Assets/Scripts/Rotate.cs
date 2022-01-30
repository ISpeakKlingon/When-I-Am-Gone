using UnityEngine;

public class Rotate : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up, 25.0f * Time.deltaTime);
    }
}
