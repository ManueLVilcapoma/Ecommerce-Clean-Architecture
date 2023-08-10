using FluentValidation.Results;

namespace Ecommerce.Application.Exception;

public class ValidationException:ApplicationException{
    public IDictionary<string,string[]>Errors{get;}
    public ValidationException():base("Se presentarion uno o mas errores de validacion"){
        Errors=new Dictionary<string,string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure>failures):this(){
        Errors=failures
        .GroupBy(x => x.PropertyName,e=>e.ErrorMessage)
        .ToDictionary(failureGroup=>failureGroup.Key,failureGroup=>failureGroup.ToArray());
    }
}