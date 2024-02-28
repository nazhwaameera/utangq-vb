Imports UtangQBO
Public Interface IBillRecipient
    Inherits ICrud(Of BillRecipient)
    Function UpdateBillRecipientPaymentStatus(ByVal BillRecipientID As Integer, ByVal NewStatus As Integer) As Integer
End Interface

