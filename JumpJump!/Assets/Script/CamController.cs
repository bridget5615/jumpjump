using UnityEngine;

public class CamController : MonoBehaviour
{
    public string playerTag = "Player";
    public float cameraDistance = 10f;

    private Transform target;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogError($"Could not find object with tag '{playerTag}'");
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + Vector3.up; // add a small offset to keep camera above the player
        Vector3 cameraPosition = targetPosition - transform.forward * cameraDistance;

        transform.position = cameraPosition;
        transform.LookAt(targetPosition);
    }
}
