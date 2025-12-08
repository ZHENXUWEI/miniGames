Public Module GameManager
    Private s_gameData As clsGameData

    Public Property CurrentGameData As clsGameData
        Get
            Return s_gameData
        End Get
        Set(value As clsGameData)
            s_gameData = value
        End Set
    End Property

    Public ReadOnly Property IsGameDataLoaded As Boolean
        Get
            Return s_gameData IsNot Nothing AndAlso s_gameData.UserID > 0
        End Get
    End Property

    Public Sub ClearGameData()
        s_gameData = Nothing
    End Sub
End Module