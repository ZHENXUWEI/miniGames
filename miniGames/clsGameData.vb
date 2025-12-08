Imports MySqlConnector
Imports System.Data

Public Class clsGameData
    ' 替换VB6的clsDatabase为迁移后的数据库类
    Private m_db As New clsDatabase()

    ' 成员变量改用.NET自动属性，移除VB6的Let/Get语法
    Public Property UserID As Long = 0
    Public Property Username As String = ""
    Public Property GameLevel As Integer = 1
    Public Property Experience As Long = 0
    Public Property GoldCoins As Long = 1000
    Public Property WarehouseLevel As Integer = 1
    Public Property WorkbenchLevel As Integer = 1
    Public Property LastLoginTime As Date = Date.Now
    Public Property TotalOnlineMinutes As Long = 0
    Public Property LastError As String = ""

    ' 替换VB6的Class_Initialize
    Public Sub New()
        ClearGameData()
    End Sub

    ' 清空游戏数据（保留原有逻辑）
    Private Sub ClearGameData()
        UserID = 0
        Username = ""
        GameLevel = 1
        Experience = 0
        GoldCoins = 1000
        WarehouseLevel = 1
        WorkbenchLevel = 1
        LastLoginTime = Date.Now
        TotalOnlineMinutes = 0
        LastError = ""
    End Sub

    ' 初始化游戏数据（核心方法，适配ADO.NET）
    Public Function InitializeGameData(userId As Long, username As String) As Boolean
        Try
            ' 修正：将参数赋值给类的成员变量
            Me.UserID = userId
            Me.Username = username

            ' 加载已有游戏数据，无数据则创建新记录
            If Not LoadGameDataFromDB() Then
                If Not CreateNewGameData() Then
                    LastError = "创建游戏数据失败"
                    Return False
                End If
            End If

            UpdateLastLoginTime()
            UpdateDailyStatistics()
            LastError = "游戏数据初始化完成"
            Return True
        Catch ex As Exception
            LastError = $"初始化游戏数据出错: {ex.Message}"
            Return False
        End Try
    End Function

    ' 从数据库加载游戏数据（替换Recordset为DataTable）
    Private Function LoadGameDataFromDB() As Boolean
        Try
            Dim sql As String = $"SELECT game_level, experience, gold_coins, warehouse_level, workbench_level, last_login_time, total_online_minutes FROM vb_game_data WHERE user_id = {UserID}"
            Dim dt As DataTable = m_db.ExecuteQuery(sql)

            If dt Is Nothing Or dt.Rows.Count = 0 Then
                Return False
            End If

            ' 读取DataTable数据，替换原Recordset取值逻辑
            Dim row = dt.Rows(0)
            GameLevel = If(IsDBNull(row("game_level")), 1, CInt(row("game_level")))
            Experience = If(IsDBNull(row("experience")), 0, CLng(row("experience")))
            GoldCoins = If(IsDBNull(row("gold_coins")), 1000, CLng(row("gold_coins")))
            WarehouseLevel = If(IsDBNull(row("warehouse_level")), 1, CInt(row("warehouse_level")))
            WorkbenchLevel = If(IsDBNull(row("workbench_level")), 1, CInt(row("workbench_level")))
            LastLoginTime = If(IsDBNull(row("last_login_time")), Date.Now, CDate(row("last_login_time")))
            TotalOnlineMinutes = If(IsDBNull(row("total_online_minutes")), 0, CLng(row("total_online_minutes")))

            Return True
        Catch ex As Exception
            LastError = $"加载游戏数据出错: {ex.Message}"
            Return False
        End Try
    End Function

    ' 创建新游戏数据（适配ADO.NET的增删改逻辑）
    Private Function CreateNewGameData() As Boolean
        Try
            Dim sql As String = $"INSERT INTO vb_game_data (user_id, game_level, experience, gold_coins, warehouse_level, workbench_level, last_login_time, total_online_minutes) VALUES ({UserID}, {GameLevel}, {Experience}, {GoldCoins}, {WarehouseLevel}, {WorkbenchLevel}, NOW(), {TotalOnlineMinutes})"
            If m_db.ExecuteNonQuery(sql) Then
                LastError = "创建新的游戏数据成功"
                Return True
            Else
                LastError = $"创建游戏数据失败: {m_db.LastError}"
                Return False
            End If
        Catch ex As Exception
            LastError = $"创建游戏数据出错: {ex.Message}"
            Return False
        End Try
    End Function

    ' 保存游戏数据（保留原有判断逻辑）
    Public Function SaveGameData() As Boolean
        Try
            ' 检查记录是否存在
            Dim checkSql As String = $"SELECT COUNT(*) as cnt FROM vb_game_data WHERE user_id = {UserID}"
            Dim dt As DataTable = m_db.ExecuteQuery(checkSql)
            Dim recordExists As Boolean = dt.Rows(0)("cnt") > 0

            Dim sql As String = ""
            If recordExists Then
                sql = $"UPDATE vb_game_data SET game_level = {GameLevel}, experience = {Experience}, gold_coins = {GoldCoins}, warehouse_level = {WarehouseLevel}, workbench_level = {WorkbenchLevel}, last_login_time = NOW(), total_online_minutes = {TotalOnlineMinutes} WHERE user_id = {UserID}"
            Else
                sql = $"INSERT INTO vb_game_data (user_id, game_level, experience, gold_coins, warehouse_level, workbench_level, last_login_time, total_online_minutes) VALUES ({UserID}, {GameLevel}, {Experience}, {GoldCoins}, {WarehouseLevel}, {WorkbenchLevel}, NOW(), {TotalOnlineMinutes})"
            End If

            If m_db.ExecuteNonQuery(sql) Then
                LastError = "游戏数据保存成功"
                Return True
            Else
                LastError = $"保存游戏数据失败: {m_db.LastError}"
                Return False
            End If
        Catch ex As Exception
            LastError = $"保存游戏数据出错: {ex.Message}"
            Return False
        End Try
    End Function

    ' 以下为业务方法（保留原有逻辑，仅适配语法）
    Private Sub UpdateLastLoginTime()
        Try
            Dim sql As String = $"UPDATE vb_game_data SET last_login_time = NOW() WHERE user_id = {UserID}"
            m_db.ExecuteNonQuery(sql)
        Catch
            ' 忽略非致命错误
        End Try
    End Sub

    Private Sub UpdateDailyStatistics()
        Try
            If Not TableExists("vb_game_statistics") Then Exit Sub
            Dim today As String = Date.Now.ToString("yyyy-MM-dd")
            Dim sql As String = $"INSERT INTO vb_game_statistics (user_id, stat_date, login_count) VALUES ({UserID}, '{today}', 1) ON DUPLICATE KEY UPDATE login_count = login_count + 1"
            m_db.ExecuteNonQuery(sql)
        Catch
            ' 忽略非致命错误
        End Try
    End Sub

    Public Sub AddOnlineMinutes(minutes As Integer)
        If minutes <= 0 Then Exit Sub
        TotalOnlineMinutes += minutes
        UpdatePlayTimeToday(minutes)
        SaveGameData()
    End Sub

    Private Sub UpdatePlayTimeToday(minutes As Integer)
        Try
            If Not TableExists("vb_game_statistics") Then Exit Sub
            Dim today As String = Date.Now.ToString("yyyy-MM-dd")
            Dim sql As String = $"INSERT INTO vb_game_statistics (user_id, stat_date, play_minutes) VALUES ({UserID}, '{today}', {minutes}) ON DUPLICATE KEY UPDATE play_minutes = play_minutes + {minutes}"
            m_db.ExecuteNonQuery(sql)
        Catch
            ' 忽略非致命错误
        End Try
    End Sub

    Public Function AddExperience(exp As Long) As Boolean
        If exp <= 0 Then
            LastError = "经验值必须为正数"
            Return False
        End If
        Experience += exp
        CheckLevelUp()
        UpdateStatToday(exp, "experience")
        LastError = $"获得经验值: {exp}"
        Return True
    End Function

    Private Sub CheckLevelUp()
        Dim requiredExp As Long = GameLevel * 1000
        If Experience >= requiredExp Then
            GameLevel += 1
            Experience -= requiredExp
            GoldCoins += GameLevel * 100
            LastError = $"恭喜升级！当前等级: {GameLevel}"
        End If
    End Sub

    Public Function AddGoldCoins(amount As Long) As Boolean
        If amount <= 0 Then
            LastError = "金币数量必须为正数"
            Return False
        End If
        GoldCoins += amount
        UpdateStatToday(amount, "gold_earned")
        LastError = $"获得金币: {amount}"
        Return True
    End Function

    Public Function SpendGoldCoins(amount As Long) As Boolean
        If amount <= 0 Then
            LastError = "消费金额必须为正数"
            Return False
        End If
        If GoldCoins < amount Then
            LastError = $"金币不足，当前金币: {GoldCoins}"
            Return False
        End If
        GoldCoins -= amount
        UpdateStatToday(amount, "gold_spent")
        LastError = $"消费金币: {amount}，剩余金币: {GoldCoins}"
        Return True
    End Function

    Public Function UpgradeWarehouse() As Boolean
        Dim upgradeCost As Long = WarehouseLevel * 500
        If GoldCoins < upgradeCost Then
            LastError = $"金币不足！升级需要 {upgradeCost} 金币，当前只有 {GoldCoins}"
            Return False
        End If
        If WarehouseLevel >= 20 Then
            LastError = "仓库已达到最高等级"
            Return False
        End If
        If Not SpendGoldCoins(upgradeCost) Then Return False
        WarehouseLevel += 1
        GoldCoins += 200
        LastError = $"仓库升级成功！当前等级: {WarehouseLevel}，升级奖励: 200金币"
        Return True
    End Function

    Public Function UpgradeWorkbench() As Boolean
        Dim upgradeCost As Long = WorkbenchLevel * 800
        If GoldCoins < upgradeCost Then
            LastError = $"金币不足！升级需要 {upgradeCost} 金币，当前只有 {GoldCoins}"
            Return False
        End If
        If WorkbenchLevel >= 15 Then
            LastError = "工作站已达到最高等级"
            Return False
        End If
        If Not SpendGoldCoins(upgradeCost) Then Return False
        WorkbenchLevel += 1
        Dim expReward As Long = WorkbenchLevel * 50
        Experience += expReward
        GoldCoins += 300
        CheckLevelUp()
        LastError = $"工作站升级成功！当前等级: {WorkbenchLevel}，获得经验: {expReward}，升级奖励: 300金币"
        Return True
    End Function

    Private Sub UpdateStatToday(amount As Long, statType As String)
        Try
            If Not TableExists("vb_game_statistics") Then Exit Sub
            Dim today As String = Date.Now.ToString("yyyy-MM-dd")
            Dim sql As String = ""
            Select Case statType
                Case "gold_earned"
                    sql = $"INSERT INTO vb_game_statistics (user_id, stat_date, gold_earned) VALUES ({UserID}, '{today}', {amount}) ON DUPLICATE KEY UPDATE gold_earned = gold_earned + {amount}"
                Case "gold_spent"
                    sql = $"INSERT INTO vb_game_statistics (user_id, stat_date, gold_spent) VALUES ({UserID}, '{today}', {amount}) ON DUPLICATE KEY UPDATE gold_spent = gold_spent + {amount}"
                Case "experience"
                    Exit Sub
            End Select
            m_db.ExecuteNonQuery(sql)
        Catch
            ' 忽略非致命错误
        End Try
    End Sub

    Public Function GetWarehouseCapacity() As Long
        Return 1000 * WarehouseLevel
    End Function

    Public Function GetWorkbenchEfficiency() As Double
        Return 1.0 * (1 + (WorkbenchLevel - 1) * 0.1)
    End Function

    Public Function DoWorkbenchWork() As Boolean
        Dim workCost As Long = 50
        If GoldCoins < workCost Then
            LastError = $"金币不足，无法工作！需要 {workCost} 金币"
            Return False
        End If
        If Not SpendGoldCoins(workCost) Then Return False
        Dim expReward As Long = 100 * WorkbenchLevel
        Dim goldReward As Long = 200 * WorkbenchLevel
        AddExperience(expReward)
        AddGoldCoins(goldReward)
        LastError = $"工作完成！{vbCrLf}消耗金币: {workCost}{vbCrLf}获得经验: {expReward}{vbCrLf}获得金币: {goldReward}"
        Return True
    End Function

    Public Function GetGameInfo() As String
        Return $"=== 游戏信息 ==={vbCrLf}" &
               $"用户: {Username} (ID: {UserID}){vbCrLf}" &
               $"等级: {GameLevel}级{vbCrLf}" &
               $"经验: {Experience}{vbCrLf}" &
               $"金币: {GoldCoins}{vbCrLf}" &
               $"仓库等级: {WarehouseLevel}级 (容量: {GetWarehouseCapacity()}){vbCrLf}" &
               $"工作站等级: {WorkbenchLevel}级 (效率: {GetWorkbenchEfficiency():0.0}x){vbCrLf}" &
               $"总游戏时长: {TotalOnlineMinutes}分钟{vbCrLf}" &
               $"最后登录: {LastLoginTime:yyyy-MM-dd HH:mm:ss}"
    End Function

    Public Function GetGameStatus() As String
        Return $"Lv.{GameLevel} | 金币: {GoldCoins} | 仓库: Lv.{WarehouseLevel} | 工作站: Lv.{WorkbenchLevel}"
    End Function

    ' 检查表是否存在（适配ADO.NET）
    Private Function TableExists(tableName As String) As Boolean
        Try
            Dim sql As String = $"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = '{m_db.EscapeSQL(tableName)}'"
            Dim dt As DataTable = m_db.ExecuteQuery(sql)
            Return dt.Rows(0)(0) > 0
        Catch
            Return False
        End Try
    End Function
End Class