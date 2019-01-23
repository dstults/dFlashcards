    Dim card0() As String, card1() As String, card2() As String
    Dim cardScore0() As Integer, cardScore1() As Integer, cardScore2() As Integer
    Dim card_total As Integer, card_total0 As Integer, card_total1 As Integer, card_total2 As Integer
    Dim cardOrder() As Integer, current As Integer
    Dim threshold As Integer
    Dim versNum As Single

Option Explicit

Private Sub Form_Load()
    
    versNum = "1.5"
    clearCards
    Me.Caption = "Darren's Flashcards v" & versNum
    
    If Command$ <> "" Then txtFile.Text = Command$
    If txtFile.Text = "" Then
        txtFile.Text = "Click 'Open'"
    Else
        openFile txtFile.Text
    End If
End Sub

Private Function SIDE_TOTAL() As Integer
    SIDE_TOTAL = card_total0 + card_total1 + card_total2
End Function

Private Function CARDORDER_CARD() As Integer
    Dim sub_temp As Integer
    
    For CARDORDER_CARD = 1 To card_total
        If card0(CARDORDER_CARD) <> "" Then sub_temp = sub_temp + 1
        If card1(CARDORDER_CARD) <> "" Then sub_temp = sub_temp + 1
        If card2(CARDORDER_CARD) <> "" Then sub_temp = sub_temp + 1
        If sub_temp >= cardOrder(current) Then Exit For
    Next
End Function

Private Function CARDORDER_SIDE() As Integer
    Dim sub_temp As Integer, card As Integer
    
    For card = 1 To card_total
        If card0(card) <> "" Then sub_temp = sub_temp + 1: If sub_temp = cardOrder(current) Then CARDORDER_SIDE = 0: Exit For
        If card1(card) <> "" Then sub_temp = sub_temp + 1: If sub_temp = cardOrder(current) Then CARDORDER_SIDE = 1: Exit For
        If card2(card) <> "" Then sub_temp = sub_temp + 1: If sub_temp = cardOrder(current) Then CARDORDER_SIDE = 2: Exit For
    Next
End Function

Private Sub cmdOpen_Click()
    Dim app_path As String, curFile As String

    app_path = App.Path
    If Right(app_path, 1) <> "\" Then app_path = app_path & "\"
    comDiag.FileName = ""
    
    comDiag.InitDir = app_path '& "cards\"
    comDiag.Filter = "*.txt"
    comDiag.ShowOpen
    
    If comDiag.FileName = "" Then Exit Sub
    curFile = comDiag.FileName
    
    openFile curFile
    txtFile.Text = curFile
End Sub

Private Sub openFile(file1 As String)
    Dim card As Integer
    
    If Dir(file1) = "" And Dir(file1 & ".txt") <> "" Then file1 = file1 & ".txt"
    If Dir(file1) = "" Then
        MsgBox "File not found."
    ElseIf Dir(file1) <> "" Then
        threshold = 1
        card_total0 = 0: card_total1 = 0: card_total2 = 0
        ReDim card0(0), card1(0), card2(0), cardScore0(0), cardScore1(0), cardScore2(0)
        Open file1 For Input As #1
            Do Until EOF(1)
                card = card + 1
                ReDim Preserve card0(card), card1(card), card2(card), cardScore0(card), cardScore1(card), cardScore2(card)
                Input #1, card0(card), card1(card), card2(card)
                If card0(card) = "" And card1(card) = "" And card2(card) = "" Then
                    card = card - 1
                Else
                    If card0(card) <> "" Then card_total0 = card_total0 + 1
                    If card1(card) <> "" Then card_total1 = card_total1 + 1
                    If card2(card) <> "" Then card_total2 = card_total2 + 1
                End If
            Loop
            card_total = card
        Close #1
        For card = 0 To 4
            order(card).Enabled = True
        Next
        prepareNewDeck
    End If
End Sub

Private Sub prepareNewDeck()
    shuffleCards
    Select Case lblAutoBlock.Caption
        Case "Auto-block: OFF"
            showRetreat
        Case "Auto-block: ON"
            showRestore
    End Select
