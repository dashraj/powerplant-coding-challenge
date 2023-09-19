
namespace Web.Controllers.Tests.Controllers
{
    [TestClass]
    public class ProductionPlanControllerTests
    {
        private ProductionPlanController? controller;
        private Mock<IProductionPlanManager>? managerMock;

        [TestInitialize]
        public void Setup()
        {
            managerMock = new Mock<IProductionPlanManager>();
            controller = new ProductionPlanController(managerMock.Object);
        }        

        [TestMethod]
        public async Task PostAsync_InvalidInput_ThrowsBadRequestException()
        {
            // Arrange
            controller!.ModelState.AddModelError("errorKey", "ModelErrorMessage");
            var requestDto = new ProductionRequestDto();

            // Act and Assert
            await Assert.ThrowsExceptionAsync<BadHttpRequestException>(async () => await controller.PostAsync(requestDto));
        }

        [TestMethod]
        public async Task PostAsync_ManagerThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var requestDto = new ProductionRequestDto();
            managerMock!.Setup(m => m.GetProductionPlansAsync(requestDto)).ThrowsAsync(new Exception("Simulated Exception"));

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await controller!.PostAsync(requestDto));
        }

        [TestMethod]
        public async Task PostAsync_ValidInput_ReturnsProductionResponse()
        {
            // Arrange
            var requestDto = new ProductionRequestDto();
            var expectedResponse = new List<ProductionResponseDto> { new ProductionResponseDto() };
            managerMock!.Setup(m => m.GetProductionPlansAsync(requestDto)).ReturnsAsync(expectedResponse);

            // Act
            var result = await controller!.PostAsync(requestDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ProductionResponseDto>));
            CollectionAssert.AreEqual(expectedResponse.ToList(), result.ToList());
        }
    }

}