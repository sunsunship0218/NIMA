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
        //��player��skinnedMeshRendrer
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
                Destroy(gameObject); // �p�G�w����L��ҡA�P���o�ӡA�O�����
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
                //��ܮɶ�����
                ActiveDuration -= mesFreshRate;
                if (skinnedMeshRenderers == null)
                {
                    skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
                }
                //����ghoast trail���v�l�ҫ�
                for (int i = 0; i < skinnedMeshRenderers.Length; i++)
                {
                    //�إ�empty gameobject
                    GameObject ghTrailGOJ = new GameObject();
                    ghTrailGOJ.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);
                    //�[�Jcomponment,filter���F�B�s�P��Vmesh�����
                   MeshRenderer  meshRenderer =   ghTrailGOJ.AddComponent<MeshRenderer>();
                   MeshFilter meshFilter = ghTrailGOJ.AddComponent<MeshFilter>();
                   //�إ�mesh,�s��bake�᪺�ҫ�
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

