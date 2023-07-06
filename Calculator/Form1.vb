Public Class Form1
    Dim assign_input As Double = 0
    Dim operation As String
    Dim found_expression As Boolean = False
    Dim firstnum, secondnum, q As String

    Private Sub BtnCE_Click(sender As Object, e As EventArgs) Handles BtnCE.Click
        TxtDisplay.Text = "0"
        lblEquation.Text = ""
    End Sub

    Private Sub BtnC_Click(sender As Object, e As EventArgs) Handles BtnC.Click
        TxtDisplay.Text = "0"
    End Sub

    Private Sub Numbers_Click(sender As Object, e As EventArgs) Handles Btn9.Click, Btn8.Click, Btn7.Click, Btn6.Click, Btn5.Click, Btn4.Click, Btn3.Click, Btn2.Click, Btn1.Click, Btn0.Click, BtnPoint.Click
        Dim b As Button = sender
        Dim keyValue As String = b.Text

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
        If (assign_input <> 0) Then
            BtnEquals.PerformClick()
            found_expression = True
            operation = b.Text
            lblEquation.Text = assign_input & "  " & operation
        Else
            operation = b.Text
            assign_input = Double.Parse(TxtDisplay.Text)
            found_expression = True
            lblEquation.Text = assign_input & "  " & operation
        End If
    End Sub

    Private Sub BtnEquals_Click(sender As Object, e As EventArgs) Handles BtnEquals.Click
        lblEquation.Text = assign_input & operation & TxtDisplay.Text & " ="
        Select Case operation
            Case "+"
                TxtDisplay.Text = (assign_input + Double.Parse(TxtDisplay.Text)).ToString()
            Case "-"
                TxtDisplay.Text = (assign_input - Double.Parse(TxtDisplay.Text)).ToString()
            Case "×"
                TxtDisplay.Text = (assign_input * Double.Parse(TxtDisplay.Text)).ToString()
            Case "/"
                TxtDisplay.Text = (assign_input / Double.Parse(TxtDisplay.Text)).ToString()
        End Select
        assign_input = Double.Parse(TxtDisplay.Text)
        operation = ""
    End Sub

    Private Sub BtnPercent_Click(sender As Object, e As EventArgs) Handles BtnPercent.Click
        Dim a As Double
        a = Convert.ToDouble(TxtDisplay.Text) / Convert.ToDouble(100)
        TxtDisplay.Text = System.Convert.ToString(a)
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
        TxtDisplay.Text = System.Convert.ToString(a)
    End Sub

    Private Sub Btn1x_Click(sender As Object, e As EventArgs) Handles Btn1x.Click
        Dim a As Double
        a = Convert.ToDouble(1.0 / Convert.ToDouble(TxtDisplay.Text))
        TxtDisplay.Text = System.Convert.ToString(a)
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Backspace
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

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
            Dim keyDigit As Integer = e.KeyCode - Keys.NumPad0
            Dim digitButton As Button = GetButtonByDigit(keyDigit)
            If digitButton IsNot Nothing Then
                digitButton.PerformClick()
            End If
        ElseIf e.KeyCode = Keys.Decimal Then
            BtnPoint.PerformClick()
        ElseIf e.KeyCode = Keys.Add Then
            BtnPlus.PerformClick()
        ElseIf e.KeyCode = Keys.Subtract Then
            BtnMinus.PerformClick()
        ElseIf e.KeyCode = Keys.Multiply Then
            BtnMultiply.PerformClick()
        ElseIf e.KeyCode = Keys.Divide Then
            BtnDivide.PerformClick()
        ElseIf e.KeyCode = Keys.Enter Then
            BtnEquals.PerformClick()
        End If
    End Sub

    Private Function GetButtonByDigit(digit As Integer) As Button
        Dim buttonName As String = "Btn" & digit
        Return Controls.Find(buttonName, True).FirstOrDefault()
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Enable keyboard events for the form
        Me.KeyPreview = True
    End Sub
End Class
