Public Interface ICrud(Of T)
    Function Create(ByVal obj As T) As Integer
    Function GetById(ByVal id As Integer) As T
    Function GetAll() As List(Of T)

End Interface
