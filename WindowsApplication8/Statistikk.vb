Public Class Statistikk

    'Jeg ønsker her å hente data fra sql'er i StatistikkDAO 
    '(Her må også de riktige verdiene plukkes ut, 
    'putte inn her og få ut riktig pris og utstpris, som igjen kan hentes ut i statistikk

    Private prisnokkel As Integer = 0

    'Setter prisnøkkel 
    'Public Sub New(ByVal pn As integer)
    'setPrisnokkel(pn) - HER FÅR JEG PROBLEM, IKKE ACCESSIBLE
    ' End Sub

    'Get- og set-funksjoner
    Public Function getPrisnokkel() As String
        Return prisnokkel
    End Function



    'Konstant dagspris for sykkelleie
    Const dagspris As Integer = 500
    Private inntid, uttid As Date

    'Funksjon for å finne prisen for en sykkelbestilling
    'Må bytte ut prosentkonstanter her med tallene fra prisnøkkeltabellen
    Public Function leieprisSykkel(ByVal pris As Integer)

        Dim prisnokkel As Integer


        If prisnokkel = 1 Then
            pris = dagspris / 24 * 1.5 * (uttid.Hour - inntid.Hour)
        ElseIf prisnokkel = 2 Then
            pris = dagspris * (uttid.Day - inntid.Day)
        ElseIf prisnokkel = 3 Then
            pris = dagspris * 7 * 0.7 * (uttid.Day - inntid.Day)
        Else
            Throw New Exception("Ugyldig prisnokkel")
        End If

        Return pris
    End Function

    'Funksjon for å hente finne prisen for et utstyrsleie
    'Må hente pris fra tabell pdk_Ekstrautstyr
    Public Function leieprisUtstyr(ByVal utsPris As Integer)

        Dim utstyrpris As Integer = 0

        utsPris = utstyrpris * (uttid.Day - inntid.Day)
        Return utsPris
    End Function

    'Må lage en metode for å få ut totalprisen per booking





End Class
