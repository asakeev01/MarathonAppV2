﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Users;

namespace Domain.Entities.Documents;

public class Document
{
    public long Id { get; set; }
    public string? FrontPassportPath { get; set; }
    public string? InsurancePath { get; set; }
    public string? DisabilityPath { get; set; }
    public string? BackPassportPath { get; set; }
    public string? BackInsurancePath { get; set; }
    public string? BackDisabilityPath { get; set; }
    public bool IsArchived { get; set; } = false;
    public long UserId { get; set; }
    public User User { get; set; }
}

