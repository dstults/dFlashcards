Public Module ModMain

    Public Enum CardSide
        Unset = 0
        Left = 1
        Middle = 2
        Right = 3
    End Enum

    Public ReadOnly CardDeck As New ClsDeck
    Private ReadOnly MainWindow As New FrmMain

    Public Sub Main()
        Randomize()
        MainWindow.Show()
        Application.Run()
    End Sub

End Module
