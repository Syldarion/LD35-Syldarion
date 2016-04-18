using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NonRectangularButton : MonoBehaviour
{

    public bool Interactable = true;
    public Image TargetImage;
    public float FadeSpeed = 10f;
    public Color NormalColor = new Color(1f, 1f, 1f, 0.7f);
    public Color HighlightedColor = new Color(1f, 1f, 1f, 1f);
    public Color SelectedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    public Color DisabledColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);
    public UnityEvent OnClick;
    public UnityEvent OnMouseEnter;
    public UnityEvent OnMouseExit;
    public UnityEvent WhileMouseOver;
    public UnityEvent WhileMouseAway;

    private PolygonCollider2D polygonCollider;
    private ColliderDetectMouseover filter;
    private Color targetColor = Color.white;
    private bool mouseoverDone = false;

    // Use this for initialization
    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        filter = GetComponent<ColliderDetectMouseover>();
        targetColor = NormalColor;

        if (TargetImage == null || polygonCollider == null || filter == null)
        {
            Debug.LogWarning("A radial button must have an Image, PolygonCollider2D, and a Collider2DRaycastFilter to function properly.");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Controller support
        // Detect mouse over and mouse click, and invoke events based on this, along with color changes as necessary.
        if (Interactable)
        {
            if (filter.isMouseOver())
            {
                WhileMouseOver.Invoke();
                if (Input.GetMouseButtonUp(0))
                {
                    OnClick.Invoke();
                }

                if (Input.GetMouseButton(0))
                {
                    targetColor = SelectedColor;
                }
                else {
                    targetColor = HighlightedColor;
                }

                if (!mouseoverDone)
                {
                    mouseoverDone = true;
                    OnMouseEnter.Invoke();
                }

            }
            else {

                if (mouseoverDone)
                {
                    mouseoverDone = false;
                    OnMouseExit.Invoke();
                }

                WhileMouseAway.Invoke();
                targetColor = NormalColor;
            }
        }
        else {
            targetColor = DisabledColor;
        }

        TargetImage.color = Color.Lerp(TargetImage.color, targetColor, Time.deltaTime * FadeSpeed);
    }

    public void testPrint()
    {
        print("test");
    }
}