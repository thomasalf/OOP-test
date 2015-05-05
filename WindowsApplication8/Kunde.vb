Public Class Kunde
    Inherits Person

    'NB: Har valgt å fjerne rabatten fra "kunde" og overføre rabatt til "booking"
    'Koden som ligger her er altså ikke aktivt i bruk nå.
    Private rabatt As Integer = 0

    Private Const MIN As Integer = 0
    Private Const MAX As Integer = 50 'Antar at ingen får mer enn 50% rabatt

    'Konstruktører

    'Setter alle verdier
    Public Sub New(ByVal fn As String, _
                   en As String, _
                   tlf As String, _
                   pnr As String, _
                   adr As String, _
                   ep As String)
        MyBase.New(fn, en, tlf, pnr, adr, ep)

    End Sub

    'Setter navn og telefonnummer
    Public Sub New(ByVal fn As String, _
               en As String, _
               tlf As String)
        MyBase.New(fn, en, tlf)

    End Sub













    'Get- og Set-funksjoner
    'Public Function getRabatt() As Integer
    '    Return rabatt
    ' End Function

    'Public Sub setRabatt(ByVal rab As Integer)
    '    If rab < MIN Then
    '        rabatt = MIN
    '    ElseIf rab > MAX Then
    '        rabatt = MAX
    '    Else
    '        rabatt = rab
    '    End If
    'End Sub


    'ToString()-funksjon
    ' Public Overrides Function ToString() As String
    '    Return MyBase.ToString() & " Stilling: " & stilling & " Lønnstrinn: " & lønnstrinn
    'End Function


End Class

