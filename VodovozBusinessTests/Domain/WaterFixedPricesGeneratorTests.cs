using System.Linq;
using NSubstitute;
using NUnit.Framework;
using QS.DomainModel.UoW;
using Vodovoz.Domain;
using Vodovoz.Domain.Goods;
using Vodovoz.EntityRepositories.Goods;

namespace VodovozBusinessTests.Domain {
    [TestFixture]
    public class WaterFixedPricesGeneratorTests {
        
        [Test(Description = "Проверка метода GenerateFixedPricesForAllWater(Nomenclature waterNomenclature, decimal fixedPrice)")]
        public void TestGenerateFixedPricesForAllWaterMethod() {
            // arrange
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            Nomenclature semiozerieMock = Substitute.For<Nomenclature>();
            semiozerieMock.Id.Returns(1);
            Nomenclature snyatogorskayaMock = Substitute.For<Nomenclature>();
            snyatogorskayaMock.Id.Returns(2);
            Nomenclature stroykaMock = Substitute.For<Nomenclature>();
            stroykaMock.Id.Returns(7);
            Nomenclature kislorodnayaMock = Substitute.For<Nomenclature>();
            kislorodnayaMock.Id.Returns(12);
            Nomenclature kislorodnayaDeluxMock = Substitute.For<Nomenclature>();
            kislorodnayaDeluxMock.Id.Returns(655);
            Nomenclature ruchkiMock = Substitute.For<Nomenclature>();
            ruchkiMock.Id.Returns(15);
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetWaterSemiozerie(uowMock).Returns(semiozerieMock);
            nomenclatureRepositoryMock.GetWaterSnyatogorskaya(uowMock).Returns(snyatogorskayaMock);
            nomenclatureRepositoryMock.GetWaterStroika(uowMock).Returns(stroykaMock);
            nomenclatureRepositoryMock.GetWaterKislorodnaya(uowMock).Returns(kislorodnayaMock);
            nomenclatureRepositoryMock.GetWaterKislorodnayaDeluxe(uowMock).Returns(kislorodnayaDeluxMock);
            nomenclatureRepositoryMock.GetWaterRuchki(uowMock).Returns(ruchkiMock);
            nomenclatureRepositoryMock.GetWaterPriceIncrement.Returns(20);
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = new WaterFixedPricesGenerator(nomenclatureRepositoryMock);

            // act
            decimal fixedPrice = 200;
            var dict = 
                waterFixedPricesGeneratorMock.GenerateFixedPricesForAllWater(uowMock, semiozerieMock.Id, fixedPrice);
            
            // assert
            Assert.AreEqual(5, dict.Count);
        }
        
        [Test(Description = "Проверка метода GenerateFixedPricesForAllWater(Nomenclature waterNomenclature, decimal fixedPrice)")]
        public void TestGenerateFixedPricesForAllWaterMethod2() {
            // arrange
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            Nomenclature semiozerieMock = Substitute.For<Nomenclature>();
            semiozerieMock.Id.Returns(1);
            Nomenclature snyatogorskayaMock = Substitute.For<Nomenclature>();
            snyatogorskayaMock.Id.Returns(2);
            Nomenclature stroykaMock = Substitute.For<Nomenclature>();
            stroykaMock.Id.Returns(7);
            Nomenclature kislorodnayaMock = Substitute.For<Nomenclature>();
            kislorodnayaMock.Id.Returns(12);
            Nomenclature kislorodnayaDeluxMock = Substitute.For<Nomenclature>();
            kislorodnayaDeluxMock.Id.Returns(655);
            Nomenclature ruchkiMock = Substitute.For<Nomenclature>();
            ruchkiMock.Id.Returns(15);
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetWaterSemiozerie(uowMock).Returns(semiozerieMock);
            nomenclatureRepositoryMock.GetWaterSnyatogorskaya(uowMock).Returns(snyatogorskayaMock);
            nomenclatureRepositoryMock.GetWaterStroika(uowMock).Returns(stroykaMock);
            nomenclatureRepositoryMock.GetWaterKislorodnaya(uowMock).Returns(kislorodnayaMock);
            nomenclatureRepositoryMock.GetWaterKislorodnayaDeluxe(uowMock).Returns(kislorodnayaDeluxMock);
            nomenclatureRepositoryMock.GetWaterRuchki(uowMock).Returns(ruchkiMock);
            nomenclatureRepositoryMock.GetWaterPriceIncrement.Returns(20);
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = new WaterFixedPricesGenerator(nomenclatureRepositoryMock);

            // act
            decimal fixedPrice = 200;
            var dict = 
                waterFixedPricesGeneratorMock.GenerateFixedPricesForAllWater(uowMock, stroykaMock.Id, fixedPrice);
            
            // assert
            Assert.AreEqual(1, dict.Count);
            Assert.True(dict.Any(x => x.Key == stroykaMock.Id));
        }
    }
}