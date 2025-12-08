Imports System.Threading

Public Class frmLoginLoad
    Private m_gameData As clsGameData

    Private Sub frmLoginLoad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadBar.Value = 0
        Me.Text = "加载页面..."
        ZeroTo25.Enabled = True
    End Sub

    Private Sub ZeroTo25_Tick(sender As Object, e As EventArgs) Handles ZeroTo25.Tick
        loadBar.Value = 25
        ZeroTo25.Enabled = False
        to67.Enabled = True
        Thread.Sleep(500)
    End Sub

    Private Sub to67_Tick(sender As Object, e As EventArgs) Handles to67.Tick
        If loadBar.Value < 67 Then
            Me.Text = "从服务器获取数据..."
            loadBar.Value += 6
        Else
            to67.Enabled = False
            to100.Enabled = True
            Thread.Sleep(500)
        End If
    End Sub

    Private Sub stop1s_Tick(sender As Object, e As EventArgs) Handles stop1s.Tick
        ' 停止所有定时器
        ZeroTo25.Enabled = False
        to67.Enabled = False
        to100.Enabled = False
        stop1s.Enabled = False

        ' 验证游戏数据
        If Not GameManager.IsGameDataLoaded Then
            MessageBox.Show("游戏数据加载失败，请重新登录", "错误",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        ' 创建并显示主窗体
        Dim mainFrm As New frmMain()
        Me.Hide()
        mainFrm.Show()
        Me.Close()  ' 关闭加载窗体
    End Sub

    Private Sub to100_Tick(sender As Object, e As EventArgs) Handles to100.Tick
        If loadBar.Value < 100 Then
            Me.Text = "加载数据..."
            loadBar.Value += 11
            Thread.Sleep(100)
        Else
            to100.Enabled = False
            stop1s.Enabled = True
        End If
    End Sub

    Private Sub loadBar_Click(sender As Object, e As EventArgs) Handles loadBar.Click

    End Sub
End Class