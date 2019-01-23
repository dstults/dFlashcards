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
                If Left_Tested And nextCard.Left_NeedsTesting Then nextSides.Add(CardSide.Left)
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

    Public Sub LoadFile(theFile As String)
        Dim fileNum As Integer, strTemp As String, intX As Integer
        Dim lineCnt As Integer, errCnt As Integer

        ClearCards()

        fileNum = FreeFile()
        intX = 0

        FileOpen(fileNum, theFile, OpenMode.Input)
        Do Until EOF(fileNum)
            strTemp = LineInput(fileNum)
            lineCnt = lineCnt + 1
            If Not AddCard(strTemp) Then
                ' If unable to parse card data
                errCnt = errCnt + 1
                MsgBox("Invalid Card Data (Line " & Format(lineCnt, "0") & "):" & vbNewLine & strTemp)
                If errCnt >= 3 Then
                    MsgBox("Too many errors, aborting file load!")
                    ClearCards()
                    Exit Sub
                End If
            End If

        Loop

        FileClose(fileNum)

        If FlashcardDeck.Count > 0 Then
            ShuffleDeck()
            CurrentCard = ShuffledDeck(0)
        End If

    End Sub

    Private Function AddCard(inString As String) As Boolean
        Dim parsedString() As String
        parsedString = Split(inString, "|")
        If parsedString.Length = 2 Then
            FlashcardDeck.Add(New ClsCard)
            If parsedString(0) <> "" Then FlashcardDeck.Last.Left = parsedString(0)
            If parsedString(1) <> "" Then FlashcardDeck.Last.Right = parsedString(1)
            Return True
        ElseIf parsedString.Length = 3 Then
            FlashcardDeck.Add(New ClsCard)
            If parsedString(0) <> "" Then FlashcardDeck.Last.Left = parsedString(0)
            If parsedString(1) <> "" Then FlashcardDeck.Last.Middle = parsedString(1)
            If parsedString(2) <> "" Then FlashcardDeck.Last.Right = parsedString(2)
            Return True
        ElseIf inString = "" Then
            Return True
            Beep()
        Else
            Return False
        End If

    End Function

    Public Sub ClearCards()
        FlashcardDeck.Clear()
        ShuffledDeck.Clear()
        CurrentCard = Nothing
    End Sub

End Module
