using EmailSignup.Domain.Enums;

namespace EmailSignup.Application.DTOs;

public sealed record RegionOption(Region Value, string Code, string NameTh, string NameEn);
