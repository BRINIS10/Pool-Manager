using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Dictionary<string, List<GameObject>> all = new Dictionary<string, List<GameObject>>();
    //public GameObject parent;
    GameObject spawned;
    public static PoolManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Push(string key, GameObject g)
    {
        CheckHierarchy(key);
        all[key].Add(g);
        g.gameObject.SetActive(false);
        g.transform.position = transform.position;

        if (transform.Find(key))
            g.transform.parent = transform.Find(key);
        else
        g.transform.parent = transform;


    }
    void CheckHierarchy(string key)
    {
        if (!all.ContainsKey(key))
        {
            all.Add(key, new List<GameObject>());
            spawned = new GameObject();// Instantiate(parent);
            spawned.transform.parent = transform;
            spawned.transform.localPosition = Vector3.zero;
            spawned.name = key;
        }
    }

    GameObject pivot;
    public GameObject Pool(string key, GameObject model = null)
    {
        CheckHierarchy(key);

        if (all[key].Count != 0)
        {
            pivot = all[key][0];
            all[key].RemoveAt(0);
            return pivot;
        }
        if (!spawned) return null;
        spawned = Instantiate(model);
        spawned.transform.parent = model.transform.parent;
        spawned.transform.localScale = model.transform.localScale;
        //spawned.transform.parent = null; //transform.Find(key);
        return spawned;


    }
}
   
