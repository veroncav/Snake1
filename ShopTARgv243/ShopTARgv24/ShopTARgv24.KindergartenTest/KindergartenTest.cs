using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.KindergartenTest
{
    public class KindergartenTest : TestBase
    {

        // === TESTS ===

        // Test kontrollib, et kehtiva lasteaia loomine tagastab mitte-null tulemuse
        [Fact]
        public async Task ShouldNot_AddEmptyKindergarten_WhenReturnResult()
        {
            // Arrange
            KindergartenDto dto = MockKindergartenData();

            // Act
            var result = await Svc<IKindergartenServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        // Test kontrollib, et juhuslik ID ei ole võrdne olemasoleva lasteaia ID-ga
        [Fact]
        public async Task ShouldNot_GetByIdKindergarten_WhenReturnsNotEqual()
        {
            // Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("c4ef2d3f-0f95-4082-873c-7f7ea3c54b5f");

            // Act
            await Svc<IKindergartenServices>().DetailAsync(guid);

            // Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        // Test kontrollib, et kustutatud lasteaed tagastatakse sama ID-ga
        [Fact]
        public async Task Should_DeleteByIdKindergarten_WhenDeleteKindergarten()
        {
            // Arrange
            KindergartenDto dto = MockKindergartenData();

            // Act
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);
            var deletedKindergarten = await Svc<IKindergartenServices>().Delete((Guid)createdKindergarten.Id);

            // Assert
            Assert.Equal(createdKindergarten.Id, deletedKindergarten.Id);
        }

        // Test kontrollib, et uuendatud lasteaia andmed erinevad algsetest
        [Fact]
        public async Task Should_UpdateKindergarten_WhenUpdateData()
        {
            // Arrange
            KindergartenDto dto = MockKindergartenData();
            KindergartenDto update = MockUpdateKindergartenData();

            // Act
            await Svc<IKindergartenServices>().Create(dto);
            var result = await Svc<IKindergartenServices>().Update(update);

            // Assert
            Assert.NotEqual(dto.GroupName, result.GroupName);
            Assert.NotEqual(dto.ChildrenCount, result.ChildrenCount);
            Assert.NotEqual(dto.KindergartenName, result.KindergartenName);
            Assert.NotEqual(dto.TeacherName, result.TeacherName);
            Assert.NotEqual(dto.CreatedAt, result.CreatedAt);
            Assert.NotEqual(dto.UpdatedAt, result.UpdatedAt);
        }

        // Test kontrollib, et muudetud ID-ga päring ei uuenda olemasolevat lasteaia kirjet
        [Fact]
        public async Task ShouldNot_UpdateKindergarten_WhenDidNotUpdateData()
        {
            // Arrange
            KindergartenDto dto = MockKindergartenData();
            KindergartenDto update = MockNullKindergartenData();

            // Act
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);
            var result = await Svc<IKindergartenServices>().Update(update);

            // Assert
            Assert.NotEqual(dto.Id, result.Id);
        }

        // Test kontrollib, et negatiivse laste arvuga lasteaed luuakse edukalt
        [Fact]
        public async Task Should_AddKindergarten_WhenChildrenCountIsNegative()
        {
            // Arrange
            var service = Svc<IKindergartenServices>();
            KindergartenDto dto = MockKindergartenData();
            dto.ChildrenCount = -10; // negatiivne

            // Act
            var created = await service.Create(dto);

            // Assert
            Assert.NotNull(created);
            Assert.Equal(dto.ChildrenCount, created.ChildrenCount);
            Assert.True(created.ChildrenCount < 0);
        }

        // Test kontrollib, et lasteaia kustutamisel eemaldatakse kõik seotud pildid
        [Fact]
        public async Task Should_DeleteRelatedImages_WhenDeleteKindergarten()
        {
            // Arrange
            KindergartenDto dto = MockKindergartenData();

            // Act
            var created = await Svc<IKindergartenServices>().Create(dto);
            var id = (Guid)created.Id;

            // Arrange
            var db = Svc<ShopTARgv24Context>();
            db.FileToDatabase.Add(new FileToDatabase
            {
                Id = Guid.NewGuid(),
                KindergartenId = id,
                ImageTitle = "kindergarten_1.jpg",
                ImageData = new byte[] { 1, 2, 3 }
            });
            db.FileToDatabase.Add(new FileToDatabase
            {
                Id = Guid.NewGuid(),
                KindergartenId = id,
                ImageTitle = "kindergarten_2.jpg",
                ImageData = new byte[] { 4, 5, 6 }
            });

            // Act
            await db.SaveChangesAsync();
            await Svc<IKindergartenServices>().Delete(id);

            // Assert
            var leftovers = db.FileToDatabase.Where(x => x.KindergartenId == id).ToList();

            Assert.Empty(leftovers);
        }



        // === HELPERS ===
        private KindergartenDto MockKindergartenData()
        {
            KindergartenDto dto = new()
            {
                GroupName = "Group A",
                ChildrenCount = 20,
                KindergartenName = "Happy Kids",
                TeacherName = "Ms. Smith",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return dto;
        }

        private KindergartenDto MockUpdateKindergartenData()
        {
            KindergartenDto dto = new()
            {
                GroupName = "Group B",
                ChildrenCount = 25,
                KindergartenName = "Bright Futures",
                TeacherName = "Mr. Johnson",
                CreatedAt = DateTime.Now.AddYears(1),
                UpdatedAt = DateTime.Now.AddYears(1)
            };

            return dto;
        }

        private KindergartenDto MockNullKindergartenData()
        {
            KindergartenDto dto = new()
            {
                Id = null,
                GroupName = null,
                ChildrenCount = null,
                KindergartenName = null,
                TeacherName = null,
                CreatedAt = null,
                UpdatedAt = null
            };

            return dto;
        }
    }
}
