Imports System.Windows.Forms
Imports Microsoft.Extensions.Logging

Public Class frmMain
    ' VB.NET 使用 New 创建对象，无需 Set
    Private m_gameData As clsGameData
    Private m_onlineTimer As Boolean = False
    Private m_startTime As DateTime = DateTime.Now
    Private m_isAnimating As Boolean = False ' 防止动画重叠

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

        '设置等级数显示位置
        lblLevel.Parent = imgLevel
        lblLevel.Location = New Point(40, 5)


        ' 配置 ToolTip
        ' 为控件设置提示
        toolTip.IsBalloon = True                    ' 气球样式
        toolTip.ToolTipIcon = ToolTipIcon.Info      ' 图标类型
        toolTip.ToolTipTitle = "提示"               ' 标题
        toolTip.AutomaticDelay = 500                ' 延迟时间(ms)
        toolTip.AutoPopDelay = 5000                 ' 显示时间(ms)
        toolTip.InitialDelay = 500                  ' 初始延迟
        toolTip.ReshowDelay = 100                   ' 重新显示延迟
        SetControlToolTips()

        ' 开始在线计时0
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
            lblLevel.Text = $"{m_gameData.GameLevel}"
            Dim requiredExp As Integer = m_gameData.GameLevel * 1000 ' 当前等级×1000
            lblExp.Text = $"经验: {m_gameData.Experience} / {requiredExp}"
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

            ' 【重要】更新经验条（不带动画）
            UpdateExperienceBar()

            ' 更新动态ToolTip
            UpdateDynamicToolTips()

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
                ' 保存当前经验值
                Dim oldExp As Long = m_gameData.Experience

                If m_gameData.AddExperience(exp) Then
                    ' 立即更新显示
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
            ' 只更新显示，不调用动画
            UpdateGameDisplay()
        Else
            MessageBox.Show(m_gameData.LastError, "提示",
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub SetControlToolTips()
        ' 经验进度条
        ' 查找经验条控件
        Dim expBar As ExperienceProgressBar = Nothing
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is ExperienceProgressBar Then
                expBar = DirectCast(ctrl, ExperienceProgressBar)
                Exit For
            End If
        Next

        If expBar IsNot Nothing Then
            UpdateExperienceBarToolTip(expBar)
        End If

        ' 游戏按钮
        toolTip.SetToolTip(cmdWorkbench, "消耗50金币进行工作，获得经验和金币奖励")
        toolTip.SetToolTip(cmdUpgradeWarehouse, $"升级仓库，当前等级: {m_gameData?.WarehouseLevel}")
        toolTip.SetToolTip(cmdUpgradeWorkbench, $"升级工作站，当前等级: {m_gameData?.WorkbenchLevel}")

        ' 资源显示
        toolTip.SetToolTip(lblGold, "当前拥有的金币数量")
        toolTip.SetToolTip(lblExp, "当前经验值，达到升级要求可提升等级")
        toolTip.SetToolTip(lblLevel, "当前玩家等级，等级越高解锁功能越多")

        ' 根据游戏状态动态设置
        UpdateDynamicToolTips()
    End Sub

    Private Sub UpdateDynamicToolTips()
        If m_gameData Is Nothing Then Return

        ' 根据游戏数据动态更新提示
        Dim warehouseCost As Long = m_gameData.WarehouseLevel * 500
        Dim workbenchCost As Long = m_gameData.WorkbenchLevel * 800

        toolTip.SetToolTip(cmdUpgradeWarehouse,
            $"升级仓库到 {m_gameData.WarehouseLevel + 1} 级" & vbCrLf &
            $"需要: {warehouseCost} 金币" & vbCrLf &
            $"新容量: {1000 * (m_gameData.WarehouseLevel + 1)}")

        toolTip.SetToolTip(cmdUpgradeWorkbench,
            $"升级工作站到 {m_gameData.WorkbenchLevel + 1} 级" & vbCrLf &
            $"需要: {workbenchCost} 金币" & vbCrLf &
            $"新效率: {(1 + m_gameData.WorkbenchLevel * 0.1):0.1}x")
    End Sub

    ' 鼠标悬停事件（额外效果）
    Private Sub cmdWorkbench_MouseEnter(sender As Object, e As EventArgs) Handles cmdWorkbench.MouseEnter
        Dim btn As Button = CType(sender, Button)
        btn.BackColor = Color.LightGray
    End Sub

    Private Sub cmdWorkbench_MouseLeave(sender As Object, e As EventArgs) Handles cmdWorkbench.MouseLeave
        Dim btn As Button = CType(sender, Button)
        btn.BackColor = SystemColors.Control
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

    Private Sub experienceProgressBar_Load(sender As Object, e As EventArgs) Handles experienceProgressBar.Load

    End Sub

    Private Sub UpdateExperienceBar()
        Dim expBar As ExperienceProgressBar = FindExperienceBar()
        If expBar IsNot Nothing AndAlso m_gameData IsNot Nothing Then
            ' 计算升级所需经验
            Dim requiredExp As Long = m_gameData.GameLevel * 1000

            ' 直接设置值（不带动画）
            expBar.Level = m_gameData.GameLevel
            expBar.MaxValue = CInt(requiredExp)
            expBar.CurrentValue = CInt(Math.Min(m_gameData.Experience, requiredExp))

            ' 更新ToolTip
            UpdateExperienceBarToolTip(expBar)
        End If
    End Sub

    ' 更新经验条ToolTip
    Private Sub UpdateExperienceBarToolTip(expBar As ExperienceProgressBar)
        If m_gameData Is Nothing Then Return

        Dim requiredExp As Long = m_gameData.GameLevel * 1000
        Dim neededExp As Long = requiredExp - m_gameData.Experience
        Dim progressPercent As Single = If(requiredExp > 0,
                                         CSng(m_gameData.Experience) / CSng(requiredExp) * 100,
                                         0)

        toolTip.SetToolTip(expBar,
            $"经验进度: {progressPercent:F1}%" & vbCrLf &
            $"当前经验: {m_gameData.Experience}/{requiredExp}" & vbCrLf &
            $"还需经验: {neededExp}" & vbCrLf &
            $"升级奖励: {m_gameData.GameLevel * 100} 金币")
    End Sub

    ' 【新增】平滑经验动画
    Private Sub SmoothExperienceAnimation(oldExp As Long, newExp As Long)
        If m_isAnimating Then Return ' 防止动画重叠

        Dim expBar As ExperienceProgressBar = FindExperienceBar()
        If expBar Is Nothing Then Return

        ' 计算当前等级所需经验
        Dim requiredExp As Long = m_gameData.GameLevel * 1000

        ' 确保不超过最大值
        Dim targetValue As Integer = CInt(Math.Min(newExp, requiredExp))
        Dim currentValue As Integer = CInt(Math.Min(oldExp, requiredExp))

        If targetValue <= currentValue Then
            ' 没有增加或减少，直接更新
            UpdateExperienceBar()
            Return
        End If

        m_isAnimating = True

        ' 使用Timer实现平滑动画
        Dim animateTimer As New Timer()
        Dim stepCount As Integer = 0
        Dim totalSteps As Integer = 20 ' 动画步数
        Dim delta As Integer = targetValue - currentValue
        Dim stepValue As Single = delta / totalSteps

        AddHandler animateTimer.Tick, Sub(sender, e)
                                          stepCount += 1

                                          If stepCount <= totalSteps Then
                                              ' 更新经验条当前值（不更新实际数据）
                                              Dim animatedValue As Integer = CInt(currentValue + stepValue * stepCount)
                                              expBar.CurrentValue = Math.Min(animatedValue, requiredExp)
                                              expBar.Invalidate()
                                          Else
                                              ' 动画完成
                                              expBar.CurrentValue = targetValue
                                              expBar.Invalidate()
                                              animateTimer.Stop()
                                              animateTimer.Dispose()
                                              m_isAnimating = False
                                          End If
                                      End Sub

        animateTimer.Interval = 20 ' 每20ms更新一次
        animateTimer.Start()
    End Sub

    ' 查找经验条控件
    Private Function FindExperienceBar() As ExperienceProgressBar
        ' 优先查找特定名称
        Dim expBar As ExperienceProgressBar = TryCast(Me.Controls.Find("expBar", True).FirstOrDefault(), ExperienceProgressBar)

        If expBar Is Nothing Then
            ' 查找所有 ExperienceProgressBar
            For Each ctrl As Control In Me.Controls
                If TypeOf ctrl Is ExperienceProgressBar Then
                    expBar = DirectCast(ctrl, ExperienceProgressBar)
                    Exit For
                End If
            Next
        End If

        Return expBar
    End Function
End Class