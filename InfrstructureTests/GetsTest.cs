using Domain;
using Infrastructure.Interfaces;
using Moq;

namespace InfrstructureTests
{
    public class GetsTest
    {
        [Fact]
        public void GetMoneyById()
        {
            //Arrange
            var dummy = new Money(0.50, 3);
            var mockRepository = new Mock<IRepository<Money>>();
            mockRepository.Setup(r => r.GetById(1)).Returns(dummy);
            //Act
            var model = mockRepository.Object.GetById(1);
            //Assert
            Assert.Equal(model, dummy);
            Assert.NotNull(model);
        }

        [Fact]
        public void GetAllMoney()
        {
            //Arrange
            var dummyOne = new Money(0.5, 3);
            var dummyTwo = new Money(0.05, 3);
            var dummyThree = new Money(0.25, 3);
            var dummyList = new List<Money>();
            dummyList.Add(dummyOne);
            dummyList.Add(dummyTwo);
            dummyList.Add(dummyThree);
            var mockRepository = new Mock<IRepository<Money>>();
            mockRepository.Setup(r => r.GetAll()).Returns(dummyList);

            //Act
            var listMoney = mockRepository.Object.GetAll();
            //Assert
            Assert.Equal(listMoney, dummyList);
            Assert.Equal((int)listMoney.Count, 3);
            Assert.NotNull(listMoney);

        }

    }
}