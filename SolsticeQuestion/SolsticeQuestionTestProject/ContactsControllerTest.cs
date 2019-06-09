using System;
using SolsticeQuestion.Controllers;
using SolsticeQuestion.Models;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.IO;

namespace SolsticeQuestionTestProject
{
    public class ContactsControllerTest
    {
        public DbContextOptions<ContactsDBContext> options = null;
        public ContactsControllerTest()
            {
            if (this.options == null)
            {
                List<ContactItem> myContactItems = CreateDumpTestData();
                this.options = new DbContextOptionsBuilder<ContactsDBContext>()
                .UseInMemoryDatabase(databaseName: "ContactsTestDatabase")
                .Options;
                using (var context = new ContactsDBContext(options))
                {
                    foreach (ContactItem item in myContactItems)
                    {
                        context.Add(item);
                    }

                    context.SaveChanges();

                }
            }
        }
        [Fact]
        public async System.Threading.Tasks.Task GetContactItemsTestAsync()
        {
            using (var context = new ContactsDBContext(this.options))
            {
                ContactsController controller = new ContactsController(context);
                var results = await controller.GetContactItems();
                Assert.Equal(4,results.Value.Count());
                //Clean up Code
                context.Database.EnsureDeleted();
            }
            
        }

        [Fact]
        public async System.Threading.Tasks.Task GetContactItembyIDTestAsync()
        {

            using (var context = new ContactsDBContext(options))
            {
                ContactsController controller = new ContactsController(context);
                var result = await controller.GetContactItem(1);
                Assert.Equal(1, result.Value.Id);
                context.Database.EnsureDeleted();
            }
        }

        // ToDo -> Implement Unit Test for all functions

        private List<ContactItem> CreateDumpTestData()
        {
            // Initialize a list of MyEntity objects to back the DbSet with.
            var myContactItems = new List<ContactItem>()
            {
                new ContactItem { 
                                    Name= "Tomy1",
                                    Company= "ghiy",
                                    ProfileImage= "ace5abb5-f467-4cb8-b078-b7053b9fd662.jpeg",
                                    BirthDate=  Convert.ToDateTime("1991-11-01T00:00:00"),
                                    HomePhoneNumber= "9917671135",
                                    WorkPhoneNumber= "9917671135",
                                    Address= "rul rul street nino city",
                                    City= "Ninol",
                                    State= "Ninal",
                                    Email= "amyis3@gmail.com" },
                new ContactItem {
                                Name= "Tom1",
                                Company= "grhiy",
                                ProfileImage= "afe5abb5-f467-4cb8-b078-b7053b9fd662.jpeg",
                                BirthDate=  Convert.ToDateTime("1990-11-01T00:00:00"),
                                HomePhoneNumber= "9917671165",
                                WorkPhoneNumber= "6783458901",
                                Address= "rula rul street nino city",
                                City= "Ninal",
                                State= "Ninal",
                                Email= "amyis3@gmail.com" },
                new ContactItem {
                                Name= "Tom1",
                                Company= "grhiy",
                                ProfileImage= "bfe5abb5-e467-4cb8-b078-b7053b9fd662.jpeg",
                                BirthDate=  Convert.ToDateTime("1989-11-01T00:00:00"),
                                HomePhoneNumber= "9917671165",
                                WorkPhoneNumber= "6783458901",
                                Address= "rula rul street nin city",
                                City= "Nial",
                                State= "Nial",
                                Email= "amyis3@gmail.com" },
                new ContactItem {
                                Name= "Tom1",
                                Company= "grhiy",
                                ProfileImage= "bfe5abb5-f467-4cb8-b078-b7053b9fd662.jpeg",
                                BirthDate=  Convert.ToDateTime("1991-11-01T00:00:00"),
                                HomePhoneNumber= "8917671165",
                                WorkPhoneNumber= "5783458901",
                                Address= "rula rul street nino city",
                                City= "Nial",
                                State= "Nial",
                                Email= "amyis3@gmail.com" }

            };

            return myContactItems;
        }
    }


    // Tried with Mocking the database but failed due to lot of configuration in the new EFCore.
    //DbContextOptions<ContactsDBContext> options = new DbContextOptions<ContactsDBContext>();
    //// Create a mock DbContext.
    //Mock<ContactsDBContext> dbContext;
    //using (dbContext = new Mock<ContactsDBContext>(options))
    //{

    //    // Create a mock DbSet.
    //    // var dbSet = MockDbSetFactory.Create(myContactItems);
    //    var dbSet = new Mock<DbSet<ContactItem>>();
    //    var queryable = myContactItems.AsQueryable();
    //    dbSet.As<IQueryable<ContactItem>>().Setup(m => m.Provider).Returns(queryable.Provider);
    //    dbSet.As<IQueryable<ContactItem>>().Setup(m => m.Expression).Returns(queryable.Expression);
    //    dbSet.As<IQueryable<ContactItem>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
    //    dbSet.As<IQueryable<ContactItem>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
    //    dbContext.Setup(o => o.Set<ContactItem>()).Returns(dbSet.Object);

    //    var controller = new ContactsController(dbContext.Object);

    //    var results = await controller.GetContactItems();
    //}

    //Assert.Equal(2, 2);
    //public static class MockDbSetFactory
    //{
    //    // Creates a mock DbSet from the specified data.
    //    public static Mock<DbSet<T>> Create<T>(IEnumerable<T> data) where T : class
    //    {
    //        var queryable = data.AsQueryable();
    //        var mock = new Mock<DbSet<T>>();
    //        //mock.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Blog>(data.GetEnumerator()));
    //        mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
    //        mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
    //        mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
    //        mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

    //        return mock;
    //    }
    //}
}
