using System.Collections;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    public float delay = 5f;
    
    public delegate void ExitAction();
    public static event ExitAction OnChunkExited;

    private bool exited = false;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            if (!exited)
            {
                exited = true;
                OnChunkExited();
                StartCoroutine(WaitAndDeactivate());
            }


        }
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(delay);

        Destroy(transform.root.gameObject);
    }



}
