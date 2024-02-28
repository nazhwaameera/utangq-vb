Imports UtangQBO

Public Interface IBillRecipientStatusReport
    Function GetBillRecipientStatusReportById(ByVal id As Integer) As List(Of BillRecipientStatusReport)
End Interface
