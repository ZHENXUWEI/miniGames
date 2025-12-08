Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class ExperienceProgressBar
    Inherits UserControl

    ' 枚举定义
    Public Enum ExperienceBarStyle
        Minimal
        Modern
        Classic
        Game
    End Enum

    ' 私有字段
    Private _minValue As Integer = 0
    Private _maxValue As Integer = 100
    Private _currentValue As Integer = 0
    Private _level As Integer = 1
    Private _showText As Boolean = True
    Private _borderWidth As Integer = 1
    Private _style As ExperienceBarStyle = ExperienceBarStyle.Modern

    ' 属性定义（带序列化特性）
    <System.ComponentModel.Browsable(True)>
    <System.ComponentModel.Category("Behavior")>
    <System.ComponentModel.Description("最小值")>
    <System.ComponentModel.DefaultValue(0)>
    Public Property MinValue As Integer
        Get
            Return _minValue
        End Get
        Set(value As Integer)
            _minValue = Math.Max(0, value)
            If _currentValue < _minValue Then _currentValue = _minValue
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Browsable(True)>
    <System.ComponentModel.Category("Behavior")>
    <System.ComponentModel.Description("最大值")>
    <System.ComponentModel.DefaultValue(100)>
    Public Property MaxValue As Integer
        Get
            Return _maxValue
        End Get
        Set(value As Integer)
            _maxValue = Math.Max(_minValue + 1, value)
            If _currentValue > _maxValue Then _currentValue = _maxValue
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Browsable(True)>
    <System.ComponentModel.Category("Behavior")>
    <System.ComponentModel.Description("当前值")>
    <System.ComponentModel.DefaultValue(0)>
    Public Property CurrentValue As Integer
        Get
            Return _currentValue
        End Get
        Set(value As Integer)
            _currentValue = Math.Max(_minValue, Math.Min(_maxValue, value))
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Browsable(True)>
    <System.ComponentModel.Category("Appearance")>
    <System.ComponentModel.Description("当前等级")>
    <System.ComponentModel.DefaultValue(1)>
    Public Property Level As Integer
        Get
            Return _level
        End Get
        Set(value As Integer)
            _level = Math.Max(1, value)
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Browsable(True)>
    <System.ComponentModel.Category("Appearance")>
    <System.ComponentModel.Description("是否显示文字")>
    <System.ComponentModel.DefaultValue(True)>
    Public Property ShowText As Boolean
        Get
            Return _showText
        End Get
        Set(value As Boolean)
            _showText = value
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Browsable(True)>
    <System.ComponentModel.Category("Appearance")>
    <System.ComponentModel.Description("边框宽度")>
    <System.ComponentModel.DefaultValue(1)>
    Public Property BorderWidth As Integer
        Get
            Return _borderWidth
        End Get
        Set(value As Integer)
            _borderWidth = Math.Max(1, Math.Min(5, value))
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Browsable(True)>
    <System.ComponentModel.Category("Appearance")>
    <System.ComponentModel.Description("进度条样式")>
    <System.ComponentModel.DefaultValue(GetType(ExperienceBarStyle), "Modern")>
    Public Property Style As ExperienceBarStyle
        Get
            Return _style
        End Get
        Set(value As ExperienceBarStyle)
            _style = value
            Select Case _style
                Case ExperienceBarStyle.Minimal
                    Me.ShowText = False
                    Me.BorderWidth = 1
                Case ExperienceBarStyle.Modern
                    Me.ShowText = True
                    Me.BorderWidth = 2
                Case ExperienceBarStyle.Classic
                    Me.ShowText = True
                    Me.BorderWidth = 1
                Case ExperienceBarStyle.Game
                    Me.ShowText = True
                    Me.BorderWidth = 0
            End Select
            Invalidate()
        End Set
    End Property

    ' 只读属性（不序列化）
    <System.ComponentModel.Browsable(False)>
    Public ReadOnly Property Percentage As Single
        Get
            If _maxValue = _minValue Then Return 0
            Return CSng(_currentValue - _minValue) / CSng(_maxValue - _minValue) * 100.0F
        End Get
    End Property

    ' 构造函数
    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer Or
                 ControlStyles.ResizeRedraw Or
                 ControlStyles.SupportsTransparentBackColor, True)

        Me.Size = New Size(300, 30)
        Me.BackColor = Color.White
        Me.ForeColor = Color.Black
        Me.Font = New Font("Microsoft YaHei UI", 9, FontStyle.Regular)
    End Sub

    ' 重写绘制方法
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        ' 计算可用区域（考虑边框）
        Dim rect As New Rectangle(_borderWidth, _borderWidth,
                                  Me.Width - _borderWidth * 2 - 1,
                                  Me.Height - _borderWidth * 2 - 1)

        ' 绘制背景（白色）
        Using brush As New SolidBrush(Me.BackColor)
            g.FillRectangle(brush, rect)
        End Using

        ' 绘制进度条背景（浅灰色）
        Using brush As New SolidBrush(Color.FromArgb(240, 240, 240))
            g.FillRectangle(brush, rect)
        End Using

        ' 计算进度条宽度
        Dim progressWidth As Integer = CInt(rect.Width * Percentage / 100.0F)

        ' 绘制进度条（黑色渐变）
        If progressWidth > 0 Then
            Dim progressRect As New Rectangle(rect.X, rect.Y, progressWidth, rect.Height)

            Using brush As New LinearGradientBrush(progressRect,
                                                   Color.FromArgb(50, 50, 50),  ' 深灰
                                                   Color.Black,                 ' 纯黑
                                                   LinearGradientMode.Horizontal)
                g.FillRectangle(brush, progressRect)
            End Using

            ' 添加纹理效果（可选）
            DrawProgressTexture(g, progressRect)
        End If

        ' 绘制边框
        Using pen As New Pen(Color.Black, _borderWidth)
            g.DrawRectangle(pen, rect)
        End Using

        ' 绘制文字
        If _showText Then
            DrawProgressText(g, rect)
        End If
    End Sub

    ' 绘制进度纹理
    Private Sub DrawProgressTexture(g As Graphics, rect As Rectangle)
        ' 添加细条纹纹理
        Using pen As New Pen(Color.FromArgb(80, 255, 255, 255), 1)
            For i As Integer = rect.Y To rect.Bottom Step 4
                If i + 2 <= rect.Bottom Then
                    g.DrawLine(pen, rect.X, i, rect.Right, i)
                End If
            Next
        End Using
    End Sub

    ' 绘制进度文字（已修复字体创建）
    Private Sub DrawProgressText(g As Graphics, rect As Rectangle)
        Dim textFormat As New StringFormat()
        textFormat.Alignment = StringAlignment.Center
        textFormat.LineAlignment = StringAlignment.Center

        ' 主要文本：当前经验/最大经验
        Dim mainText As String = $"{_currentValue}/{_maxValue}"

        ' 次要文本：百分比和等级
        Dim subText As String = $"{Percentage:F1}%"

        ' 修复：正确创建字体（使用 Single 类型）
        Dim mainFontSize As Single = Me.Font.Size
        Dim subFontSize As Single = mainFontSize * 0.8F  ' 注意：0.8F 表示 Single 类型

        Dim mainFont As New Font(Me.Font.FontFamily, mainFontSize, FontStyle.Bold)
        Dim subFont As New Font(Me.Font.FontFamily, subFontSize, FontStyle.Regular)

        ' 绘制阴影文字（轻微偏移，增加立体感）
        Using brush As New SolidBrush(Color.FromArgb(100, 0, 0, 0))
            g.DrawString(mainText, mainFont, brush,
                        New RectangleF(rect.X + 1, rect.Y + 1, rect.Width, rect.Height / 2),
                        textFormat)

            g.DrawString(subText, subFont, brush,
                        New RectangleF(rect.X + 1, rect.Y + rect.Height / 2 + 1, rect.Width, rect.Height / 2),
                        textFormat)
        End Using

        ' 绘制主文字
        Using brush As New SolidBrush(Me.ForeColor)
            g.DrawString(mainText, mainFont, brush,
                        New RectangleF(rect.X, rect.Y, rect.Width, rect.Height / 2),
                        textFormat)

            g.DrawString(subText, subFont, brush,
                        New RectangleF(rect.X, rect.Y + rect.Height / 2, rect.Width, rect.Height / 2),
                        textFormat)
        End Using

        ' 释放字体资源
        mainFont.Dispose()
        subFont.Dispose()
    End Sub

    ' 设置经验值（游戏专用方法）
    Public Sub SetExperience(currentExp As Long, level As Integer, expForNextLevel As Long)
        Me._currentValue = CInt(Math.Min(currentExp, Integer.MaxValue))
        Me._maxValue = CInt(Math.Min(expForNextLevel, Integer.MaxValue))
        Me._level = level
        Invalidate()
    End Sub

    ' 动画效果：平滑改变值
    Public Async Sub AnimateTo(newValue As Integer, durationMs As Integer)
        ' 保存当前上下文，确保回到UI线程
        Dim uiContext As TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()

        Dim startValue As Integer = _currentValue
        Dim delta As Integer = newValue - startValue

        If delta = 0 Then Return

        Dim steps As Integer = 30
        Dim stepTime As Integer = durationMs \ steps
        Dim stepValue As Single = delta / steps

        For i As Integer = 1 To steps
            _currentValue = CInt(startValue + stepValue * i)

            ' 在UI线程上更新界面
            Await Task.Run(Sub()
                               ' 这里可以做一些计算
                           End Sub).ContinueWith(Sub()
                                                     Invalidate()
                                                 End Sub, uiContext)

            ' 非阻塞延迟
            Await Task.Delay(stepTime)
        Next

        _currentValue = newValue
        Invalidate()
    End Sub
End Class