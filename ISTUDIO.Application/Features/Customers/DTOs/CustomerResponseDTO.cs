﻿using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Customers.DTOs;

public class CustomerResponseDTO : IMapWith<CustomersEntity>
{
    public int Id { get; set; }
    public string PIN { get; set; }
    public string? FullName { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Sex { get; set; }
    public string? Nationality { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? SeriesNumDocument { get; set; }
    public DateTime? DateOfExpiry { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? Authority { get; set; }
    public DateTime? DateOfIssue { get; set; }
    public string? Ethnicity { get; set; }
    public string? Address { get; set; }
    public string? UserId { get; set; }
    public bool? Identification { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CustomersEntity, CustomerResponseDTO>();
    }
}
