<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoginLoad
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
        loadBar = New ProgressBar()
        ZeroTo25 = New Timer(components)
        to67 = New Timer(components)
        to100 = New Timer(components)
        stop1s = New Timer(components)
        SuspendLayout()
        ' 
        ' loadBar
        ' 
        loadBar.Location = New Point(0, -3)
        loadBar.Name = "loadBar"
        loadBar.Size = New Size(325, 31)
        loadBar.Step = 7
        loadBar.TabIndex = 0
        ' 
        ' ZeroTo25
        ' 
        ZeroTo25.Interval = 800
        ' 
        ' to67
        ' 
        to67.Interval = 200
        ' 
        ' to100
        ' 
        to100.Interval = 300
        ' 
        ' stop1s
        ' 
        stop1s.Interval = 700
        ' 
        ' frmLoginLoad
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(324, 27)
        Controls.Add(loadBar)
        Name = "frmLoginLoad"
        StartPosition = FormStartPosition.CenterScreen
        Text = "加载页面..."
        ResumeLayout(False)
    End Sub

    Friend WithEvents loadBar As ProgressBar
    Friend WithEvents ZeroTo25 As Timer
    Friend WithEvents to67 As Timer
    Friend WithEvents to100 As Timer
    Friend WithEvents stop1s As Timer
End Class
