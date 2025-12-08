Imports System.Windows.Forms
Imports Microsoft.Extensions.Logging

Public Class frmMain
    ' VB.NET 使用 New 创建对象，无需 Set
    Private m_gameData As clsGameData
    Private m_onlineTimer As Boolean = False
    Private m_startTime As DateTime = DateTime.Now

    ' 窗体加载事件
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 从全局管理器获取游戏数据
        If GameManager.IsGameDataLoaded Then
            m_gameData = GameManager.CurrentGameData
        Else
            ' 如果没有数据，显示错误并返回登录
            MessageBox.Show("游戏数据未初始化！请重新登录。", "错误",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()

            ' 显示登录窗体
            Dim loginForm As New frmLogin()
            loginForm.Show()
            Return
        End If

        ' 初始化显示
        UpdateGameDisplay()

        ' 开始在线计时
        StartOnlineTimer()
    End Sub

    ' 窗体关闭事件
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' 停止在线计时
        StopOnlineTimer()

        ' 保存游戏数据
        If m_gameData IsNot Nothing Then
            If m_gameData.UserID > 0 Then
                m_gameData.SaveGameData()
            End If
        End If

        ' VB6: End → VB.NET: Application.Exit()
        Application.Exit()
    End Sub

    ' 设置游戏数据（从登录窗体传递）
    Public Sub SetGameData(gameDataObj As clsGameData)
        m_gameData = gameDataObj
    End Sub

    ' 更新游戏显示
    Private Sub UpdateGameDisplay()
        If m_gameData Is Nothing Then
            Exit Sub
        End If

        Try
            ' 基本信息
            lblUsername.Text = $"用户: {m_gameData.Username}"
            lblLevel.Text = $"等级: {m_gameData.GameLevel}级"
            lblExp.Text = $"经验: {m_gameData.Experience}"
            lblGold.Text = m_gameData.GoldCoins.ToString()

            ' 建筑信息
            lblWarehouse.Text = $"仓库: Lv.{m_gameData.WarehouseLevel}"
            lblWarehouseInfo.Text = $"容量: {m_gameData.GetWarehouseCapacity()}"

            lblWorkbench.Text = $"工作站: Lv.{m_gameData.WorkbenchLevel}"
            lblWorkbenchInfo.Text = $"效率: {m_gameData.GetWorkbenchEfficiency():0.0}x"

            ' 升级按钮文本
            cmdUpgradeWarehouse.Text = $"升级仓库{vbCrLf}({m_gameData.WarehouseLevel * 500}金币)"
            cmdUpgradeWorkbench.Text = $"升级工作站{vbCrLf}({m_gameData.WorkbenchLevel * 800}金币)"

            ' 游戏时长
            lblPlayTime.Text = $"游戏时长: {m_gameData.TotalOnlineMinutes}分钟"

            ' 状态栏
            'toolStripStatusLabel1.Text = m_gameData.GetGameStatus()

        Catch ex As Exception
            MessageBox.Show($"更新显示时出错: {ex.Message}", "错误",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 开始在线计时
    Private Sub StartOnlineTimer()
        m_onlineTimer = True
        m_startTime = DateTime.Now
        tmrOnline.Interval = 60000  ' 1分钟 = 60000毫秒（VB6中是1000，但那只有1秒）
        tmrOnline.Enabled = True
    End Sub

    ' 停止在线计时
    Private Sub StopOnlineTimer()
        m_onlineTimer = False
        tmrOnline.Enabled = False
    End Sub

    ' 定时器事件
    Private Sub tmrOnline_Tick(sender As Object, e As EventArgs) Handles tmrOnline.Tick
        If m_onlineTimer AndAlso m_gameData IsNot Nothing Then
            Try
                Dim minutes As Integer = CInt((DateTime.Now - m_startTime).TotalMinutes)

                If minutes > 0 Then
                    m_gameData.AddOnlineMinutes(minutes)
                    m_startTime = DateTime.Now
                    UpdateGameDisplay()

                    ' 每10分钟自动保存（原VB6是1000分钟，这里改为10分钟更合理）
                    Static lastSaveTime As DateTime = DateTime.Now
                    If (DateTime.Now - lastSaveTime).TotalMinutes >= 10 Then
                        'If m_gameData.SaveGameData() Then
                        'AddGameLog("游戏数据已自动保存")
                        'End If
                        lastSaveTime = DateTime.Now
                    End If
                End If
            Catch ex As Exception
                ' 静默处理定时器错误
                Debug.WriteLine($"定时器错误: {ex.Message}")
            End Try
        End If
    End Sub

    ' ========== 按钮事件 ==========

    ' 增加经验
    Private Sub cmdAddExp_Click(sender As Object, e As EventArgs) Handles cmdAddExp.Click
        If m_gameData Is Nothing Then
            MessageBox.Show("游戏数据未初始化", "错误",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim input As String = InputBox("请输入要增加的经验值:", "增加经验", "100")

        If Not String.IsNullOrEmpty(input) AndAlso IsNumeric(input) Then
            Dim exp As Long = CLng(input)
            If exp > 0 Then
                If m_gameData.AddExperience(exp) Then
                    UpdateGameDisplay()
                Else
                    MessageBox.Show(m_gameData.LastError, "提示",
                                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                MessageBox.Show("经验值必须为正数", "提示",
                              MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    ' 增加金币
    Private Sub cmdAddGold_Click(sender As Object, e As EventArgs) Handles cmdAddGold.Click
        If m_gameData Is Nothing Then
            MessageBox.Show("游戏数据未初始化", "错误",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim input As String = InputBox("请输入要增加的金币数量:", "增加金币", "500")

        If Not String.IsNullOrEmpty(input) AndAlso IsNumeric(input) Then
            Dim gold As Long = CLng(input)
            If gold > 0 Then
                If m_gameData.AddGoldCoins(gold) Then
                    UpdateGameDisplay()
                Else
                    MessageBox.Show(m_gameData.LastError, "提示",
                                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                MessageBox.Show("金币数量必须为正数", "提示",
                              MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    ' 升级仓库
    Private Sub cmdUpgradeWarehouse_Click(sender As Object, e As EventArgs) Handles cmdUpgradeWarehouse.Click
        If m_gameData Is Nothing Then
            MessageBox.Show("游戏数据未初始化", "错误",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If m_gameData.UpgradeWarehouse() Then
            UpdateGameDisplay()
        Else
            MessageBox.Show(m_gameData.LastError, "提示",
                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    ' 升级工作站
    Private Sub cmdUpgradeWorkbench_Click(sender As Object, e As EventArgs) Handles cmdUpgradeWorkbench.Click
        If m_gameData Is Nothing Then
            MessageBox.Show("游戏数据未初始化", "错误",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If m_gameData.UpgradeWorkbench() Then
            UpdateGameDisplay()
        Else
            MessageBox.Show(m_gameData.LastError, "提示",
                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    ' 工作站工作
    Private Sub cmdWorkbench_Click(sender As Object, e As EventArgs) Handles cmdWorkbench.Click
        If m_gameData Is Nothing Then
            MessageBox.Show("游戏数据未初始化", "错误",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If m_gameData.DoWorkbenchWork() Then
            UpdateGameDisplay()
        Else
            MessageBox.Show(m_gameData.LastError, "提示",
                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    ' 保存游戏
    Private Sub cmdSaveGame_Click(sender As Object, e As EventArgs) Handles cmdSaveGame.Click
        If m_gameData Is Nothing Then
            MessageBox.Show("游戏数据未初始化", "错误",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If m_gameData.SaveGameData() Then
            MessageBox.Show(m_gameData.LastError, "保存成功",
                          MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(m_gameData.LastError, "保存失败",
                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    ' 退出游戏
    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        If MessageBox.Show("确定要退出游戏吗？", "确认退出",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    ' 窗体显示事件
    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' 确保焦点在合适的位置
        cmdWorkbench.Focus()
    End Sub

    ' 键盘快捷键
    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                cmdAddExp.PerformClick()
            Case Keys.F2
                cmdAddGold.PerformClick()
            Case Keys.F5
                cmdSaveGame.PerformClick()
            Case Keys.Escape
                cmdExit.PerformClick()
        End Select
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        frmPersonalCenter.Show()
    End Sub
End Class