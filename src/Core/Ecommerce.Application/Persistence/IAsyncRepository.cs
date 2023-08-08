using System.Linq.Expressions;

namespace Ecommerce.Application.Persistence;

public interface IAsyncRepository<T>where T : class{
    //Me devuelve una lista de objetos
    Task<IReadOnlyList<T>> GetAllAsync();
    //Me devuelve una lista de objetos pero con una condicion logica
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    // lo mismo pero me devuevel con un orden , y si quiero agregar una condicion que invollucre otra cantidad
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
                                   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
                                   string? includeString,
                                   bool disableTracking = true);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
                                   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                   List<Expression<Func<T, object>>>? includes = null,
                                   bool disableTracking = true);

    
    Task<T> GetEntityAsync(Expression<Func<T, bool>>? predicate,
                                     List<Expression<Func<T, object>>>? includes = null,
                                   bool disableTracking = true);


    Task<T> GetByIdAsync(int id);

    Task<T> AddAsync(T entity);



    Task<T> UpdateAsync(T entity);

    Task DeleteAsync(T entity);


    void AddEntity(T entity);

    void UpdateEntity(T entity);

    void DeleteEntity(T entity);

    void AddRange(List<T> entities);

    void DeleteRange(IReadOnlyList<T> entities);
}