using UnityEngine;
using System.Collections;


public class ItemGenerator2 : MonoBehaviour {
        //carPrefabを入れる
        public GameObject carPrefab;
        //coinPrefabを入れる
        public GameObject coinPrefab;
        //cornPrefabを入れる
        public GameObject conePrefab;
        //スタート地点
        private int startPos = -160;
        //ゴール地点
        private int goalPos = 120;
        //アイテムを出すx方向の範囲
        private float posRange = 3.4f;

    //Unityちゃんのオブジェクト
    GameObject unitychan;
    GameObject cone;
    GameObject car;
    GameObject coin;

    //アイテムが出る地点
    float itemPos;

        // Use this for initialization
    void Start () {

        //Unityちゃんオブジェクト取得
        unitychan = GameObject.Find("unitychan");
        //Unityちゃんの正面40mの地点を取得
        //失敗１：Start()でやるから、itemPosのデータが更新されてない。
        //itemPos = 40 + unitychan.transform.position.z;

        //なぜか、最初しかオブジェクト現れない。Uptodateの負担が重過ぎるのか？
        //失敗３：ここに置くと、最初の1回？しか工場が動かないのか、ゲーム途中からGameObbjectがDestroyedと言われる。
        /* あー、だから、開始時点で必ずUnityちゃんの左に謎の車がいたのかな？CarPrefabのx条件がUnityちゃんのとなりだわ。
        ↑正解！コーンが一つしか生まれなかった理由もそれ。
        Instantiate（）もfor文で毎回呼び出す必要あり。
        cone = Instantiate(conePrefab) as GameObject;
        coin = Instantiate(coinPrefab) as GameObject;
        car = Instantiate(carPrefab) as GameObject;
        */
    }

    // Update is called once per frame
    void Update () {
        //失敗1より;Start()からUpdate()に移す。
        //結果、初期位置以降もアイテムは表れるが、途中でおおもとのPrefabがDestroyされたことになってる。
        itemPos = 50 + unitychan.transform.position.z;

        //ユニティちゃんの50m先にアイテムを生成
        if (itemPos<=120)
        {
            //どのアイテムを出すのかをランダムに設定
            //Range(0,130)でもvery hard。しかし、Scoreと連動してここを変えるなど、ゲーム性は上がりそう！
            int num = Random.Range(0, 200);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, itemPos);
                }
                Debug.Log("cone");
            }
            else if(num <= 10)
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j < 2; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    //失敗２：Unityちゃんの真横に車がいたりするのはオフセットのせい？
                    //int offsetZ = Random.Range(-5, 6);
                    int offsetZ = 0; 
                    //50%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成

                        coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, itemPos + offsetZ);
                        Debug.Log("coin");
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, itemPos + offsetZ);
                        Debug.Log("car");
                    }
                }
            }
        }

    }
}
