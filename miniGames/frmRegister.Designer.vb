<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegister
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
        lblUsername = New Label()
        lblPassword = New Label()
        lblConfirmPassword = New Label()
        lblEmail = New Label()
        txtUsername = New TextBox()
        txtPassword = New TextBox()
        txtConfirmPassword = New TextBox()
        txtEmail = New TextBox()
        Label1 = New Label()
        txtAuth = New TextBox()
        lblAuths = New Label()
        cmdRegister = New Button()
        cmdCancel = New Button()
        cleanColor = New Timer(components)
        SuspendLayout()
        ' 
        ' lblUsername
        ' 
        lblUsername.AutoSize = True
        lblUsername.Font = New Font("黑体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lblUsername.Location = New Point(32, 12)
        lblUsername.Name = "lblUsername"
        lblUsername.Size = New Size(69, 19)
        lblUsername.TabIndex = 0
        lblUsername.Text = "用户名"
        ' 
        ' lblPassword
        ' 
        lblPassword.AutoSize = True
        lblPassword.Font = New Font("黑体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lblPassword.Location = New Point(52, 55)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(49, 19)
        lblPassword.TabIndex = 1
        lblPassword.Text = "密码"
        ' 
        ' lblConfirmPassword
        ' 
        lblConfirmPassword.AutoSize = True
        lblConfirmPassword.Font = New Font("黑体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lblConfirmPassword.Location = New Point(12, 100)
        lblConfirmPassword.Name = "lblConfirmPassword"
        lblConfirmPassword.Size = New Size(89, 19)
        lblConfirmPassword.TabIndex = 2
        lblConfirmPassword.Text = "确认密码"
        ' 
        ' lblEmail
        ' 
        lblEmail.AutoSize = True
        lblEmail.Font = New Font("黑体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lblEmail.Location = New Point(52, 145)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New Size(49, 19)
        lblEmail.TabIndex = 3
        lblEmail.Text = "邮箱"
        ' 
        ' txtUsername
        ' 
        txtUsername.BorderStyle = BorderStyle.FixedSingle
        txtUsername.Cursor = Cursors.IBeam
        txtUsername.Font = New Font("黑体", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        txtUsername.Location = New Point(107, 10)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(167, 26)
        txtUsername.TabIndex = 4
        ' 
        ' txtPassword
        ' 
        txtPassword.BorderStyle = BorderStyle.FixedSingle
        txtPassword.Cursor = Cursors.IBeam
        txtPassword.Font = New Font("黑体", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        txtPassword.Location = New Point(107, 52)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(167, 26)
        txtPassword.TabIndex = 5
        ' 
        ' txtConfirmPassword
        ' 
        txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle
        txtConfirmPassword.Cursor = Cursors.IBeam
        txtConfirmPassword.Font = New Font("黑体", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        txtConfirmPassword.Location = New Point(107, 97)
        txtConfirmPassword.Name = "txtConfirmPassword"
        txtConfirmPassword.Size = New Size(167, 26)
        txtConfirmPassword.TabIndex = 6
        ' 
        ' txtEmail
        ' 
        txtEmail.BorderStyle = BorderStyle.FixedSingle
        txtEmail.Cursor = Cursors.IBeam
        txtEmail.Font = New Font("黑体", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        txtEmail.Location = New Point(107, 142)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(167, 26)
        txtEmail.TabIndex = 7
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("黑体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Label1.Location = New Point(32, 190)
        Label1.Name = "Label1"
        Label1.Size = New Size(69, 19)
        Label1.TabIndex = 8
        Label1.Text = "验证码"
        ' 
        ' txtAuth
        ' 
        txtAuth.BorderStyle = BorderStyle.FixedSingle
        txtAuth.Cursor = Cursors.IBeam
        txtAuth.Font = New Font("黑体", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        txtAuth.Location = New Point(107, 187)
        txtAuth.Name = "txtAuth"
        txtAuth.Size = New Size(96, 26)
        txtAuth.TabIndex = 9
        ' 
        ' lblAuths
        ' 
        lblAuths.AutoSize = True
        lblAuths.Font = New Font("黑体", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lblAuths.Location = New Point(209, 185)
        lblAuths.Name = "lblAuths"
        lblAuths.Size = New Size(73, 29)
        lblAuths.TabIndex = 10
        lblAuths.Text = "ABCD"
        ' 
        ' cmdRegister
        ' 
        cmdRegister.Cursor = Cursors.Hand
        cmdRegister.Font = New Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        cmdRegister.Location = New Point(106, 227)
        cmdRegister.Name = "cmdRegister"
        cmdRegister.Size = New Size(167, 52)
        cmdRegister.TabIndex = 11
        cmdRegister.Text = "注册"
        cmdRegister.UseVisualStyleBackColor = True
        ' 
        ' cmdCancel
        ' 
        cmdCancel.Cursor = Cursors.Hand
        cmdCancel.Font = New Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        cmdCancel.Location = New Point(24, 227)
        cmdCancel.Name = "cmdCancel"
        cmdCancel.Size = New Size(76, 52)
        cmdCancel.TabIndex = 12
        cmdCancel.Text = "返回"
        cmdCancel.UseVisualStyleBackColor = True
        ' 
        ' cleanColor
        ' 
        cleanColor.Interval = 2000
        ' 
        ' frmRegister
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(293, 291)
        ControlBox = False
        Controls.Add(cmdCancel)
        Controls.Add(cmdRegister)
        Controls.Add(lblAuths)
        Controls.Add(txtAuth)
        Controls.Add(Label1)
        Controls.Add(txtEmail)
        Controls.Add(txtConfirmPassword)
        Controls.Add(txtPassword)
        Controls.Add(txtUsername)
        Controls.Add(lblEmail)
        Controls.Add(lblConfirmPassword)
        Controls.Add(lblPassword)
        Controls.Add(lblUsername)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmRegister"
        StartPosition = FormStartPosition.CenterScreen
        Text = "注册"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblUsername As Label
    Friend WithEvents lblPassword As Label
    Friend WithEvents lblConfirmPassword As Label
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtConfirmPassword As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAuth As TextBox
    Friend WithEvents lblAuths As Label
    Friend WithEvents cmdRegister As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents cleanColor As Timer
End Class
