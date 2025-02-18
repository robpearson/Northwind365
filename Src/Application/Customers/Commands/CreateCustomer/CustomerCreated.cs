﻿using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Notifications.Models;
using System.Threading;
using System.Threading.Tasks;
using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Commands.CreateCustomer;

public record CustomerCreated(CustomerId CustomerId) : INotification;

// ReSharper disable once UnusedType.Global
public class CustomerCreatedHandler : INotificationHandler<CustomerCreated>
{
    private readonly INotificationService _notification;

    public CustomerCreatedHandler(INotificationService notification)
    {
        _notification = notification;
    }

    public async Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
    {
        await _notification.SendAsync(new MessageDto("From", "To", "Subject", "Body"));
    }
}