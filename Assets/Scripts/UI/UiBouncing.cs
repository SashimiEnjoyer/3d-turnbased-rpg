using UnityEngine;

public class UiBouncing : MonoBehaviour
{
    [SerializeField] private RectTransform rec;
    private float tracker;

    [SerializeField] private float freq;
    [SerializeField] private float height;

    private Vector2 startPos;

    private void Start()
    {
        startPos = rec.localPosition;

        if(rec == null)
            rec = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tracker += Time.deltaTime;
        tracker %= (2 * 3.14f);

        rec.localPosition = (height * Mathf.Sin(tracker * freq) * Vector2.up) + startPos;
    }
}
