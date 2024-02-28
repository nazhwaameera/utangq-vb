Imports UtangQBO

Public Interface IBillPaymentReport
    Function GetBillPaymentReportById(ByVal id As Integer) As List(Of BillPaymentReport)
End Interface
