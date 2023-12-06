using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeTransparency : MonoBehaviourWithPause{

    
    [SerializeField] Shader transparentShader;
    [SerializeField] float alphaValue;
    [SerializeField] bool changeAllChildrenTransparency;

    Shader originalShader;
    public bool isTransparent { get; private set; }

    private void Awake(){
        isTransparent = false;
    }

    public void SetTransparency() {

        FindMaterialsAndChangeTransparency(GetComponent<Renderer>(), alphaValue);

        if (!changeAllChildrenTransparency)
            isTransparent = !isTransparent;

        if (!changeAllChildrenTransparency)
            return;


        List<Transform> allChildrenTransforms=new List<Transform>();

        foreach (Transform r in transform.GetComponentsInChildren<Transform>()) 
            allChildrenTransforms.Add(r);

        Debug.Log(allChildrenTransforms.Count);

        for (int i = 0; i < allChildrenTransforms.Count; i++)
            FindMaterialsAndChangeTransparency(allChildrenTransforms[i].GetComponent<Renderer>(), alphaValue);

        isTransparent = !isTransparent;

    }

    void FindMaterialsAndChangeTransparency(Renderer r,float pTransparency) {
        if (r == null)
            return;


        Material[] materials = r.materials;

        if (!isTransparent)
            originalShader = materials[0].shader;

        //Debug.Log(originalShader);

        foreach (Material m in materials){
            Color color = Color.white;

            if (!isTransparent){
                color = m.color;
                m.shader = transparentShader;
                m.SetFloat("_Opacity", pTransparency);
                m.SetTexture("_AlbedoTexture", m.mainTexture);
                if (color != Color.white)
                    m.SetColor("_AlbedoColor", color);

            }
            else {
                m.shader = originalShader;
            }



        }
    }
    
}
