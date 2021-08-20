Module ModCardHelper

    Public Sub NextCard()
        ShowCards = False

        Dim nextIndex As Integer = ShuffledDeck.IndexOf(CurrentCard)
        Dim nextCard As ClsCard
        Dim nextSides As New HashSet(Of Integer)
        Do
            nextIndex += 1
            If nextIndex = ShuffledDeck.Count Then nextIndex = 0
            nextCard = ShuffledDeck(nextIndex)
            If nextCard Is CurrentCard Then
                MsgBox("You have memorized all the cards!" & vbNewLine & vbNewLine & "Now do it again!", vbOKOnly, "Congratulations!")
                ShuffleDeck()
            Else
                If Left_Tested And nextCard.LeftNeedsTesting Then nextSides.Add(CardSide.Left)
                If Middle_Tested And nextCard.Middle_NeedsTesting Then nextSides.Add(CardSide.Middle)
                If Right_Tested And nextCard.Right_NeedsTesting Then nextSides.Add(CardSide.Right)
                If nextSides.Count > 0 Then
                    CurrentSide = nextSides(Int(Rnd() * nextSides.Count))
                    CurrentCard = nextCard
                    Exit Do
                End If
            End If
        Loop

    End Sub

    Public Sub ShuffleDeck()

        ShuffledDeck.Clear()
        Dim TempDeck As New List(Of ClsCard)
        For Each iCard As ClsCard In FlashcardDeck
            ' Make the next round remember and retest any cards that were troublesome last round.
            If iCard.Left_BadHistory Then iCard.Left_Score = -1 : iCard.Left_BadHistory = False Else iCard.Left_Score = 0
            If iCard.Middle_BadHistory Then iCard.Middle_Score = -1 : iCard.Middle_BadHistory = False Else iCard.Middle_Score = 0
            If iCard.Right_BadHistory Then iCard.Right_Score = -1 : iCard.Right_BadHistory = False Else iCard.Right_Score = 0
            ' Clone to a new deck that will have its cards shuffled into the shuffled deck.
            TempDeck.Add(iCard)
        Next

        ' Shuffle new clone deck into shuffled deck.
        Dim RandomCard As ClsCard = Nothing
        Do Until TempDeck.Count = 0
            RandomCard = TempDeck(Int(Rnd() * TempDeck.Count))
            ShuffledDeck.Add(RandomCard)
            TempDeck.Remove(RandomCard)
        Loop

    End Sub

    Private Sub DeleteCard(cardID As Integer, ByRef theDeck() As ClsCard, ByRef deckTotal As Integer)
        deckTotal = deckTotal - 1
        For intA As Integer = cardID To deckTotal
            theDeck(intA) = theDeck(intA + 1)
        Next
        ReDim Preserve theDeck(deckTotal)
    End Sub

    Public Sub LoadDeck(filePath As String)
        Dim errorCount As Integer = 0
        Dim currentLineNumber As Integer = 0

        ClearCards()

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
            ShuffledDeck = newDeck
            ShuffleDeck()
        End If

    End Sub

    Public Sub ClearCards()
        FlashcardDeck.Clear()
        ShuffledDeck.Clear()
        CurrentCard = Nothing
    End Sub

End Module
