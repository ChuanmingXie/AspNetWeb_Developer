/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Model
*项目描述:
*类 名 称:WeChatAccessToken
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/5 9:23:14
*修 改 人:
*修改时间:
*作用描述: 各个类中的参数对应平台接口传递的 JSON 键值的键,不可更改！！！
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvc_WeChat_Base.Model
{
    /// <summary>
    /// 微信平台的许可令牌
    /// </summary>
    public class WeChatAccessToken
    {
        /// <summary>
        /// 许可令牌
        /// 参数对应平台接口传递的 JSON 键值的键，不可变更
        /// </summary>
        public string access_token;

        /// <summary>
        /// 有效时长(秒)
        /// </summary>
        public int expires_in;
    }

    /// <summary>
    /// 获取微信平台IP地址
    /// </summary>
    public class WeChatCallbackIP
    {
        /// <summary>
        /// ip地址列表ip_list与接口返回的键值相对应,不可更改
        /// </summary>
        public List<string> ip_list;
    }
}
