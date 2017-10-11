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
    private float hoveringTime;
    private EventTrigger trigger;
    private EventTrigger.Entry mouseEnterEntry;
    private EventTrigger.Entry mouseExitEntry;

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

        if (hoveringTime >= 0.3f) {
            if (tooltipWaypoint && tooltipObject)
            {
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
