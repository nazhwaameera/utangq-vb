Imports UtangQBO
Public Interface IBill
    Inherits ICrud(Of Bill)

    Function GetUserBill(ByVal id As Integer) As List(Of Bill)
End Interface
