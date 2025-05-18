using AuroraLib.AI;

namespace Aurora.Test.IntegrationTest
{
    public class AiModelFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnAiModelInstance()
        {
            // Arrange

            // Act
            var aiModel = AiModelFactory.Create();

            // Assert
            Assert.NotNull(aiModel);
        }
        [Fact]
        public void ListModels_ShouldReturnListOfModels()
        {
            // Arrange
            var aiModel = AiModelFactory.Create();

            // Act
            var models = aiModel.ListModels();

            // Assert
            Assert.NotNull(models);
            Assert.NotEmpty(models);
        }
    }
}
