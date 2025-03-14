using System;
using UnityEngine;

public class DetectCollider : MonoBehaviour
{
    public ItemObject MyItemObject;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("OnTriggerStay2D");
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
    }
}
