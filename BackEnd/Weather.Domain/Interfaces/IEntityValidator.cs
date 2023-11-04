namespace Weather.Domain.Interfaces;

public interface IEntityValidator<T>
{
    void Validate(T entity);
}