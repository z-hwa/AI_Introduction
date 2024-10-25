using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public GameObject[] points;

    int v;
    public string filename;
    string output_name;
    public float waitTime = 0.1f;
    public int light_batch = 10;

    public Light directionalLight;

    // Update is called once per frame
    void Update()
    {
        //測試隨機生成點字
        if (Input.GetKeyDown(KeyCode.R))
        {
            v = Random.Range(0, 64);
            for(int i = 0; i < 6; i++)
            {
                if(v % 2 == 1)
                {
                    points[i].gameObject.SetActive(true);
                }else
                {
                    points[i].gameObject.SetActive(false);
                }


                v /= 2;
            }
        }

        //生成點字圖片
        if (Input.GetKeyDown(KeyCode.T))
        {

            StartCoroutine(ProduceWord());

        }

        //測試燈光旋轉
        if (Input.GetKeyDown(KeyCode.S)) { 
            StartCoroutine (LightChange());
        }
    }

    //燈光效果
    IEnumerator LightChange()
    {
        for (int i = -80; i < 80; i++)
        {
            for (int j = -80; j < 80; j++)
            {
                directionalLight.gameObject.transform.eulerAngles = new Vector3(i, j, 0);
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

    IEnumerator ProduceWord()
    {
        float x, y;

        for (int k = 0; k < light_batch; k++) {
            for (int j = 0; j < 64; j++)
            {
                x = Random.Range(-80, 80);
                y = Random.Range(-80, 80);
                directionalLight.gameObject.transform.eulerAngles = new Vector3(x, y, 0);

                v = j;
                for (int i = 0; i < 6; i++)
                {
                    if (v % 2 == 1)
                    {
                        points[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        points[i].gameObject.SetActive(false);
                    }


                    v /= 2;
                }
                yield return new WaitForSeconds(waitTime);

                output_name = filename + "_" + j.ToString() + "_" + k.ToString();
                UnityEngine.ScreenCapture.CaptureScreenshot("images/" + output_name + ".png");
            }
        }
    }
}
