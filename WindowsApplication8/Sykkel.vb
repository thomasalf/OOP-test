Public Class Sykkel
    Private sykkelmerke As String = ""
    Private sykkelmodell As String = ""
    Private sykkeltype As String = ""
    Private sykkeltransportor As String = ""
    Private sykkelhjemsted As String = ""
    Private sykkelstatus As String = ""
    Private sykkelid As String = ""



        'Konstruktører/Constructors

    'Setter alle verdier bortsett fra ID
    Public Sub New(ByVal mer As String, _
                       mdl As String, _
                       typ As String, _
                       tran As String, _
                       hjem As String, _
                       stat As String)
        setSykkelMerke(mer)
        setSykkelModell(mdl)
        setSykkelType(typ)
        setSykkelTransportor(tran)
        setSykkelHjemsted(hjem)
        setSykkelStatus(stat)
    End Sub

    'setter kun sykkelID
    Public Sub New(ByVal id As String)
        setSykkelID(id)
    End Sub

    'Setter alle verdier bortsett fra hjemsted og transportør
    Public Sub New(ByVal mer As String, _
                       mdl As String, _
                       typ As String, _
                       stat As String)
        setSykkelMerke(mer)
        setSykkelModell(mdl)
        setSykkelType(typ)
        setSykkelStatus(stat)
    End Sub

    'Setter alle verdier bortsett fra transportør
    Public Sub New(ByVal mer As String, _
                       mdl As String, _
                       typ As String, _
                       stat As String, _
                       hjem As String)
        setSykkelMerke(mer)
        setSykkelModell(mdl)
        setSykkelType(typ)
        setSykkelStatus(stat)
        setSykkelHjemsted(hjem)
    End Sub




    'Get- og set-funksjoner
    Public Function getSykkelID() As String
        Return sykkelid
    End Function

    Public Sub setSykkelID(ByVal id As String)
        If id.Length > 0 Then 'sjekker at sykkelmodell er skrevet inn
            sykkelid = id
        Else
            Throw New Exception("Ugyldig sykkelID")
        End If
    End Sub

    Public Function getSykkelModell() As String
        Return sykkelmodell
    End Function

    Public Sub setSykkelModell(ByVal mdl As String)
        If mdl.Length > 0 Then 'sjekker at sykkelmodell er skrevet inn
            sykkelmodell = mdl
        Else
            Throw New Exception("Ugyldig sykkelmodell")
        End If
    End Sub



    Public Function getSykkelMerke() As String
        Return sykkelmerke
    End Function

    Public Sub setSykkelMerke(ByVal mer As String)
        If mer.Length > 0 Then 'sjekker at sykkelmerke er skrevet inn
            sykkelmerke = mer
        Else
            Throw New Exception("Ugyldig sykkelmerke")
        End If
    End Sub



    Public Function getSykkelType() As String
        Return sykkeltype
    End Function

    Public Sub setSykkelType(ByVal typ As String)
        If typ.Length > 0 Then 'sjekker at sykkeltypen er skrevet inn
            sykkeltype = typ
        Else
            Throw New Exception("Ugyldig sykkeltype")
        End If
    End Sub




    Public Function getSykkelHjemsted() As String
        Return sykkelhjemsted
    End Function

    Public Sub setSykkelHjemsted(ByVal hjem As String)
        If hjem.Length <> 4 Or IsNumeric(hjem) = False Then 'sjekker at postnummer består av tall og har riktig lengde
            Throw New Exception("Ugyldig postnummer")
        Else
            sykkelhjemsted = hjem
        End If
    End Sub


    Public Function getSykkelTransportor() As String
        Return sykkeltransportor
    End Function

    Public Sub setSykkelTransportor(ByVal tran As String)
        If tran.Length <= 0 Then 'Or IsNumeric(tran) = False Then 'sjekker at transportørID består av tall og har riktig lengde
            Throw New Exception("Ugyldig transportørID")
        Else
            sykkeltransportor = tran
        End If
    End Sub


    Public Function getSykkelStatus() As String
        Return sykkelstatus
    End Function

    Public Sub setSykkelStatus(ByVal stat As String)
        If stat.Length <= 0 Or IsNumeric(stat) = False Then 'sjekker at statusID består av tall og har riktig lengde
            Throw New Exception("Ugyldig statusID")
        Else
            sykkelstatus = stat
        End If
    End Sub



End Class
