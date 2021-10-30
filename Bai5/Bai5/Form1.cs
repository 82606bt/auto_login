using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Bai5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
    
        ChromeDriver chr; Thread th; 

        private void TxtBoxChanged(object sender, EventArgs e)
        {
            if(Username_textBox.Text != "" && Password_TextBox.Text != "")
            {
                Login_BTN.ForeColor = Color.Lime;
                Login_BTN.Cursor = Cursors.Hand;
            }
            else
            {
                Login_BTN.ForeColor = Color.Red;
                Login_BTN.Cursor = Cursors.No;
            }
        }

        private void Login_BTN_Click(object sender, EventArgs e)
        {
            if(Login_BTN.Cursor == Cursors.Hand)
            {
                th = new Thread(Result); th.Start();

            }
        }
        private void Result()
        {
            Login_BTN.ForeColor = Color.Gold;
            Login_BTN.UseWaitCursor = true;
            Login_BTN.Text = "Testing....";
            OpenSelenium();
            Thread.Sleep(3000);
            Login(Username_textBox.Text, Password_TextBox.Text);
            if (TestAccount())
                MessageBox.Show("Đăng nhập thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Tài khoản hoặc mật khẩu sai", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            CloseSelenium();
            Login_BTN.ForeColor = Color.Lime;
            Login_BTN.UseWaitCursor = false;
            Login_BTN.Text = "Đăng nhập";

        }

        private void Login(string username, string password)
        {
            try
            {
                chr.FindElements(By.XPath("//input[@class='field text largue requi']"))[0].SendKeys(username); Thread.Sleep(3000);
                chr.FindElements(By.XPath("//input[@class='field text largue requi']"))[1].SendKeys(password); Thread.Sleep(3000);
                chr.FindElement(By.Id("_ctl18_butLogin")).Click(); Thread.Sleep(1000);
            }
            catch (Exception exc)

            {
                MessageBox.Show(exc.Message);
            }

            
        }
        private bool TestAccount()
        {
            if (chr.Url == "https://sv.dntu.edu.vn/home/newslist/bang-tin.htm")
                return true;
            else
                return false;
        }

        private void OpenSelenium()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            chr = new ChromeDriver(@"D:\Login_Selenium\chromedriver_win32");
            chr.Navigate().GoToUrl("https://sv.dntu.edu.vn/login/dang-nhap.htm");

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseSelenium();
        }
        private void CloseSelenium()
        {
            chr.Quit();
        }
    }
}
