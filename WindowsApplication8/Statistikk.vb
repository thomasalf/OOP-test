Public Class Statistikk

    'Kan brukes for å få ut avanse, men legger foreløpig inn fjorårets tall
    Private prisnokkel As Integer = 0
    Private omsetning As Integer
    Private resultat As Integer = 1300000
    Private utgifter As Integer


    'Konstruktør
    'Setter alle verdier
    Public Sub New(ByVal pn As Integer, _
                   oms As Integer, _
                   res As Integer, _
                   utg As Integer)
        setPrisnokkel(pn)
        setOmsetning(oms)
        setResultat(res)
        setUtgifter(utg)
    End Sub


    'Get- og set-funksjoner
    Public Function getPrisnokkel() As String
        Return prisnokkel
    End Function

    Public Sub setPrisnokkel(ByVal pn As Integer)
        prisnokkel = pn
    End Sub



    Public Function getOmsetning() As String
        Return omsetning
    End Function

    Public Sub setOmsetning(ByVal oms As Integer)
        omsetning = oms
    End Sub



    Public Function getResultat() As String
        Return resultat
    End Function

    Public Sub setResultat(ByVal res As Integer)
        resultat = res
    End Sub



    Public Function getUtgifter() As String
        Return utgifter
    End Function

    Public Sub setUtgifter(ByVal utg As Integer)
        utgifter = utg
    End Sub



    'Funksjon for å hente ut foreløpig resultat
    Public Function visUtgifter(ByVal omsetning As Integer, ByVal resultat As Integer)

        utgifter = omsetning - resultat
        Return utgifter
    End Function





    'Dagspris for sykkelleie
    Private dagspris As Integer = 500
    Private inntid, uttid As Date

    'Funksjon for å finne prisen for en sykkelbestilling
    'Hente tallene fra prisnøkkeltabellen
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
    'Hente pris fra tabell pdk_Ekstrautstyr
    Public Function leieprisUtstyr(ByVal utsPris As Integer)

        Dim utstyrpris As Integer = 0

        utsPris = utstyrpris * (uttid.Day - inntid.Day)
        Return utsPris
    End Function


End Class
