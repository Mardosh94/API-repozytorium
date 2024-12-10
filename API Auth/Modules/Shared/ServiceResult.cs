namespace API_Auth.Modules.Shared
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessege { get; set; }


        public static ServiceResult Success()
        {
            return new ServiceResult { IsSuccess = true};
        }
        public static ServiceResult Failure(string errorMessege)
        {
            return new ServiceResult { IsSuccess = false, ErrorMessege = errorMessege };
        }
    }
    public class ServiceResult<T>: ServiceResult where T : class
    {
        public T Data { get; set; }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T> { IsSuccess = true, Data = data };
        }
        public static ServiceResult<T> Failure(string errorMessege)
        {
            return new ServiceResult<T> { IsSuccess = false, ErrorMessege = errorMessege };
        }
    }
}
