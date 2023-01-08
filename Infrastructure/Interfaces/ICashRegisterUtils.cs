using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ICashRegisterUtils
    {
        void Init();

        void Reset();
        bool IsValid(InputMoney model);
        bool VerifyAndInsert(Money model);
        
    }
}
