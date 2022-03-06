/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Model
*项目描述:
*类 名 称:WeChatMenu
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/6 17:45:48
*修 改 人:
*修改时间:
*作用描述:创建一组与微信平台菜单有关的Model
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
    /// 菜单配置类
    /// </summary>
    public class WeChatMenuConfig
    {
        /// <summary>
        /// 对应接口返回的JSON键值,不可更改
        /// </summary>
        public int is_menu_open;

        public WeChatMenuInfo selfmenu_info;
    }

    /// <summary>
    /// 菜单信息列表类
    /// </summary>
    public class WeChatMenuInfo
    {
        public List<WeChatMenuButton> button;
    }

    /// <summary>
    /// 一级菜单按钮类
    /// </summary>
    public class WeChatMenuButton: WeChatMenuSubButton
    {
        /// <summary>
        /// 对于设置的自定菜单 value用于保存文本
        /// 对于Img和voice类型菜单 value用于保存素材mediaID
        /// 对于Video类型的菜单,value用于保存视频的下载链接
        /// </summary>
        public string value;

        public WeChatMenuSubButtons sub_button;

    }

    /// <summary>
    /// 二级子菜单按钮类
    /// </summary>
    public class WeChatMenuSubButton
    {
        /// <summary>
        /// 菜单按钮类型
        /// </summary>
        public string type;

        /// <summary>
        /// 菜单按钮名称
        /// </summary>
        public string name;

        /// <summary>
        /// 菜单按钮关键字
        /// </summary>
        public string key;

        /// <summary>
        /// 菜单按钮跳转地址,当按钮类型为View时使用
        /// </summary>
        public string url;
    }

    /// <summary>
    /// 二级子菜单列表
    /// </summary>
    public class WeChatMenuSubButtons
    {
        public List<WeChatMenuSubButton> list;
    }

    /// <summary>
    /// 图文消息信息类
    /// </summary>
    public class WeChatMenuNewsInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title;

        /// <summary>
        /// 摘要
        /// </summary>
        public string degit;

        /// <summary>
        /// 作者
        /// </summary>
        public string author;

        /// <summary>
        /// 指定图文消息是否显示封面
        /// </summary>
        public int show_cover;

        /// <summary>
        /// 图文消息的封面图片的URL
        /// </summary>
        public string show_url;

        /// <summary>
        /// 图文消息的正文图片的URL
        /// </summary>
        public string content_url;

        /// <summary>
        /// 图文消息的原文URL,若置空则没有查看原文入口
        /// </summary>
        public string source_url;
    }
}