End Sub

Private Sub updateCardCaption()
    lblCard.Caption = "Card: " & Mid$(Str$(current), 2) & " of " & Mid$(Str$(SIDE_TOTAL), 2) & " T: " & Mid$(Str$(threshold), 2)
End Sub

Private Sub lblAbout_Click()
    MsgBox "Flashity Flash McFlasherson flashcard training program by," & vbNewLine & vbNewLine & "        Darren M. Stults        (drankof@hotmail.com)" & vbNewLine & vbNewLine & "        Updated: 2011-09-13" & vbNewLine & vbNewLine & vbNewLine & "Remember kids:  All your base are belong to us."
End Sub

Private Sub lblAutoBlock_DblClick()
    Select Case lblAutoBlock.Caption
        Case "Auto-block: OFF"
            lblAutoBlock.Caption = "Auto-block: ON"
            showRestore
        Case "Auto-block: ON"
            lblAutoBlock.Caption = "Auto-block: OFF"
            showRetreat
    End Select
End Sub

Private Sub lblCardChk_DblClick(Index As Integer)
    Select Case lblCardChk(Index).Caption
        Case "TEST":    lblCardChk(Index).Caption = "SKIP"
        Case "SKIP":    lblCardChk(Index).Caption = "TEST"
    End Select
End Sub

Private Sub order_Click(Index As Integer)
    Select Case Index
        Case 0 'GO BACK
            showRestore
            prevCard True
        Case 1 'GOOD
            showRestore
            Select Case CARDORDER_SIDE
                Case 0: cardScore0(CARDORDER_CARD) = cardScore0(CARDORDER_CARD) + 1
                Case 1: cardScore1(CARDORDER_CARD) = cardScore1(CARDORDER_CARD) + 1
                Case 2: cardScore2(CARDORDER_CARD) = cardScore2(CARDORDER_CARD) + 1
            End Select
            nextCard True
        Case 2 'SKIP
            showRestore
            nextCard True
        Case 3 'BAD
            showRestore
            Select Case CARDORDER_SIDE
                Case 0: cardScore0(CARDORDER_CARD) = cardScore0(CARDORDER_CARD) - 1
                        If cardScore0(CARDORDER_CARD) < 0 Then cardScore0(CARDORDER_CARD) = 0
                Case 1: cardScore1(CARDORDER_CARD) = cardScore1(CARDORDER_CARD) - 1
                        If cardScore1(CARDORDER_CARD) < 0 Then cardScore1(CARDORDER_CARD) = 0
                Case 2: cardScore2(CARDORDER_CARD) = cardScore2(CARDORDER_CARD) - 1
                        If cardScore2(CARDORDER_CARD) < 0 Then cardScore2(CARDORDER_CARD) = 0
            End Select
            nextCard True
        Case 4 'SHOW
            showRetreat
            clearCards
            showCards CARDORDER_CARD
    End Select
End Sub

Private Sub nextCard(skipKnown As Boolean)
    Dim start0 As Integer, start1 As Integer, counter As Integer
    
    If skipKnown Then
        start0 = current
        Do Until start0 <> current And start1 <> current
            counter = counter + 1
            If current = SIDE_TOTAL Then current = 1 Else current = current + 1
            Select Case CARDORDER_SIDE
                Case 0: If cardScore0(CARDORDER_CARD) >= threshold Or lblCardChk(0) = "SKIP" Then start1 = current
                Case 1: If cardScore1(CARDORDER_CARD) >= threshold Or lblCardChk(1) = "SKIP" Then start1 = current
                Case 2: If cardScore2(CARDORDER_CARD) >= threshold Or lblCardChk(2) = "SKIP" Then start1 = current
            End Select
            If counter = SIDE_TOTAL Then
                counter = 0
                threshold = threshold + 1
                MsgBox "Congratulations!  You've learned all the cards!  Now learn them again."
                shuffleCards
                current = 0
                nextCard True
                Exit Do
            End If
        Loop
    Else
        If current = SIDE_TOTAL Then current = 1 Else current = current + 1
    End If
    updateCardCaption
    clearCards
    Select Case CARDORDER_SIDE
        Case 0: displayCard CARDORDER_SIDE, card0(CARDORDER_CARD)
        Case 1: displayCard CARDORDER_SIDE, card1(CARDORDER_CARD)
        Case 2: displayCard CARDORDER_SIDE, card2(CARDORDER_CARD)
    End Select
