Public Class utstyr
    Private utId As String = "" 'Idnr til utstyret
    Private utType As String = "" 'Beskrivelse av type utstyr

    'tester property
    Public Property utstyrId() As String
        Get
            Return utId
        End Get
        Set(ByVal value As String)
            utId = value
        End Set
    End Property

    Public Property utstyrType() As String
        Get
            Return utType
        End Get
        Set(ByVal value As String)
            utType = value
        End Set
    End Property

 



    'Her er en Constructor for å registrere nytt utstyr
    'Public Sub New(ByVal utId As String, _
    'ByVal utType As String)
    '   utstyrsId = utId
    '  utstyrsType = utType
    'End Sub


End Class
