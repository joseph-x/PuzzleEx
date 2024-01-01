using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand;

    private Vector3 PointerWorldPosition => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    private bool canClick;
    private bool holdItem;

    private ItemName currentItemName;

    private void OnEnable()
    {
        EventHandler.ItemSelecetedEvent += OnItemSelecetedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelecetedEvent -= OnItemSelecetedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }

    private void Update()
    {
        canClick = ObjectAtPointerPosition();

        if (hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }

        if (canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtPointerPosition().gameObject);

        }
    }


    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();
                if (holdItem)
                {
                    interactive?.CheckItem(currentItemName);
                }
                else
                {
                    interactive?.OnEmptyClicked();
                }
                break;
        }
    }

    private Collider2D ObjectAtPointerPosition()
    {
        return Physics2D.OverlapPoint(PointerWorldPosition);
    }

    private void OnItemSelecetedEvent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected;

        if (isSelected)
        {
            currentItemName = itemDetails.itemName;
             
        }

        hand.gameObject.SetActive(isSelected);

    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        currentItemName = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }
}
