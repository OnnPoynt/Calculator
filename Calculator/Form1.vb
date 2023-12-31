﻿Public Class Form1
    Dim assign_input As Decimal = 0
    Dim operation As String
    Dim found_expression As Boolean = False
    Dim firstnum, secondnum, q As String
    Dim previous_result As Decimal = 0
    Dim hasPerformedCalculation As Boolean = False
    Dim originalRhs As String = ""

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.NumPad0, Keys.D0
                Btn0.PerformClick()
            Case Keys.NumPad1, Keys.D1
                Btn1.PerformClick()
            Case Keys.NumPad2, Keys.D2
                Btn2.PerformClick()
            Case Keys.NumPad3, Keys.D3
                Btn3.PerformClick()
            Case Keys.NumPad4, Keys.D4
                Btn4.PerformClick()
            Case Keys.NumPad5, Keys.D5
                Btn5.PerformClick()
            Case Keys.NumPad6, Keys.D6
                Btn6.PerformClick()
            Case Keys.NumPad7, Keys.D7
                Btn7.PerformClick()
            Case Keys.NumPad8, Keys.D8
                Btn8.PerformClick()
            Case Keys.NumPad9, Keys.D9
                Btn9.PerformClick()
            Case Keys.D1, Keys.OemMinus
                BtnMinus.PerformClick()
            Case Keys.D2, Keys.Oemplus
                BtnPlus.PerformClick()
            Case Keys.D3
                BtnMultiply.PerformClick()
            Case Keys.D4, Keys.OemQuestion
                BtnDivide.PerformClick()
            Case Keys.D5
                If e.Modifiers = Keys.Shift Then
                    BtnPercent.PerformClick()
                End If
            Case Keys.D6
                If e.Modifiers = Keys.Shift Then
                    BtnSqrt.PerformClick()
                Else
                    BtnMultiply.PerformClick()
                End If
            Case Keys.D7
                Btn7.PerformClick()
            Case Keys.D8
                If e.Modifiers = Keys.Shift Then
                    BtnX2.PerformClick()
                Else
                    Btn8.PerformClick()
                End If
            Case Keys.D9
                Btn9.PerformClick()
            Case Keys.Decimal, Keys.OemPeriod
                BtnPoint.PerformClick()
        End Select

        Select Case e.KeyCode
            Case Keys.Add
                BtnPlus.PerformClick()
            Case Keys.Subtract
                BtnMinus.PerformClick()
            Case Keys.Multiply
                BtnMultiply.PerformClick()
            Case Keys.Divide
                BtnDivide.PerformClick()
            Case Keys.Enter
                BtnEquals.PerformClick()
            Case Keys.Back
                BtnBack.PerformClick()
            Case Keys.Delete
                BtnC.PerformClick()
            Case Keys.Escape
                BtnCE.PerformClick()
            Case Keys.I
                BtnHistory.PerformClick()
        End Select
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        AddHandler ListBoxHistory.SelectedIndexChanged, AddressOf ListBoxHistory_SelectedIndexChanged
        panelHistory.Visible = False
        TxtDisplay.ReadOnly = True
    End Sub

    Private Sub TxtDisplay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDisplay.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub BtnC_Click(sender As Object, e As EventArgs) Handles BtnC.Click
        TxtDisplay.Text = "0"
        lblEquation.Text = ""
        operation = ""
        assign_input = 0
        found_expression = False
        previous_result = 0
        originalRhs = 0
        hasPerformedCalculation = False
        secondnum = 0
    End Sub

    Private Sub BtnCE_Click(sender As Object, e As EventArgs) Handles BtnCE.Click
        TxtDisplay.Text = "0"
    End Sub

    Dim numclick As Boolean = False

    Private Sub Numbers_Click(sender As Object, e As EventArgs) Handles Btn9.Click, Btn8.Click, Btn7.Click, Btn6.Click, Btn5.Click, Btn4.Click, Btn3.Click, Btn2.Click, Btn1.Click, Btn0.Click, BtnPoint.Click
        Dim b As Button = sender
        Dim keyValue As String = b.Text

        If operation <> "" Then
            secondnum = secondnum & keyValue
        Else
            If TxtDisplay.Text.Length >= 16 Then
                Return
            End If
        End If

        'secondnum = b.Text

        If ((TxtDisplay.Text = "0") Or (found_expression)) Then
            TxtDisplay.Clear()
            TxtDisplay.Text = keyValue
            found_expression = False
        ElseIf (keyValue = ".") Then
            If (Not TxtDisplay.Text.Contains(".")) Then
                TxtDisplay.Text = TxtDisplay.Text + keyValue
            End If
        Else
            TxtDisplay.Text = TxtDisplay.Text + keyValue
        End If
        FormatNumberIfExceedsMaxLength()

        numclick = True
    End Sub

    Private Sub FormatNumberIfExceedsMaxLength()
        Dim number As Decimal

        If Decimal.TryParse(TxtDisplay.Text, number) Then
            Dim formattedNumber As String = FormatLargeNumber(number)
            TxtDisplay.Text = formattedNumber
        End If
    End Sub

    Private Function FormatLargeNumber(number As Decimal) As String
        Dim formattedNumber As String = number.ToString("G16")

        If formattedNumber.Contains("E+") Then
            Dim parts() As String = formattedNumber.Split("E+")
            Dim coefficient As Decimal = Decimal.Parse(parts(0))
            Dim exponent As Integer = Integer.Parse(parts(1))

            Dim coefficientString As String = coefficient.ToString((coefficient))

            If coefficientString.Length > 16 Then
                coefficientString = coefficient.ToString("E15")
                Return coefficientString
            End If

            formattedNumber = coefficientString & "e+" & exponent.ToString()
        End If

        Return formattedNumber
    End Function



    Private Sub operation_Click(sender As Object, e As EventArgs) Handles BtnDivide.Click, BtnMultiply.Click, BtnMinus.Click, BtnPlus.Click
        Dim b As Button = sender

        If operation <> "" AndAlso secondnum <> "" Then
            Dim result As Decimal = PerformOperation(Decimal.Parse(assign_input), operation, Decimal.Parse(secondnum))

            If Not hasPerformedCalculation Then
                Dim equation As String = lblEquation.Text & " " & secondnum & " = " & result.ToString()
                ListBoxHistory.Items.Add(equation)
            Else
                Dim equation As String = previous_result & " " & operation & " " & secondnum & " = " & result.ToString()
                ListBoxHistory.Items.Add(equation)
            End If

            lblEquation.Text = result & " " & b.Text & " "
            TxtDisplay.Text = result.ToString()
            assign_input = result
        Else
            If previous_result = 0 Then
                assign_input = Decimal.Parse(TxtDisplay.Text)
            End If

            lblEquation.Text = If(hasPerformedCalculation, previous_result, assign_input) & " " & b.Text & " "
        End If
        secondnum = ""
        operation = b.Text
        found_expression = True
        FormatNumberIfExceedsMaxLength()
        numclick = False
    End Sub


    Private Function PerformOperation(lhs As Decimal, operatorSymbol As String, rhs As Decimal) As Decimal
        Select Case operatorSymbol
            Case "+"
                Return lhs + rhs
            Case "-"
                Return lhs - rhs
            Case "×"
                Return lhs * rhs
            Case "÷"
                Return lhs / rhs
            Case Else
                Return 0
        End Select
    End Function

    Private Sub BtnEquals_Click(sender As Object, e As EventArgs) Handles BtnEquals.Click
        Dim rhs As String = TxtDisplay.Text
        Dim result As Decimal = 0

        If operation = "÷" AndAlso rhs = "0" Then
            TxtDisplay.Text = "Cannot divide by 0"
            BtnPlus.Enabled = False
            BtnMinus.Enabled = False
            BtnMultiply.Enabled = False
            BtnDivide.Enabled = False
            BtnPercent.Enabled = False
            BtnPlusMinus.Enabled = False
            BtnPoint.Enabled = False
            BtnX2.Enabled = False
            BtnSqrt.Enabled = False
            Btn1x.Enabled = False
            Return
        End If

        If operation = "" Then
            lblEquation.Text = rhs & " ="
            Return
        End If

        If Not hasPerformedCalculation Then
            originalRhs = rhs
        End If

        If hasPerformedCalculation And numclick Then
            secondnum = originalRhs
            assign_input = Decimal.Parse(TxtDisplay.Text)
            previous_result = TxtDisplay.Text
        End If

        If secondnum = "" Then
            secondnum = assign_input
        End If

        Select Case operation
            Case "+"
                result = If(hasPerformedCalculation, previous_result, assign_input) + Decimal.Parse(secondnum)
            Case "-"
                result = If(hasPerformedCalculation, previous_result, assign_input) - Decimal.Parse(secondnum)
            Case "×"
                result = If(hasPerformedCalculation, previous_result, assign_input) * Decimal.Parse(secondnum)
            Case "÷"
                result = If(hasPerformedCalculation, previous_result, assign_input) / Decimal.Parse(secondnum)
        End Select

        If Not hasPerformedCalculation Then
            Dim equation As String = lblEquation.Text & " " & secondnum & " = " & result.ToString()
            ListBoxHistory.Items.Add(equation)
        Else
            Dim equation As String = previous_result & " " & operation & " " & secondnum & " = " & result.ToString()
            ListBoxHistory.Items.Add(equation)

        End If

        lblEquation.Text = If(hasPerformedCalculation, previous_result, assign_input) & " " & operation & " " & secondnum & " ="
        TxtDisplay.Text = result.ToString()

        assign_input = If(hasPerformedCalculation, previous_result, assign_input)
        previous_result = result

        hasPerformedCalculation = True
        found_expression = True
        FormatNumberIfExceedsMaxLength()
    End Sub


    Private Sub BtnPercent_Click(sender As Object, e As EventArgs) Handles BtnPercent.Click
        If operation = "" Then
            TxtDisplay.Text = "0"
        Else
            Dim currentNumber As Decimal = Decimal.Parse(TxtDisplay.Text)
            Dim result As Decimal

            Select Case operation
                Case "+"
                    result = assign_input + (assign_input * (currentNumber / 100))
                Case "-"
                    result = assign_input - (assign_input * (currentNumber / 100))
                Case "×"
                    result = assign_input * (currentNumber / 100)
                Case "÷"
                    result = assign_input / (currentNumber / 100)
            End Select

            Dim equation As String = assign_input & " " & operation & " " & currentNumber.ToString() & " %"
            lblEquation.Text = equation
            TxtDisplay.Text = result.ToString()

            assign_input = result
            operation = ""
            found_expression = True
            AddToHistory(equation, result)
        End If
    End Sub


    Private Function CalculateSquareRoot(ByVal number As Decimal) As Decimal
        Return Math.Sqrt(number)
    End Function

    Private Function CalculateSquare(ByVal number As Decimal) As Decimal
        Return number * number
    End Function

    Private Function CalculateInverse(ByVal number As Decimal) As Decimal
        Return 1 / number
    End Function


    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        If TxtDisplay.Text.Length > 0 Then
            TxtDisplay.Text = TxtDisplay.Text.Remove(TxtDisplay.Text.Length - 1, 1)
        End If
        If (TxtDisplay.Text = "") Then
            TxtDisplay.Text = "0"
        End If
    End Sub

    Private Sub BtnPlusMinus_Click(sender As Object, e As EventArgs) Handles BtnPlusMinus.Click
        If (TxtDisplay.Text.Contains("-")) Then
            TxtDisplay.Text = TxtDisplay.Text.Remove(0, 1)
        Else
            TxtDisplay.Text = $"-{TxtDisplay.Text}"
        End If
    End Sub

    Private Sub BtnHistory_Click(sender As Object, e As EventArgs) Handles BtnHistory.Click
        panelHistory.Visible = Not panelHistory.Visible

        Dim existingItemsCount As Integer = ListBoxHistory.Items.Count
        Dim newItemsCount As Integer = ListBoxHistory.Items.Count - existingItemsCount

        For i As Integer = 0 To newItemsCount - 1
            Dim item As String = ListBoxHistory.Items(existingItemsCount + i).ToString()
            ListBoxHistory.Items.Add(item)
        Next
    End Sub

    Private Sub BtnSqrt_Click(sender As Object, e As EventArgs) Handles BtnSqrt.Click
        Dim a As Decimal = CalculateSquareRoot(Decimal.Parse(TxtDisplay.Text))
        Dim equation As String = "√(" & TxtDisplay.Text & ")"
        lblEquation.Text = equation
        TxtDisplay.Text = a.ToString()
        AddToHistory(equation, a)
    End Sub


    Private Sub BtnX2_Click(sender As Object, e As EventArgs) Handles BtnX2.Click
        Dim a As Decimal = CalculateSquare(Decimal.Parse(TxtDisplay.Text))
        Dim equation As String = "(" & TxtDisplay.Text & ")²"
        lblEquation.Text = equation
        TxtDisplay.Text = a.ToString()
        AddToHistory(equation, a)
    End Sub


    Private Sub Btn1x_Click(sender As Object, e As EventArgs) Handles Btn1x.Click
        Dim inputNumber As Decimal

        If Decimal.TryParse(TxtDisplay.Text, inputNumber) AndAlso inputNumber <> 0 Then
            Dim result As Decimal = CalculateInverse(inputNumber)
            Dim equation As String = "1 / (" & TxtDisplay.Text & ")"
            TxtDisplay.Text = result.ToString()
            AddToHistory(equation, result)
        Else
            TxtDisplay.Text = "Cannot divide by 0"
        End If
    End Sub

    Private Sub AddToHistory(ByVal equation As String, ByVal result As Decimal)
        Dim equationWithResult As String = equation & " = " & result.ToString()
        ListBoxHistory.Items.Add(equationWithResult)
    End Sub

    Private Sub ListBoxHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxHistory.SelectedIndexChanged
        If ListBoxHistory.SelectedIndex >= 0 Then
            Dim selectedItem As String = ListBoxHistory.SelectedItem.ToString()

            Dim equationParts() As String = selectedItem.Split("=")
            Dim equation As String = equationParts(0).Trim()
            Dim result As String = equationParts(1).Trim()

            hasPerformedCalculation = True

            If equation.Contains("√") Then
                Dim equationNumbers() As String = equation.Split("("c, ")"c)
                Dim firstNum As Decimal = Decimal.Parse(equationNumbers(1))
                assign_input = firstNum
                previous_result = Decimal.Parse(result)
            ElseIf equation.Contains("²") Then
                Dim equationNumbers() As String = equation.Split("("c, ")"c)
                Dim firstNum As Decimal = Decimal.Parse(equationNumbers(1))
                Dim originalValue As Decimal = Decimal.Parse(TxtDisplay.Text)
                assign_input = originalValue
                previous_result = originalValue
            ElseIf equation.Contains("1 /") Then
                Dim equationNumbers() As String = equation.Split("("c, ")"c)
                Dim firstNum As Decimal = Decimal.Parse(equationNumbers(1))
                assign_input = firstNum
                previous_result = Decimal.Parse(result)
            Else
                Dim equationNumbers() As String = equation.Split(" ")
                Dim firstNum As Decimal = Decimal.Parse(equationNumbers(0))
                assign_input = firstNum
                previous_result = Decimal.Parse(result)
            End If

            lblEquation.Text = equation
            TxtDisplay.Text = result

            panelHistory.Visible = False
        End If
    End Sub


    Private Sub BtnClearHistory_Click(sender As Object, e As EventArgs) Handles BtnClearHistory.Click
        ListBoxHistory.Items.Clear()
    End Sub
End Class