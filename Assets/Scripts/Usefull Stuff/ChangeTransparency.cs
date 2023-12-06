using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTransparency : MonoBehaviourWithPause{

    [SerializeField] bool changeAllChildrenTransparency;

    public void SetTransparency(float pTransparency) {

        FindMaterialsAndChangeTransparency(GetComponent<Renderer>(), pTransparency);

        if (!changeAllChildrenTransparency)
            return;


        List<Transform> allChildrenTransforms=new List<Transform>();

        foreach (Transform r in transform.GetComponentsInChildren<Transform>()) {
            allChildrenTransforms.Add(r);
        }

        Debug.Log(allChildrenTransforms.Count);

        for (int i = 0; i < allChildrenTransforms.Count; i++){
            FindMaterialsAndChangeTransparency(allChildrenTransforms[i].GetComponent<Renderer>(), pTransparency);

        }

        //Debug.Log(allChildrenTransforms.Count);

    }

    void FindMaterialsAndChangeTransparency(Renderer r,float pTransparency) {
        if (r == null)
            return;
        Material[] materials = r.materials;
        foreach (Material m in materials)
        {
            m.color = new Color(m.color.r, m.color.g, m.color.b, pTransparency);
            Debug.Log("cock");
        }
    }
    
}
