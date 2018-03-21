using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URL {

    private const string OSS_ROOT = "http://editor.test.idprint.cn";// "http://box.idprint.cn";

    private const string WX_ROOT = "http://editor.wx.idprint.cn";

    public static string REQUEST_OSS_ACCESS_URL
    {
        get { return OSS_ROOT + "/api/system/policy"; }
    }

    /// <summary>
    /// 识别名片
    /// 户拍照之后，上传到阿里云，然后调用接口
    /// </summary>
    public static string REQUEST_ORC_ACCESS_URL
    {
        get { return WX_ROOT + "/api/image/scan"; }
    }
}
