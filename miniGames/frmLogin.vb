Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Security.Cryptography.Xml
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement


Public Class frmLogin
    Private m_user As New clsUser()
    Private m_gameData As clsGameData
    Private m_database As clsDatabase
    Private Const EncryptKey As String = "VB.NET_Login_Key_2025"

    Private Sub cmd_login_Click(sender As Object, e As EventArgs) Handles cmd_login.Click
        Try
            Dim username As String = input_username.Text.Trim()
            Dim password As String = input_password.Text.Trim()

            ' 输入验证
            If String.IsNullOrEmpty(username) Then
                MessageBox.Show("请输入用户名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                input_username.Focus()
                Return
            End If
            If String.IsNullOrEmpty(password) Then
                MessageBox.Show("请输入密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                input_password.Focus()
                Return
            End If

            ' 保存凭据（如果勾选记住密码）到配置文件
            If chkRemember.Checked Then
                AppConfig.SaveCredentials(username, password, True)
            Else
                ' 不记住密码，清除保存的密码
                AppConfig.SaveCredentials(username, "", False)
            End If

            ' 调用迁移后的Login方法
            Dim loginSuccess As Boolean = m_user.Login(username, password)
            If loginSuccess Then
                ' 初始化游戏数据
                m_gameData = New clsGameData()
                If m_gameData.InitializeGameData(m_user.UserID, m_user.Username) Then
                    ' 保存到全局管理器
                    GameManager.CurrentGameData = m_gameData
                    frmLoginLoad.Show()
                    Me.Hide()
                Else
                    MessageBox.Show($"游戏数据初始化失败: {m_gameData.LastError}")
                End If
            Else
                MessageBox.Show($"登录失败: {m_user.LastError}")
                input_password.BackColor = Color.LightBlue
                Timer1.Start() ' 恢复输入框颜色
            End If
        Catch ex As Exception
            MessageBox.Show($"登录错误: {ex.Message}")
        End Try
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        input_password.PasswordChar = "o"
        'input_password.UseSystemPasswordChar = True

        LoadSavedCredentials()
        m_database = New clsDatabase()
        Dim success As Boolean = m_database.OpenConnection()
        If success Then
            imgStatusRed.Visible = False
            imgStatusGreen.Visible = True
            lblStatus.Text = "服务器正常"
            lblStatus.ForeColor = Color.Green
        Else
            imgStatusRed.Visible = True
            imgStatusGreen.Visible = False
            lblStatus.Text = "服务器维护中"
            lblStatus.ForeColor = Color.Red
        End If
    End Sub

    Private Sub lb_reg_Click(sender As Object, e As EventArgs) Handles lb_reg.Click
        frmRegister.Show()
    End Sub


    ' 加载保存的用户名和密码（从配置文件）
    Private Sub LoadSavedCredentials()
        Dim savedCreds = AppConfig.LoadCredentials()

        If Not String.IsNullOrEmpty(savedCreds.Username) Then
            input_username.Text = savedCreds.Username
            chkRemember.Checked = savedCreds.RememberPassword

            If savedCreds.RememberPassword AndAlso Not String.IsNullOrEmpty(savedCreds.Password) Then
                input_password.Text = savedCreds.Password
            End If
        End If
    End Sub
End Class