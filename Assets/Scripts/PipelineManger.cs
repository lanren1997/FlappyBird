using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManger : MonoBehaviour
{
    public GameObject template;
    public float sleep;
    List<Pipeline> pipelines = new List<Pipeline>();


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Coroutine coroutine = null;//定义一个协程的变量
    public void Init()
    {
        for (int i = 0; i < pipelines.Count; i++)
        {
            Destroy(pipelines[i].gameObject);
        }
        pipelines.Clear();
    }
    public void StartRun()
    {
        coroutine = StartCoroutine(GeneratPipelines());       
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
        for (int i = 0; i < pipelines.Count; i++)
        {
            pipelines[i].enabled = false;                
        }
    }

    IEnumerator GeneratPipelines()
    {
        for (int i = 0; i < 3; i++)
        {
            if (pipelines.Count<3)
            {
                GeneratPipeline();
            }
            else
            {
                pipelines[i].enabled = true;
                pipelines[i].Init();
            }

            yield return new WaitForSeconds(sleep);
        }
    }
    
    void  GeneratPipeline()
    {
        if (pipelines.Count<3)
        {
           GameObject obj= Instantiate(template,this.transform);
           Pipeline p=  obj.GetComponent<Pipeline>();
           pipelines.Add(p);
        }
    }
}
