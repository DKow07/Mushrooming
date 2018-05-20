using UnityEngine;

public class Interactable : MonoBehaviour {


    public float radius = 3f;

    bool isFocused = false;
    Transform player;

    bool hasInteracted = false;
    public Transform interactionTransform;
  
    public virtual void Interact()
    {
        Debug.Log("Interacted with " + transform.name);
        player.gameObject.GetComponent<Rigidbody>().velocity = Vector2.zero;
    }

    void Update()
    {
        if(isFocused && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
                player.gameObject.GetComponent<PlayerController>().motor.StopFollowingTarget();
            }
        }

      
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }


    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }


}
