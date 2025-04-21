using NextMind.NeuroTags;
using UnityEngine;

[RequireComponent(typeof(NeuroTag))]
public class ConfidenceLogger : MonoBehaviour
{
    private NeuroTag neuroTag;
    public bool isDead;
    public float percentage;
    // pls
    private void Awake()
    {
        // Get the NeuroTag and add the listener through code. 
        // This is given as an example but it could be done by referencing the OnConfidenceChanged function in the editor as well.
        neuroTag = this.GetComponent<NeuroTag>();
        neuroTag.onConfidenceChanged.AddListener(OnConfidenceChanged);
        
       
    }

    private void OnDestroy()
    {
        neuroTag.onConfidenceChanged.RemoveListener(OnConfidenceChanged);
    }

    private void OnConfidenceChanged(float val)
    {
        // Map the value on a 0 - 100 range to get a percentage.
        percentage = val * 100;
        var enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<RangeEnemyBehavior>();

        if (percentage >= 15)
        {
            enemyScript.isNTDead = true;
            isDead = true;
            Destroy(this.gameObject);
        }
       // Debug.Log($"User is focusing on this NeuroTag at {percentage} % !");
    }
}