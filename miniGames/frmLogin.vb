Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Security.Cryptography.Xml
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement


Public Class frmLogin
    Private m_user As New clsUser()
    Private m_db As New clsDatabase()
    Private m_gameData As clsGameData
    Private m_database As clsDatabase
    Private Const EncryptKey As String = "VB.NET_Login_Key_2025"
    Private Const nowVersion As String = "1.2.3"
    ' 阿里云 OSS 上的更新文件 URL
    Private Const UPDATE_FILE_URL As String = "https://lygamestest.oss-cn-hangzhou.aliyuncs.com/miniGames.exe"
    ' 本地保存的文件名
    Private Const LOCAL_FILE_NAME As String = "MG_L.exe"

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
        CheckVersion()
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

    ' ========== 检查是否最新版本 ==========
    Private Function IsLastedVersion(nowVersion As String) As Boolean
        Try
            Dim sql As String = $"SELECT version FROM server_info WHERE id = 1"
            Dim dt As DataTable = m_db.ExecuteQuery(sql)

            ' 检查DataTable是否有数据行
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim serverVersion As String = dt.Rows(0)("version").ToString()

                If nowVersion = serverVersion Then
                    Return True
                End If
            End If

            Return False
        Catch
            Return False
        End Try
    End Function
    Private Sub CheckVersion()
        If IsLastedVersion(nowVersion) Then
            imgVersionGreen.Visible = True
            imgVersionOrange.Visible = False
            lblVersion.Text = "已是最新版本"
            lblVersion.ForeColor = Color.Green
            lblVersion.Cursor = Cursors.Default
            lblVersion.Enabled = False
            cmd_login.Enabled = True
            Me.Text = "登录"
        Else
            imgVersionGreen.Visible = False
            imgVersionOrange.Visible = True
            lblVersion.Text = "版本需要更新,点击下载"
            lblVersion.ForeColor = Color.Orange
            lblVersion.Cursor = Cursors.Hand
            lblVersion.Enabled = True
            cmd_login.Enabled = False
            Me.Text = "当前版本:" & nowVersion & "过旧,请下载最新版本"
        End If
    End Sub

    ' 下载更新文件的方法
    Private Sub DownloadUpdateFile(downloadUrl As String, savePath As String)
        Try
            Using client As New WebClient()
                ' 添加下载进度事件处理
                AddHandler client.DownloadProgressChanged, AddressOf DownloadProgressChanged
                AddHandler client.DownloadFileCompleted, AddressOf DownloadFileCompleted

                ' 开始异步下载
                client.DownloadFileAsync(New Uri(downloadUrl), savePath)

                ' 禁用下载按钮，防止重复点击
                lblVersion.Enabled = False
                lblVersion.Text = "开始下载..."
                ProgressBar1.Visible = True
                lblBar.Visible = True
            End Using
        Catch ex As Exception
            MessageBox.Show("下载更新文件失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblVersion.Enabled = True
        End Try
    End Sub

    ' 下载进度事件处理
    Private Sub DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        ' 更新进度条
        ProgressBar1.Value = e.ProgressPercentage
        ' 显示下载信息
        lblBar.Text = $"正在下载：{e.BytesReceived / 1024 / 1024:F2} MB / {e.TotalBytesToReceive / 1024 / 1024:F2} MB ({e.ProgressPercentage}%)"
    End Sub

    ' 下载完成事件处理
    Private Sub DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)
        If e.Error IsNot Nothing Then
            ' 下载出错
            MessageBox.Show("下载更新文件失败：" & e.Error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblVersion.Text = "下载失败"
        Else
            ' 下载成功
            Dim savePath As String = CType(e.UserState, String)
            MessageBox.Show($"更新文件已成功下载到桌面：{LOCAL_FILE_NAME}", "下载完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
            lblVersion.Text = "下载完成"
            ProgressBar1.Visible = False
            lblBar.Visible = False
            ' 询问用户是否立即打开文件
            'If MessageBox.Show("是否立即打开下载的文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '    Try
            '        Process.Start(savePath)
            '    Catch ex As Exception
            '        MessageBox.Show("无法打开文件：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    End Try
            'End If
        End If


        ' 恢复下载按钮状态
        lblVersion.Enabled = True
        Application.Exit()
    End Sub

    Private Sub lblVersion_Click(sender As Object, e As EventArgs) Handles lblVersion.Click
        ' 获取桌面路径
        Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        ' 完整保存路径
        Dim savePath As String = Path.Combine(desktopPath, LOCAL_FILE_NAME)

        ' 开始下载
        DownloadUpdateFile(UPDATE_FILE_URL, savePath)
    End Sub
End Class