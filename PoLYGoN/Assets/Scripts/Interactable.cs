using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 5f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted =false;

    public virtual void Interact()
    {
        //this method is meant to be overwritten for different objects
        Debug.Log("Interacting with" + interactionTransform.name);

    }

    void Update() 
    {
        if ( isFocus)
        {
            
            float distance = Vector3.Distance(player.position,transform.position);
            if(distance <= radius && !hasInteracted)
            {
                Debug.Log("Interact");
                hasInteracted= true;
                Interact();
            }
        }    
    }

    public void OnFocused ( Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus= false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected() 
    {
        if(interactionTransform == null)
            interactionTransform = transform;

      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(interactionTransform.position,radius);
    }
}
