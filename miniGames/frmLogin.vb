Public Class frmLogin
    Private m_user As New clsUser()
    Private m_gameData As clsGameData

    Private Sub cmd_login_Click(sender As Object, e As EventArgs) Handles cmd_login.Click
        Try
            Dim username As String = input_username.Text.Trim()
            Dim password As String = input_password.Text.Trim()

            ' 调用迁移后的Login方法
            Dim loginSuccess As Boolean = m_user.Login(username, password)
            If loginSuccess Then
                ' 初始化游戏数据
                m_gameData = New clsGameData()
                If m_gameData.InitializeGameData(m_user.UserID, m_user.Username) Then
                    ' 跳转主窗体（替换VB6的Me.Hide和frmMain.Show）
                    'Dim mainFrm As New frmMain()
                    'mainFrm.SetGameData(m_gameData)
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
End Class