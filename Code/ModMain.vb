Module ModMain

    Public Enum CardSide
        Left = 1
        Middle = 2
        Right = 3
    End Enum

    Public FlashcardDeck As New List(Of ClsCard)
    Public ShuffledDeck As New List(Of ClsCard)
    Public CurrentCard As ClsCard
    Public CurrentSide As Integer = 0

    Public Left_Testable As Boolean = False
    Public Left_Tested As Boolean = True
    Public Middle_Testable As Boolean = False
    Public Middle_Tested As Boolean = True
    Public Right_Testable As Boolean = False
    Public Right_Tested As Boolean = True

    Private MainWindow As New FrmMain
    Public ShowCards As Boolean

    Public Sub Main()
        Randomize()
        ClearCards()
        MainWindow.Show()
        Application.Run()
    End Sub

End Module
