using AccountingSystem.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Services.DataAccess
{
    public class LoanRepository : ILoansHub, IDisposable
    {
        private bool disposed = false;
        public ILoan GetLoanById(int loanId)
        {
            if (loanId == 2)
                return null;

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                // TODO
            }

            disposed = true;
        }

        ~LoanRepository()
        {
            Dispose(false);
        }
    }
}
