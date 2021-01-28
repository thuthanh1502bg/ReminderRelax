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

namespace ReminderRelax
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDialogFields();
            InitializeSound();
        }

        // Khoi tao hop thoai
        private void InitializeDialogFields()
        {
            dtStart.Value = DateTime.Now;
            btnStop.Enabled = false;
        }



        // Loi nhac nho
        private void InitializeNotifyBalloon()
        {
            notifyIcon1.BalloonTipTitle = "Lời nhắc yêu thương";
            notifyIcon1.BalloonTipText = txtMessage.Text;
        }

        private void SetupTimer()
        {
            // Chuyen doi tu phut sang giay 
            timer1.Interval = (int)udInterval.Value * 1000 * 60; 
            timer1.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Hien thi thong bao tao nhac nho thanh cong
            MessageSuccess();

            // Tao icon nho goc man hinh va setup thoi gian
            InitializeNotifyBalloon();
            SetupTimer();

            // Khoi tao tinh Enable cua cac phan tu khac
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnReset.Enabled = false;
            dtStart.Enabled = false;
            udInterval.Enabled = false;
            txtMessage.Enabled = false;
            txtSound.Enabled = false;

            // An ung dung
            this.ShowInTaskbar = false;
            this.Hide();
        }


        // Khoi tao am thanh
        private void InitializeSound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = txtSound.Text;
        }



        // Khi nhan vao nut am thanh
        private void btnSound_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog() { Filter = "Audio File (*.mp3;*.wav)|*.mp3;*.wav;", Multiselect = false, ValidateNames = true };
            {
                if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtSound.Text = file.FileName;
                }

            }
        }


        // Hien thi thong bao tao nhac nho thanh cong
        private void MessageSuccess()
        {
            string message = "Bạn đã tạo nhắc nhở thành công. Chúc bạn một ngày tốt lành! ";
            string title = "Thông báo";
            MessageBox.Show(message, title);
        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            // Kiem tra thoi gian
            if (CheckIfStartTimeHappened())
            {
                // Hien thi thong bao
                ShowReminder();
            }
        }


        // Kiem tra thoi gian 
        private bool CheckIfStartTimeHappened()
        {
            if (DateTime.Now > dtStart.Value)
                return true;

            return false;
        }



        // Hien thi thong bao
        private void ShowReminder()
        {
            notifyIcon1.ShowBalloonTip(2);
            PlaySound();
        }


        // Nut nghe thu am thanh thong bao
        private void PlaySound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = txtSound.Text;
            player.Play();
        }


        // Cai dat hop thoai khong bi chinh sua khi ung dung chưa bat
        private void notifyIcon1_BalloonTipShown(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                System.Threading.Thread.Sleep(8000);
                notifyIcon1.Visible = false;
                notifyIcon1.Visible = true;
            }
        }

        // Hien thi hop thoai khi click chuot hai lan
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        // Ham xoa thong tin trong o loi nhac nho va am thanh
        void Clear()
        {
            txtMessage.Clear();
            txtSound.Clear();
            InitializeDialogFields();

        }

        // Click nut xoa loi nhac nho
        private void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        // Click nut nghe thu
        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlaySound();
        }

        // Click nut ngung nhac nho
        private void btnStop_Click(object sender, EventArgs e)
        {
            // Dong ho dem thoi gian ngung
            timer1.Stop();

            // Khoi tao lai tinh Enable cua cac phan tu khac
            btnStart.Enabled = true;
            btnReset.Enabled = true;
            btnStop.Enabled = false;
            dtStart.Enabled = false;
            udInterval.Enabled = true;
            txtMessage.Enabled = true;
            txtSound.Enabled = true;
        }


        // Hien thi ung dung
        private void showDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }


        // Thoat ung dung
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
