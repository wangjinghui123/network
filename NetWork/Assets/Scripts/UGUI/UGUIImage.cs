using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UGUIImage : Image
{
    public BoxCollider2D polyCollider;

    protected override void Awake()
    {
        this.polyCollider = this.GetComponent<BoxCollider2D>();
        Debug.Assert(this.polyCollider != null);
    }

    public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        Debug.LogError("=========");
        Vector3 world = eventCamera.ScreenToWorldPoint(new Vector3(sp.x, sp.y, 0));
        return this.polyCollider.bounds.Contains(world);
    }

}
