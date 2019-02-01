using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioChome.Equipment.Dialog;
using System.Xml;
using System.IO;

namespace BioChome
{
    partial class FrmUV
    {
        string UVMethodXMLPath = "UVMethod.Config";

        private void UVMethod_New_Click(object sender, EventArgs e)
        {
            MethodNewer newer = new MethodNewer();
            newer.ShowDialog();
            if (newer.methodName == null) return;
            if (NewUVMethod(UVMethodXMLPath, newer.methodName))
                UVMethod_Browser.Items.Add(newer.methodName);
        }
        private void UVMethod_Save_Click(object sender, EventArgs e)
        {
            if (UVMethod_Browser.SelectedIndex >= 0)
            {
                DialogResult ret = MessageBox.Show("是否覆盖原配置？", "保存配置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                switch (ret)
                {
                    case DialogResult.Yes:
                        SaveUVXmlFile(UVMethodXMLPath, UVMethod_Browser.SelectedIndex);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }
        private void UVMethod_Del_Click(object sender, EventArgs e)
        {
            if (UVMethod_Browser.SelectedIndex > 0)
            {
                DelUVXmlNode(UVMethodXMLPath, UVMethod_Browser.SelectedIndex);
                UVMethod_Browser.Items.RemoveAt(UVMethod_Browser.SelectedIndex);
                if (UVMethod_Browser.SelectedIndex < 0) UVMethod_Browser.SelectedIndex = UVMethod_Browser.Items.Count - 1;
                ReadUVMethod(UVMethodXMLPath, UVMethod_Browser.SelectedIndex);
            }
        }

        private void UVMethod_Browser_DoubleClick(object sender, EventArgs e)
        {
            if (UVMethod_Browser.SelectedIndex == -1) return;
            DialogResult ret = MessageBox.Show("是否载入配置" + UVMethod_Browser.SelectedItem.ToString() + "？", "载入配置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            switch (ret)
            {
                case DialogResult.Yes:
                    ReadUVMethod(UVMethodXMLPath, UVMethod_Browser.SelectedIndex);

                    AppConfig.frmRight.ClearMethodSnapShotFocus();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void UVMethod_Browser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                UVMethod_Del_Click(sender, e);
            }
        }

        public void CreateUVXmlFile(string path)
        {
            if (File.Exists(path)) return;

            XmlDocument xmlDoc = new XmlDocument();
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);

            XmlNode root = xmlDoc.CreateElement("Method");
            xmlDoc.AppendChild(root);

            XmlNode config = xmlDoc.CreateNode(XmlNodeType.Element, "MethodConfig", null);
            AppConfig.CreateNode(xmlDoc, config, "LastMethodName", "默认");
            root.AppendChild(config);
            XmlNode user = xmlDoc.CreateNode(XmlNodeType.Element, "默认", null);
            AppConfig.CreateNode(xmlDoc, user, "UVVPS", UVVPS_ComboBox.Text);
            AppConfig.CreateNode(xmlDoc, user, "UVAUUnit", UVAUUnit_ComboBox.Text);
            AppConfig.CreateNode(xmlDoc, user, "UVTimeUnit", UVTimeUnit_ComboBox.Text);
            AppConfig.CreateNode(xmlDoc, user, "UVMaxTime", UVMaxTime_TextBox.Text);
            AppConfig.CreateNode(xmlDoc, user, "UVWaveLength1", UVWaveLength1_TextBox.Text);
            AppConfig.CreateNode(xmlDoc, user, "UVWaveLength2", UVWaveLength2_TextBox.Text);
            AppConfig.CreateNode(xmlDoc, user, "UVWaveLength3", UVWaveLength3_TextBox.Text);
            AppConfig.CreateNode(xmlDoc, user, "UVCurv0Color", System.Drawing.ColorTranslator.ToHtml(UVWaveLength1Color_Set.BackColor));
            AppConfig.CreateNode(xmlDoc, user, "UVCurv1Color", System.Drawing.ColorTranslator.ToHtml(UVWaveLength2Color_Set.BackColor));
            AppConfig.CreateNode(xmlDoc, user, "UVCurv2Color", System.Drawing.ColorTranslator.ToHtml(UVWaveLength3Color_Set.BackColor));
            root.AppendChild(user);

            try
            {
                xmlDoc.Save(path);
            }
            catch (Exception e)
            {
                ////显示错误信息  
                //Console.WriteLine(e.Message);
            }
        }

        public XmlNodeList ReadUVXmlFile(string path)
        {
            if (!File.Exists(path)) return null;
            //try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                return xmlDoc.ChildNodes;
            }
            //catch
            {

            }
        }

        public bool NewUVMethod(string path, string methodName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode root = xmlDoc.ChildNodes[1];
            foreach (XmlNode node in root.ChildNodes)
            {
                if (methodName == node.Name)
                {
                    MessageBox.Show("已存在此配置", "新建配置", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }
            try
            {
                XmlNode user = xmlDoc.CreateNode(XmlNodeType.Element, methodName, null);
                AppConfig.CreateNode(xmlDoc, user, "UVVPS", UVVPS_ComboBox.Text);
                AppConfig.CreateNode(xmlDoc, user, "UVAUUnit", UVAUUnit_ComboBox.Text);
                AppConfig.CreateNode(xmlDoc, user, "UVTimeUnit", UVTimeUnit_ComboBox.Text);
                AppConfig.CreateNode(xmlDoc, user, "UVMaxTime", UVMaxTime_TextBox.Text);
                AppConfig.CreateNode(xmlDoc, user, "UVWaveLength1", UVWaveLength1_TextBox.Text);
                AppConfig.CreateNode(xmlDoc, user, "UVWaveLength2", UVWaveLength2_TextBox.Text);
                AppConfig.CreateNode(xmlDoc, user, "UVWaveLength3", UVWaveLength3_TextBox.Text);
                AppConfig.CreateNode(xmlDoc, user, "UVCurv0Color", System.Drawing.ColorTranslator.ToHtml(UVWaveLength1Color_Set.BackColor));
                AppConfig.CreateNode(xmlDoc, user, "UVCurv1Color", System.Drawing.ColorTranslator.ToHtml(UVWaveLength2Color_Set.BackColor));
                AppConfig.CreateNode(xmlDoc, user, "UVCurv2Color", System.Drawing.ColorTranslator.ToHtml(UVWaveLength3Color_Set.BackColor));
                root.AppendChild(user);
                xmlDoc.Save(path);
            }
            catch (Exception e)
            {
                ////显示错误信息  
                //Console.WriteLine(e.Message);
                MessageBox.Show(e.Message, "新建配置", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        public void ReadUVMethod(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1];
            UVVPS_ComboBox.Text = rootNode.ChildNodes[index].ChildNodes[0].InnerText;
            UVAUUnit_ComboBox.Text = rootNode.ChildNodes[index].ChildNodes[1].InnerText;
            UVTimeUnit_ComboBox.Text = rootNode.ChildNodes[index].ChildNodes[2].InnerText;
            UVMaxTime_TextBox.Text = rootNode.ChildNodes[index].ChildNodes[3].InnerText;
            UVWaveLength1_TextBox.Text = rootNode.ChildNodes[index].ChildNodes[4].InnerText;
            UVWaveLength2_TextBox.Text = rootNode.ChildNodes[index].ChildNodes[5].InnerText;
            UVWaveLength3_TextBox.Text = rootNode.ChildNodes[index].ChildNodes[6].InnerText;
            UVWaveLength1Color_Set.BackColor = System.Drawing.ColorTranslator.FromHtml(rootNode.ChildNodes[index].ChildNodes[7].InnerText);
            UVWaveLength2Color_Set.BackColor = System.Drawing.ColorTranslator.FromHtml(rootNode.ChildNodes[index].ChildNodes[8].InnerText);
            UVWaveLength3Color_Set.BackColor = System.Drawing.ColorTranslator.FromHtml(rootNode.ChildNodes[index].ChildNodes[9].InnerText);
            currentMethod = rootNode.ChildNodes[index].Name;
        }

        public void SaveUVXmlFile(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1];
            rootNode.ChildNodes[index].ChildNodes[0].InnerText = UVVPS_ComboBox.Text;
            rootNode.ChildNodes[index].ChildNodes[1].InnerText = UVAUUnit_ComboBox.Text;
            rootNode.ChildNodes[index].ChildNodes[2].InnerText = UVTimeUnit_ComboBox.Text;
            rootNode.ChildNodes[index].ChildNodes[3].InnerText = UVMaxTime_TextBox.Text;
            rootNode.ChildNodes[index].ChildNodes[4].InnerText = UVWaveLength1_TextBox.Text;
            rootNode.ChildNodes[index].ChildNodes[5].InnerText = UVWaveLength2_TextBox.Text;
            rootNode.ChildNodes[index].ChildNodes[6].InnerText = UVWaveLength3_TextBox.Text;
            rootNode.ChildNodes[index].ChildNodes[7].InnerText = System.Drawing.ColorTranslator.ToHtml(UVWaveLength1Color_Set.BackColor);
            rootNode.ChildNodes[index].ChildNodes[8].InnerText = System.Drawing.ColorTranslator.ToHtml(UVWaveLength2Color_Set.BackColor);
            rootNode.ChildNodes[index].ChildNodes[9].InnerText = System.Drawing.ColorTranslator.ToHtml(UVWaveLength3Color_Set.BackColor);
            xmlDoc.Save(path);
        }

        public void DelUVXmlNode(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1];
            rootNode.RemoveChild(rootNode.ChildNodes[index]);
            xmlDoc.Save(path);
        }

        private string currentMethod;
        public int ReadUVMethodConfig(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1];
            for (int methodIndex = 1; methodIndex < rootNode.ChildNodes.Count; ++methodIndex)
            {
                if (rootNode.ChildNodes[methodIndex].Name == rootNode.ChildNodes[0].ChildNodes[0].InnerText)
                {
                    currentMethod = rootNode.ChildNodes[methodIndex].Name;
                    return methodIndex - 1;
                }
            }
            return 0;
        }
        public void SaveUVMethodConfig(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1];
            rootNode.ChildNodes[0].ChildNodes[0].InnerText = currentMethod;
            xmlDoc.Save(path);
        }

        /***********************************************/
        public void SetUVVPS_ComboBox(string s)
        {
            UVVPS_ComboBox.Text = s;
        }
        public string GetUVVPS_ComboBox()
        {
            return UVVPS_ComboBox.Text;
        }
        public void SetUVAUUnit_ComboBox(string s)
        {
            UVAUUnit_ComboBox.Text = s;
        }
        public string GetUVAUUnit_ComboBox()
        {
            return UVAUUnit_ComboBox.Text;
        }
        public void SetUVTimeUnit_ComboBox(string s)
        {
            UVTimeUnit_ComboBox.Text = s;
        }
        public string GetUVTimeUnit_ComboBox()
        {
            return UVTimeUnit_ComboBox.Text;
        }
        public void SetUVMaxTime_TextBox(string s)
        {
            UVMaxTime_TextBox.Text = s;
        }
        public string GetUVMaxTime_TextBox()
        {
            return UVMaxTime_TextBox.Text;
        }
        public void SetUVWaveLength1_TextBox(string s)
        {
            UVWaveLength1_TextBox.Text = s;
        }
        public string GetUVWaveLength1_TextBox()
        {
            return UVWaveLength1_TextBox.Text;
        }
        public void SetUVWaveLength2_TextBox(string s)
        {
            UVWaveLength2_TextBox.Text = s;
        }
        public string GetUVWaveLength2_TextBox()
        {
            return UVWaveLength2_TextBox.Text;
        }
        public void SetUVWaveLength3_TextBox(string s)
        {
            UVWaveLength3_TextBox.Text = s;
        }
        public string GetUVWaveLength3_TextBox()
        {
            return UVWaveLength3_TextBox.Text;
        }
        public void SetUVWaveLength1Color_Set(string s)
        {
            UVWaveLength1Color_Set.BackColor = System.Drawing.ColorTranslator.FromHtml(s);
        }
        public string GetUVWaveLength1Color_Set()
        {
            return System.Drawing.ColorTranslator.ToHtml(UVWaveLength1Color_Set.BackColor);
        }
        public void SetUVWaveLength2Color_Set(string s)
        {
            UVWaveLength2Color_Set.BackColor = System.Drawing.ColorTranslator.FromHtml(s);
        }
        public string GetUVWaveLength2Color_Set()
        {
            return System.Drawing.ColorTranslator.ToHtml(UVWaveLength2Color_Set.BackColor);
        }
        public void SetUVWaveLength3Color_Set(string s)
        {
            UVWaveLength3Color_Set.BackColor = System.Drawing.ColorTranslator.FromHtml(s);
        }
        public string GetUVWaveLength3Color_Set()
        {
            return System.Drawing.ColorTranslator.ToHtml(UVWaveLength3Color_Set.BackColor);
        }

        public void ClearUVMethodFocus()
        {
            UVMethod_Browser.SelectedIndex = -1;
        }
        /***********************************************/

    }
}
