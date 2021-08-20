Public Class ClsDeck

    Public Cards As New List(Of ClsCard)

    Private _leftTestable As Boolean
    Private _middleTestable As Boolean
    Private _rightTestable As Boolean

    Public Property LeftTestable As Boolean
        Get
            Return _leftTestable
        End Get
        Private Set(value As Boolean)
            _leftTestable = value
        End Set
    End Property

    Public Property MiddleTestable As Boolean
        Get
            Return _middleTestable
        End Get
        Private Set(value As Boolean)
            _middleTestable = value
        End Set
    End Property

    Public Property RightTestable As Boolean
        Get
            Return _rightTestable
        End Get
        Private Set(value As Boolean)
            _rightTestable = value
        End Set
    End Property

    Public Sub Shuffle()
        Dim tempCard As ClsCard
        Dim rng As New Random()
        Dim randomSlot As Integer

        For i = 0 To Cards.Count - 1
            randomSlot = rng.Next(i)
            tempCard = Cards(randomSlot)
            Cards(randomSlot) = Cards(i)
            Cards(i) = tempCard
        Next
    End Sub

    Public Sub LoadCardsFromFile(filePath As String)
        Dim errorCount As Integer = 0
        Dim currentLineNumber As Integer = 0
        Dim fileData As String = IO.File.ReadAllText(filePath)
        Dim fileLines As String() = fileData.Split(vbNewLine)
        Dim newDeck As New List(Of ClsCard)

        For Each line As String In fileLines
            currentLineNumber += 1
            Dim lineData As String() = line.Split("|"c)
            If lineData.Length <= 1 OrElse lineData.Length > 3 Then
                MsgBox($"Invalid Card Data (Line {currentLineNumber}): {vbNewLine}{line}")
                If errorCount >= 3 Then
                    MsgBox("Too many errors, aborting file load!")
                    Exit Sub
                End If
            ElseIf lineData.Length = 2 Then
                newDeck.Add(New ClsCard(lineData(0), lineData(1)))
            ElseIf lineData.Length = 3 Then
                newDeck.Add(New ClsCard(lineData(0), lineData(1), lineData(2)))
            End If
        Next

        If newDeck.Count > 0 Then
            Cards = newDeck
            CardDeck.Shuffle()
            CheckSideTestability()
            CurrentCard = Nothing
        End If

    End Sub

    Public Sub LowerScoreBasedOnHistory()
        For Each card As ClsCard In Cards
            ' Make the next round remember and retest any cards that were troublesome last round.
            If card.Left_BadHistory Then
                card.Left_Score = -1
                card.Left_BadHistory = False
            Else
                card.Left_Score = 0
            End If
            If card.Middle_BadHistory Then
                card.Middle_Score = -1
                card.Middle_BadHistory = False
            Else
                card.Middle_Score = 0
            End If
            If card.Right_BadHistory Then
                card.Right_Score = -1
                card.Right_BadHistory = False
            Else
                card.Right_Score = 0
            End If
        Next
    End Sub

    Private Sub CheckSideTestability()
        ' Reset
        _leftTestable = False
        _middleTestable = False
        _rightTestable = False

        ' Check against cards
        For Each iCard As ClsCard In Cards
            If iCard.Left <> "" Then _leftTestable = True
            If iCard.Middle <> "" Then _middleTestable = True
            If iCard.Right <> "" Then _rightTestable = True
        Next
    End Sub

End Class
