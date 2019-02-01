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
    partial class FrmCollector
    {
        string CollectorMethodXMLPath = "CollectorMethod.Config";

        private void CollectorMethod_Del_Click(object sender, EventArgs e)
        {
            if (CollectorMethod_Browser.SelectedIndex >= 1)
            {
                DelCollectorXmlNode(CollectorMethodXMLPath, CollectorMethod_Browser.SelectedIndex);
                CollectorMethod_Browser.Items.RemoveAt(CollectorMethod_Browser.SelectedIndex);
                if (CollectorMethod_Browser.SelectedIndex < 0) CollectorMethod_Browser.SelectedIndex = CollectorMethod_Browser.Items.Count - 1;
                ReadCollectorMethod(CollectorMethodXMLPath, CollectorMethod_Browser.SelectedIndex);
                CollectorProgram_Grid_CellValueChanged(null, null);
            }
        }
        private void CollectorMethod_New_Click(object sender, EventArgs e)
        {
            MethodNewer newer = new MethodNewer();
            newer.ShowDialog();
            if (newer.methodName == null) return;
            if (NewCollectorMethod(CollectorMethodXMLPath, newer.methodName))
                CollectorMethod_Browser.Items.Add(newer.methodName);
        }
        private void CollectorMethod_Save_Click(object sender, EventArgs e)
        {
            if (CollectorMethod_Browser.SelectedIndex >= 0)
            {
                DialogResult ret = MessageBox.Show("是否覆盖原配置？", "保存配置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                switch (ret)
                {
                    case DialogResult.Yes:
                        SaveCollectorXmlFile(CollectorMethodXMLPath, CollectorMethod_Browser.SelectedIndex);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }
        private void CollectorMethod_Browser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                CollectorMethod_Del_Click(sender, e);
            }
        }
        private void CollectorMethod_Browser_DoubleClick(object sender, EventArgs e)
        {
            if (CollectorMethod_Browser.SelectedIndex == -1) return;
            DialogResult ret = MessageBox.Show("是否载入配置" + CollectorMethod_Browser.SelectedItem.ToString() + "？", "载入配置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            switch (ret)
            {
                case DialogResult.Yes:
                    ReadCollectorMethod(CollectorMethodXMLPath, CollectorMethod_Browser.SelectedIndex);
                    CollectorProgram_Grid_CellValueChanged(null, null);

                    AppConfig.frmRight.ClearMethodSnapShotFocus();
                    break;
                case DialogResult.No:
                    break;
            }
        }


        public void CreateCollectorXmlFile(string path)
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
            AppConfig.CreateNode(xmlDoc, user, "ButtleVolume", (ButtleVolume_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, user, "PipeLength", (PipeLength_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, user, "PipeDiameter", (PipeDiameter_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, user, "CollectorProgram_MinPeakWidth", (CollectorProgram_MinPeakWidth_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, user, "StartButtleNo", (StartButtleNo_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, user, "StopButtleNo", (StopButtleNo_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, user, "StartFilter", "");
            AppConfig.CreateNode(xmlDoc, user, "StartTime", "");
            AppConfig.CreateNode(xmlDoc, user, "StartThreshold", "");
            AppConfig.CreateNode(xmlDoc, user, "StartSlope", "");
            AppConfig.CreateNode(xmlDoc, user, "StopFilter", "");
            AppConfig.CreateNode(xmlDoc, user, "StopTime", "");
            AppConfig.CreateNode(xmlDoc, user, "StopThreshold", "");
            AppConfig.CreateNode(xmlDoc, user, "StopSlope", "");
            AppConfig.CreateNode(xmlDoc, user, "MaxVolume", "");
            AppConfig.CreateNode(xmlDoc, user, "MaxTime", "");
            AppConfig.CreateNode(xmlDoc, user, "CollectorProgram_Channel", "");
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

        public XmlNodeList ReadCollectorXmlFile(string path)
        {
            if (!File.Exists(path)) return null;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            return xmlDoc.ChildNodes;
        }

        public bool NewCollectorMethod(string path, string methodName)
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
                AppConfig.CreateNode(xmlDoc, user, "ButtleVolume", (ButtleVolume_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, user, "PipeLength", (PipeLength_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, user, "PipeDiameter", (PipeDiameter_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, user, "CollectorProgram_MinPeakWidth", (CollectorProgram_MinPeakWidth_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, user, "StartButtleNo", (StartButtleNo_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, user, "StopButtleNo", (StopButtleNo_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, user, "StartFilter", "");
                AppConfig.CreateNode(xmlDoc, user, "StartTime", "");
                AppConfig.CreateNode(xmlDoc, user, "StartThreshold", "");
                AppConfig.CreateNode(xmlDoc, user, "StartSlope", "");
                AppConfig.CreateNode(xmlDoc, user, "StopFilter", "");
                AppConfig.CreateNode(xmlDoc, user, "StopTime", "");
                AppConfig.CreateNode(xmlDoc, user, "StopThreshold", "");
                AppConfig.CreateNode(xmlDoc, user, "StopSlope", "");
                AppConfig.CreateNode(xmlDoc, user, "MaxVolume", "");
                AppConfig.CreateNode(xmlDoc, user, "MaxTime", "");
                AppConfig.CreateNode(xmlDoc, user, "CollectorProgram_Channel", "");
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

        public void ReadCollectorMethod(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1].ChildNodes[index];
            ButtleVolume_TextBox.Text = rootNode.ChildNodes[0].InnerText;
            PipeLength_TextBox.Text = rootNode.ChildNodes[1].InnerText;
            PipeDiameter_TextBox.Text = rootNode.ChildNodes[2].InnerText;
            CollectorProgram_MinPeakWidth_TextBox.Text = rootNode.ChildNodes[3].InnerText;
            StartButtleNo_TextBox.Text = rootNode.ChildNodes[4].InnerText;
            StopButtleNo_TextBox.Text = rootNode.ChildNodes[5].InnerText;

            StringToGridColumn(rootNode.ChildNodes);

            currentMethod = rootNode.Name;
        }

        public void SaveCollectorXmlFile(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1].ChildNodes[index];
            rootNode.ChildNodes[0].InnerText = ButtleVolume_TextBox.Text;
            rootNode.ChildNodes[1].InnerText = PipeLength_TextBox.Text;
            rootNode.ChildNodes[2].InnerText = PipeDiameter_TextBox.Text;
            rootNode.ChildNodes[3].InnerText = CollectorProgram_MinPeakWidth_TextBox.Text;
            rootNode.ChildNodes[4].InnerText = StartButtleNo_TextBox.Text;
            rootNode.ChildNodes[5].InnerText = StopButtleNo_TextBox.Text;

            rootNode.ChildNodes[6].InnerText = GridColumnToString(0);
            rootNode.ChildNodes[7].InnerText = GridColumnToString(1);
            rootNode.ChildNodes[8].InnerText = GridColumnToString(2);
            rootNode.ChildNodes[9].InnerText = GridColumnToString(3);
            rootNode.ChildNodes[10].InnerText = GridColumnToString(4);
            rootNode.ChildNodes[11].InnerText = GridColumnToString(5);
            rootNode.ChildNodes[12].InnerText = GridColumnToString(6);
            rootNode.ChildNodes[13].InnerText = GridColumnToString(7);
            rootNode.ChildNodes[14].InnerText = GridColumnToString(8);
            rootNode.ChildNodes[15].InnerText = GridColumnToString(9);
            rootNode.ChildNodes[16].InnerText = ProgramChannelToString();
            xmlDoc.Save(path);
        }

        public void DelCollectorXmlNode(string path, int index)
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
        public int ReadCollectorMethodConfig(string path)
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
        public void SaveCollectorMethodConfig(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1];
            rootNode.ChildNodes[0].ChildNodes[0].InnerText = currentMethod;
            xmlDoc.Save(path);
        }

        /****************************************************************************/
        public string GridColumnToString(int column)
        {
            string ret = "";
            for (int i = 0; i < CollectorProgram_Grid.RowCount; ++i)
            {
                if (CollectorProgram_Grid.Rows[i].Cells[column].Value != null)
                    ret = ret + CollectorProgram_Grid.Rows[i].Cells[column].Value.ToString();
                else
                    ret = ret + " ";
                if (i < CollectorProgram_Grid.RowCount - 1) ret = ret + ",";
            }
            return ret;
        }
        public string ProgramChannelToString()
        {
            return CollectorProgramChannel1.Checked.ToString() + "," +
                CollectorProgramChannel2.Checked.ToString() + "," +
                CollectorProgramChannel3.Checked.ToString();
        }
        public void StringToGridColumn(XmlNodeList list)
        {
            CollectorProgram_Grid.Rows.Clear();
            string[] startFilter = list[6].InnerText.Split(',');
            string[] startTime = list[7].InnerText.Split(',');
            string[] startThreshold = list[8].InnerText.Split(',');
            string[] startSlop = list[9].InnerText.Split(',');
            string[] stopFilter = list[10].InnerText.Split(',');
            string[] stopTime = list[11].InnerText.Split(',');
            string[] stopThreshold = list[12].InnerText.Split(',');
            string[] stopSlop = list[13].InnerText.Split(',');
            string[] maxVolume = list[14].InnerText.Split(',');
            string[] maxTime = list[15].InnerText.Split(',');
            string[] channel = list[16].InnerText.Split(',');
            for (int i = 0; i < startFilter.Length; ++i) if (startFilter[i] == "") continue; else CollectorProgram_Grid.Rows.Add();
            for (int i = 0; i < startFilter.Length; ++i) if (startFilter[i] == "") continue; else if (startFilter[i] != " ") CollectorProgram_Grid.Rows[i].Cells[0].Value = startFilter[i].ToString();
            for (int i = 0; i < startTime.Length; ++i) if (startTime[i] == "") continue; else if (startTime[i] != " ") CollectorProgram_Grid.Rows[i].Cells[1].Value = Convert.ToDouble(startTime[i]);
            for (int i = 0; i < startThreshold.Length; ++i) if (startThreshold[i] == "") continue; else if (startThreshold[i] != " ") CollectorProgram_Grid.Rows[i].Cells[2].Value = Convert.ToDouble(startThreshold[i]);
            for (int i = 0; i < stopSlop.Length; ++i) if (startSlop[i] == "") continue; else if (startSlop[i] != " ") CollectorProgram_Grid.Rows[i].Cells[3].Value = Convert.ToDouble(startSlop[i]);
            for (int i = 0; i < stopFilter.Length; ++i) if (stopFilter[i] == "") continue; else if (stopFilter[i] != " ") CollectorProgram_Grid.Rows[i].Cells[4].Value = stopFilter[i].ToString();
            for (int i = 0; i < stopTime.Length; ++i) if (stopTime[i] == "") continue; else if (stopTime[i] != " ") CollectorProgram_Grid.Rows[i].Cells[5].Value = Convert.ToDouble(stopTime[i]);
            for (int i = 0; i < stopThreshold.Length; ++i) if (stopThreshold[i] == "") continue; else if (stopThreshold[i] != " ") CollectorProgram_Grid.Rows[i].Cells[6].Value = Convert.ToDouble(stopThreshold[i]);
            for (int i = 0; i < stopSlop.Length; ++i) if (stopSlop[i] == "") continue; else if (stopSlop[i] != " ") CollectorProgram_Grid.Rows[i].Cells[7].Value = Convert.ToDouble(stopSlop[i]);
            for (int i = 0; i < maxVolume.Length; ++i) if (maxVolume[i] == "") continue; else if (maxVolume[i] != " ") CollectorProgram_Grid.Rows[i].Cells[8].Value = Convert.ToDouble(maxVolume[i]);
            for (int i = 0; i < maxTime.Length; ++i) if (maxTime[i] == "") continue; else if (maxTime[i] != " ") CollectorProgram_Grid.Rows[i].Cells[9].Value = Convert.ToDouble(maxTime[i]);
            for (int i = 0; i < channel.Length; ++i)
                if (channel[i] == "")
                    continue;
                else if (channel[i] != " ")
                {
                    if (i == 0) CollectorProgramChannel1.Checked = Convert.ToBoolean(channel[i]);
                    if (i == 1) CollectorProgramChannel2.Checked = Convert.ToBoolean(channel[i]);
                    if (i == 2) CollectorProgramChannel3.Checked = Convert.ToBoolean(channel[i]);
                    //CollectorProgramChannel_CheckedList.SetItemChecked(i, Convert.ToBoolean(channel[i]));
                }
        }
        /***********************************************/
        public string SetButtleVolume_TextBox(string s)
        {
            return ButtleVolume_TextBox.Text = s;
        }
        public string GetButtleVolume_TextBox()
        {
            return ButtleVolume_TextBox.Text;
        }
        public string SetPipeLength_TextBox(string s)
        {
            return PipeLength_TextBox.Text = s;
        }
        public string GetPipeLength_TextBox()
        {
            return PipeLength_TextBox.Text;
        }
        public string SetPipeDiameter_TextBox(string s)
        {
            return PipeDiameter_TextBox.Text = s;
        }
        public string GetPipeDiameter_TextBox()
        {
            return PipeDiameter_TextBox.Text;
        }
        public string SetCollectorProgram_MinPeakWidth_TextBox(string s)
        {
            return CollectorProgram_MinPeakWidth_TextBox.Text = s;
        }
        public string GetCollectorProgram_MinPeakWidth_TextBox()
        {
            return CollectorProgram_MinPeakWidth_TextBox.Text;
        }
        public string SetStartButtleNo_TextBox(string s)
        {
            return StartButtleNo_TextBox.Text = s;
        }
        public string GetStartButtleNo_TextBox()
        {
            return StartButtleNo_TextBox.Text;
        }
        public string SetStopButtleNo_TextBox(string s)
        {
            return StopButtleNo_TextBox.Text = s;
        }
        public string GetStopButtleNo_TextBox()
        {
            return StopButtleNo_TextBox.Text;
        }
        public void SetCollectorProgram(XmlNodeList list)
        {
            StringToGridColumn(list);
        }
        public string GetStartFilter()
        {
            return GridColumnToString(0);
        }
        public string GetStartTime()
        {
            return GridColumnToString(1);
        }
        public string GetStartThreshold()
        {
            return GridColumnToString(2);
        }
        public string GetStartSlope()
        {
            return GridColumnToString(3);
        }
        public string GetStopFilter()
        {
            return GridColumnToString(4);
        }
        public string GetStopTime()
        {
            return GridColumnToString(5);
        }
        public string GetStopThreshold()
        {
            return GridColumnToString(6);
        }
        public string GetStopSlope()
        {
            return GridColumnToString(7);
        }
        public string GetMaxVolume()
        {
            return GridColumnToString(8);
        }
        public string GetMaxTime()
        {
            return GridColumnToString(9);
        }
        public string GetCollectorProgram_Channel()
        {
            return ProgramChannelToString();
        }

        public void ClearCollectorMethodFocus()
        {
            CollectorMethod_Browser.SelectedIndex = -1;
        }
        /***********************************************/

    }
}
