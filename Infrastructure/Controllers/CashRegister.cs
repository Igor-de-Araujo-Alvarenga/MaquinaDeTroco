using Domain;
using Infrastructure.Interfaces;

namespace Infrastructure.Controllers
{
    /*
     Classe core do processamento do troco
     */
    public class CashRegister : ICashRegister
    {
        private readonly IRepository<Money> _repo;

        public CashRegister(IRepository<Money> repo)
        {
            _repo = repo;
        }

        public IList<Money> GenerateCash(InputMoney input)
        {
            //Verifica o total do caixa 
            if (!VerifyTotalCash(input))
                return new List<Money>();

            var listMoney = _repo.GetAll();
            var value = input.Input - input.Price;
            var listMoneyUsage = new List<Money>();

            foreach (Money money in listMoney)
            {
                // verifica se tem quantidades para usar
                if (money.Quantity == 0)
                    continue;

                // Verificamos quantas vezes precisamos da moeda
                int requireQuantity = (int)(value / money.MoneyValue);

                if (requireQuantity == 0)
                    continue;

                // Resto do valor após a subtração com 2 casas decimais
                if (requireQuantity <= money.Quantity)
                    value -= Math.Round(money.MoneyValue, 2) * requireQuantity;
                else
                    value -= Math.Round(money.MoneyValue, 2) * money.Quantity;


                value = Math.Round(value, 2);

                //Quantidades das moedas necessarias
                int beforeQuantity = money.Quantity;
                money.Quantity -= requireQuantity;
                var quantityUsage = beforeQuantity - money.Quantity;
                // Cria historico das moedas usadas
                var moneyUsage = new Money(money.MoneyValue, quantityUsage);

                // Caso a quantidade necessaria não for suficiente zeramos o caixa
                if (money.Quantity <= 0 || quantityUsage <= 0)
                {
                    moneyUsage.Quantity = beforeQuantity;
                    money.Quantity = 0;
                    _repo.Update(money);
                }
                else
                {
                    _repo.Update(money);
                }


                listMoneyUsage.Add(moneyUsage);
                if (value <= 0)
                    return listMoneyUsage;
            }

            return new List<Money>();
        }


        public bool VerifyTotalCash(InputMoney input)
        {
            double change = input.Input - input.Price;
            var listMoney = _repo.GetAll();
            double total = 0;
            foreach (var money in listMoney)
            {
                total += money.MoneyValue * money.Quantity;
            }

            if (total >= change)
            {
                return true;
            }
            return false;
        }

    }
}
