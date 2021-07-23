﻿using System;
using Dwapi.Mnch.Core.Command;
using Dwapi.Mnch.Core.Domain;
using Dwapi.Mnch.Core.Interfaces.Repository;
using Dwapi.Mnch.Infrastructure.Data;
using Dwapi.Mnch.Infrastructure.Data.Repository;
using Dwapi.Mnch.SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.Mnch.Core.Tests.CommandHandler
{
    [TestFixture]
    public class ValidateFacilityHandlerTests
    {
        private ServiceProvider _serviceProvider;
        private IMediator _mediator;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<MnchContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IMasterFacilityRepository, MasterFacilityRepository>()
                .AddMediatR(typeof(ValidateFacilityHandler))
                .BuildServiceProvider();


           var  context = _serviceProvider.GetService<MnchContext>();
            context.MasterFacilities.Add(new MasterFacility(1, "XFacility", "XCounty"));
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = _serviceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Throw_Exception_Invalid_SiteCode()
        {
          var ex=  Assert.Throws<System.AggregateException>(() =>CheckMasterFacility(2));
            Assert.AreEqual(typeof(FacilityNotFoundException),ex.InnerException.GetType());
            Console.WriteLine($"{ex.InnerException.Message}");
        }

        [Test]
        public void should_return_Validated_Facility()
        {
            var masterFacility = CheckMasterFacility(1);
            Assert.NotNull(masterFacility);
            Console.WriteLine(masterFacility);
        }

        private  MasterFacility CheckMasterFacility(int code)
        {
            return _mediator.Send(new ValidateFacility(code)).Result;
        }
    }
}
