using UnityEngine;
using System.Collections;

public class EndGameFalling : MonoBehaviour
{
    private bool Ended = false;
    
    void Start()
    {
        OzManager.Instance.OnEnding += OnEnd;
    }

    private void OnEnd()
    {
        if (!Ended)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .005f);

            StartCoroutine(fallin(1, 3));
            Ended = true;
        }
    }

    private IEnumerator fallin(float speed, float duration)
    {
        yield return new WaitForSeconds(5f);
        
        var posY = transform.position.y;
        var elapsed = 0f;

        while (elapsed < duration)
        {
            posY -= speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
