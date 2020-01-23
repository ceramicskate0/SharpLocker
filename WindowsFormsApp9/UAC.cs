﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices;
using System.DirectoryServices;
using System.Collections;

namespace WindowsFormsApp9
{
    public partial class UAC : Form
    {
        public UAC()
        {
            InitializeComponent();
            Taskbar.Hide();
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            BackgroundImageLayout = ImageLayout.Stretch;
            this.TopMost = true;

            List<string> lstUsers = new List<string>();
            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
            DirectoryEntry admGroup = localMachine.Children.Find("administrators", "group");
            object members = admGroup.Invoke("members", null);
            foreach (object groupMember in (IEnumerable)members)
            {
                DirectoryEntry member = new DirectoryEntry(groupMember);
                if (member.Name.ToLower().Contains("admin"))
                {
                    lstUsers.Add(member.Name);
                }
            }
            string userName = "";
            if (lstUsers.ElementAt(0).ToLower() == "administrator" && lstUsers.Count == 1)
            {
                userName = lstUsers.ElementAt(0);
            }
            else
            {
                userName = lstUsers.ElementAt(1);
            }
            label2.Text = userName;
            label2.BackColor = System.Drawing.Color.Transparent;
            //int usernameloch = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 64;
            //int usericonh = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 29;
            //int buttonh = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 64;
            //int usernameh = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 50;
            //int locked = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 57;
            //int bottomname = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 95;
            //textBox2.Top = usernameloch;
            //button1.Top = buttonh;
            //label2.Top = usernameh;
            textBox2.UseSystemPasswordChar = true;
            string domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            if (string.IsNullOrEmpty(domain))
            {
                label1_ComputerName.Text =  Environment.MachineName;

            }
            else
            {
                label1_ComputerName.Text = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName + "\\" + Environment.MachineName;

            }
            foreach (var screen in Screen.AllScreens)
            {

                Thread thread = new Thread(() => WorkThreadFunction(screen));
                thread.Start();
            }


        }

        public class Taskbar
        {
            [DllImport("user32.dll")]
            private static extern int FindWindow(string className, string windowText);

            [DllImport("user32.dll")]
            private static extern int ShowWindow(int hwnd, int command);

            [DllImport("user32.dll")]
            public static extern int FindWindowEx(int parentHandle, int childAfter, string className, int windowTitle);

            [DllImport("user32.dll")]
            private static extern int GetDesktopWindow();

            private const int SW_HIDE = 0;
            private const int SW_SHOW = 1;

            protected static int Handle
            {
                get
                {
                    return FindWindow("Shell_TrayWnd", "");
                }
            }

            protected static int HandleOfStartButton
            {
                get
                {
                    int handleOfDesktop = GetDesktopWindow();
                    int handleOfStartButton = FindWindowEx(handleOfDesktop, 0, "button", 0);
                    return handleOfStartButton;
                }
            }

            private Taskbar()
            {
                // hide ctor
            }

            public static void Show()
            {
                ShowWindow(Handle, SW_SHOW);
                ShowWindow(HandleOfStartButton, SW_SHOW);
            }

            public static void Hide()
            {
                ShowWindow(Handle, SW_HIDE);
                ShowWindow(HandleOfStartButton, SW_HIDE);
            }
        }

        public void WorkThreadFunction(Screen screen)
        {
            try
            {
                if (screen.Primary == true)
                {


                }

                if (screen.Primary == false)
                {
                    int mostLeft = screen.WorkingArea.Left;
                    int mostTop = screen.WorkingArea.Top;
                    Debug.WriteLine(mostLeft.ToString(), mostTop.ToString());
                    using (Form form = new Form())
                    {
                        form.WindowState = FormWindowState.Normal;
                        form.StartPosition = FormStartPosition.Manual;
                        form.Location = new Point(mostLeft, mostTop);
                        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        form.Size = new Size(screen.Bounds.Width, screen.Bounds.Height);
                        form.BackColor = Color.Black;
                        form.TransparencyKey = Color.Black;
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                // log errors
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                parms.ExStyle |= 0x02000000;
                return parms;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Taskbar.Show();
            System.Windows.Forms.Application.Exit();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Taskbar.Show();
            base.OnClosing(e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Taskbar.Show();
            System.Windows.Forms.Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Taskbar.Show();
            System.Windows.Forms.Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Taskbar.Show();
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Password Entered DO SOMETHING WITH IT
            Taskbar.Show();
            System.Windows.Forms.Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Taskbar.Show();
            System.Windows.Forms.Application.Exit();
        }
    }


}
