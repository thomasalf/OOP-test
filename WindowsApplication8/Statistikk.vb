Public Class Statistikk

    'Konstant dagspris for sykkelleie
    Const dagspris As Integer = 500
    Private inntid, uttid As Date

    'Funksjon for å finne prisen for en sykkelbestilling
    'Må bytte ut prosentkonstanter her med tallene fra prisnøkkeltabellen
    Public Function leieprisSykkel(ByVal pris As Double)

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
    Public Function leieprisUtstyr(ByVal utsPris As Double)

        Dim utstyrpris As Double = 0

        utsPris = utstyrpris * (uttid.Day - inntid.Day)
        Return utsPris
    End Function


End Class
