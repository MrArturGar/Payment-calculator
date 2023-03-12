using AccountingSystem.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Services.DataAccess
{
    public interface ILoansHub
    {
        ILoan GetLoanById(int loanId); //returns null if object was not found
    }

}
