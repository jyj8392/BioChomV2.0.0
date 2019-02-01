using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using BioChome.Equipment.Dialog;

namespace BioChome
{
    partial class FrmRight
    {
        string MethodSnapShotXMLPath = "MethodSnapShot.Config";

        private void Method_New_Click(object sender, EventArgs e)
        {
            MethodNewer newer = new MethodNewer();
            newer.ShowDialog();
            if (newer.methodName == null) return;
            if (NewMethodSnapShotMethod(MethodSnapShotXMLPath, newer.methodName))
                Method_Browser.Items.Add(newer.methodName);
        }

        private void Method_Save_Click(object sender, EventArgs e)
        {
            if (Method_Browser.SelectedIndex >= 0)
            {
                DialogResult ret = MessageBox.Show("是否覆盖原配置？", "保存配置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                switch (ret)
                {
                    case DialogResult.Yes:
                        SaveMethodSnapShotXmlFile(MethodSnapShotXMLPath, Method_Browser.SelectedIndex);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void Method_Del_Click(object sender, EventArgs e)
        {
            if (Method_Browser.SelectedIndex > 0)
            {
                DelMethodSnapShotXmlNode(MethodSnapShotXMLPath, Method_Browser.SelectedIndex);
                Method_Browser.Items.RemoveAt(Method_Browser.SelectedIndex);
                if (Method_Browser.SelectedIndex < 0) Method_Browser.SelectedIndex = Method_Browser.Items.Count - 1;
                ReadMethodSnapShotMethod(MethodSnapShotXMLPath, Method_Browser.SelectedIndex);
            }
        }

        private void Method_Browser_Click(object sender, EventArgs e)
        {
            if (Method_Browser.SelectedIndex == -1) return;
            ReadMethodSnapShotMethodSaveTime(MethodSnapShotXMLPath, Method_Browser.SelectedIndex);
        }

        private void Method_Browser_DoubleClick(object sender, EventArgs e)
        {
            if (Method_Browser.SelectedIndex == -1) return;

            DialogResult ret = MessageBox.Show("是否载入配置" + Method_Browser.SelectedItem.ToString() + "？", "载入配置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            switch (ret)
            {
                case DialogResult.Yes:
                    ReadMethodSnapShotMethod(MethodSnapShotXMLPath, Method_Browser.SelectedIndex);

                    AppConfig.frmUV.ClearUVMethodFocus();
                    AppConfig.frmPump.ClearPumpMethodFocus();
                    AppConfig.frmCollector.ClearCollectorMethodFocus();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void Method_Browser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                Method_Del_Click(sender, e);
            }
        }

        public void CreateMethodSnapShotXmlFile(string path)
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
            root.AppendChild(user);

            AppConfig.CreateNode(xmlDoc, user, "SaveTime", DateTime.Now.ToString("g"));

            XmlNode uv = xmlDoc.CreateNode(XmlNodeType.Element, "UV", null);
            AppConfig.CreateNode(xmlDoc, uv, "UVVPS", AppConfig.frmUV.GetUVVPS_ComboBox());
            AppConfig.CreateNode(xmlDoc, uv, "UVAUUnit", AppConfig.frmUV.GetUVAUUnit_ComboBox());
            AppConfig.CreateNode(xmlDoc, uv, "UVTimeUnit", AppConfig.frmUV.GetUVTimeUnit_ComboBox());
            AppConfig.CreateNode(xmlDoc, uv, "UVMaxTime", AppConfig.frmUV.GetUVMaxTime_TextBox());
            AppConfig.CreateNode(xmlDoc, uv, "UVWaveLength1", AppConfig.frmUV.GetUVWaveLength1_TextBox());
            AppConfig.CreateNode(xmlDoc, uv, "UVWaveLength2", AppConfig.frmUV.GetUVWaveLength2_TextBox());
            AppConfig.CreateNode(xmlDoc, uv, "UVWaveLength3", AppConfig.frmUV.GetUVWaveLength3_TextBox());
            AppConfig.CreateNode(xmlDoc, uv, "UVCurv0Color", AppConfig.frmUV.GetUVWaveLength1Color_Set());
            AppConfig.CreateNode(xmlDoc, uv, "UVCurv1Color", AppConfig.frmUV.GetUVWaveLength2Color_Set());
            AppConfig.CreateNode(xmlDoc, uv, "UVCurv2Color", AppConfig.frmUV.GetUVWaveLength3Color_Set());
            user.AppendChild(uv);

            XmlNode pump = xmlDoc.CreateNode(XmlNodeType.Element, "Pump", null);
            user.AppendChild(pump);
            XmlNode constant = xmlDoc.CreateNode(XmlNodeType.Element, "Constant", null);
            AppConfig.CreateNode(xmlDoc, constant, "ConstantFlow", AppConfig.frmPump.SetConstantFlow_TextBox(""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantTime", AppConfig.frmPump.SetConstantTime_TextBox(""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpA", AppConfig.frmPump.SetConstantPumpA_TextBox(""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpB", AppConfig.frmPump.SetConstantPumpB_TextBox(""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpC", AppConfig.frmPump.SetConstantPumpC_TextBox(""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpD", AppConfig.frmPump.SetConstantPumpD_TextBox(""));
            pump.AppendChild(constant);
            XmlNode grad = xmlDoc.CreateNode(XmlNodeType.Element, "Gradient", null);
            AppConfig.CreateNode(xmlDoc, grad, "PumpACurvEn", AppConfig.frmPump.SetPumpA_ColorChecked(false));
            AppConfig.CreateNode(xmlDoc, grad, "PumpBCurvEn", AppConfig.frmPump.SetPumpB_ColorChecked(false));
            AppConfig.CreateNode(xmlDoc, grad, "PumpCCurvEn", AppConfig.frmPump.SetPumpC_ColorChecked(false));
            AppConfig.CreateNode(xmlDoc, grad, "PumpDCurvEn", AppConfig.frmPump.SetPumpD_ColorChecked(false));
            AppConfig.CreateNode(xmlDoc, grad, "PumpACurvColor", AppConfig.frmPump.GetPumpA_Color());
            AppConfig.CreateNode(xmlDoc, grad, "PumpBCurvColor", AppConfig.frmPump.GetPumpB_Color());
            AppConfig.CreateNode(xmlDoc, grad, "PumpCCurvColor", AppConfig.frmPump.GetPumpC_Color());
            AppConfig.CreateNode(xmlDoc, grad, "PumpDCurvColor", AppConfig.frmPump.GetPumpD_Color());
            AppConfig.CreateNode(xmlDoc, grad, "GradTime", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradFlow", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradPumpA", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradPumpB", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradPumpC", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradPumpD", "");
            pump.AppendChild(grad);

            XmlNode collector = xmlDoc.CreateNode(XmlNodeType.Element, "Collector", null);
            AppConfig.CreateNode(xmlDoc, collector, "ButtleVolume", AppConfig.frmCollector.SetButtleVolume_TextBox(""));
            AppConfig.CreateNode(xmlDoc, collector, "PipeLength", AppConfig.frmCollector.SetPipeLength_TextBox(""));
            AppConfig.CreateNode(xmlDoc, collector, "PipeDiameter", AppConfig.frmCollector.SetPipeDiameter_TextBox(""));
            AppConfig.CreateNode(xmlDoc, collector, "CollectorProgram_MinPeakWidth", AppConfig.frmCollector.SetCollectorProgram_MinPeakWidth_TextBox(""));
            AppConfig.CreateNode(xmlDoc, collector, "StartButtleNo", AppConfig.frmCollector.SetStartButtleNo_TextBox(""));
            AppConfig.CreateNode(xmlDoc, collector, "StopButtleNo", AppConfig.frmCollector.SetStopButtleNo_TextBox(""));
            AppConfig.CreateNode(xmlDoc, collector, "StartFilter", "");
            AppConfig.CreateNode(xmlDoc, collector, "StartTime", "");
            AppConfig.CreateNode(xmlDoc, collector, "StartThreshold", "");
            AppConfig.CreateNode(xmlDoc, collector, "StartSlope", "");
            AppConfig.CreateNode(xmlDoc, collector, "StopFilter", "");
            AppConfig.CreateNode(xmlDoc, collector, "StopTime", "");
            AppConfig.CreateNode(xmlDoc, collector, "StopThreshold", "");
            AppConfig.CreateNode(xmlDoc, collector, "StopSlope", "");
            AppConfig.CreateNode(xmlDoc, collector, "MaxVolume", "");
            AppConfig.CreateNode(xmlDoc, collector, "MaxTime", "");
            AppConfig.CreateNode(xmlDoc, collector, "CollectorProgram_Channel", "");
            user.AppendChild(collector);

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

        public XmlNodeList ReadMethodSnapShotXmlFile(string path)
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

        public bool NewMethodSnapShotMethod(string path, string methodName)
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
                root.AppendChild(user);

                AppConfig.CreateNode(xmlDoc, user, "SaveTime", DateTime.Now.ToString("g"));

                XmlNode uv = xmlDoc.CreateNode(XmlNodeType.Element, "UV", null);
                AppConfig.CreateNode(xmlDoc, uv, "UVVPS", AppConfig.frmUV.GetUVVPS_ComboBox());
                AppConfig.CreateNode(xmlDoc, uv, "UVAUUnit", AppConfig.frmUV.GetUVAUUnit_ComboBox());
                AppConfig.CreateNode(xmlDoc, uv, "UVTimeUnit", AppConfig.frmUV.GetUVTimeUnit_ComboBox());
                AppConfig.CreateNode(xmlDoc, uv, "UVMaxTime", AppConfig.frmUV.GetUVMaxTime_TextBox());
                AppConfig.CreateNode(xmlDoc, uv, "UVWaveLength1", AppConfig.frmUV.GetUVWaveLength1_TextBox());
                AppConfig.CreateNode(xmlDoc, uv, "UVWaveLength2", AppConfig.frmUV.GetUVWaveLength2_TextBox());
                AppConfig.CreateNode(xmlDoc, uv, "UVWaveLength3", AppConfig.frmUV.GetUVWaveLength3_TextBox());
                AppConfig.CreateNode(xmlDoc, uv, "UVCurv0Color", AppConfig.frmUV.GetUVWaveLength1Color_Set());
                AppConfig.CreateNode(xmlDoc, uv, "UVCurv1Color", AppConfig.frmUV.GetUVWaveLength2Color_Set());
                AppConfig.CreateNode(xmlDoc, uv, "UVCurv2Color", AppConfig.frmUV.GetUVWaveLength3Color_Set());
                user.AppendChild(uv);

                XmlNode pump = xmlDoc.CreateNode(XmlNodeType.Element, "Pump", null);
                user.AppendChild(pump);
                XmlNode constant = xmlDoc.CreateNode(XmlNodeType.Element, "Constant", null);
                AppConfig.CreateNode(xmlDoc, constant, "ConstantFlow", AppConfig.frmPump.SetConstantFlow_TextBox(""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantTime", AppConfig.frmPump.SetConstantTime_TextBox(""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpA", AppConfig.frmPump.SetConstantPumpA_TextBox(""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpB", AppConfig.frmPump.SetConstantPumpB_TextBox(""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpC", AppConfig.frmPump.SetConstantPumpC_TextBox(""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpD", AppConfig.frmPump.SetConstantPumpD_TextBox(""));
                pump.AppendChild(constant);
                XmlNode grad = xmlDoc.CreateNode(XmlNodeType.Element, "Gradient", null);
                AppConfig.CreateNode(xmlDoc, grad, "PumpACurvEn", AppConfig.frmPump.SetPumpA_ColorChecked(false));
                AppConfig.CreateNode(xmlDoc, grad, "PumpBCurvEn", AppConfig.frmPump.SetPumpB_ColorChecked(false));
                AppConfig.CreateNode(xmlDoc, grad, "PumpCCurvEn", AppConfig.frmPump.SetPumpC_ColorChecked(false));
                AppConfig.CreateNode(xmlDoc, grad, "PumpDCurvEn", AppConfig.frmPump.SetPumpD_ColorChecked(false));
                AppConfig.CreateNode(xmlDoc, grad, "PumpACurvColor", AppConfig.frmPump.GetPumpA_Color());
                AppConfig.CreateNode(xmlDoc, grad, "PumpBCurvColor", AppConfig.frmPump.GetPumpB_Color());
                AppConfig.CreateNode(xmlDoc, grad, "PumpCCurvColor", AppConfig.frmPump.GetPumpC_Color());
                AppConfig.CreateNode(xmlDoc, grad, "PumpDCurvColor", AppConfig.frmPump.GetPumpD_Color());
                AppConfig.CreateNode(xmlDoc, grad, "GradTime", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradFlow", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradPumpA", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradPumpB", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradPumpC", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradPumpD", "");
                pump.AppendChild(grad);

                XmlNode collector = xmlDoc.CreateNode(XmlNodeType.Element, "Collector", null);
                AppConfig.CreateNode(xmlDoc, collector, "ButtleVolume", AppConfig.frmCollector.SetButtleVolume_TextBox(""));
                AppConfig.CreateNode(xmlDoc, collector, "PipeLength", AppConfig.frmCollector.SetPipeLength_TextBox(""));
                AppConfig.CreateNode(xmlDoc, collector, "PipeDiameter", AppConfig.frmCollector.SetPipeDiameter_TextBox(""));
                AppConfig.CreateNode(xmlDoc, collector, "CollectorProgram_MinPeakWidth", AppConfig.frmCollector.SetCollectorProgram_MinPeakWidth_TextBox(""));
                AppConfig.CreateNode(xmlDoc, collector, "StartButtleNo", AppConfig.frmCollector.SetStartButtleNo_TextBox(""));
                AppConfig.CreateNode(xmlDoc, collector, "StopButtleNo", AppConfig.frmCollector.SetStopButtleNo_TextBox(""));
                AppConfig.CreateNode(xmlDoc, collector, "StartFilter", "");
                AppConfig.CreateNode(xmlDoc, collector, "StartTime", "");
                AppConfig.CreateNode(xmlDoc, collector, "StartThreshold", "");
                AppConfig.CreateNode(xmlDoc, collector, "StartSlope", "");
                AppConfig.CreateNode(xmlDoc, collector, "StopFilter", "");
                AppConfig.CreateNode(xmlDoc, collector, "StopTime", "");
                AppConfig.CreateNode(xmlDoc, collector, "StopThreshold", "");
                AppConfig.CreateNode(xmlDoc, collector, "StopSlope", "");
                AppConfig.CreateNode(xmlDoc, collector, "MaxVolume", "");
                AppConfig.CreateNode(xmlDoc, collector, "MaxTime", "");
                AppConfig.CreateNode(xmlDoc, collector, "CollectorProgram_Channel", "");
                user.AppendChild(collector);

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

        public void ReadMethodSnapShotMethod(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1].ChildNodes[index];
            SaveTIme_Method.Text = rootNode.ChildNodes[0].InnerText;
            /***************************UV*********************************/
            AppConfig.frmUV.SetUVVPS_ComboBox(rootNode.ChildNodes[1].ChildNodes[0].InnerText);
            AppConfig.frmUV.SetUVAUUnit_ComboBox(rootNode.ChildNodes[1].ChildNodes[1].InnerText);
            AppConfig.frmUV.SetUVTimeUnit_ComboBox(rootNode.ChildNodes[1].ChildNodes[2].InnerText);
            AppConfig.frmUV.SetUVMaxTime_TextBox(rootNode.ChildNodes[1].ChildNodes[3].InnerText);
            AppConfig.frmUV.SetUVWaveLength1_TextBox(rootNode.ChildNodes[1].ChildNodes[4].InnerText);
            AppConfig.frmUV.SetUVWaveLength2_TextBox(rootNode.ChildNodes[1].ChildNodes[5].InnerText);
            AppConfig.frmUV.SetUVWaveLength3_TextBox(rootNode.ChildNodes[1].ChildNodes[6].InnerText);
            AppConfig.frmUV.SetUVWaveLength1Color_Set(rootNode.ChildNodes[1].ChildNodes[7].InnerText);
            AppConfig.frmUV.SetUVWaveLength2Color_Set(rootNode.ChildNodes[1].ChildNodes[8].InnerText);
            AppConfig.frmUV.SetUVWaveLength3Color_Set(rootNode.ChildNodes[1].ChildNodes[9].InnerText);
            /***************************UV*********************************/

            /***************************Pump*********************************/
            AppConfig.frmPump.SetConstantFlow_TextBox(rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[0].InnerText);
            AppConfig.frmPump.SetConstantTime_TextBox(rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[1].InnerText);
            AppConfig.frmPump.SetConstantPumpA_TextBox(rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[2].InnerText);
            AppConfig.frmPump.SetConstantPumpB_TextBox(rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[3].InnerText);
            AppConfig.frmPump.SetConstantPumpC_TextBox(rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[4].InnerText);
            AppConfig.frmPump.SetConstantPumpD_TextBox(rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[5].InnerText);

            AppConfig.frmPump.SetPumpA_ColorChecked(!Convert.ToBoolean(rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[0].InnerText));
            AppConfig.frmPump.SetPumpB_ColorChecked(!Convert.ToBoolean(rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[1].InnerText));
            AppConfig.frmPump.SetPumpC_ColorChecked(!Convert.ToBoolean(rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[2].InnerText));
            AppConfig.frmPump.SetPumpD_ColorChecked(!Convert.ToBoolean(rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[3].InnerText));
            AppConfig.frmPump.SetPumpA_Color(rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[4].InnerText);
            AppConfig.frmPump.SetPumpB_Color(rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[5].InnerText);
            AppConfig.frmPump.SetPumpC_Color(rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[6].InnerText);
            AppConfig.frmPump.SetPumpD_Color(rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[7].InnerText);

            AppConfig.frmPump.SetGradProgram(rootNode.ChildNodes[2].ChildNodes[1].ChildNodes);
            /***************************Pump*********************************/

            /***************************Collector*********************************/
            AppConfig.frmCollector.SetButtleVolume_TextBox(rootNode.ChildNodes[3].ChildNodes[0].InnerText);
            AppConfig.frmCollector.SetPipeLength_TextBox(rootNode.ChildNodes[3].ChildNodes[1].InnerText);
            AppConfig.frmCollector.SetPipeDiameter_TextBox(rootNode.ChildNodes[3].ChildNodes[2].InnerText);
            AppConfig.frmCollector.SetCollectorProgram_MinPeakWidth_TextBox(rootNode.ChildNodes[3].ChildNodes[3].InnerText);
            AppConfig.frmCollector.SetStartButtleNo_TextBox(rootNode.ChildNodes[3].ChildNodes[4].InnerText);
            AppConfig.frmCollector.SetStopButtleNo_TextBox(rootNode.ChildNodes[3].ChildNodes[5].InnerText);

            AppConfig.frmCollector.SetCollectorProgram(rootNode.ChildNodes[3].ChildNodes);
            /***************************Collector*********************************/
            currentMethod = rootNode.Name;
        }

        public void ReadMethodSnapShotMethodSaveTime(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1].ChildNodes[index];
            SaveTIme_Method.Text = rootNode.ChildNodes[0].InnerText;
        }

        public void SaveMethodSnapShotXmlFile(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1].ChildNodes[index];
            rootNode.ChildNodes[0].InnerText = DateTime.Now.ToString("g");
            /***************************UV*********************************/
            rootNode.ChildNodes[1].ChildNodes[0].InnerText = AppConfig.frmUV.GetUVVPS_ComboBox();
            rootNode.ChildNodes[1].ChildNodes[1].InnerText = AppConfig.frmUV.GetUVAUUnit_ComboBox();
            rootNode.ChildNodes[1].ChildNodes[2].InnerText = AppConfig.frmUV.GetUVTimeUnit_ComboBox();
            rootNode.ChildNodes[1].ChildNodes[3].InnerText = AppConfig.frmUV.GetUVMaxTime_TextBox();
            rootNode.ChildNodes[1].ChildNodes[4].InnerText = AppConfig.frmUV.GetUVWaveLength1_TextBox();
            rootNode.ChildNodes[1].ChildNodes[5].InnerText = AppConfig.frmUV.GetUVWaveLength2_TextBox();
            rootNode.ChildNodes[1].ChildNodes[6].InnerText = AppConfig.frmUV.GetUVWaveLength3_TextBox();
            rootNode.ChildNodes[1].ChildNodes[7].InnerText = AppConfig.frmUV.GetUVWaveLength1Color_Set();
            rootNode.ChildNodes[1].ChildNodes[8].InnerText = AppConfig.frmUV.GetUVWaveLength2Color_Set();
            rootNode.ChildNodes[1].ChildNodes[9].InnerText = AppConfig.frmUV.GetUVWaveLength3Color_Set();
            /***************************UV*********************************/

            /***************************Pump*********************************/
            rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[0].InnerText = AppConfig.frmPump.GetConstantFlow_TextBox();
            rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[1].InnerText = AppConfig.frmPump.GetConstantTime_TextBox();
            rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[2].InnerText = AppConfig.frmPump.GetConstantPumpA_TextBox();
            rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[3].InnerText = AppConfig.frmPump.GetConstantPumpB_TextBox();
            rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[4].InnerText = AppConfig.frmPump.GetConstantPumpC_TextBox();
            rootNode.ChildNodes[2].ChildNodes[0].ChildNodes[5].InnerText = AppConfig.frmPump.GetConstantPumpD_TextBox();

            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[0].InnerText = AppConfig.frmPump.GetPumpA_ColorChecked();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[1].InnerText = AppConfig.frmPump.GetPumpB_ColorChecked();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[2].InnerText = AppConfig.frmPump.GetPumpC_ColorChecked();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[3].InnerText = AppConfig.frmPump.GetPumpD_ColorChecked();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[4].InnerText = AppConfig.frmPump.GetPumpA_Color();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[5].InnerText = AppConfig.frmPump.GetPumpB_Color();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[6].InnerText = AppConfig.frmPump.GetPumpC_Color();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[7].InnerText = AppConfig.frmPump.GetPumpD_Color();

            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[8].InnerText = AppConfig.frmPump.GetGradTime();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[9].InnerText = AppConfig.frmPump.GetGradFlow();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[10].InnerText = AppConfig.frmPump.GetGradPumpA();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[11].InnerText = AppConfig.frmPump.GetGradPumpB();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[12].InnerText = AppConfig.frmPump.GetGradPumpC();
            rootNode.ChildNodes[2].ChildNodes[1].ChildNodes[13].InnerText = AppConfig.frmPump.GetGradPumpD();
            /***************************Pump*********************************/

            /***************************Collector*********************************/
            rootNode.ChildNodes[3].ChildNodes[0].InnerText = AppConfig.frmCollector.GetButtleVolume_TextBox();
            rootNode.ChildNodes[3].ChildNodes[1].InnerText = AppConfig.frmCollector.GetPipeLength_TextBox();
            rootNode.ChildNodes[3].ChildNodes[2].InnerText = AppConfig.frmCollector.GetPipeDiameter_TextBox();
            rootNode.ChildNodes[3].ChildNodes[3].InnerText = AppConfig.frmCollector.GetCollectorProgram_MinPeakWidth_TextBox();
            rootNode.ChildNodes[3].ChildNodes[4].InnerText = AppConfig.frmCollector.GetStartButtleNo_TextBox();
            rootNode.ChildNodes[3].ChildNodes[5].InnerText = AppConfig.frmCollector.GetStopButtleNo_TextBox();

            rootNode.ChildNodes[3].ChildNodes[6].InnerText = AppConfig.frmCollector.GetStartFilter();
            rootNode.ChildNodes[3].ChildNodes[7].InnerText = AppConfig.frmCollector.GetStartTime();
            rootNode.ChildNodes[3].ChildNodes[8].InnerText = AppConfig.frmCollector.GetStartThreshold();
            rootNode.ChildNodes[3].ChildNodes[9].InnerText = AppConfig.frmCollector.GetStartSlope();
            rootNode.ChildNodes[3].ChildNodes[10].InnerText = AppConfig.frmCollector.GetStopFilter();
            rootNode.ChildNodes[3].ChildNodes[11].InnerText = AppConfig.frmCollector.GetStopTime();
            rootNode.ChildNodes[3].ChildNodes[12].InnerText = AppConfig.frmCollector.GetStopThreshold();
            rootNode.ChildNodes[3].ChildNodes[13].InnerText = AppConfig.frmCollector.GetStopSlope();
            rootNode.ChildNodes[3].ChildNodes[14].InnerText = AppConfig.frmCollector.GetMaxVolume();
            rootNode.ChildNodes[3].ChildNodes[15].InnerText = AppConfig.frmCollector.GetMaxTime();
            rootNode.ChildNodes[3].ChildNodes[16].InnerText = AppConfig.frmCollector.GetCollectorProgram_Channel(); 
            /***************************Collector*********************************/
            xmlDoc.Save(path);
        }

        public void DelMethodSnapShotXmlNode(string path, int index)
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
        public int ReadMethodSnapShotMethodConfig(string path)
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
        public void SaveMethodSnapShotMethodConfig(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1];
            rootNode.ChildNodes[0].ChildNodes[0].InnerText = currentMethod;
            xmlDoc.Save(path);
        }


        public void ClearMethodSnapShotFocus()
        {
            Method_Browser.SelectedIndex = -1;
        }

    }
}