End Sub

Private Sub displayCard(side As Integer, cardText As String)
    flash(side).FontSize = 72
    flash(side).Caption = cardText
    
    Do Until flash(side).Height <= 1920 And flash(side).Width <= 3000
        'flash(side).Height = 1920
        flash(side).Width = 3000
        flash(side).FontSize = flash(side).FontSize / 1.2
    Loop
    flash(side).Left = 120 + 3240 * side
    flash(side).Top = 240 + 1920 / 2 - flash(side).Height / 2
    flash(side).Visible = True
End Sub

Private Sub prevCard(skipKnown As Boolean)
    If current = 1 Then current = SIDE_TOTAL Else current = current - 1
        
    updateCardCaption
    clearCards
    Select Case CARDORDER_SIDE
        Case 0: displayCard CARDORDER_SIDE, card0(CARDORDER_CARD)
        Case 1: displayCard CARDORDER_SIDE, card1(CARDORDER_CARD)
        Case 2: displayCard CARDORDER_SIDE, card2(CARDORDER_CARD)
    End Select
End Sub

Private Sub clearCards()
    Dim intA As Integer
    
    For intA = 0 To 2
        flash(intA).Visible = False
        flash(intA).Caption = ""
    Next
End Sub

Private Sub showCards(card As Integer)
    'displayCard flash(0).Caption, card0(card)
    'displayCard flash(1).Caption, card1(card)
    'displayCard flash(2).Caption, card2(card)
    displayCard 0, card0(card)
    displayCard 1, card1(card)
    displayCard 2, card2(card)
End Sub

Private Sub showRestore()
    If lblAutoBlock.Caption = "Auto-block: ON" Then
        order(4).Top = 2880
        order(4).Left = 2160
        order(4).Height = 1455
        order(4).Width = 5655
        order(1).Enabled = False
        order(2).Enabled = False
        order(3).Enabled = False
    End If
End Sub

Private Sub showRetreat()
    order(4).Top = 3000
    order(4).Left = 8160
    order(4).Height = 1215
    order(4).Width = 1575
    order(1).Enabled = True
    order(2).Enabled = True
    order(3).Enabled = True
End Sub

Private Sub shuffleCards()
    Dim side As Integer, pick As Integer, sub_temp As Integer
    
    ReDim cardOrder(SIDE_TOTAL)
    For side = 1 To SIDE_TOTAL
        Do Until sub_temp > side
            pick = Int(Rnd * SIDE_TOTAL + 1)
            For sub_temp = 1 To side
                If cardOrder(sub_temp) = pick Then Exit For
            Next
        Loop
        cardOrder(side) = pick
    Next
    current = 0
    nextCard True
End Sub

Private Sub picHotKeys_KeyPress(KeyAscii As Integer)
    Select Case KeyAscii
        Case 65, 97:   order_Click 0
        Case 83, 115:  order_Click 1
        Case 68, 100:  order_Click 2
        Case 70, 102:  order_Click 3
        Case 32:       order_Click 4
        'Case Else:     MsgBox KeyAscii
    End Select
End Sub

Private Sub tmrSizeFix_Timer()
    Dim side As Integer
    
    For side = 0 To 2
        If flash(side).Visible Then
            If flash(side).Height > 1920 Or flash(side).Width > 3000 Then
                flash(side).Height = 1920
                flash(side).Width = 3000
                flash(side).FontSize = flash(side).FontSize / 1.2
                flash(side).Left = 120 + 3240 * side
                flash(side).Top = 240 + 1920 / 2 - flash(side).Height / 2
            End If
        End If
    Next
End Sub

Private Sub txtFile_KeyPress(KeyAscii As Integer)
    If KeyAscii = 13 Then cmdOpen_Click
End Sub
