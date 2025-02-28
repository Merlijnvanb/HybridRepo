using UnityEngine;

public class InputPoller : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OzManager.Instance.StartGame();
        }
    }
}
