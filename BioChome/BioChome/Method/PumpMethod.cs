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
    partial class FrmPump
    {
        string PumpMethodXMLPath = "PumpMethod.Config";

        private void PumpMethod_Del_Click(object sender, EventArgs e)
        {
            if (PumpMethod_Browser.SelectedIndex >= 1)
            {
                DelPumpXmlNode(PumpMethodXMLPath, PumpMethod_Browser.SelectedIndex);
                PumpMethod_Browser.Items.RemoveAt(PumpMethod_Browser.SelectedIndex);
                if (PumpMethod_Browser.SelectedIndex < 0) PumpMethod_Browser.SelectedIndex = PumpMethod_Browser.Items.Count - 1;
                ReadPumpMethod(PumpMethodXMLPath, PumpMethod_Browser.SelectedIndex);
                GradProgram_Grid_CellValueChanged(null, null);
            }
        }

        private void PumpMethod_New_Click(object sender, EventArgs e)
        {
            MethodNewer newer = new MethodNewer();
            newer.ShowDialog();
            if (newer.methodName == null) return;
            if (NewPumpMethod(PumpMethodXMLPath, newer.methodName))
                PumpMethod_Browser.Items.Add(newer.methodName);
        }

        private void PumpMethod_Save_Click(object sender, EventArgs e)
        {
            if (PumpMethod_Browser.SelectedIndex >= 0)
            {
                DialogResult ret = MessageBox.Show("是否覆盖原配置？", "保存配置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                switch (ret)
                {
                    case DialogResult.Yes:
                        SavePumpXmlFile(PumpMethodXMLPath, PumpMethod_Browser.SelectedIndex);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void PumpMethod_Browser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                PumpMethod_Del_Click(sender, e);
            }
        }

        private void PumpMethod_Browser_DoubleClick(object sender, EventArgs e)
        {
            if (PumpMethod_Browser.SelectedIndex == -1) return;
            DialogResult ret = MessageBox.Show("是否载入配置" + PumpMethod_Browser.SelectedItem.ToString() + "？", "载入配置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            switch (ret)
            {
                case DialogResult.Yes:
                    ReadPumpMethod(PumpMethodXMLPath, PumpMethod_Browser.SelectedIndex);
                    GradProgram_Grid_CellValueChanged(null, null);

                    AppConfig.frmRight.ClearMethodSnapShotFocus();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        public void CreatePumpXmlFile(string path)
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
            XmlNode constant = xmlDoc.CreateNode(XmlNodeType.Element, "Constant", null);
            AppConfig.CreateNode(xmlDoc, constant, "ConstantFlow", (ConstantFlow_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantTime", (ConstantTime_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpA", (ConstantPumpA_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpB", (ConstantPumpB_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpC", (ConstantPumpC_TextBox.Text = ""));
            AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpD", (ConstantPumpD_TextBox.Text = ""));
            user.AppendChild(constant);
            XmlNode grad = xmlDoc.CreateNode(XmlNodeType.Element, "Gradient", null);
            AppConfig.CreateNode(xmlDoc, grad, "PumpACurvEn", (!(PumpA_Color.Checked = false)).ToString());
            AppConfig.CreateNode(xmlDoc, grad, "PumpBCurvEn", (!(PumpB_Color.Checked = false)).ToString());
            AppConfig.CreateNode(xmlDoc, grad, "PumpCCurvEn", (!(PumpC_Color.Checked = false)).ToString());
            AppConfig.CreateNode(xmlDoc, grad, "PumpDCurvEn", (!(PumpD_Color.Checked = false)).ToString());
            AppConfig.CreateNode(xmlDoc, grad, "PumpACurvColor", System.Drawing.ColorTranslator.ToHtml(PumpA_Color.BackColor));
            AppConfig.CreateNode(xmlDoc, grad, "PumpBCurvColor", System.Drawing.ColorTranslator.ToHtml(PumpB_Color.BackColor));
            AppConfig.CreateNode(xmlDoc, grad, "PumpCCurvColor", System.Drawing.ColorTranslator.ToHtml(PumpC_Color.BackColor));
            AppConfig.CreateNode(xmlDoc, grad, "PumpDCurvColor", System.Drawing.ColorTranslator.ToHtml(PumpD_Color.BackColor));
            AppConfig.CreateNode(xmlDoc, grad, "GradTime", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradFlow", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradPumpA", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradPumpB", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradPumpC", "");
            AppConfig.CreateNode(xmlDoc, grad, "GradPumpD", "");
            user.AppendChild(grad);

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

        public XmlNodeList ReadPumpXmlFile(string path)
        {
            if (!File.Exists(path)) return null;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            return xmlDoc.ChildNodes;
        }

        public bool NewPumpMethod(string path, string methodName)
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
                XmlNode constant = xmlDoc.CreateNode(XmlNodeType.Element, "Constant", null);
                AppConfig.CreateNode(xmlDoc, constant, "ConstantFlow", (ConstantFlow_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantTime", (ConstantTime_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpA", (ConstantPumpA_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpB", (ConstantPumpB_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpC", (ConstantPumpC_TextBox.Text = ""));
                AppConfig.CreateNode(xmlDoc, constant, "ConstantPumpD", (ConstantPumpD_TextBox.Text = ""));
                user.AppendChild(constant);
                XmlNode grad = xmlDoc.CreateNode(XmlNodeType.Element, "Gradient", null);
                AppConfig.CreateNode(xmlDoc, grad, "PumpACurvEn", (!(PumpA_Color.Checked = false)).ToString());
                AppConfig.CreateNode(xmlDoc, grad, "PumpBCurvEn", (!(PumpB_Color.Checked = false)).ToString());
                AppConfig.CreateNode(xmlDoc, grad, "PumpCCurvEn", (!(PumpC_Color.Checked = false)).ToString());
                AppConfig.CreateNode(xmlDoc, grad, "PumpDCurvEn", (!(PumpD_Color.Checked = false)).ToString());
                AppConfig.CreateNode(xmlDoc, grad, "PumpACurvColor", System.Drawing.ColorTranslator.ToHtml(PumpA_Color.BackColor));
                AppConfig.CreateNode(xmlDoc, grad, "PumpBCurvColor", System.Drawing.ColorTranslator.ToHtml(PumpB_Color.BackColor));
                AppConfig.CreateNode(xmlDoc, grad, "PumpCCurvColor", System.Drawing.ColorTranslator.ToHtml(PumpC_Color.BackColor));
                AppConfig.CreateNode(xmlDoc, grad, "PumpDCurvColor", System.Drawing.ColorTranslator.ToHtml(PumpD_Color.BackColor));
                AppConfig.CreateNode(xmlDoc, grad, "GradTime", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradFlow", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradPumpA", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradPumpB", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradPumpC", "");
                AppConfig.CreateNode(xmlDoc, grad, "GradPumpD", "");
                user.AppendChild(grad);

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

        public void ReadPumpMethod(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1].ChildNodes[index];
            ConstantFlow_TextBox.Text = rootNode.ChildNodes[0].ChildNodes[0].InnerText;
            ConstantTime_TextBox.Text = rootNode.ChildNodes[0].ChildNodes[1].InnerText;
            ConstantPumpA_TextBox.Text = rootNode.ChildNodes[0].ChildNodes[2].InnerText;
            ConstantPumpB_TextBox.Text = rootNode.ChildNodes[0].ChildNodes[3].InnerText;
            ConstantPumpC_TextBox.Text = rootNode.ChildNodes[0].ChildNodes[4].InnerText;
            ConstantPumpD_TextBox.Text = rootNode.ChildNodes[0].ChildNodes[5].InnerText;

            PumpA_Color.Checked = !Convert.ToBoolean(rootNode.ChildNodes[1].ChildNodes[0].InnerText);
            PumpB_Color.Checked = !Convert.ToBoolean(rootNode.ChildNodes[1].ChildNodes[1].InnerText);
            PumpC_Color.Checked = !Convert.ToBoolean(rootNode.ChildNodes[1].ChildNodes[2].InnerText);
            PumpD_Color.Checked = !Convert.ToBoolean(rootNode.ChildNodes[1].ChildNodes[3].InnerText);
            PumpA_Color.BackColor = System.Drawing.ColorTranslator.FromHtml(rootNode.ChildNodes[1].ChildNodes[4].InnerText);
            PumpB_Color.BackColor = System.Drawing.ColorTranslator.FromHtml(rootNode.ChildNodes[1].ChildNodes[5].InnerText);
            PumpC_Color.BackColor = System.Drawing.ColorTranslator.FromHtml(rootNode.ChildNodes[1].ChildNodes[6].InnerText);
            PumpD_Color.BackColor = System.Drawing.ColorTranslator.FromHtml(rootNode.ChildNodes[1].ChildNodes[7].InnerText);

            StringToGridColumn(rootNode.ChildNodes[1].ChildNodes);

            currentMethod = rootNode.Name;
        }

        public void SavePumpXmlFile(string path, int index)
        {
            if (!File.Exists(path)) return;
            index = index + 1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1].ChildNodes[index];
            rootNode.ChildNodes[0].ChildNodes[0].InnerText = ConstantFlow_TextBox.Text;
            rootNode.ChildNodes[0].ChildNodes[1].InnerText = ConstantTime_TextBox.Text;
            rootNode.ChildNodes[0].ChildNodes[2].InnerText = ConstantPumpA_TextBox.Text;
            rootNode.ChildNodes[0].ChildNodes[3].InnerText = ConstantPumpB_TextBox.Text;
            rootNode.ChildNodes[0].ChildNodes[4].InnerText = ConstantPumpC_TextBox.Text;
            rootNode.ChildNodes[0].ChildNodes[5].InnerText = ConstantPumpD_TextBox.Text;

            rootNode.ChildNodes[1].ChildNodes[0].InnerText = (!PumpA_Color.Checked).ToString();
            rootNode.ChildNodes[1].ChildNodes[1].InnerText = (!PumpB_Color.Checked).ToString();
            rootNode.ChildNodes[1].ChildNodes[2].InnerText = (!PumpC_Color.Checked).ToString();
            rootNode.ChildNodes[1].ChildNodes[3].InnerText = (!PumpD_Color.Checked).ToString();
            rootNode.ChildNodes[1].ChildNodes[4].InnerText = System.Drawing.ColorTranslator.ToHtml(PumpA_Color.BackColor);
            rootNode.ChildNodes[1].ChildNodes[5].InnerText = System.Drawing.ColorTranslator.ToHtml(PumpB_Color.BackColor);
            rootNode.ChildNodes[1].ChildNodes[6].InnerText = System.Drawing.ColorTranslator.ToHtml(PumpC_Color.BackColor);
            rootNode.ChildNodes[1].ChildNodes[7].InnerText = System.Drawing.ColorTranslator.ToHtml(PumpD_Color.BackColor);

            rootNode.ChildNodes[1].ChildNodes[8].InnerText = GridColumnToString(0);
            rootNode.ChildNodes[1].ChildNodes[9].InnerText = GridColumnToString(1);
            rootNode.ChildNodes[1].ChildNodes[10].InnerText = GridColumnToString(2);
            rootNode.ChildNodes[1].ChildNodes[11].InnerText = GridColumnToString(3);
            rootNode.ChildNodes[1].ChildNodes[12].InnerText = GridColumnToString(4);
            rootNode.ChildNodes[1].ChildNodes[13].InnerText = GridColumnToString(5);
            xmlDoc.Save(path);
        }

        public void DelPumpXmlNode(string path, int index)
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
        public int ReadPumpMethodConfig(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1];
            //foreach (XmlNode node in rootNode.ChildNodes)     
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
        public void SavePumpMethodConfig(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.ChildNodes[1];
            rootNode.ChildNodes[0].ChildNodes[0].InnerText = currentMethod;
            xmlDoc.Save(path);
        }

        /****************************************************************************/
        private string GridColumnToString(int column)
        {
            string ret = "";
            for (int i = 0; i < GradProgram_Grid.RowCount; ++i)
            {
                if (GradProgram_Grid.Rows[i].Cells[column].Value != null)
                    ret = ret + GradProgram_Grid.Rows[i].Cells[column].Value.ToString();
                else
                    ret = ret + " ";
                if (i < GradProgram_Grid.RowCount - 1) ret = ret + ",";
            }
            return ret;
        }
        private void StringToGridColumn(XmlNodeList list)
        {
            GradProgram_Grid.Rows.Clear();
            string[] gradTime = list[8].InnerText.Split(',');
            string[] gradFlow = list[9].InnerText.Split(',');
            string[] gradPumpA = list[10].InnerText.Split(',');
            string[] gradPumpB = list[11].InnerText.Split(',');
            string[] gradPumpC = list[12].InnerText.Split(',');
            string[] gradPumpD = list[13].InnerText.Split(',');
            for (int i = 0; i < gradTime.Length; ++i) if (gradTime[i] == "") continue; else GradProgram_Grid.Rows.Add();
            for (int i = 0; i < gradTime.Length; ++i) if (gradTime[i] == "") continue; else if (gradTime[i] != " ") GradProgram_Grid.Rows[i].Cells[0].Value = Convert.ToDouble(gradTime[i]);
            for (int i = 0; i < gradFlow.Length; ++i) if (gradFlow[i] == "") continue; else if (gradFlow[i] != " ") GradProgram_Grid.Rows[i].Cells[1].Value = Convert.ToDouble(gradFlow[i]);
            for (int i = 0; i < gradPumpA.Length; ++i) if (gradPumpA[i] == "") continue; else if (gradPumpA[i] != " ") GradProgram_Grid.Rows[i].Cells[2].Value = Convert.ToDouble(gradPumpA[i]);
            for (int i = 0; i < gradPumpB.Length; ++i) if (gradPumpB[i] == "") continue; else if (gradPumpB[i] != " ") GradProgram_Grid.Rows[i].Cells[3].Value = Convert.ToDouble(gradPumpB[i]);
            for (int i = 0; i < gradPumpC.Length; ++i) if (gradPumpC[i] == "") continue; else if (gradPumpC[i] != " ") GradProgram_Grid.Rows[i].Cells[4].Value = Convert.ToDouble(gradPumpC[i]);
            for (int i = 0; i < gradPumpD.Length; ++i) if (gradPumpD[i] == "") continue; else if (gradPumpD[i] != " ") GradProgram_Grid.Rows[i].Cells[5].Value = Convert.ToDouble(gradPumpD[i]);
        }

        /***********************************************/
        public string SetConstantFlow_TextBox(string s)
        {
            return ConstantFlow_TextBox.Text = s;
        }
        public string GetConstantFlow_TextBox()
        {
            return ConstantFlow_TextBox.Text;
        }
        public string SetConstantTime_TextBox(string s)
        {
            return ConstantTime_TextBox.Text = s;
        }
        public string GetConstantTime_TextBox()
        {
            return ConstantTime_TextBox.Text;
        }
        public string SetConstantPumpA_TextBox(string s)
        {
            return ConstantPumpA_TextBox.Text = s;
        }
        public string GetConstantPumpA_TextBox()
        {
            return ConstantPumpA_TextBox.Text;
        }
        public string SetConstantPumpB_TextBox(string s)
        {
            return ConstantPumpB_TextBox.Text = s;
        }
        public string GetConstantPumpB_TextBox()
        {
            return ConstantPumpB_TextBox.Text;
        }
        public string SetConstantPumpC_TextBox(string s)
        {
            return ConstantPumpC_TextBox.Text = s;
        }
        public string GetConstantPumpC_TextBox()
        {
            return ConstantPumpC_TextBox.Text;
        }
        public string SetConstantPumpD_TextBox(string s)
        {
            return ConstantPumpD_TextBox.Text = s;
        }
        public string GetConstantPumpD_TextBox()
        {
            return ConstantPumpD_TextBox.Text;
        }
        public string SetPumpA_ColorChecked(bool s)
        {
            return (PumpA_Color.Checked = s).ToString();
        }
        public string GetPumpA_ColorChecked()
        {
            return (!PumpA_Color.Checked).ToString();
        }
        public string SetPumpB_ColorChecked(bool s)
        {
            return (PumpB_Color.Checked = s).ToString();
        }
        public string GetPumpB_ColorChecked()
        {
            return (!PumpB_Color.Checked).ToString();
        }
        public string SetPumpC_ColorChecked(bool s)
        {
            return (PumpC_Color.Checked = s).ToString();
        }
        public string GetPumpC_ColorChecked()
        {
            return (!PumpC_Color.Checked).ToString();
        }
        public string SetPumpD_ColorChecked(bool s)
        {
            return (PumpD_Color.Checked = s).ToString();
        }
        public string GetPumpD_ColorChecked()
        {
            return (!PumpD_Color.Checked).ToString();
        }
        public void SetPumpA_Color(string s)
        {
            PumpA_Color.BackColor = System.Drawing.ColorTranslator.FromHtml(s);
        }
        public string GetPumpA_Color()
        {
            return System.Drawing.ColorTranslator.ToHtml(PumpA_Color.BackColor);
        }
        public void SetPumpB_Color(string s)
        {
            PumpB_Color.BackColor = System.Drawing.ColorTranslator.FromHtml(s);
        }
        public string GetPumpB_Color()
        {
            return System.Drawing.ColorTranslator.ToHtml(PumpB_Color.BackColor);
        }
        public void SetPumpC_Color(string s)
        {
            PumpC_Color.BackColor = System.Drawing.ColorTranslator.FromHtml(s);
        }
        public string GetPumpC_Color()
        {
            return System.Drawing.ColorTranslator.ToHtml(PumpC_Color.BackColor);
        }
        public void SetPumpD_Color(string s)
        {
            PumpD_Color.BackColor = System.Drawing.ColorTranslator.FromHtml(s);
        }
        public string GetPumpD_Color()
        {
            return System.Drawing.ColorTranslator.ToHtml(PumpD_Color.BackColor);
        }
        public void SetGradProgram(XmlNodeList list)
        {
            StringToGridColumn(list);
        }
        public string GetGradTime()
        {
            return GridColumnToString(0);
        }
        public string GetGradFlow()
        {
            return GridColumnToString(1);
        }
        public string GetGradPumpA()
        {
            return GridColumnToString(2);
        }
        public string GetGradPumpB()
        {
            return GridColumnToString(3);
        }
        public string GetGradPumpC()
        {
            return GridColumnToString(4);
        }
        public string GetGradPumpD()
        {
            return GridColumnToString(5);
        }

        public void ClearPumpMethodFocus()
        {
            PumpMethod_Browser.SelectedIndex = -1;
        }
        /***********************************************/

    }
}
