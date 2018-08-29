using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.IO.Compression;


namespace EasyInstallerGenerator
{
    public partial class Form1 : Form
    {
        public String IconPath;
        public String OutputPath;
        public String folderPath;
        public String SourcePath;
        public String EXEPath;
        public String Licenses;
        public String ShortcutIconPath;
        public String UseAdmin;
        public String NSISScript;
        public String MainInstallDir;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(File.Exists(Application.StartupPath + "\\" + "logo.png"))
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\" + "logo.png");
            }
            if(File.Exists(Application.StartupPath +"\\" + "makensis.exe"))
            {
                
            }
            else
            {
                MessageBox.Show("NSIS needs to be downloaded. Click on Download NSIS button.");
                
            }
            UseAdmin = "admin";
            checkedListBox1.SetItemCheckState(0, CheckState.Checked);
            checkedListBox1.SetItemCheckState(1, CheckState.Checked);
            checkedListBox1.SetItemCheckState(2, CheckState.Checked);
            checkedListBox1.SetItemCheckState(3, CheckState.Checked);

        }
        public void NSISProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
           // MessageBox.Show("Currently, " + e.ProgressPercentage);
            progressBar1.Value = e.ProgressPercentage;

        }
        public void NSISDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if(File.Exists(Application.StartupPath +"\\" + "nsis.zip"))
            {
                ZipFile.ExtractToDirectory(Application.StartupPath + "\\" + "nsis.zip", Application.StartupPath);
                if(File.Exists(Application.StartupPath + "\\" + "makensis.exe"))
                {
                    MessageBox.Show("Successfully Downloaded");
                    progressBar1.Value = 0;
                }
                else
                {
                    MessageBox.Show("Error in downloading, please download the file http://lightspeedmedia.ddns.net:500/installgen/nsis.zip and extract it to your install dir (of this program)");
                    progressBar1.Value = 0;
                }
            }
        }
        
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int SelectedIndex = checkedListBox1.SelectedIndex;
            //if (SelectedIndex != -1)
            //{
                //MessageBox.Show("Value of " + checkedListBox1.SelectedIndex.ToString() + " is " + checkedListBox1.GetItemCheckState(SelectedIndex).ToString());
           //     System.Windows.Forms.CheckState IsChecked = checkedListBox1.GetItemCheckState(SelectedIndex);
             //   if (IsChecked==CheckState.Checked)
      //          {
        //            checkedListBox1.SetItemCheckState(SelectedIndex,CheckState.Unchecked);
          //      }
      //          else
        //        {
          //          checkedListBox1.SetItemCheckState(SelectedIndex, CheckState.Checked);
            //    }
               // MessageBox.Show(checkedListBox1.SelectedItem.ToString());
          //  }
        }
        

        private void checkedListBox1_ValueChanged(object sender, ItemCheckEventArgs e)
        {
            //MessageBox.Show("Value changed");
            int SelectedIndex = checkedListBox1.SelectedIndex;
            //MessageBox.Show(SelectedIndex.ToString());
            if (SelectedIndex == 1)
            {
                if (checkedListBox1.GetItemCheckState(1)==CheckState.Checked)
                {
                    button4.Enabled = false;
                }
                else
                {
                    button4.Enabled = true;
                }
            }
            if (SelectedIndex == 3)
            {
                if (checkedListBox1.GetItemCheckState(3) == CheckState.Checked)
                {
                    button5.Enabled = false;
                    button6.Enabled = false;
                }
                else
                {
                    button5.Enabled = true;
                    button6.Enabled = true;
                }
            }
            if (SelectedIndex == 0)
            {
                if (checkedListBox1.GetItemCheckState(0) == CheckState.Checked)
                {
                    button7.Enabled = false;
                    dataGridView1.Rows.Clear();
                    Licenses = "";
                }
                else
                {
                    button7.Enabled = true;
                }
            }
            if (SelectedIndex == 2)
            {
                if (checkedListBox1.GetItemCheckState(2) == CheckState.Checked)
                {
                    UseAdmin = "user";
                    MainInstallDir = "$DOCUMENTS\\";
                }
                else
                {
                    UseAdmin = "admin";
                    MainInstallDir = "$PROGRAMFILES\\";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Icon Files (*.ico)|*.ico|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IconPath = openFileDialog1.FileName;
                label4.Text = openFileDialog1.SafeFileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "EXE Files (*.exe)|*.exe|All Files (*.*)|*.*";
            saveFileDialog1.DefaultExt = ".exe";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OutputPath = saveFileDialog1.FileName;
                String[] output1;
                Char Splitter ='\\';
                output1 = saveFileDialog1.FileName.Split(Splitter);
                for(int count = 0; count < output1.Length + 1; count++)
                {
                    if (count == output1.Length)
                    {
                        label5.Text = output1[count-1];
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                label7.Text = folderPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                SourcePath = folderBrowserDialog1.SelectedPath;
                label8.Text = SourcePath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "EXE Files (*.exe)|*.exe|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                EXEPath = openFileDialog1.FileName;
                label9.Text = openFileDialog1.SafeFileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Icon Files (*.ico)|*.ico|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ShortcutIconPath = openFileDialog1.FileName;
                label10.Text = openFileDialog1.SafeFileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Licenses = Licenses +"SPLITT"+ openFileDialog1.FileName;
                dataGridView1.Rows.Add(openFileDialog1.SafeFileName);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            NSISScript = "!include \"MUI.nsh\"\n" + "!define MUI_ABORTWARNING\n";
            String[] output1;
            output1 = Regex.Split(Licenses, "SPLITT");
            foreach(String licenses in output1)
            {
                if (String.IsNullOrEmpty(licenses))
                {

                }
                else
                {
                    NSISScript = NSISScript + "!insertmacro MUI_PAGE_LICENSE \"" + licenses + "\"\n";
                }
            }
            NSISScript = NSISScript + "!insertmacro MUI_PAGE_COMPONENTS\n!define MUI_TEXT_LICENSE_TITLE \"License Agreement\"\n!insertmacro MUI_LANGUAGE \"English\"\n";
            NSISScript = NSISScript + "Name \"" + textBox1.Text + "\"\nCaption \"" + textBox2.Text + "\"\nIcon \"" + IconPath + "\"\nOutFile \"" + OutputPath + "\"\n";
            NSISScript = NSISScript + "SetDateSave on\nSetDatablockOptimize on\nCRCCheck on\nSilentInstall normal\n";
            NSISScript = NSISScript + "InstallDir \"" +MainInstallDir+ textBox3.Text + "\"\n" + "RequestExecutionLevel " + UseAdmin + "\nManifestSupportedOS all\n";
            NSISScript = NSISScript + "Page directory\nPage instfiles\nUninstPage uninstConfirm\nUninstPage instfiles\nAutoCloseWindow false\nShowInstDetails show\n";
            NSISScript = NSISScript + "Section \"\"\nSetOutPath $INSTDIR\nFile /nonfatal /a /r \"" + folderPath + "\\\"\n" + "SectionEnd\n";
            if (checkedListBox1.GetItemCheckState(1) == CheckState.Checked)
            {
                NSISScript = NSISScript + "Section \"Install Source Code\"\nSetOutPath \"$INSTDIR\\Source\"\n" + "File /nonfatal /a /r \"" + SourcePath + "\\\"\nSectionEnd\n";
            }
            if (checkedListBox1.GetItemCheckState(3) == CheckState.Checked)
            {
                if (EXEPath.Contains(folderPath))
                {
                    String[] output2;
                    String[] output22;
                    String[] output23;
                    String[] output24;
                    String temp1;
                    String temp2;
                    String temp3;
                    temp1 = "";
                    temp2 = "";
                    temp3 = "";
                    Char Splitter = '\\';
                    output22 = folderPath.Split(Splitter);
                    for(int count=0; count<output22.Length+1; count++)
                    {
                        if (count == output22.Length)
                        {
                            temp1 = output22[count - 1];
                        }
                    }
                    //        output23 = folderPath.Split(Splitter);
                    //      foreach(String tempstring in output23)
                    //    {
                    //      temp2 = temp2 + "\\\\" + tempstring;
                    //   }
                    output23 = Regex.Split(EXEPath, temp1);
                    for(int count = 0; count < output23.Length+1; count++)
                    {
                        if (count == output23.Length)
                        {
                            temp2 = output23[count - 1];
                        }
                    }
                    output24 = Regex.Split(ShortcutIconPath, temp1);
                    for (int count = 0; count < output24.Length + 1; count++)
                    {
                        if (count == output24.Length)
                        {
                            temp3 = output24[count - 1];
                        }
                    }
                    //       temp1.TrimStart(Splitter);
                    //     temp2.TrimStart(Splitter);
     //               output2 = Regex.Split(temp1, temp2);
       //             output24 = ShortcutIconPath.Split(Splitter);
         //           foreach(String tempstring in output24)
           //         {
             //           temp3 = temp3 + "\\\\" + tempstring;
               //     }
                 //   temp3.TrimStart(Splitter);
                
                    if (ShortcutIconPath.Contains(folderPath))
                  {
                    //    String[] output3;
                    //  output3 = Regex.Split(temp3, temp2);
                        NSISScript = NSISScript + "Section \"Create Shortcut on Desktop\"\nCreateShortCut \"$DESKTOP\\" + textBox1.Text + ".lnk\" \"$INSTDIR\\" + temp2 + "\" \"\" \"$INSTDIR\\" + temp3 + "\" 0 SW_SHOWNORMAL ALT|CONTROL|SHIFT|F4 \"" + textBox1.Text + "\"\nSectionEnd";

                    }
                }
            }
            //MessageBox.Show(NSISScript);
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                StreamWriter ScriptExport;
                ScriptExport = new StreamWriter(saveFileDialog2.FileName);
                ScriptExport.Write(NSISScript);
                ScriptExport.Close();
                MessageBox.Show("Successfilly output to NSIS Script File");
            }
        }

        public void button10_Click(object sender, EventArgs e)
        {
            System.Net.NetworkInformation.Ping DownloadServer = new System.Net.NetworkInformation.Ping();
            if (DownloadServer.Send("89.203.4.93", 300).Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                System.Net.WebClient DownloadNSIS = new System.Net.WebClient();
                Uri DownloadAddress = new Uri("http://89.203.4.93:500/installgen/nsis.zip");
                DownloadNSIS.DownloadProgressChanged += new DownloadProgressChangedEventHandler(NSISProgressChanged);
                DownloadNSIS.DownloadFileCompleted += new AsyncCompletedEventHandler(NSISDownloadCompleted);
                DownloadNSIS.DownloadFileAsync(DownloadAddress, "nsis.zip");

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            NSISScript = "!include \"MUI.nsh\"\n" + "!define MUI_ABORTWARNING\n";
            String[] output1;
            output1 = Regex.Split(Licenses, "SPLITT");
            foreach (String licenses in output1)
            {
                if (String.IsNullOrEmpty(licenses))
                {

                }
                else
                {
                    NSISScript = NSISScript + "!insertmacro MUI_PAGE_LICENSE \"" + licenses + "\"\n";
                }
            }
            NSISScript = NSISScript + "!insertmacro MUI_PAGE_COMPONENTS\n!define MUI_TEXT_LICENSE_TITLE \"License Agreement\"\n!insertmacro MUI_LANGUAGE \"English\"\n";
            NSISScript = NSISScript + "Name \"" + textBox1.Text + "\"\nCaption \"" + textBox2.Text + "\"\nIcon \"" + IconPath + "\"\nOutFile \"" + OutputPath + "\"\n";
            NSISScript = NSISScript + "SetDateSave on\nSetDatablockOptimize on\nCRCCheck on\nSilentInstall normal\n";
            NSISScript = NSISScript + "InstallDir \"" + MainInstallDir + textBox3.Text + "\"\n" + "RequestExecutionLevel " + UseAdmin + "\nManifestSupportedOS all\n";
            NSISScript = NSISScript + "Page directory\nPage instfiles\nUninstPage uninstConfirm\nUninstPage instfiles\nAutoCloseWindow false\nShowInstDetails show\n";
            NSISScript = NSISScript + "Section \"\"\nSetOutPath $INSTDIR\nFile /nonfatal /a /r \"" + folderPath + "\\\"\n" + "SectionEnd\n";
            if (checkedListBox1.GetItemCheckState(1) == CheckState.Checked)
            {
                NSISScript = NSISScript + "Section \"Install Source Code\"\nSetOutPath \"$INSTDIR\\Source\"\n" + "File /nonfatal /a /r \"" + SourcePath + "\\\"\nSectionEnd\n";
            }
            if (checkedListBox1.GetItemCheckState(3) == CheckState.Checked)
            {
                if (EXEPath.Contains(folderPath))
                {
                    String[] output2;
                    String[] output22;
                    String[] output23;
                    String[] output24;
                    String temp1;
                    String temp2;
                    String temp3;
                    temp1 = "";
                    temp2 = "";
                    temp3 = "";
                    Char Splitter = '\\';
                    output22 = folderPath.Split(Splitter);
                    for (int count = 0; count < output22.Length + 1; count++)
                    {
                        if (count == output22.Length)
                        {
                            temp1 = output22[count - 1];
                        }
                    }
                    //        output23 = folderPath.Split(Splitter);
                    //      foreach(String tempstring in output23)
                    //    {
                    //      temp2 = temp2 + "\\\\" + tempstring;
                    //   }
                    output23 = Regex.Split(EXEPath, temp1);
                    for (int count = 0; count < output23.Length + 1; count++)
                    {
                        if (count == output23.Length)
                        {
                            temp2 = output23[count - 1];
                        }
                    }
                    output24 = Regex.Split(ShortcutIconPath, temp1);
                    for (int count = 0; count < output24.Length + 1; count++)
                    {
                        if (count == output24.Length)
                        {
                            temp3 = output24[count - 1];
                        }
                    }
                    //       temp1.TrimStart(Splitter);
                    //     temp2.TrimStart(Splitter);
                    //               output2 = Regex.Split(temp1, temp2);
                    //             output24 = ShortcutIconPath.Split(Splitter);
                    //           foreach(String tempstring in output24)
                    //         {
                    //           temp3 = temp3 + "\\\\" + tempstring;
                    //     }
                    //   temp3.TrimStart(Splitter);

                    if (ShortcutIconPath.Contains(folderPath))
                    {
                        //    String[] output3;
                        //  output3 = Regex.Split(temp3, temp2);
                        NSISScript = NSISScript + "Section \"Create Shortcut on Desktop\"\nCreateShortCut \"$DESKTOP\\" + textBox1.Text + ".lnk\" \"$INSTDIR\\" + temp2 + "\" \"\" \"$INSTDIR\\" + temp3 + "\" 0 SW_SHOWNORMAL ALT|CONTROL|SHIFT|F4 \"" + textBox1.Text + "\"\nSectionEnd";

                    }
                }
            }
            StreamWriter WriteToNSI;
            WriteToNSI = new StreamWriter(Application.StartupPath + "\\" + "temp.nsi");
            WriteToNSI.Write(NSISScript);
            WriteToNSI.Close();
            System.Diagnostics.Process CompileNSI = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo CompileNSIInfo = new System.Diagnostics.ProcessStartInfo(Application.StartupPath +"\\" +"makensis.exe");
            CompileNSIInfo.Arguments = "temp.nsi";
            CompileNSIInfo.CreateNoWindow = true;
            CompileNSIInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            CompileNSI.StartInfo = CompileNSIInfo;
            CompileNSI.Start();
            CompileNSI.WaitForExit();
            CompileNSI.Close();
            MessageBox.Show("Completed");
        }
    }
}
