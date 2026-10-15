using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    //オブジェクトの参照
    public Transform Target;//カメラの注視点となるオブジェクトの参照
    public Transform CameraJoint;//距離をとるためのオブジェクト参照
    public Camera Camera;//カメラの参照
    //パラメーターとなる変数
    public Vector3 Position;//カメラの位置
    public Vector3 Angle;//カメラの角度
    public Vector3 Distance;//原点（親オブジェクト）空にからの距離

    public Paramater paramater;//パラメータークラスの参照
    [System.Serializable]
    public class Paramater
    {
        //クラスの中にクラスの定義ができる
        public Vector3 Position;//カメラの位置
        public Vector3 Angle;//カメラの角度
        public Vector3 Distance;//原点（親オブジェクト）空にからの距離
        public float tiltMin = -90;
        public float tiltMax = 90;
        public float CameraIntencity = 1.0f;
        //プレイヤーインプット
        public PlayerInput PlayerInput;//プレイヤーインプットの参照
    }

    public float tiltMin = -90;
    public float tiltMax = 90;
    public float CameraIntencity = 1.0f;

    //プレイヤーインプット
    public PlayerInput PlayerInput;//プレイヤーインプットの参照
    void Start()
    {
        //入力アクションの登録
        InputAction();
    }

    
    void Update()
    {
        //カメラの移動や入力などの実行をおこなう
        Position = Vector3.Lerp(Position,Target.position,Time.deltaTime * 10);

        //カメラのチルト(X回転軸)に制限を設ける
        Angle.x = Mathf.Clamp(Angle.x, tiltMin, tiltMax);
        //スクロールで距離変更
        var scroll = Mouse.current.scroll.value;
        Distance.z += scroll.y;
        Distance.z = Mathf.Clamp(Distance.z, -5, 0);
        Angle.z = Mathf.Clamp(Angle.x, tiltMin, tiltMax);
    }
    private void LateUpdate()
    {
        //カメラの位置と回転を調整する
        transform.position = Position;
        transform.eulerAngles = Angle;
        // 注視点からの距離を更新する
        CameraJoint.localPosition = Distance;
    }
    public  void InputAction()
    {
        if(PlayerInput == null)
        {
            Debug.Log("PlayerInputが見つかりません");
            return;//メソッドの強制終了
        }
        PlayerInput.actions["Look"].performed += context =>
        {
            var value = context.ReadValue<Vector2>();
            Angle.x += value.y *-1* CameraIntencity;
            Angle.y += value.x * CameraIntencity;
        };
        PlayerInput.actions["Look"].Enable();
    }
}
