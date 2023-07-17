Public Class Form1
    Dim assign_input As Double = 0
    Dim operation As String
    Dim found_expression As Boolean = False
    Dim firstnum, secondnum, q As String
    Dim previous_result As Double = 0
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
        End Select
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
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
    End Sub

    Private Sub BtnCE_Click(sender As Object, e As EventArgs) Handles BtnCE.Click
        TxtDisplay.Text = "0"
    End Sub

    Private Sub Numbers_Click(sender As Object, e As EventArgs) Handles Btn9.Click, Btn8.Click, Btn7.Click, Btn6.Click, Btn5.Click, Btn4.Click, Btn3.Click, Btn2.Click, Btn1.Click, Btn0.Click, BtnPoint.Click
        Dim b As Button = sender
        Dim keyValue As String = b.Text

        If TxtDisplay.Text.Length >= 16 Then
            Return
        End If

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
    End Sub

    Private Sub operation_Click(sender As Object, e As EventArgs) Handles BtnDivide.Click, BtnMultiply.Click, BtnMinus.Click, BtnPlus.Click
        Dim b As Button = sender

        If operation <> "" Then
            lblEquation.Text = lblEquation.Text.Remove(lblEquation.Text.Length - 2)
        End If

        operation = b.Text

        If previous_result = 0 Then
            assign_input = Double.Parse(TxtDisplay.Text)
        End If

        lblEquation.Text = assign_input & "  " & operation
        found_expression = True
    End Sub

    Private Sub BtnEquals_Click(sender As Object, e As EventArgs) Handles BtnEquals.Click
        Dim rhs As String = TxtDisplay.Text
        Dim result As Double = 0

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

        Select Case operation
            Case "+"
                result = If(hasPerformedCalculation, previous_result, assign_input) + Double.Parse(originalRhs)
            Case "-"
                result = If(hasPerformedCalculation, previous_result, assign_input) - Double.Parse(originalRhs)
            Case "×"
                result = If(hasPerformedCalculation, previous_result, assign_input) * Double.Parse(originalRhs)
            Case "÷"
                result = If(hasPerformedCalculation, previous_result, assign_input) / Double.Parse(originalRhs)
        End Select

        lblEquation.Text = If(hasPerformedCalculation, previous_result, assign_input) & " " & operation & " " & originalRhs & " ="
        TxtDisplay.Text = result.ToString()

        assign_input = If(hasPerformedCalculation, previous_result, assign_input)
        previous_result = result

        hasPerformedCalculation = True
        found_expression = True
    End Sub




    Private Sub BtnPercent_Click(sender As Object, e As EventArgs) Handles BtnPercent.Click
        If operation = "" Then
            TxtDisplay.Text = "0"
        Else
            Dim currentNumber As Double = Double.Parse(TxtDisplay.Text)
            Dim result As Double

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

            lblEquation.Text = assign_input & " " & operation & " " & currentNumber.ToString() & " % ="
            TxtDisplay.Text = result.ToString()

            assign_input = result
            operation = ""
            found_expression = True
        End If
    End Sub

    Private Sub BtnSqrt_Click(sender As Object, e As EventArgs) Handles BtnSqrt.Click
        Dim a As Double
        lblEquation.Text = "√ (" & (TxtDisplay.Text) & ")"
        a = Math.Sqrt(TxtDisplay.Text)
        TxtDisplay.Text = System.Convert.ToString(a)
    End Sub

    Private Sub BtnX2_Click(sender As Object, e As EventArgs) Handles BtnX2.Click
        Dim a As Double
        a = Convert.ToDouble(TxtDisplay.Text) * Convert.ToDouble(TxtDisplay.Text)
        lblEquation.Text = "(" & TxtDisplay.Text & ")^2"
        TxtDisplay.Text = System.Convert.ToString(a)
    End Sub

    Private Sub Btn1x_Click(sender As Object, e As EventArgs) Handles Btn1x.Click
        Dim a As Double
        a = Convert.ToDouble(1.0 / Convert.ToDouble(TxtDisplay.Text))
        lblEquation.Text = "1 / (" & TxtDisplay.Text & ")"
        TxtDisplay.Text = System.Convert.ToString(a)
    End Sub

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

End Class