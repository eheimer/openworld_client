using System;

namespace Openworld.Models
{
	[Serializable]
	public class ErrorResponse: BaseModel
    {
        [Serializable]
        public class Error: BaseModel
        {
            public string type;
            public string message;
        }

        public Error error;
    }
}

