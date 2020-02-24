using Microsoft.Win32;
using System;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Compression;


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
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
        public string[] server_ports;
        public string[] server_ports_arr;
        public string FileName = "C:\\Windows\\System32\\NETSTAT.EXE";
        public string netstatargs = " -n";
        public string server_port;
        public string[] server_properties;



public Form1()
        {

            string startServer = ConfigurationManager.AppSettings["startServer"].ToString();
            string automaticBackups = ConfigurationManager.AppSettings["automaticBackups"].ToString();
            string date = ConfigurationManager.AppSettings["dateTime"].ToString();
            string[] userEffects = {"speed","slowness","haste","mining_fatigue","strength","instant_health","instant_damage","jump_boost","nausea","regeneration","resistance","fire_resistance","water_breathing",
                                    "invisibility","blindness","night_vision","hunger","weakness","poison","wither","health_boost","absorption","saturation","levitation","fatal_poison","conduit_power","slow_falling",
                                    "bad_omen","village_hero"};
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
            ServerCommandList.Add("");
            ServerCommandList.Add("ban {player}");
            ServerCommandList.Add("kick {player}");
            ServerCommandList.Add("give {player} {item} {count}");
            ServerCommandList.Add("tp {who} { to where }");
            ServerCommandList.Add("effect <player: target> <effect: Effect> [seconds: int] [amplifier: int-255] [true|false]");
            ServerCommandList.Add("summon ravager ~ ~ ~ minecraft:spawn_for_raid_with_pillager_rider");
            ServerCommandList.Add("summon ravager ~ ~ ~ minecraft:spawn_with_vindicator_captain_rider");
            ServerCommandList.Add("time set day");
            ServerCommandList.Add("time set midnight");
            ServerCommandList.Add("time set night");
            ServerCommandList.Add("time set noon");
            ServerCommandList.Add("time set sunrise");
            ServerCommandList.Add("time set sunset");
            ServerCommandList.Add("weather clear");
            ServerCommandList.Add("weather rain");
            ServerCommandList.Add("weather thunder");
            foreach(string effect in userEffects)
            {
                ServerCommandList.Add("effect @a " + effect + " 500 255 true");
            }
            if (startServer == "true")
            {
                startServerCheckbox.Checked = true;
            }
            if (automaticBackups == "true")
            {
                automaticBackupsCheckBox.Checked = true;
                SetUpTimer();
            }
            gameRuleComboBox.DataSource = gameRuleList;
            ComboBox1.DataSource = ServerCommandList;               
            if (startServer == "true")
            {
                startServerButton_Click(null, EventArgs.Empty);
            }
        } 
        private int restartTryLimit = 0;
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
        static string ConvertStringArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append('.');
            }
            return builder.ToString();
        }
        public void AddTextToOutputTextBox(string strText)
        {
            string[] avoidmelist = {"NO LOG FILE!","Version","Session ID","Level Name:","Game mode","Difficulty"};
            string blah = strText.Replace("\r", "");
            try
            {
                if (avoidmelist.Any(strText.Contains))
                {
                    return;
                }
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
                if (blah.Contains("Unable to summon object"))
                {
                    this.txtOutput.AppendText("\nSummon Object Failed. If using ~ ~ ~, please change to finite coords on an X Y Z axis or the username you wish to summon to..\r\n");
                    return;
                }
                if (blah.Contains("Object successfully summoned"))
                {
                    this.txtOutput.AppendText("\nsummon Object was successful.\r\n");
                    return;
                }
                if (strText.Contains("Opped:"))
                {
                    string nameofuser = strText.Replace("Opped: ", "").Replace("\r\n", "");
                    nameofuser = "\"" + nameofuser + "\"";
                    this.txtOutput.AppendText("\r\n Opped " + nameofuser);
                }
                if (strText.Contains("Could not op (already op or higher): "))
                {
                    try
                    {
                        string nameofuser = strText.Replace("Could not op (already op or higher): ", "").Replace("\r\n","");
                        nameofuser = "\""+nameofuser+"\"";
                        mcInputStream.WriteLine("deop "+nameofuser);
                        return;
                    }
                    catch{ }
                }
                if (strText.Contains("De-opped: "))
                {
                    string nameofuser = strText.Replace("De-opped: ", "").Replace("\r\n", "");
                    this.txtOutput.AppendText("\r\nRemoved Op from "+nameofuser);
                    return;
                }
                if (blah.Contains("Unknown command:"))
                {
                    if (strText.Contains("aboutme"))
                    {
                        var aboutme = "\r\n\r\n\r\n     Updated by Prunuspopper@NetGenSys https://github.com/NetGenSys \r\n " +
                                      "Location: RA 0h 42m 44s | Dec +41° 16′ 9″\r\n " +
                                      "Original source by Benjerman https://github.com/Benjerman \r\n " +
                                      "Written in: C# \r\n" +
                                      "Thank you for Choosing this software :)\r\n";
                        txtOutput.Clear();
                        this.txtOutput.AppendText(aboutme);
                        txtOutput.ScrollToCaret();
                        return;
                    }
                    else
                    {
                        txtOutput.ScrollToCaret();
                        MessageBox.Show(blah, "Unknown Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (strText.Contains("No targets matched selector"))
                {
                    txtOutput.AppendText("\r\n No one on by that name, or No one is on.");
                    return;
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
                if (strText.Contains("IPv4") || strText.Contains("IPv6"))
                {
                    string removeCR = strText.Replace("\r\n", "");
                    string[] server_ports = removeCR.Split(',');
                    Array.Sort(server_ports);
                    string server_ports_list = string.Join("\r\n", server_ports);
                    server_ports_list = removeCR.Remove(0, 49).Replace("IPv6 supported,", "").Replace("IPv4 supported,", "");
                    if (isoStore.FileExists("serverPorts.txt") == false)
                    {
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("serverPorts.txt", FileMode.CreateNew, isoStore))
                        {
                            using (StreamWriter writer = new StreamWriter(isoStream))
                            {
                                writer.WriteLine(server_ports_list);
                                writer.Close();
                            }
                        }
                    }
                    if (isoStore.FileExists("serverPorts.txt") == true)
                    {
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("serverPorts.txt", FileMode.Open, isoStore))
                        {
                            using (StreamReader reader = new StreamReader(isoStream))
                            {
                                server_port = reader.ReadToEnd();
                            }
                        }
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("serverPorts.txt", FileMode.Truncate, isoStore))
                        {
                            using (StreamWriter writer = new StreamWriter(isoStream))
                            {
                                writer.WriteLine(server_port +" ,"+ server_ports_list);
                                writer.Close();
                            }
                        }
                    }
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
        private void ConsoleOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                if (this.InvokeRequired)
                    this.Invoke(fpTextBoxCallback, Environment.NewLine + outLine.Data);
                else
                    fpTextBoxCallback(Environment.NewLine + outLine.Data);
            }
        }
        private void ServerInfoOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                if (this.InvokeRequired)
                    this.Invoke(fpTextBoxCallback, Environment.NewLine + outLine.Data);
                else
                    fpTextBoxCallback(Environment.NewLine + outLine.Data);
            }
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
                    Process[] procList = Process.GetProcesses();
                    foreach (Process p in procList)
                    {
                        if (p.ProcessName.ToString() == "bedrock_server")
                        {
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
            if (File.Exists(Directory.GetCurrentDirectory() + "/bedrock_server.exe") == false)
            {
                var confirmResult = MessageBox.Show("bedrock_server not present, would you like to download the Latest Version?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    string version = GetMinecraftServerVersion();
                    string uriName = "https://minecraft.azureedge.net/bin-win/bedrock-server-"+version;
                    uriName = uriName.Replace(" ", "");
                    string urlName = "";
                    for (int i = 0; i <= 9; i++)
                    {
                        urlName = uriName+"."+i+".zip";

                        if (Uri.IsWellFormedUriString(urlName, UriKind.RelativeOrAbsolute))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(urlName))
                                {
                                    UriBuilder uriBuilder = new UriBuilder(urlName);
                                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
                                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                    if (response.StatusCode == HttpStatusCode.NotFound)
                                    {
                                        //txtOutput.Text = "Broken - 404 Not Found";
                                    }
                                    if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        //txtOutput.Text = "URL appears to be good.";
                                        txtOutput.Text = "Downloading from :"+ uriBuilder.Uri;
                                        using (WebClient wc = new WebClient())
                                        {
                                            wc.DownloadFile(uriBuilder.Uri, Directory.GetCurrentDirectory() + "/BedRockServer.zip");
                                        }
                                        Thread.Sleep(1000);
                                        ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + "/BedRockServer.zip", Directory.GetCurrentDirectory());
                                        Thread.Sleep(1000);
                                    }
                                    else //There are a lot of other status codes you could check for...
                                    {
                                       // txtOutput.Text = string.Format("URL might be ok. Status: {0}.",response.StatusCode.ToString());
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                               // txtOutput.Text = string.Format("Broken- Other error: {0}", ex.Message);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Server not found @"+urlName, "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (confirmResult == DialogResult.No)
                {
                    return;
                }

            }
            if (isoStore.FileExists("serverPorts.txt") == true)
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("serverPorts.txt", FileMode.Truncate, isoStore))
                {
                    using (StreamWriter writer = new StreamWriter(isoStream))
                    {
                        writer.WriteLine("");
                        writer.Close();
                    }
                }
            }
            minecraftProcess = new Process();
            minecraftProcess.StartInfo.FileName = "bedrock_server.exe";
            AddTextToOutputTextBox("\n\r  Using this terminal: " + minecraftProcess.StartInfo.FileName);
            minecraftProcess.StartInfo.UseShellExecute = false;
            minecraftProcess.StartInfo.CreateNoWindow = true;
            minecraftProcess.StartInfo.RedirectStandardInput = true;
            minecraftProcess.StartInfo.RedirectStandardOutput = true;
            minecraftProcess.StartInfo.RedirectStandardError = true;
            minecraftProcess.EnableRaisingEvents = false;
            minecraftProcess.Exited += new EventHandler(ProcessExited);
            minecraftProcess.ErrorDataReceived += new DataReceivedEventHandler(ConsoleOutputHandler);
            minecraftProcess.OutputDataReceived += new DataReceivedEventHandler(ConsoleOutputHandler);
            minecraftProcess.Start();
            mcInputStream = minecraftProcess.StandardInput;
            minecraftProcess.BeginOutputReadLine();
            minecraftProcess.BeginErrorReadLine();
            mcInputStream.WriteLine("gamerule");
        }
        private void stopServerFunction(EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("bedrock_server");                   
            try
            {
                if (stopServer == true)
                {
                    gameRulesTxt.Clear();
                     Process[] procList = Process.GetProcesses();
                    foreach (Process p in procList)
                    {
                        if (p.ProcessName.ToString() == "bedrock_server")
                        {
                            p.Kill();
                        }
                    }
                    stopServer = false;
               }else
                {
                     if (stopServer == false && this.minecraftProcess.HasExited)
                      {
                            MessageBox.Show("server stopped, restarting", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            startServerButton_Click("server", e);
                      }
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void OnProcessExit(object sender, EventArgs e){     }
        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInputCommand.Text == "clear")
                {
                    txtOutput.Clear();
                    return;
                }
                else
                {
                    mcInputStream.WriteLine(txtInputCommand.Text);
                }
            }
            catch { }
        }
        private void stopServerButton_Click(object sender, EventArgs e)
        {
            stopServer = true;
            mcInputStream.WriteLine("stop");
            stopServerFunction(e);
        }
        private void ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e) 
        {
            try
            {
                txtInputCommand.Text = ComboBox1.SelectedItem.ToString();
            }
            catch { }
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
                MessageBox.Show(getServerInfo(),"SERVER INFO");
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
                mcInputStream.WriteLine("op " + "\""+opPlayerTextBox1.Text.ToString()+"\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnClose_Click(object sender, EventArgs eventArgs)
        {
            Application.ExitThread();
        }
        public void show_error(string error)
        {
            MessageBox.Show(error);
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
                mcInputStream.WriteLine("list");
                players = players2;
                IPBox1.Clear();
                IPBox1.AppendText(GetLocalIPAddress());
                ServerInfoOutput.Clear();
                ServerInfoOutput.AppendText(ShowActiveTcpConnections());
            }
            catch{}
        }
        private bool IsNumeric(object Expression)
        {
            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out _);
            return isNum;
        }
        public string ShowActiveTcpConnections()
        {
            string[] bedrock_ports = Array.Empty<string>();
            try
            {
                if (isoStore.FileExists("serverPorts.txt") == true)
                {
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("serverPorts.txt", FileMode.Open, isoStore))
                    {
                        using (StreamReader reader = new StreamReader(isoStream))
                        {
                            server_port = reader.ReadToEnd().Replace("\r\n", "");
                            server_port = server_port.Substring(server_port.IndexOf(',') + 1);
                            server_port += " ,80";//added for checking.
                            server_ports_arr = server_port.Split(',');
                        }
                    }
                }
                else
                {
                    ServerInfoOutput.AppendText("SERVER PORTS ARE NOT NUMBERS!!\r\n");
                }
                ServerInfoOutput.AppendText("    running on ports: "+ server_port + "     ");
                ServerInfoOutput.AppendText("\n    These ports are being monitored below");
                ServerInfoOutput.AppendText("\n   PROTO            LOCAL IP                        REMOTE IP                     STATE");
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
                    process.ErrorDataReceived += new DataReceivedEventHandler(ServerInfoOutputHandler);
                    process.OutputDataReceived += new DataReceivedEventHandler(ServerInfoOutputHandler);
                    process.Start();
                    StreamReader reader = process.StandardOutput;
                    string output = reader.ReadToEnd();
                    string[] stringSeparators = new string[] { "TCP" };
                    var words = output.Split(stringSeparators, StringSplitOptions.None);
                    foreach (var word in words)
                    {
                        foreach(string ptz in server_ports_arr)
                        if (word.Contains(":"+ ptz) && word.Contains("ESTABLISHED"))// on bedrock server default ports 
                        {
                            ServerInfoOutput.AppendText("\r   TCP      " + word.Replace("  ", "    ").Replace("\r\n", "").Replace("ESTABLISHED", "ESTAB"));
                        }
                    }
                    process.WaitForExit();
                }               
            }
            catch { }
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
        private void opPlayerTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                opPlayerButton_Click(sender, e);
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
                    File.AppendAllText(@"ServerLog.csv", "\r\n" + DateTime.Now.ToString() + " " + "Telling players the server is pausing in " + time + "\r\n");
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
        public static string GetMinecraftServerVersion()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
            {
                const string Address = "http://97.107.69.83/bedrockserver/";
                string ver = new WebClient().DownloadString(Address);
                if (ver != null && ver != "")
                {
                    return ver;
                }
            }
            return "";
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
        private void playerTxtOutput_TextChanged(object sender, EventArgs e) { }
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
            Application.ExitThread();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void IPBox1_Click(Object sender, EventArgs e)
        {
            string text = IPBox1.Text;
            Clipboard.SetText(text);
        }
        private void IPBox1_TextChanged(object sender, EventArgs e){  }
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
            catch { }
        }
        private void label10_Click(object sender, EventArgs e){}
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
            }
        }
    }
}