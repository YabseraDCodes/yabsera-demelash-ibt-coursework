namespace AddisEats.Api.DTOs;

public record CollectionResponse<T>(IReadOnlyList<T> Items);
