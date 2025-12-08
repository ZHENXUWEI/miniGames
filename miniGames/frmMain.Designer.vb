<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        PictureBox1 = New PictureBox()
        lblUsername = New Label()
        lblLevel = New Label()
        lblExp = New Label()
        lblGold = New Label()
        lblWarehouse = New Label()
        lblWarehouseInfo = New Label()
        lblWorkbench = New Label()
        lblWorkbenchInfo = New Label()
        lblPlayTime = New Label()
        cmdAddExp = New Button()
        cmdAddGold = New Button()
        cmdUpgradeWarehouse = New Button()
        cmdUpgradeWorkbench = New Button()
        cmdWorkbench = New Button()
        cmdSaveGame = New Button()
        cmdExit = New Button()
        tmrOnline = New Timer(components)
        cmdRank = New Button()
        cmdMarket = New Button()
        cmdIntoWarehouse = New Button()
        cmdCasualMode = New Button()
        cmdNormalMode = New Button()
        cmdMatchMode = New Button()
        lable2 = New Label()
        lblNowRanked = New Label()
        Label1 = New Label()
        lblNowScore = New Label()
        Label2 = New Label()
        Label3 = New Label()
        lblHighestRanked = New Label()
        lblHighestScore = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Cursor = Cursors.Hand
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(747, 2)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(46, 46)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' lblUsername
        ' 
        lblUsername.AutoSize = True
        lblUsername.Location = New Point(712, 65)
        lblUsername.Name = "lblUsername"
        lblUsername.Size = New Size(81, 17)
        lblUsername.TabIndex = 1
        lblUsername.Text = "lblUsername"
        ' 
        ' lblLevel
        ' 
        lblLevel.AutoSize = True
        lblLevel.Location = New Point(12, 9)
        lblLevel.Name = "lblLevel"
        lblLevel.Size = New Size(51, 17)
        lblLevel.TabIndex = 2
        lblLevel.Text = "lblLevel"
        ' 
        ' lblExp
        ' 
        lblExp.AutoSize = True
        lblExp.Location = New Point(12, 31)
        lblExp.Name = "lblExp"
        lblExp.Size = New Size(43, 17)
        lblExp.TabIndex = 3
        lblExp.Text = "lblExp"
        ' 
        ' lblGold
        ' 
        lblGold.AutoSize = True
        lblGold.Location = New Point(12, 65)
        lblGold.Name = "lblGold"
        lblGold.Size = New Size(50, 17)
        lblGold.TabIndex = 4
        lblGold.Text = "lblGold"
        ' 
        ' lblWarehouse
        ' 
        lblWarehouse.AutoSize = True
        lblWarehouse.Location = New Point(12, 118)
        lblWarehouse.Name = "lblWarehouse"
        lblWarehouse.Size = New Size(88, 17)
        lblWarehouse.TabIndex = 5
        lblWarehouse.Text = "lblWarehouse"
        ' 
        ' lblWarehouseInfo
        ' 
        lblWarehouseInfo.AutoSize = True
        lblWarehouseInfo.Location = New Point(12, 145)
        lblWarehouseInfo.Name = "lblWarehouseInfo"
        lblWarehouseInfo.Size = New Size(111, 17)
        lblWarehouseInfo.TabIndex = 6
        lblWarehouseInfo.Text = "lblWarehouseInfo"
        ' 
        ' lblWorkbench
        ' 
        lblWorkbench.AutoSize = True
        lblWorkbench.Location = New Point(12, 175)
        lblWorkbench.Name = "lblWorkbench"
        lblWorkbench.Size = New Size(89, 17)
        lblWorkbench.TabIndex = 7
        lblWorkbench.Text = "lblWorkbench"
        ' 
        ' lblWorkbenchInfo
        ' 
        lblWorkbenchInfo.AutoSize = True
        lblWorkbenchInfo.Location = New Point(12, 206)
        lblWorkbenchInfo.Name = "lblWorkbenchInfo"
        lblWorkbenchInfo.Size = New Size(112, 17)
        lblWorkbenchInfo.TabIndex = 8
        lblWorkbenchInfo.Text = "lblWorkbenchInfo"
        ' 
        ' lblPlayTime
        ' 
        lblPlayTime.AutoSize = True
        lblPlayTime.Location = New Point(712, 96)
        lblPlayTime.Name = "lblPlayTime"
        lblPlayTime.Size = New Size(73, 17)
        lblPlayTime.TabIndex = 9
        lblPlayTime.Text = "lblPlayTime"
        ' 
        ' cmdAddExp
        ' 
        cmdAddExp.Cursor = Cursors.Hand
        cmdAddExp.FlatStyle = FlatStyle.Flat
        cmdAddExp.Location = New Point(686, 349)
        cmdAddExp.Name = "cmdAddExp"
        cmdAddExp.Size = New Size(99, 23)
        cmdAddExp.TabIndex = 10
        cmdAddExp.Text = "cmdAddExp"
        cmdAddExp.UseVisualStyleBackColor = True
        ' 
        ' cmdAddGold
        ' 
        cmdAddGold.Cursor = Cursors.Hand
        cmdAddGold.FlatStyle = FlatStyle.Flat
        cmdAddGold.Location = New Point(686, 378)
        cmdAddGold.Name = "cmdAddGold"
        cmdAddGold.Size = New Size(99, 23)
        cmdAddGold.TabIndex = 11
        cmdAddGold.Text = "cmdAddGold"
        cmdAddGold.UseVisualStyleBackColor = True
        ' 
        ' cmdUpgradeWarehouse
        ' 
        cmdUpgradeWarehouse.Cursor = Cursors.Hand
        cmdUpgradeWarehouse.FlatStyle = FlatStyle.Flat
        cmdUpgradeWarehouse.Location = New Point(14, 226)
        cmdUpgradeWarehouse.Name = "cmdUpgradeWarehouse"
        cmdUpgradeWarehouse.Size = New Size(149, 28)
        cmdUpgradeWarehouse.TabIndex = 12
        cmdUpgradeWarehouse.Text = "cmdUpgradeWarehouse"
        cmdUpgradeWarehouse.UseVisualStyleBackColor = True
        ' 
        ' cmdUpgradeWorkbench
        ' 
        cmdUpgradeWorkbench.Cursor = Cursors.Hand
        cmdUpgradeWorkbench.FlatStyle = FlatStyle.Flat
        cmdUpgradeWorkbench.Location = New Point(13, 275)
        cmdUpgradeWorkbench.Name = "cmdUpgradeWorkbench"
        cmdUpgradeWorkbench.Size = New Size(150, 28)
        cmdUpgradeWorkbench.TabIndex = 13
        cmdUpgradeWorkbench.Text = "cmdUpgradeWorkbench"
        cmdUpgradeWorkbench.UseVisualStyleBackColor = True
        ' 
        ' cmdWorkbench
        ' 
        cmdWorkbench.Cursor = Cursors.Hand
        cmdWorkbench.FlatStyle = FlatStyle.Flat
        cmdWorkbench.Location = New Point(12, 319)
        cmdWorkbench.Name = "cmdWorkbench"
        cmdWorkbench.Size = New Size(150, 28)
        cmdWorkbench.TabIndex = 14
        cmdWorkbench.Text = "cmdWorkbench"
        cmdWorkbench.UseVisualStyleBackColor = True
        ' 
        ' cmdSaveGame
        ' 
        cmdSaveGame.Cursor = Cursors.Hand
        cmdSaveGame.FlatStyle = FlatStyle.Flat
        cmdSaveGame.Location = New Point(13, 359)
        cmdSaveGame.Name = "cmdSaveGame"
        cmdSaveGame.Size = New Size(99, 25)
        cmdSaveGame.TabIndex = 15
        cmdSaveGame.Text = "cmdSaveGame"
        cmdSaveGame.UseVisualStyleBackColor = True
        ' 
        ' cmdExit
        ' 
        cmdExit.Cursor = Cursors.Hand
        cmdExit.FlatStyle = FlatStyle.Flat
        cmdExit.Location = New Point(12, 388)
        cmdExit.Name = "cmdExit"
        cmdExit.Size = New Size(99, 25)
        cmdExit.TabIndex = 16
        cmdExit.Text = "cmdExit"
        cmdExit.UseVisualStyleBackColor = True
        ' 
        ' tmrOnline
        ' 
        ' 
        ' cmdRank
        ' 
        cmdRank.Cursor = Cursors.No
        cmdRank.FlatStyle = FlatStyle.Flat
        cmdRank.Location = New Point(203, 12)
        cmdRank.Name = "cmdRank"
        cmdRank.Size = New Size(99, 28)
        cmdRank.TabIndex = 17
        cmdRank.Text = "排行榜"
        cmdRank.UseVisualStyleBackColor = True
        ' 
        ' cmdMarket
        ' 
        cmdMarket.Cursor = Cursors.No
        cmdMarket.FlatStyle = FlatStyle.Flat
        cmdMarket.Location = New Point(203, 59)
        cmdMarket.Name = "cmdMarket"
        cmdMarket.Size = New Size(99, 28)
        cmdMarket.TabIndex = 18
        cmdMarket.Text = "市场"
        cmdMarket.UseVisualStyleBackColor = True
        ' 
        ' cmdIntoWarehouse
        ' 
        cmdIntoWarehouse.Cursor = Cursors.No
        cmdIntoWarehouse.FlatStyle = FlatStyle.Flat
        cmdIntoWarehouse.Location = New Point(203, 106)
        cmdIntoWarehouse.Name = "cmdIntoWarehouse"
        cmdIntoWarehouse.Size = New Size(99, 28)
        cmdIntoWarehouse.TabIndex = 19
        cmdIntoWarehouse.Text = "仓库"
        cmdIntoWarehouse.UseVisualStyleBackColor = True
        ' 
        ' cmdCasualMode
        ' 
        cmdCasualMode.Cursor = Cursors.Hand
        cmdCasualMode.FlatStyle = FlatStyle.Flat
        cmdCasualMode.Location = New Point(203, 175)
        cmdCasualMode.Name = "cmdCasualMode"
        cmdCasualMode.Size = New Size(111, 28)
        cmdCasualMode.TabIndex = 20
        cmdCasualMode.Text = "休闲"
        cmdCasualMode.UseVisualStyleBackColor = True
        ' 
        ' cmdNormalMode
        ' 
        cmdNormalMode.Cursor = Cursors.Hand
        cmdNormalMode.FlatStyle = FlatStyle.Flat
        cmdNormalMode.Location = New Point(332, 175)
        cmdNormalMode.Name = "cmdNormalMode"
        cmdNormalMode.Size = New Size(111, 28)
        cmdNormalMode.TabIndex = 21
        cmdNormalMode.Text = "标准"
        cmdNormalMode.UseVisualStyleBackColor = True
        ' 
        ' cmdMatchMode
        ' 
        cmdMatchMode.Cursor = Cursors.Hand
        cmdMatchMode.FlatStyle = FlatStyle.Flat
        cmdMatchMode.Location = New Point(460, 175)
        cmdMatchMode.Name = "cmdMatchMode"
        cmdMatchMode.Size = New Size(111, 28)
        cmdMatchMode.TabIndex = 22
        cmdMatchMode.Text = "赛事"
        cmdMatchMode.UseVisualStyleBackColor = True
        ' 
        ' lable2
        ' 
        lable2.AutoSize = True
        lable2.Location = New Point(202, 221)
        lable2.Name = "lable2"
        lable2.Size = New Size(56, 17)
        lable2.TabIndex = 23
        lable2.Text = "当前排名"
        ' 
        ' lblNowRanked
        ' 
        lblNowRanked.AutoSize = True
        lblNowRanked.Location = New Point(264, 221)
        lblNowRanked.Name = "lblNowRanked"
        lblNowRanked.Size = New Size(33, 17)
        lblNowRanked.TabIndex = 24
        lblNowRanked.Text = "--/--"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(202, 247)
        Label1.Name = "Label1"
        Label1.Size = New Size(56, 17)
        Label1.TabIndex = 25
        Label1.Text = "当前积分"
        ' 
        ' lblNowScore
        ' 
        lblNowScore.AutoSize = True
        lblNowScore.Location = New Point(264, 247)
        lblNowScore.Name = "lblNowScore"
        lblNowScore.Size = New Size(18, 17)
        lblNowScore.TabIndex = 26
        lblNowScore.Text = "--"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(202, 275)
        Label2.Name = "Label2"
        Label2.Size = New Size(56, 17)
        Label2.TabIndex = 27
        Label2.Text = "最高排名"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(202, 302)
        Label3.Name = "Label3"
        Label3.Size = New Size(56, 17)
        Label3.TabIndex = 28
        Label3.Text = "最高积分"
        ' 
        ' lblHighestRanked
        ' 
        lblHighestRanked.AutoSize = True
        lblHighestRanked.Location = New Point(264, 275)
        lblHighestRanked.Name = "lblHighestRanked"
        lblHighestRanked.Size = New Size(18, 17)
        lblHighestRanked.TabIndex = 29
        lblHighestRanked.Text = "--"
        ' 
        ' lblHighestScore
        ' 
        lblHighestScore.AutoSize = True
        lblHighestScore.Location = New Point(264, 302)
        lblHighestScore.Name = "lblHighestScore"
        lblHighestScore.Size = New Size(18, 17)
        lblHighestScore.TabIndex = 30
        lblHighestScore.Text = "--"
        ' 
        ' frmMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(lblHighestScore)
        Controls.Add(lblHighestRanked)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(lblNowScore)
        Controls.Add(Label1)
        Controls.Add(lblNowRanked)
        Controls.Add(lable2)
        Controls.Add(cmdMatchMode)
        Controls.Add(cmdNormalMode)
        Controls.Add(cmdCasualMode)
        Controls.Add(cmdIntoWarehouse)
        Controls.Add(cmdMarket)
        Controls.Add(cmdRank)
        Controls.Add(cmdExit)
        Controls.Add(cmdSaveGame)
        Controls.Add(cmdWorkbench)
        Controls.Add(cmdUpgradeWorkbench)
        Controls.Add(cmdUpgradeWarehouse)
        Controls.Add(cmdAddGold)
        Controls.Add(cmdAddExp)
        Controls.Add(lblPlayTime)
        Controls.Add(lblWorkbenchInfo)
        Controls.Add(lblWorkbench)
        Controls.Add(lblWarehouseInfo)
        Controls.Add(lblWarehouse)
        Controls.Add(lblGold)
        Controls.Add(lblExp)
        Controls.Add(lblLevel)
        Controls.Add(lblUsername)
        Controls.Add(PictureBox1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "frmMain"
        StartPosition = FormStartPosition.CenterScreen
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents lblLevel As Label
    Friend WithEvents lblExp As Label
    Friend WithEvents lblGold As Label
    Friend WithEvents lblWarehouse As Label
    Friend WithEvents lblWarehouseInfo As Label
    Friend WithEvents lblWorkbench As Label
    Friend WithEvents lblWorkbenchInfo As Label
    Friend WithEvents lblPlayTime As Label
    Friend WithEvents cmdAddExp As Button
    Friend WithEvents cmdAddGold As Button
    Friend WithEvents cmdUpgradeWarehouse As Button
    Friend WithEvents cmdUpgradeWorkbench As Button
    Friend WithEvents cmdWorkbench As Button
    Friend WithEvents cmdSaveGame As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents tmrOnline As Timer
    Friend WithEvents cmdRank As Button
    Friend WithEvents cmdMarket As Button
    Friend WithEvents cmdIntoWarehouse As Button
    Friend WithEvents cmdCasualMode As Button
    Friend WithEvents cmdNormalMode As Button
    Friend WithEvents cmdMatchMode As Button
    Friend WithEvents lable2 As Label
    Friend WithEvents lblNowRanked As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblNowScore As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblHighestRanked As Label
    Friend WithEvents lblHighestScore As Label
End Class
