Public Class Person
    Private fornavn As String = ""
    Private etternavn As String = ""
    Private gateadresse As String = ""
    Private postnummer As String = ""
    Private epost As String = ""
    Private telefon As String = ""



    'Konstruktører/Constructors

    'Setter alle verdier
    Public Sub New(ByVal fn As String, _
                   en As String, _
                   tlf As String, _
                   pnr As String, _
                   adr As String, _
                   ep As String)
        setFornavn(fn)
        setEtternavn(en)
        setGateadresse(adr)
        setPostnummer(pnr)
        setEpost(ep)
        setTelefon(tlf)
    End Sub

    'Setter navn og telefonnummer
    Public Sub New(ByVal fn As String, _
                   en As String, _
                   tlf As String)
        setFornavn(fn)
        setEtternavn(en)
        setTelefon(tlf)
    End Sub

    'Setter navn og e-post
    ' Public Sub New(ByVal fn As String, _
    '                en As String, _
    '                ep As String)
    '     setFornavn(fn)
    '     setEtternavn(en)
    '     setEpost(ep)
    ' End Sub









    'Get- og set-funksjoner
    Public Function getFornavn() As String
        Return fornavn
    End Function

    Public Sub setFornavn(ByVal fn As String)
        If fn.Length > 0 Then 'sjekker at fornavnet er skrevet inn
            fornavn = fn
        Else
            Throw New Exception("Ugyldig fornavn")
        End If
    End Sub



    Public Function getEtternavn() As String
        Return etternavn
    End Function

    Public Sub setEtternavn(ByVal en As String)
        If en.Length > 0 Then 'sjekker at etternavnet er skrevet inn
            etternavn = en
        Else
            Throw New Exception("Ugyldig etternavn")
        End If
    End Sub



    Public Function getGateadresse() As String
        Return gateadresse
    End Function

    Public Sub setGateadresse(ByVal adr As String)
        If adr.Length > 0 Then 'sjekker at gateadressen er skrevet inn
            gateadresse = adr
        Else
            Throw New Exception("Ugyldig gateadresse")
        End If
    End Sub




    Public Function getPostnummer() As String
        Return postnummer
    End Function

    Public Sub setPostnummer(ByVal pnr As String)
        If pnr.Length <> 4 Or IsNumeric(pnr) = False Then 'sjekker at postnummer består av tall og har riktig lengde
            Throw New Exception("Ugyldig postnummer")
        Else
            postnummer = pnr
        End If
    End Sub




    Public Function getEpost() As String
        Return epost
    End Function

    Public Sub setEpost(ByVal ep As String) 'sjekker at e-postadressen inneholder alfakrøll og punktum
        If ep.Length <= 0 _
            Or ep.IndexOf("@") = -1 _
            Or ep.IndexOf(".") = -1 Then
            Throw New Exception("Ugyldig epostadresse")
            'epost = ep
        Else
            epost = ep
            'Throw New Exception("Ugyldig epostadresse")
        End If
    End Sub





    Public Function getTelefon() As String
        Return telefon
    End Function

    Public Sub setTelefon(ByVal tlf As String)
        If tlf.Length <> 8 Or IsNumeric(tlf) = False Then 'sjekker at telefonnummer består av tall og har riktig lengde
            Throw New Exception("Ugyldig telefonnummer")
        Else
            telefon = tlf
        End If
    End Sub






 


    '    Public Sub New(ByVal etfornavn As String, _
    '                   etEtternavn As String, _
    '                   enAdresse As String)
    '        fornavn = etfornavn
    '        etternavn = etEtternavn
    '        adresse = enAdresse
    '    End Sub

    'Tostring()-funksjon
    'Public Overrides Function ToString() As String
    '   Return fornavn & " " & etternavn & ". " & "Født: " & fødselsdato & " Adresse: " & adresse & " Telefon: " & _
    '      telefon & "E-post: " & epost
    'End Function




End Class
