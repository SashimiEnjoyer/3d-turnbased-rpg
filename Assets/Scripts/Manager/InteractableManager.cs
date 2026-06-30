using Animancer;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] private bool interactOnce;
    [SerializeField] private UnityEvent OnObjectTrigger;

    private bool isInteracted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (interactOnce && isInteracted)
            return;


        if (other.CompareTag("Player"))
        {
            OnObjectTrigger?.Invoke();

            isInteracted = true;
        }
    }
}
