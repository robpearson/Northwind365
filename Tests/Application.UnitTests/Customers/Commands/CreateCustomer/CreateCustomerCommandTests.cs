﻿using AutoFixture;
using MediatR;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.Application.UnitTests.Common;
using Northwind.Domain.Customers;
using NSubstitute;
using Xunit;

namespace Northwind.Application.UnitTests.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandTests : CommandTestBase
{
    [Fact]
    public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
    {
        // Arrange
        var mediatorMock = Substitute.For<IMediator>();
        var sut = new CreateCustomerCommandHandler(_context, mediatorMock);
        var newCustomerId = new CustomerId("123");

        var fixture = new Fixture();
        var command = fixture.Build<CreateCustomerCommand>()
            .With(x => x.ContactName, "ContactName")
            .With(x => x.ContactTitle, "ContactTitle")
            .With(x => x.Id, newCustomerId.Value)
            .Create();

        // Act
        var result = sut.Handle(command, CancellationToken.None);

        // Assert
        mediatorMock.Received()
            .Publish(Arg.Is<CustomerCreated>(cc => cc.CustomerId == newCustomerId), Arg.Any<CancellationToken>());
    }
}