Module ModMain

    Public Enum CardSide
        Unset = 0
        Left = 1
        Middle = 2
        Right = 3
    End Enum

    Public ReadOnly CardDeck As ClsDeck
    Public CurrentCard As ClsCard = Nothing
    Public CurrentSide As CardSide = CardSide.Unset

    Private ReadOnly MainWindow As New FrmMain
    Public ShowCards As Boolean

    Public Sub Main()
        Randomize()
        MainWindow.Show()
        Application.Run()
    End Sub

End Module
