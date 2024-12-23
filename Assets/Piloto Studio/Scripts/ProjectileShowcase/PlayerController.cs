using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PilotoStudio
{
    public class PlayerController : MonoBehaviour
    {
        public Camera cam;
        public float raycastDistance;
        public Transform spawnPoint;

        private Ray mouseR;

        private Vector3 dir;
        private Quaternion rotation;

        private int currentFX = 0;
        public List<GameObject> prefabs = new List<GameObject>();
        [SerializeField]
        private Text uiT;
        void Update()
        {
            if (cam != null)
            {
                RaycastHit hit;
                Vector3 mousePos = Input.mousePosition;
                mouseR = cam.ScreenPointToRay(mousePos);
                if (Physics.Raycast(mouseR.origin, mouseR.direction, out hit, raycastDistance))
                {
                    RotateToMouseDirection(this.gameObject, hit.point);
                }
                else
                {
                    var pos = mouseR.GetPoint(raycastDistance);
                    RotateToMouseDirection(this.gameObject, pos);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GameObject instance = Instantiate(prefabs[currentFX], spawnPoint.position, spawnPoint.rotation);
                    instance.GetComponent<ProjectileController>().SpawnSubFX(instance.GetComponent<ProjectileController>().muzzle, spawnPoint);
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    currentFX++;
                    if (currentFX > prefabs.Count - 1)
                    {
                        currentFX = 0;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    currentFX--;
                    if (currentFX < 0)
                    {
                        currentFX = prefabs.Count - 1;
                    }
                }
            }


            uiT.text = prefabs[currentFX].name;
        }

        void RotateToMouseDirection(GameObject obj, Vector3 destination)
        {
            dir = destination - obj.transform.position;
            rotation = Quaternion.LookRotation(dir);
            obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
        }
    }
}