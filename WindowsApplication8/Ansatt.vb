Public Class Ansatt
    Inherits Person

    Private lonn As Integer = 0

    'Konstruktører

    'Setter alle verdier + lønn
    Public Sub New(ByVal fn As String, _
                   en As String, _
                   tlf As String, _
                   pnr As String, _
                   adr As String, _
                   ep As String, _
                   ln As Integer)
        MyBase.New(fn, en, tlf, pnr, adr, ep)
        setLonn(ln)
    End Sub


    Public Function getLonn() As String
        Return lonn
    End Function

    Public Sub setLonn(ByVal ln As Integer)
        If IsNumeric(ln) = False Then 'sjekker at lønn er skrevet inn med siffer
            lonn = ln
        Else
            Throw New Exception("Ugyldig lønn")
        End If
    End Sub

End Class
