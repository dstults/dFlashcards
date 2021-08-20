
Public Class FrmMain

    Private Const versionSplash As String = "2021-08-20 / v1.1.0b"
    'private fG As Graphics = Me.CreateGraphics

    Private showCards As Boolean

    Private currentCard As ClsCard = Nothing
    Private currentSide As CardSide = CardSide.Unset
    Private lastToggledTest As CardSide = CardSide.Unset

    Private testLeft As Boolean = True
    Private testMiddle As Boolean = True
    Private testRight As Boolean = True

    Private alwaysShow As Boolean = False

    Private Sub FrmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub FlashMan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = $"Darren's Flashcards [ {versionSplash} ]"
        OpenFileDialog1.InitialDirectory = Application.StartupPath
        UpdateDisplay()

    End Sub

    Private Sub BtnOpen_Click(sender As Object, e As EventArgs) Handles BtnOpen.Click
        Dim optionPicked As Integer
        optionPicked = OpenFileDialog1.ShowDialog

        If optionPicked = vbCancel Then
            'MsgBox("Operation cancelled!")
            Return
        End If
        If optionPicked <> vbOK Then
            MsgBox("Unknown dialogue operation!" & vbNewLine & "(Code: " & Format(optionPicked, "0") & ")")
            Return
        End If
        If Dir(OpenFileDialog1.FileName) = "" Then
            MsgBox("File not found:" & vbNewLine & OpenFileDialog1.FileName)
            Return
        End If

        CardDeck.LoadCardsFromFile(OpenFileDialog1.FileName)
        If CardDeck.Cards.Count > 0 Then
            currentCard = CardDeck.Cards(0)
            ShowNextCard()
        Else
            currentCard = Nothing
        End If

    End Sub

    Private Sub FrmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case 219
                DoBack()
            Case 221
                DoNext()
            Case 32, 65
                DoShowCards()
            Case 83
                DoGood()
            Case 68
                DoSlow()
            Case 70
                DoBad()
                'Case Else
                'MsgBox("keyCode: " & e.KeyCode & vbNewLine & "keyData: " & e.KeyData & vbNewLine & "keyValue: " & e.KeyValue)
        End Select
    End Sub

    Private Sub BtnShow_Click(sender As Object, e As EventArgs) Handles BtnShow.Click
        DoShowCards()
    End Sub

    Private Sub DoShowCards()
        If BtnShow.Enabled Then
            showCards = True
            UpdateDisplay()
        End If
    End Sub

    Private Sub BtnGood_Click(sender As Object, e As EventArgs) Handles btnGood.Click
        DoGood()
    End Sub

    Private Sub DoGood()
        If btnGood.Enabled Then
            Select Case currentSide
                Case CardSide.Left
                    currentCard.Left_Score += 1
                    If currentCard.Left_Score > 1 Then currentCard.Left_Score = 1
                Case CardSide.Middle
                    currentCard.Middle_Score += 1
                    If currentCard.Middle_Score > 1 Then currentCard.Middle_Score = 1
                Case CardSide.Right
                    currentCard.Right_Score += 1
                    If currentCard.Right_Score > 1 Then currentCard.Right_Score = 1
            End Select
            ShowNextCard()
        End If
    End Sub

    Private Sub BtnSlow_Click(sender As Object, e As EventArgs) Handles btnSlow.Click
        DoSlow()
    End Sub

    Private Sub DoSlow()
        If btnSlow.Enabled Then
            ' Doesn't modify score
            ShowNextCard()
        End If
    End Sub

    Private Sub BtnBad_Click(sender As Object, e As EventArgs) Handles btnBad.Click
        DoBad()
    End Sub

    Private Sub DoBad()
        If btnBad.Enabled Then
            Select Case currentSide
                Case CardSide.Left
                    currentCard.Left_Score -= 1
                    currentCard.Left_BadHistory = True
                    If currentCard.Left_Score < -1 Then currentCard.Left_Score = -1
                Case CardSide.Middle
                    currentCard.Middle_Score -= 1
                    currentCard.Middle_BadHistory = True
                    If currentCard.Middle_Score < -1 Then currentCard.Middle_Score = -1
                Case CardSide.Right
                    currentCard.Right_Score -= 1
                    currentCard.Right_BadHistory = True
                    If currentCard.Right_Score < -1 Then currentCard.Right_Score = -1
            End Select
            ShowNextCard()
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        DoBack()
    End Sub

    Private Sub DoBack()
        If BtnBack.Enabled Then
            Dim nextIndex = CardDeck.Cards.IndexOf(currentCard) - 1
            If nextIndex < 0 Then nextIndex = CardDeck.Cards.Count - 1
            currentCard = CardDeck.Cards(nextIndex)
            UpdateDisplay()
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click
        DoNext()
    End Sub

    Private Sub DoNext()
        If BtnNext.Enabled Then
            Dim nextIndex = CardDeck.Cards.IndexOf(currentCard) + 1
            If nextIndex >= CardDeck.Cards.Count Then nextIndex = 0
            currentCard = CardDeck.Cards(nextIndex)
            UpdateDisplay()
        End If
    End Sub

    Private Sub LblAbout_Click(sender As Object, e As EventArgs) Handles lblAbout.Click
        MsgBox($"Darren's Flashcards! (dFlashcards.exe){vbNewLine}{vbNewLine}Current version:  {versionSplash}{vbNewLine}{vbNewLine}For any questions please contact:{vbNewLine}Darren{vbNewLine}drankof@gmail.com", vbOKOnly, "About")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim fG As Graphics = Me.CreateGraphics
        DrawTiledBG(fG, BlueGridPic, PtOffset, -2, 1)
    End Sub

    Public Sub ShowNextCard()
        showCards = If(alwaysShow, alwaysShow, False)

        Dim nextIndex As Integer = CardDeck.Cards.IndexOf(currentCard)
        Dim nextCard As ClsCard
        Dim nextSides As New HashSet(Of Integer)
        Do
            nextIndex += 1
            If nextIndex = CardDeck.Cards.Count Then nextIndex = 0
            nextCard = CardDeck.Cards(nextIndex)
            If nextCard Is currentCard Then
                MsgBox("You have memorized all the cards!" & vbNewLine & vbNewLine & "Now do it again!", vbOKOnly, "Congratulations!")
                CardDeck.Shuffle()
                CardDeck.Rescore()
            Else
                If testLeft And nextCard.LeftNeedsTesting Then nextSides.Add(CardSide.Left)
                If testMiddle And nextCard.Middle_NeedsTesting Then nextSides.Add(CardSide.Middle)
                If testRight And nextCard.Right_NeedsTesting Then nextSides.Add(CardSide.Right)
                If nextSides.Count > 0 Then
                    currentSide = nextSides(Int(Rnd() * nextSides.Count))
                    currentCard = nextCard
                    Exit Do
                End If
            End If
        Loop
        UpdateDisplay()

    End Sub

    Private Sub DeleteCard(cardID As Integer, ByRef theDeck() As ClsCard, ByRef deckTotal As Integer)
        deckTotal = deckTotal - 1
        For intA As Integer = cardID To deckTotal
            theDeck(intA) = theDeck(intA + 1)
        Next
        ReDim Preserve theDeck(deckTotal)
    End Sub


    Private Sub UpdateDisplay()
        Dim enableButtons As Boolean = currentCard IsNot Nothing
        BtnBack.Visible = enableButtons
        BtnBack.Enabled = enableButtons
        BtnNext.Visible = enableButtons
        BtnNext.Enabled = enableButtons
        BtnShow.Visible = enableButtons AndAlso Not showCards
        BtnShow.Enabled = enableButtons AndAlso Not showCards
        btnGood.Visible = enableButtons
        btnGood.Enabled = enableButtons
        btnSlow.Visible = enableButtons
        btnSlow.Enabled = enableButtons
        btnBad.Visible = enableButtons
        btnBad.Enabled = enableButtons

        BtnAlwaysShow.Enabled = enableButtons
        BtnAlwaysShow.Text = If(alwaysShow, "Always Show", "Hide Sides")

        BtnLeft_Tested.Enabled = enableButtons
        BtnMiddle_Tested.Enabled = enableButtons
        BtnRight_Tested.Enabled = enableButtons
        BtnLeft_Tested.Text = If(testLeft, "TEST", "ignore")
        BtnMiddle_Tested.Text = If(testMiddle, "TEST", "ignore")
        BtnRight_Tested.Text = If(testRight, "TEST", "ignore")

        If Not enableButtons Then
            LblLeft.Text = ""
            LblMid.Text = ""
            LblRight.Text = ""
        Else
            Select Case showCards
                Case True
                    SetLabelText(LblLeft, currentCard.Left)
                    SetLabelText(LblMid, currentCard.Middle)
                    SetLabelText(LblRight, currentCard.Right)
                Case False
                    LblLeft.Text = ""
                    LblMid.Text = ""
                    LblRight.Text = ""
                    Select Case currentSide
                        Case CardSide.Left
                            SetLabelText(LblLeft, currentCard.Left)
                        Case CardSide.Middle
                            SetLabelText(LblMid, currentCard.Middle)
                        Case CardSide.Right
                            SetLabelText(LblRight, currentCard.Right)
                    End Select
            End Select

        End If

        BtnShow.Focus()
        BtnShow.Select()
    End Sub

    Private Sub BtnLeft_Tested_Click(sender As Object, e As EventArgs) Handles BtnLeft_Tested.Click
        lastToggledTest = CardSide.Left
        testLeft = Not testLeft
        CheckTestEnablings()
        UpdateDisplay()
    End Sub

    Private Sub BtnMiddle_Tested_Click(sender As Object, e As EventArgs) Handles BtnMiddle_Tested.Click
        lastToggledTest = CardSide.Middle
        testMiddle = Not testMiddle
        CheckTestEnablings()
        UpdateDisplay()
    End Sub

    Private Sub BtnRight_Tested_Click(sender As Object, e As EventArgs) Handles BtnRight_Tested.Click
        lastToggledTest = CardSide.Right
        testRight = Not testRight
        CheckTestEnablings()
        UpdateDisplay()
    End Sub

    Private Sub CheckTestEnablings()
        ' If all tests are disabled...
        If Not testLeft And Not testMiddle And Not testRight Then
            Select Case lastToggledTest
                Case CardSide.Left
                    testMiddle = Not testMiddle
                    testRight = Not testRight
                Case CardSide.Middle
                    testLeft = Not testLeft
                    testRight = Not testRight
                Case CardSide.Right
                    testLeft = Not testLeft
                    testMiddle = Not testMiddle
            End Select
        End If
    End Sub

    Private Sub BtnAlwaysShow_Click(sender As Object, e As EventArgs) Handles BtnAlwaysShow.Click
        alwaysShow = Not alwaysShow
        showCards = alwaysShow
        UpdateDisplay()
    End Sub

End Class
