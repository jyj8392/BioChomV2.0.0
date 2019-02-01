using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml;

namespace BioChome
{
    class AppConfig
    {
        public static DockState ms_FrmLeft = DockState.DockLeft;   // 功能窗体，左端停靠
        public static DockState ms_FrmRight = DockState.DockRight;   // 功能窗体，左端停靠
        public static DockState ms_FrmBottom = DockState.DockBottom;   // 功能窗体，左端停靠
        public static DockState ms_FrmFunction = DockState.Document;   // 功能窗体，左端停靠
        public static DockState ms_FrmUV = DockState.Document;   // 功能窗体，左端停靠

        public const byte AreaCenter = 0;
        public const byte AreaUP = 1;
        public const byte AreaDown = 2;
        public const byte AreaLEFT = 3;
        public const byte AreaRIGHT = 4;
        public static FrmLeft frmLeft;
        public static FrmRight frmRight;
        public static FrmBottom frmBottom;

        //紫外窗体
        public static FrmUV frmUV;
        public static FrmPump frmPump;
        public static FrmCollector frmCollector;
        public static FrmPH frmPH;

        public static FrmMain frmMain;
        //public static byte frmPointer;



        public static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }
    }
}
