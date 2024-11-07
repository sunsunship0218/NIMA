using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class MeshTrail : MonoBehaviour
    {
        //single patern
        public static MeshTrail Instance { get; private set; }

        [Header("Mesh Releted")]
        public float activetime;
       [ SerializeField] float meshDestoryDelayTime =3f;
        public Transform spawnPosition;

        [Header("Shader related")]
        public Material shaderMaterial;

        public bool istrailActive;
        //放player的skinnedMeshRendrer
        SkinnedMeshRenderer[] skinnedMeshRenderers;

        [SerializeField] float mesFreshRate = 0.1f;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // 如果已有其他實例，銷毀這個，保持單例
            }
        }
        public  void playGhostTrail()
        {
            Debug.Log("call playGhostTrail");
            if (!istrailActive)
            {
                Debug.Log("run condition");
                istrailActive = true;
                StartCoroutine(StartActiveTrail(activetime));
                Debug.Log("already start corountines");
            }

        }
        IEnumerator StartActiveTrail(float ActiveDuration)
        {
            while (ActiveDuration > 0)
            {
                Debug.Log("Creating trail objects...");
                //顯示時間遞減
                ActiveDuration -= mesFreshRate;
                if (skinnedMeshRenderers == null)
                {
                    skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
                }
                //產生ghoast trail的影子模型
                for (int i = 0; i < skinnedMeshRenderers.Length; i++)
                {
                    //建立empty gameobject
                    GameObject ghTrailGOJ = new GameObject();
                    ghTrailGOJ.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);
                    //加入componment,filter為了處存與渲染mesh的資料
                   MeshRenderer  meshRenderer =   ghTrailGOJ.AddComponent<MeshRenderer>();
                   MeshFilter meshFilter = ghTrailGOJ.AddComponent<MeshFilter>();
                   //建立mesh,存放bake後的模型
                    Mesh trailMesh =new Mesh();
                    skinnedMeshRenderers[i].BakeMesh(trailMesh);
                    meshFilter.mesh = trailMesh;
                    meshRenderer.material = shaderMaterial;

                    Destroy(ghTrailGOJ, meshDestoryDelayTime);
                }
           
                yield return new WaitForSeconds(mesFreshRate);
            }
            istrailActive = false;
        }
    }
}

