using UnityEngine;

public class MoveTowardsFinish : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Transform finishTarget;

    void Start()
    {
        GameObject finishObject = GameObject.FindGameObjectWithTag("Finish");
        if (finishObject != null)
        {
            finishTarget = finishObject.transform;
        }
        else
        {
            Debug.LogError("Aucun objet avec le tag 'Finish' trouvé !");
        }
    }

    void Update()
    {
        if (finishTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishTarget.position, moveSpeed * Time.deltaTime);
        }
    }
}
