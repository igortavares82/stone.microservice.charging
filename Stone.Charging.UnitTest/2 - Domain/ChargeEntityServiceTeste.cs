using Stone.Charging.Domain.Abstractions.EntityService;
using Stone.Charging.Models.Entities;
using Stone.Charging.UnitTest.DataProviders;
using Stone.Charging.UnitTest.Helpers;
using Stone.Framework.Result.Abstractions;
using Stone.Framework.Result.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Stone.Charging.UnitTest
{
    public class ChargeEntityServiceTeste
    {
        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetClients), MemberType = typeof(ChargeDataProvider))]
        public async Task RegisterCharge_SearchCharge_ReturnsTrue(List<Charge> charges, string cpf, short? referenceMonth)
        {
            // Arrange
            IChargeEntityService service = ChargeEntityServiceHelper.GetMock();
            await service.RegisterAsync(charges);

            // Act
            IDomainResult<List<Charge>> result = service.GetAsync(cpf, referenceMonth).Result;

            // Assert
            Assert.NotNull(result.Data);
        }

        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetSearchByReferenceMonth), MemberType = typeof(ChargeDataProvider))]
        public async Task GetCharge_SearchByReferenceMonth_ReturnsTrue(List<Charge> charges, string cpf, short? referenceMonth)
        {
            // Arrange
            IChargeEntityService service = ChargeEntityServiceHelper.GetMock();
            await service.RegisterAsync(charges);
            
            // Act
            IDomainResult<List<Charge>> result = service.GetAsync(cpf, referenceMonth).Result;

            // Assert
            Assert.NotNull(result.Data);
        }

        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetSearchByCpf), MemberType = typeof(ChargeDataProvider))]
        public async Task GetCharge_SearchByCpf_ReturnsTrue(List<Charge> charges, string cpf, short? referenceMonth)
        {
            // Arrange
            IChargeEntityService service = ChargeEntityServiceHelper.GetMock();
            await service.RegisterAsync(charges);

            // Act
            IDomainResult<List<Charge>> result = service.GetAsync(cpf, referenceMonth).Result;

            // Assert
            Assert.NotNull(result.Data);
        }

        [Theory]
        [MemberData(nameof(ChargeDataProvider.GetInvalidSearch), MemberType = typeof(ChargeDataProvider))]
        public async Task GetCharge_InvalidSearch_ReturnsTrue(List<Charge> charges, string cpf, short? referenceMonth)
        {
            // Arrange
            IChargeEntityService service = ChargeEntityServiceHelper.GetMock();
            await service.RegisterAsync(charges);

            // Act
            IDomainResult<List<Charge>> result = service.GetAsync(cpf, referenceMonth).Result;

            // Assert
            Assert.Null(result.Data);
            Assert.True(result.ResultType == DomainResultType.DomainError);
        }
    }
}
