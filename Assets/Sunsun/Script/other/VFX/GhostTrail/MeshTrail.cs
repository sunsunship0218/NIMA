using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace VFX
{
    public class MeshTrail : MonoBehaviour
    {
        [SerializeField] GameObject ghTrailGOJ;
        [SerializeField] Mesh mesh;
        GameObjectPool gameObjectPool;
        MeshObjectPool meshPool;
        //single patern
        public static MeshTrail Instance { get; private set; }

        [Header("Mesh Releted")]
        public float activetime;
       [ SerializeField] float meshDestoryDelayTime =3f;
        public Transform spawnPosition;

        [Header("Shader related")]
        public Material shaderMaterial;
        public string shaderVarRef;
        public float shaderVarRate = 0.1f;
        public float shaderVarRefreshRate = 0.05f;

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
        private void Start()
        {
            gameObjectPool = new GameObjectPool(ghTrailGOJ, 30);
            meshPool = new MeshObjectPool(30);
        }
        public  void playGhostTrail()
        {
        
            if (!istrailActive)
            {

                istrailActive = true;
                StartCoroutine(StartActiveTrail(activetime));
             
            }

        }
        IEnumerator StartActiveTrail(float ActiveDuration)
        { 
            while (ActiveDuration > 0)
            {
             
                //��ܮɶ�����
                ActiveDuration -= mesFreshRate;
                if (skinnedMeshRenderers == null)
                {
                    skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
                }
                //����ghoast trail���v�l�ҫ�
                for (int i = 0; i < skinnedMeshRenderers.Length; i++)
                {
                    /*  // �إ�empty gameobject
                       //     GameObject ghTrailGOJ = new GameObject();
                       //  ghTrailGOJ.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);
                       ghTrailGOJ.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);
                       //�[�Jcomponment,filter���F�B�s�P��Vmesh�����
                       MeshRenderer  meshRenderer =   ghTrailGOJ.AddComponent<MeshRenderer>();
                      MeshFilter meshFilter = ghTrailGOJ.AddComponent<MeshFilter>();
                       //�إ�mesh,�s��bake�᪺�ҫ�

                        Mesh trailMesh =new Mesh();
                        skinnedMeshRenderers[i].BakeMesh(trailMesh);
                        meshFilter.mesh = trailMesh;
                        meshRenderer.material = shaderMaterial;

                  //     Mesh trailMesh = meshPool.Get();
         */
                    GameObject trailGO = gameObjectPool.Get();
                    trailGO.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);

                    MeshRenderer meshRenderer = trailGO.GetComponent<MeshRenderer>();
                    MeshFilter meshFilter = trailGO.GetComponent<MeshFilter>();

                    // �q�����������@�� Mesh
                    Mesh trailMesh = meshPool.Get();
                    skinnedMeshRenderers[i].BakeMesh(trailMesh);
                    meshFilter.mesh = trailMesh;
                    meshRenderer.material = new Material(shaderMaterial);
                    //trail�ʵe
                    StartCoroutine(StartAnimateTrail(meshRenderer.material, 0, shaderVarRate, shaderVarRefreshRate));
                    StartCoroutine(ReturnToPool(trailGO, trailMesh, meshDestoryDelayTime));

                }
           
                yield return new WaitForSeconds(mesFreshRate);
            }
            istrailActive = false;
        }

        IEnumerator StartAnimateTrail(Material mat, float goal,float rate, float refreshrate)
        {
            float AnimateValue = mat.GetFloat(shaderVarRef);
            while (AnimateValue > goal)
            {
                AnimateValue -= rate;
                mat.SetFloat(shaderVarRef, AnimateValue);
                yield return new WaitForSeconds(refreshrate);
            }
        }

        IEnumerator ReturnToPool(GameObject trailGO, Mesh trailMesh, float delay)
        {
            yield return new WaitForSeconds(delay);

            // Return the mesh to the mesh pool
            meshPool.Return(trailMesh);

            // Return the game object to the object pool
            gameObjectPool.Return(trailGO);
        }
    }
}

public class MeshObjectPool
{
    private Queue<Mesh> pool = new Queue<Mesh>();
    private int initialSize;

    public MeshObjectPool(int initialSize)
    {
        this.initialSize = initialSize;
        for (int i = 0; i < initialSize; i++)
        {
            pool.Enqueue(new Mesh());
        }
    }

    public Mesh Get()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
            return new Mesh();
        }
    }

    public void Return(Mesh mesh)
    {
        mesh.Clear();
        pool.Enqueue(mesh);
    }
}

public class GameObjectPool
{
    private Queue<GameObject> pool = new Queue<GameObject>();
    private GameObject prefab;
    private Transform parent;

    public GameObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;
        parent = new GameObject(prefab.name + "_Pool").transform;

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, parent);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = GameObject.Instantiate(prefab, parent);
        }
        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}