using Moq;
using softdesign_test_domain.Interfaces.Repositories;
using softdesign_test_domain.Models.DTOs;
using softdesign_test_domain.Models.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace softdesign_test_repository_test
{
    public class ApplicationRepositoryTest : BaseTests
    {
        protected readonly Mock<IApplicationRepository> _applicationRepositoryMocked;
        protected IApplicationRepository _applicationRepository => _applicationRepositoryMocked.Object;

        public ApplicationRepositoryTest()
        {
            _applicationRepositoryMocked = new Mock<IApplicationRepository>();
        }

        [Fact]
        public void SHOULD_RETURN_EMPTY_LIST()
        {
            _applicationRepositoryMocked.Setup(o => o.Get()).Returns(new List<ApplicationEntity>());

            var list = _applicationRepository.Get();

            Assert.NotNull(list);
            Assert.Empty(list);
        }

        [Fact]
        public void SHOULD_RETURN_FOUR_ITEMS()
        {
            var expectedList = BuildCollection();

            _applicationRepositoryMocked.Setup(o => o.Get()).Returns(expectedList);

            var list = _applicationRepository.Get();

            Assert.NotNull(list);
            Assert.NotEmpty(list);
            Assert.Equal(4, list.Count);
        }

        [Fact]
        public async void SHOULD_BRING_ITEM_FILTERING_BY_ID()
        {
            var id = "5f6ab45e69e7cee395eb52b7";
            var expectedList = BuildCollection();

            _applicationRepositoryMocked
                .Setup(o => o.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(expectedList.FirstOrDefault(o => o.Id == id)));

            var item = await _applicationRepository.Get(id);

            Assert.NotNull(item);
            Assert.Equal(id, item.Id);
            Assert.Equal("https://softdesign.gupy.io/", item.Url);
            Assert.Equal(string.Empty, item.PathLocal);
            Assert.Equal(1, item.Application);
            Assert.False(item.DebuggingMode);
        }

        [Fact]
        public async void SHOULD_INSERT_ITEM_INTO_COLLECTION()
        {
            var entity = new ApplicationEntity
            {
                Id = "5f6ab902b4182bb4dfe711f8",
                Url = "https://github.com",
                PathLocal = string.Empty,
                Application = 1,
                DebuggingMode = false
            };

            var expectedList = BuildCollection();

            _applicationRepositoryMocked.Setup(o => o.InsertAsync(It.IsAny<ApplicationEntity>())).Callback(() =>
            {
                expectedList.Add(entity);
            });

            await _applicationRepository.InsertAsync(entity);

            Assert.NotNull(expectedList);
            Assert.NotEmpty(expectedList);
            Assert.Equal(5, expectedList.Count);
        }

        [Fact]
        public async void SHOULD_UPDATE_ITEM_INTO_COLLECTION()
        {
            var applicationEntity = new ApplicationEntity { Id = "5f6ab45e69e7cee395eb52b7" };

            applicationEntity.Map(new ApplicationDTO
            {
                Url = "https://softdesign.gupy.io/",
                PathLocal = "/carreira",
                Application = 1,
                DebuggingMode = true
            });

            var expectedList = BuildCollection();

            _applicationRepositoryMocked.Setup(o => o.UpdateAsync(It.IsAny<string>(), It.IsAny<ApplicationEntity>())).Callback(() =>
            {
                var entity = expectedList.First(o => o.Id == applicationEntity.Id);

                expectedList.Remove(entity);
                expectedList.Add(applicationEntity);
            });

            await _applicationRepository.UpdateAsync(applicationEntity.Id, applicationEntity);

            _applicationRepositoryMocked
                .Setup(o => o.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(expectedList.FirstOrDefault(o => o.Id == applicationEntity.Id)));

            var item = await _applicationRepository.Get(applicationEntity.Id);

            Assert.NotNull(item);
            Assert.Equal(applicationEntity.Id, item.Id);
            Assert.Equal("https://softdesign.gupy.io/", item.Url);
            Assert.Equal("/carreira", item.PathLocal);
            Assert.Equal(1, item.Application);
            Assert.True(item.DebuggingMode);
        }

        [Fact]
        public async void SHOULD_DELETE_FROM_COLLECTION()
        {
            var id = "5f6ab450d649375ee98758a9";
            var expectedList = BuildCollection();

            _applicationRepositoryMocked.Setup(o => o.DeleteAsync(id)).Callback(() =>
            {
                var entity = expectedList.First(o => o.Id == id);

                expectedList.Remove(entity);
            });

            await _applicationRepository.DeleteAsync(id);

            Assert.NotNull(expectedList);
            Assert.NotEmpty(expectedList);
            Assert.Equal(3, expectedList.Count);
        }

        private List<ApplicationEntity> BuildCollection()
        {
            return new List<ApplicationEntity>
            {
                new ApplicationEntity
                {
                    Id = "5f6ab450d649375ee98758a9",
                    Url = "https://outlook.live.com",
                    PathLocal = "/index",
                    Application = 1,
                    DebuggingMode = false
                },
                new ApplicationEntity
                {
                    Id = "5f6ab45e69e7cee395eb52b7",
                    Url = "https://softdesign.gupy.io/",
                    PathLocal = string.Empty,
                    Application = 1,
                    DebuggingMode = false
                },
                new ApplicationEntity
                {
                    Id = "5f6ab4623c4ecae3a197cf0d",
                    Url = "https://softdesign-test.com",
                    PathLocal = "/swagger",
                    Application = 1,
                    DebuggingMode = true
                },
                new ApplicationEntity
                {
                    Id = "5f6ab4661e059d7628318d0a",
                    Url = "https://yahoo.com.br",
                    PathLocal = string.Empty,
                    Application = 1,
                    DebuggingMode = false
                },
            };
        }
    }
}
