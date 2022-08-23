using System.Collections.Generic;

namespace ClienteApi.ViewModels
{
    public class ResultViewModel<T>
    {
        private string message;

        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(T data) => Data = data;

        public ResultViewModel(List<string> errors) => Errors = errors;

        public ResultViewModel(string error) => Errors.Add(error);

        public ResultViewModel(string error, string message) : this(error)
        {
            this.message = message;
        }

        public T Data { get; private set; }

        public List<string> Errors { get; private set; } = new();
    }
}