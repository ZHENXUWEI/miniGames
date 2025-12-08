<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPersonalCenter
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
        cmdBack = New Button()
        Label1 = New Label()
        cmdLogOut = New Button()
        cmdDeleteAccount = New Button()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        SuspendLayout()
        ' 
        ' cmdBack
        ' 
        cmdBack.Cursor = Cursors.Hand
        cmdBack.FlatStyle = FlatStyle.Flat
        cmdBack.Location = New Point(11, 224)
        cmdBack.Name = "cmdBack"
        cmdBack.Size = New Size(75, 26)
        cmdBack.TabIndex = 0
        cmdBack.Text = "返回"
        cmdBack.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(8, 5)
        Label1.Name = "Label1"
        Label1.Size = New Size(56, 17)
        Label1.TabIndex = 1
        Label1.Text = "个人资料"
        ' 
        ' cmdLogOut
        ' 
        cmdLogOut.Cursor = Cursors.Hand
        cmdLogOut.FlatStyle = FlatStyle.Flat
        cmdLogOut.Location = New Point(92, 224)
        cmdLogOut.Name = "cmdLogOut"
        cmdLogOut.Size = New Size(75, 26)
        cmdLogOut.TabIndex = 2
        cmdLogOut.Text = "退出登录"
        cmdLogOut.UseVisualStyleBackColor = True
        ' 
        ' cmdDeleteAccount
        ' 
        cmdDeleteAccount.Cursor = Cursors.Hand
        cmdDeleteAccount.FlatStyle = FlatStyle.Flat
        cmdDeleteAccount.Location = New Point(173, 224)
        cmdDeleteAccount.Name = "cmdDeleteAccount"
        cmdDeleteAccount.Size = New Size(75, 26)
        cmdDeleteAccount.TabIndex = 3
        cmdDeleteAccount.Text = "删除账户"
        cmdDeleteAccount.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(11, 38)
        Label2.Name = "Label2"
        Label2.Size = New Size(56, 17)
        Label2.TabIndex = 4
        Label2.Text = "用户名："
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(11, 72)
        Label3.Name = "Label3"
        Label3.Size = New Size(68, 17)
        Label3.TabIndex = 5
        Label3.Text = "游戏时长："
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(12, 108)
        Label4.Name = "Label4"
        Label4.Size = New Size(68, 17)
        Label4.TabIndex = 6
        Label4.Text = "注册时间："
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(12, 146)
        Label5.Name = "Label5"
        Label5.Size = New Size(92, 17)
        Label5.TabIndex = 7
        Label5.Text = "获得赛事荣誉："
        ' 
        ' frmPersonalCenter
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(263, 262)
        ControlBox = False
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(cmdDeleteAccount)
        Controls.Add(cmdLogOut)
        Controls.Add(Label1)
        Controls.Add(cmdBack)
        Name = "frmPersonalCenter"
        StartPosition = FormStartPosition.CenterScreen
        Text = "个人资料"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cmdBack As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cmdLogOut As Button
    Friend WithEvents cmdDeleteAccount As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
