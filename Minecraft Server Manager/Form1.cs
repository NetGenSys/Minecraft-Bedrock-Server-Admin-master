using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MinecraftBedrockServerAdmin
{
    public partial class Form1 : MetroFramework.Forms.MetroForm

    {
        private Process minecraftProcess;
        private StreamWriter mcInputStream;
        private string players = "Loading Player List.....";
        private string players2 = "";
        private bool stopServer = false;
        private System.Threading.Timer timer;
        public delegate void fpTextBoxCallback_t(string strText);
        public fpTextBoxCallback_t fpTextBoxCallback;
        public string output;
        readonly string[] server_properties = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\server.properties", Encoding.UTF8);
        

         public Form1()
        {
            string startServer = ConfigurationManager.AppSettings["startServer"].ToString();
            string automaticBackups = ConfigurationManager.AppSettings["automaticBackups"].ToString();
            //string automaticRestarts = ConfigurationManager.AppSettings["automaticrestart"].ToString();
            string date = ConfigurationManager.AppSettings["dateTime"].ToString();
            CheckForIllegalCrossThreadCalls = false;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            fpTextBoxCallback = new fpTextBoxCallback_t(AddTextToOutputTextBox);
            InitializeComponent();

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = (5000) * (1);
            timer.Enabled = true;
            timer.Start();

            dateTimePicker1.Text = date;
            List<string> weatherList = new List<string>();
            weatherList.Add("clear");
            weatherList.Add("rain");
            weatherList.Add("thunder");

            List<string> clockList = new List<string>();
            clockList.Add("day");
            clockList.Add("midnight");
            clockList.Add("night");
            clockList.Add("noon");
            clockList.Add("sunrise");
            clockList.Add("sunset");

            List<string> gameRuleList = new List<string>();
            gameRuleList.Add("commandblockoutput");
            gameRuleList.Add("commandblocksenabled");
            gameRuleList.Add("dodaylightcycle");
            gameRuleList.Add("doentitydrops");
            gameRuleList.Add("dofiretick");
            gameRuleList.Add("doimmediaterespawn");
            gameRuleList.Add("doinsomnia");
            gameRuleList.Add("domobloot");
            gameRuleList.Add("domobspawning");
            gameRuleList.Add("dotiledrops");
            gameRuleList.Add("doweathercycle");
            gameRuleList.Add("drowningdamage");
            gameRuleList.Add("experimentalgameplay");
            gameRuleList.Add("falldamage");
            gameRuleList.Add("firedamage");
            gameRuleList.Add("keepinventory");
            gameRuleList.Add("mobgriefing");
            gameRuleList.Add("naturalregeneration");
            gameRuleList.Add("pvp");
            gameRuleList.Add("sendcommandfeedback");
            gameRuleList.Add("showcoordinates");
            gameRuleList.Add("showdeathmessages");
            gameRuleList.Add("tntexplodes");

            List<string> ServerCommandList = new List<string>();
            ServerCommandList.Add("ban");
            ServerCommandList.Add("kick");
            ServerCommandList.Add("give");
            ServerCommandList.Add("tp");

            if (startServer == "true")
            {
                startServerCheckbox.Checked = true;
            }

            if (automaticBackups == "true")
            {
                automaticBackupsCheckBox.Checked = true;
                SetUpTimer();
            }
            weatherComboBox.DataSource = weatherList;
            clockComboBox.DataSource = clockList;
            gameRuleComboBox.DataSource = gameRuleList;
            if (startServer == "true")
            {
                startServerButton_Click(null, EventArgs.Empty);
            }
        } 
        private int restartTryLimit = 0;
        public string FileName = "C:\\Windows\\System32\\NETSTAT.EXE";
        public string netstatargs = " -n";                

        private void SetUpTimer()
        {
            TimeSpan alertTime = dateTimePicker1.Value.TimeOfDay;
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = alertTime - current.TimeOfDay;
            if (timeToGo < TimeSpan.Zero)
            {
                return; 
            }
            this.timer = new System.Threading.Timer(x =>
            {
                backgroundWorker1.RunWorkerAsync();
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        public void AddTextToOutputTextBox(string strText)
        {
            string blah = strText.Replace("\r", "");
            try
            {
                if (blah.Contains("CrashReporter") && restartTryLimit < 3)
                {
                    Thread.Sleep(5000);
                    startServerButton_Click(null, EventArgs.Empty);
                    restartTryLimit++;
                    return;
                }
                if (blah.Contains("Server Started."))
                {
                    restartTryLimit = 0;
                }                
                if (blah.Contains("Unknown command:"))
                {
                    if (strText.Contains("aboutme"))
                    {
                        var aboutme = "\r\n\r\n\r\n     Modified and Updated by AJG https://github.com/NetGenSys \r\n " +
                                      "Location: RA 0h 42m 44s | Dec +41° 16′ 9″\r\n " +
                                      "Created by Benjerman https://github.com/Benjerman \r\n " +
                                      "Written in: C# \r\n" +
                                      "Thank you for Choosing this software :)";
                        txtOutput.Clear();
                        this.txtOutput.AppendText(aboutme);
                        txtOutput.ScrollToCaret();
                        return;
                    }
                    else
                    {
                        this.txtOutput.AppendText(blah);
                        txtOutput.ScrollToCaret();
                        MessageBox.Show(blah, "Unknown Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (blah.Contains("Network port occupied, can't start server."))
                {
                    this.txtOutput.AppendText(strText);
                    txtOutput.ScrollToCaret();
                    File.AppendAllText(@"ServerLog.csv", strText + " ::: Line : 154\r\n");
                    return;
                }
                if (blah.Contains("commandblock") && blah.Contains("="))
                {
                    string removeComma = blah.Replace(", ", "\r\n");
                    gameRulesTxt.Text = removeComma;
                    return;
                }
                if (strText.Contains("players online"))
                {
                    strText = strText.Replace("\r\n", "");
                }
                if (blah.Contains(", xuid") || blah.Contains(", port"))
                {
                    this.txtOutput.AppendText(strText);
                    txtOutput.ScrollToCaret();
                    File.AppendAllText(@"ServerLog.csv", strText + " ::: Line : 171\r\n");
                }
                else if (strText.Contains("players online"))
                {
                    players = players + strText + "\r\n";
                }
                else if (!blah.Contains("Unknown command:") && !strText.Contains("players online") && blah.Length > 35 || strText.Contains("Quit") 
                            || strText.Contains("Set") || strText.Contains("Changing") || strText.Contains("Saving"))
                {
                    this.txtOutput.AppendText(strText);
                    txtOutput.ScrollToCaret(); 
                    File.AppendAllText(@"ServerLog.csv", strText + " ::: Line : 181\r\n");
                }
                else
                {
                        string removeCR = strText.Replace("\r\n", "");
                        string[] names = removeCR.Split(',');
                        Array.Sort(names);
                        string result = string.Join("\r\n", names);
                        players += result;
                        File.AppendAllText(@"ServerLog.csv", result + " ::: Line : 192\r\n");                
                }
            }
            catch (Exception) { }
        }
        private void ConsoleOutputHandler(object sendingProcess, System.Diagnostics.DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                if (this.InvokeRequired)
                    this.Invoke(fpTextBoxCallback, Environment.NewLine + outLine.Data);
                else
                    fpTextBoxCallback(Environment.NewLine + outLine.Data);
            }
        }
        private void ServerInfoOutputHandler(object sendingProcess, System.Diagnostics.DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                if (this.InvokeRequired)
                    this.Invoke(fpTextBoxCallback, Environment.NewLine + outLine.Data);
                else
                    fpTextBoxCallback(Environment.NewLine + outLine.Data);
            }
        }
        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.minecraftProcess.HasExited)
                {
                    txtOutput.AppendText("\r\n\r\nThe server has been shutdown.\r\n");
                    File.AppendAllText(@"ServerLog.csv", "\r\n" + DateTime.Now.ToString() + " " + "The server has been shutdown.\r\n");
                    return;
                }
                mcInputStream.WriteLine(txtInputCommand.Text);
            }
            catch { }
        }

        public void ProcessExited(object sender, EventArgs e)
        {
            txtOutput.AppendText("\r\n\r\nThe server has been shutdown.\r\n");
            File.AppendAllText(@"ServerLog.csv", "\r\n" + DateTime.Now.ToString() + " " + "The server has been shutdown.\r\n");
        }

        private void backupButton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void startServerButton_Click(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("bedrock_server");
            if (pname.Length > 0)
            {
                var confirmResult = MessageBox.Show("bedrock_server already running, would you like to restart it??", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    string a = "";
                    Process[] procList = Process.GetProcesses();
                    foreach (Process p in procList)
                    {
                        if (p.ProcessName.ToString() == "bedrock_server")
                        {
                            a += "Process ID=" + p.Id + "\t" + "Process Name=" +
                            p.ProcessName + "\n";
                            p.Kill();
                        }
                    }
                    Thread.Sleep(1000);
                    StartServerFunction(e);
                    
                }
                else if (confirmResult == DialogResult.No)
                {
                    return;
                }
            }
            else if(pname.Length == 0){
                StartServerFunction(e);
            }

        }
        private void StartServerFunction(EventArgs e)
        {
            if (!File.Exists(System.IO.Directory.GetCurrentDirectory() + "/bedrock_server.exe"))
            {
                string Serverpath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "bedrock_server.exe");
                File.WriteAllBytes(Serverpath, MinecraftBedrockServerAdmin.Properties.Resources.bedrock_server);
                string Propertiespath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "server.properties");
                File.WriteAllBytes(Propertiespath, MinecraftBedrockServerAdmin.Properties.Resources.server);
                string Permissionspath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "permissions.json");
                File.WriteAllBytes(Permissionspath, MinecraftBedrockServerAdmin.Properties.Resources.permissions);
                string Whitelistpath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "whitelist.json");
                File.WriteAllBytes(Whitelistpath, MinecraftBedrockServerAdmin.Properties.Resources.whitelist);
            }
            minecraftProcess = new System.Diagnostics.Process();
            minecraftProcess.StartInfo.FileName = "bedrock_server.exe";
            AddTextToOutputTextBox("Using this terminal: " + minecraftProcess.StartInfo.FileName);
            minecraftProcess.StartInfo.UseShellExecute = false;
            minecraftProcess.StartInfo.CreateNoWindow = true;
            minecraftProcess.StartInfo.RedirectStandardInput = true;
            minecraftProcess.StartInfo.RedirectStandardOutput = true;
            minecraftProcess.StartInfo.RedirectStandardError = true;
            minecraftProcess.EnableRaisingEvents = false;
            minecraftProcess.Exited += new EventHandler(ProcessExited);
            minecraftProcess.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(ConsoleOutputHandler);
            minecraftProcess.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(ConsoleOutputHandler);
            minecraftProcess.Start();
            mcInputStream = minecraftProcess.StandardInput;
            minecraftProcess.BeginOutputReadLine();
            minecraftProcess.BeginErrorReadLine();
            mcInputStream.WriteLine("gamerule");

        }
        private void stopServerFunction(EventArgs e)
        {
               
            try
            {
                if (stopServer)
                {
                    
                    Thread.Sleep(500);
                    //File.Delete(System.IO.Directory.GetCurrentDirectory() + "/bedrock_server.exe");
                    //File.Delete(System.IO.Directory.GetCurrentDirectory() + "/server.properties");
                    //File.Delete(System.IO.Directory.GetCurrentDirectory() + "/permissions.json");
                    //File.Delete(System.IO.Directory.GetCurrentDirectory() + "/whitelist.json");
                    //playerTxtOutput.Clear();
                    gameRulesTxt.Clear();
                    btnExecute_Click("server",e);
                   stopServer = false;
                }
                else
                {
                    if (!stopServer && this.minecraftProcess.HasExited)
                    {
                        MessageBox.Show("server stopped, restarting","ERROR", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        startServerButton_Click("server",e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void stopServerButton_Click(object sender, EventArgs e)
        {
               
            stopServer = true;
            mcInputStream.WriteLine("stop");
            stopServerFunction(e);
            
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
        }

        private void weatherComboBox_SelectedIndexChanged(object sender, EventArgs e) { }

        private void setWeatherButton_Click(object sender, EventArgs e)
        {
            try
            {
                mcInputStream.WriteLine("weather " + weatherComboBox.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clockComboBox_SelectedIndexChanged(object sender, EventArgs e) { }

        private void setclockButton_Click(object sender, EventArgs e)
        {
            try
            {
                mcInputStream.WriteLine("time set " + clockComboBox.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TCPIPButton_Click(object sender, EventArgs e)
        {
            try
            {
                ServerInfoOutput.Clear();
                AddTextToOutputTextBox(ShowActiveTcpConnections() + "\r\n");
            }
            catch { }
        }

        private void ServerInfoButton_Click(object sender, EventArgs e)
        {
            try
            {
                ServerInfoOutput.Clear();
                AddTextToOutputTextBox(getServerInfo() + "\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void opPlayerButton_Click(object sender, EventArgs e)
        {
            try
            {
                mcInputStream.WriteLine("op " + opPlayerTextBox1.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnClose_Click(object sender, EventArgs eventArgs)
        {
            System.Windows.Forms.Application.ExitThread();
        }
        private void deOpPlayerButton_Click(object sender, EventArgs e)
        {
            try
            {
                mcInputStream.WriteLine("deop " + deOpTextBox1.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string trueFalse = " false";
            if (trueGRRadioButton.Checked)
                trueFalse = " true";
            if (falseGRRadioButton2.Checked)
                trueFalse = " false";
            try
            {
                mcInputStream.WriteLine("gamerule " + gameRuleComboBox.SelectedItem + trueFalse);
                mcInputStream.WriteLine("gamerule ");
            }
            catch { }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {

                if (players != players2)
                {
                    playerTxtOutput.Clear();
                    playerTxtOutput.Text = players;
                }
                //playerTxtOutput.Clear();
                mcInputStream.WriteLine("list");
                players = players2;
                IPBox1.Clear();
                IPBox1.AppendText(GetLocalIPAddress());
                ServerInfoOutput.Clear();
                ServerInfoOutput.AppendText(ShowActiveTcpConnections());

            }
            catch
            {

            }
        }
        private bool IsNumeric(object Expression)
        {
            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out _);
            return isNum;
        }
        private string ShowActiveTcpConnections()
        {
            string[] bedrock_ports = Array.Empty<string>();
            try
            {
                //dirty hack job to find the pos of the server-port vars in server.properties
                //Requires update.
                if (IsNumeric(server_properties[7].Replace("server-port=", "")) == true) //pos with # and \n removed
                {
                    bedrock_ports = new string[]{ server_properties[7].Replace("server-port=", ""), server_properties[8].Replace("server-portv6=", "") };//isolate the port #
                }
                else if (IsNumeric(server_properties[30].Replace("server-port=", "")) == true)//default pos
                {
                    bedrock_ports = new string[]{ server_properties[30].Replace("server-port=", ""), server_properties[34].Replace("server-portv6=", "") };//isolate the port #
                }
                else
                {
                    ServerInfoOutput.AppendText("SERVER PORTS ARE NOT NUMBERS!!\r\n");//no port number
                }
                /*
                int ii = 0;
                foreach(string ser_prop_lines in server_properties)
                {
                    if (ser_prop_lines.Contains("server-port=") || ser_prop_lines.Contains("server-portv6="))
                    {
                        bedrock_ports[ii] = ser_prop_lines;
                        ii++;
                    }
                    ServerInfoOutput.AppendText(ser_prop_lines);
                }*/
                //ServerInfoOutput.AppendText("ports are on line " + bedrock_ports[0] + " and " + bedrock_ports[1] + "; look for : " + bedrock_ports[0] +"\n");
                ServerInfoOutput.AppendText("\n         This server is running on ports:"+ bedrock_ports[0] + "," + bedrock_ports[1] + "       ");
                ServerInfoOutput.AppendText("\nPROTO           LOCAL IP             REMOTE IP          STATE ");
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = FileName;
                    process.StartInfo.Arguments = netstatargs;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.EnableRaisingEvents = false;
                    process.Exited += new EventHandler(ProcessExited);
                    process.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(ServerInfoOutputHandler);
                    process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(ServerInfoOutputHandler);
                    process.Start();
                    StreamReader reader = process.StandardOutput;
                    string output = reader.ReadToEnd();
                    string[] stringSeparators = new string[] { "TCP" };
                    var words = output.Split(stringSeparators, StringSplitOptions.None);
                    foreach (var word in words)
                    {
                        word.Trim(' ', ' ');
                        if (word.Contains("ESTABLISHED")) // is a connection established ?
                        {
                            if (word.Contains(":"+ bedrock_ports[0]) || word.Contains(":"+ bedrock_ports[1]))// on bedrock server default ports 
                            {
                                ServerInfoOutput.AppendText("\rTCP  " + word.Replace("   ", "  ").Replace("\r\n", "").Replace("ESTABLISHED", "ESTA"));
                            }
                        }
                    }
                    process.WaitForExit();
                }               
            }
            catch
            {
            }
           return output;
        }

        private string getServerInfo()
        {
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree); 
            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    ServerInfoOutput.AppendText(Convert.ToString(processor_name.GetValue("ProcessorNameString")) + "\r\n"); 
                }
            }
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    ServerInfoOutput.AppendText("Operating System : " + managementObject["Caption"].ToString() + "\r\n"); 
                }
                if (managementObject["OSArchitecture"] != null)
                {
                    ServerInfoOutput.AppendText("OS Architecture : " + managementObject["OSArchitecture"].ToString() + "\r\n"); 
                }
                if (managementObject["CSDVersion"] != null)
                {
                    ServerInfoOutput.AppendText("Service Pack  : " + managementObject["CSDVersion"].ToString() + "\r\n"); 
                }
                else if (managementObject["Version"] != null)
                {
                    ServerInfoOutput.AppendText("Service Pack  : " + managementObject["Version"].ToString() + "\r\n"); 
                }
                if (managementObject["TotalVisibleMemorySize"] != null)
                {
                    ServerInfoOutput.AppendText("Total RAM   :  " + managementObject["TotalVisibleMemorySize"].ToString() + "\r\n"); 
                }
                if (managementObject["FreePhysicalMemory"] != null)
                {
                    ServerInfoOutput.AppendText("Free RAM   :  " + managementObject["FreePhysicalMemory"].ToString() + "\r\n"); 
                }
            }
            return "";
        }
        private void txtInputCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnExecute_Click(sender, e);
        }

        private void weatherComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                setWeatherButton_Click(sender, e);
        }

        private void clockComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                setclockButton_Click(sender, e);
        }

        private void opPlayerTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                opPlayerButton_Click(sender, e);
        }

        private void deOpTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                deOpPlayerButton_Click(sender, e);
        }

        private void gameRuleComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        private void trueGRRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        private void falseGRRadioButton2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }
        private void Button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool backedup = false;
            int[] sleepytimes = new int[] { 240000, 60000, 30000, 10000 };
            while (!backedup)
            {
                foreach (int sleeptime in sleepytimes)
                {
                    int Minutes = sleeptime / 60 / 1000;
                    int seconds = sleeptime / 1000;
                    var time = "";
                    if(Minutes > 0) { time = Minutes.ToString() + " minutes"; }
                    else if(Minutes <= 0) { time = seconds.ToString() + " seconds"; }
                    mcInputStream.WriteLine("say THE SERVER WILL BE PAUSING FOR A BACKUP IN " + time);
                    txtOutput.AppendText("\r\n\r\nTelling players the server is pausing in " + time + "\r\n");
                    File.AppendAllText(@"ServerLog.csv", "\r\n" + DateTime.Now.ToString() + " " + "Telling players the server is going down in " + time + "\r\n");
                    txtOutput.ScrollToCaret();
                    Thread.Sleep(sleeptime);
                }
                mcInputStream.WriteLine("say SERVER IS PAUSED, PLEASE WAIT ROUGHLY 12 SECONDS TO SAVE.");
                txtOutput.AppendText("\r\nPausing Server for world backup\r\n");
                File.AppendAllText(@"ServerLog.csv", DateTime.Now.ToString() + " " + "Pausing Server for world backup\r\n");
                mcInputStream.WriteLine("save hold");
                txtOutput.ScrollToCaret();
                Thread.Sleep(9000);
                mcInputStream.WriteLine("save query");
                txtOutput.ScrollToCaret();
                Thread.Sleep(1000);
                mcInputStream.WriteLine("save resume");
                mcInputStream.WriteLine("say THE SERVER HAS COMPLETED THE BACKUP");
                txtOutput.AppendText("\r\n\r\nTelling players the server has completed being backed up\r\n");
                txtOutput.ScrollToCaret();
                backedup = true;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.Save(ConfigurationSaveMode.Modified);
            if (startServerCheckbox.Checked)
            {
                configuration.AppSettings.Settings["startServer"].Value = "true";
                configuration.Save(ConfigurationSaveMode.Modified);
            }
            if (!startServerCheckbox.Checked)
            {
                configuration.AppSettings.Settings["startServer"].Value = "false";
                configuration.Save(ConfigurationSaveMode.Modified);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            SetUpTimer();
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.Save(ConfigurationSaveMode.Modified);
            configuration.AppSettings.Settings["dateTime"].Value = dateTimePicker1.Value.TimeOfDay.ToString();
            configuration.Save(ConfigurationSaveMode.Modified);
        }

        private void automaticBackupsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.Save(ConfigurationSaveMode.Modified);
            if (automaticBackupsCheckBox.Checked)
            {
                configuration.AppSettings.Settings["automaticBackups"].Value = "true";
                configuration.Save(ConfigurationSaveMode.Modified);
            }
            if (!automaticBackupsCheckBox.Checked)
            {
                configuration.AppSettings.Settings["automaticBackups"].Value = "false";
                configuration.Save(ConfigurationSaveMode.Modified);
            }
        }
        public static string GetLocalIPAddress()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
            {
                const string Address = "http://icanhazip.com";
                string ip = new WebClient().DownloadString(Address);

                if (ip != null && ip != "")
                {
                    return ip;
                }
            }
            else
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            return "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            IPBox1.AppendText(GetLocalIPAddress());

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void playerTxtOutput_TextChanged(object sender, EventArgs e) 
        {
            
        }
        private void label7_Click(object sender, EventArgs e) { }
        private void deOpTextBox1_TextChanged(object sender, EventArgs e) { }
        private void falseGRRadioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void txtInputCommand_TextChanged(object sender, EventArgs e) { }
        private void TxtOutput_TextChanged(object sender, EventArgs e) { }
        private void ServerInfoOutput_TextChanged(object sender, EventArgs e) { }
        private void trueGRRadioButton_CheckedChanged(object sender, EventArgs e) { }
        private void gameRuleComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void opPlayerTextBox1_TextChanged(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
      
        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void IPBox1_Click(Object sender, EventArgs e)
        {
            string text = IPBox1.Text;
            Clipboard.SetText(text);
        }
        private void IPBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void UWP_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (Process p = new Process())
                {

                    ProcessStartInfo ps = new ProcessStartInfo();
                    ps.Arguments = "LoopbackExempt –a –p=S-1-15-2-1958404141-86561845-1752920682-3514627264-368642714-62675701-733520436";
                    ps.FileName = "CheckNetIsolation.exe";
                    ps.UseShellExecute = false;
                    ps.WindowStyle = ProcessWindowStyle.Hidden;
                    ps.RedirectStandardInput = true;
                    ps.RedirectStandardOutput = true;
                    ps.RedirectStandardError = true;
                    p.StartInfo = ps;
                    p.Start();
                    StreamReader stdOutput = p.StandardOutput;
                    StreamReader stdError = p.StandardError;
                }
            }
            catch
            {

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            string browser = string.Empty;
            RegistryKey key = null;
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command");
                if (key != null)
                {
                    browser = key.GetValue(null).ToString().ToLower().Trim(new[] { '"' });
                }
                if (!browser.EndsWith("exe"))
                {
                    browser = browser.Substring(0, browser.LastIndexOf(".exe", StringComparison.InvariantCultureIgnoreCase) + 4);
                }
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                }
            }
            Process proc = Process.Start(browser, "https://discord.gg/UksvsU");
            if (proc != null)
            {
                Thread.Sleep(5000);
                // Close the browser.
                //proc.Kill();
            }
        }
    }
}