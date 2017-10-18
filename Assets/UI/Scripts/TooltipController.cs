using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipController : MonoBehaviour{

    [TextArea(5, 10)]
    public string tooltipText;
    public GameObject tooltipObject;
    public GameObject tooltipWaypoint;
    public float showTooltipTime = 0.3f;
    private float hoveringTime;
    private EventTrigger trigger;
    private EventTrigger.Entry mouseEnterEntry;
    private EventTrigger.Entry mouseExitEntry;

    public bool alternativeVerticalPivot = false;
    public bool alternativeHorizontalPivot = false;

    private bool isHovering = false;

    private void Awake()
    {
        if (!trigger) {
            trigger = gameObject.AddComponent<EventTrigger>();
            mouseEnterEntry = new EventTrigger.Entry();
            mouseExitEntry = new EventTrigger.Entry();
        }

        if (trigger) {
            mouseEnterEntry.eventID = EventTriggerType.PointerEnter;
            mouseEnterEntry.callback.AddListener((data) => { showTooltip((PointerEventData)data); });
            trigger.triggers.Add(mouseEnterEntry);

            mouseExitEntry.eventID = EventTriggerType.PointerExit;
            mouseExitEntry.callback.AddListener((data) => { hideTooltip((PointerEventData)data); });
            trigger.triggers.Add(mouseExitEntry);
        }
    }
    // Update is called once per frame
    void Update () {
        if (isHovering) {
            hoveringTime += Time.unscaledDeltaTime;
        }

        if (hoveringTime >= showTooltipTime) {
            if (tooltipWaypoint && tooltipObject)
            {
                if (alternativeVerticalPivot) {
                    RectTransform transform = tooltipObject.GetComponent<RectTransform>();
                    transform.anchorMin = new Vector2(transform.anchorMin.x, 0);
                    transform.anchorMax = new Vector2(transform.anchorMin.x, 0);
                    transform.pivot = new Vector2(transform.pivot.x, 0);
                }
                else {
                    RectTransform transform = tooltipObject.GetComponent<RectTransform>();
                    transform.anchorMin = new Vector2(transform.anchorMin.x, 1);
                    transform.anchorMax = new Vector2(transform.anchorMin.x, 1);
                    transform.pivot = new Vector2(transform.pivot.x, 1);
                }

                if (alternativeHorizontalPivot)
                {
                    RectTransform transform = tooltipObject.GetComponent<RectTransform>();
                    transform.anchorMin = new Vector2(1, transform.anchorMin.y);
                    transform.anchorMax = new Vector2(1,transform.anchorMin.y);
                    transform.pivot = new Vector2(1, transform.pivot.y);
                }
                else
                {
                    RectTransform transform = tooltipObject.GetComponent<RectTransform>();
                    transform.anchorMin = new Vector2(0, transform.anchorMin.y);
                    transform.anchorMax = new Vector2(0, transform.anchorMin.y);
                    transform.pivot = new Vector2(0, transform.pivot.y);
                }

                tooltipObject.SetActive(true);
                tooltipObject.transform.position = tooltipWaypoint.transform.position;
            }
        }
    }

    public void showTooltip(PointerEventData data) {
        isHovering = true;
        if (tooltipObject) {
            tooltipObject.GetComponentInChildren<Text>().text = tooltipText;
        }
    }

    public void hideTooltip(PointerEventData data) {
        isHovering = false;
        hoveringTime = 0f;
        if (tooltipObject) {
            tooltipObject.SetActive(false);
        }
    }


}
